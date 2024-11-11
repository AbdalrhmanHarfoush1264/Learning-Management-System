using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.UserDTOS;
using LMSProject.Bussiness.Interfaces;
using LMSProject.Data.Abstracts;
using LMSProject.Data.Bases;
using LMSProject.Data.Data.Enities.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LMSProject.Bussiness.Implmentions
{
    public class UserService : IUserService
    {
        #region  Fileds
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelper _urlHelper;
        private readonly IEmailService _emailService;

        #endregion

        #region  Constructors
        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager,
            IGenericRepository<User> userRepository, IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _urlHelper = urlHelper;
            _emailService = emailService;
        }
        #endregion



        #region  Functions
        public async Task<CusResponse<string>> AddUserAsync(AddUserRequest request, string roleName)
        {
            var trans = await _userRepository.BeginTransactionAsync();
            try
            {

                var isUserExistByEmail = await _userManager.FindByEmailAsync(request.Email);
                if (isUserExistByEmail != null)
                {
                    return new CusResponse<string>
                    {
                        IsSuccess = false,
                        Message = "Email already Used.",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                var isUserExistByUserName = await _userManager.FindByNameAsync(request.UserName);
                if (isUserExistByUserName != null)
                {
                    return new CusResponse<string>
                    {
                        IsSuccess = false,
                        Message = "Username already Used.",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                var user = new User();
                user.FullName = request.FullName;
                user.UserName = request.UserName;
                user.Email = request.Email;
                user.Address = request.Address;
                user.Country = request.Country;
                user.PhoneNumber = request.PhoneNumber;

                var IsAdded = await _userManager.CreateAsync(user, request.Password);
                if (!IsAdded.Succeeded)
                {
                    return ErrorResponse(GetErrors(IsAdded.Errors));
                }

                var roleExist = await _roleManager.RoleExistsAsync(roleName.ToLower());
                if (!roleExist)
                {
                    return ErrorResponse("Invalid role specified.");
                }

                await _userManager.AddToRoleAsync(user, roleName.ToLower());

                var Code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                Code = WebUtility.UrlEncode(Code);
                var requestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnURL = requestAccessor.Scheme + "://" + requestAccessor.Host +
                    _urlHelper.Action("ConfirmEmail", "Authontication", new { userId = user.Id, code = Code });

                await _emailService.SendEmailAsync(user.Email, returnURL);

                await trans.CommitAsync();
                return new CusResponse<string>
                {
                    IsSuccess = true,
                    Message = "Added Operation Successfully.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }

        }


        public async Task<CusResponse<string>> UpdateUserAsync(UpdateUserRequest request)
        {
            var trans = await _userRepository.BeginTransactionAsync();
            try
            {
                var OldUser = await _userManager.FindByIdAsync(request.Id.ToString());
                if (OldUser == null)
                {
                    return ErrorResponse($"User with Id : {request.Id} not Found!", HttpStatusCode.NotFound);
                }

                var isUserExistByEmail = await _userManager.FindByEmailAsync(request.Email);
                if (isUserExistByEmail != null && isUserExistByEmail.Id != request.Id)
                {
                    return ErrorResponse("Email already Used for anthor User.");
                }

                var isUserExistByUserName = await _userManager.FindByNameAsync(request.UserName);
                if (isUserExistByUserName != null && isUserExistByUserName.Id != request.Id)
                {
                    return ErrorResponse("Username already Used for anthor User.");
                }

                OldUser.FullName = request.FullName;
                OldUser.UserName = request.UserName;
                OldUser.Email = request.Email;
                OldUser.Address = request.Address;
                OldUser.Country = request.Country;
                OldUser.PhoneNumber = request.PhoneNumber;

                var IsUpdated = await _userManager.UpdateAsync(OldUser);
                if (!IsUpdated.Succeeded)
                {
                    return ErrorResponse(GetErrors(IsUpdated.Errors));
                }

                await trans.CommitAsync();
                return new CusResponse<string>
                {
                    IsSuccess = true,
                    Message = "Updated Operation Successfully.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<CusResponse<string>> DeleteUserAsync(int Id)
        {
            var trans = await _userRepository.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(Id.ToString());
                if (user == null)
                {
                    return ErrorResponse($"User with Id : {Id} not Found!", HttpStatusCode.NotFound);
                }

                var IsDeleted = await _userManager.DeleteAsync(user);
                if (!IsDeleted.Succeeded)
                {
                    return ErrorResponse(GetErrors(IsDeleted.Errors));
                }

                await trans.CommitAsync();
                return new CusResponse<string>
                {
                    IsSuccess = true,
                    Message = "Deleted Operation Successfully.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
        public async Task<CusResponse<List<UserResponseDto>>> GetUserListAsync()
        {
            try
            {
                var resultList = await _userManager.Users.ToListAsync();
                var usersListDto = new List<UserResponseDto>();

                foreach (var item in resultList)
                {
                    var Roles = await _userManager.GetRolesAsync(item) ?? new List<string>();
                    var userResponseDto = new UserResponseDto
                    {
                        Id = item.Id,
                        FullName = item.FullName,
                        UserName = item.UserName!,
                        Email = item.Email!,
                        Country = item.Country,
                        Address = item.Address,
                        PhoneNumber = item.PhoneNumber,
                        roles = Roles.ToList()
                    };

                    usersListDto.Add(userResponseDto);
                }

                return new CusResponse<List<UserResponseDto>>
                {
                    IsSuccess = true,
                    Message = usersListDto.Any() ? "Users retrieved successfully." : "No users available.",
                    Data = usersListDto,
                    DataCount = usersListDto.Count,
                    StatusCode = usersListDto.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };

            }
            catch (Exception ex)
            {
                return new CusResponse<List<UserResponseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<PaginatedResult<UserPaginatedListResponseDto>> GetUserPaginatedListAsync(UserPaginatedListRequest request)
        {
            var usersQuery = _userManager.Users
                       .Select(user => new UserPaginatedListResponseDto
                       {
                           Id = user.Id,
                           FullName = user.FullName,
                           UserName = user.UserName!,
                           Email = user.Email!,
                           Address = user.Address,
                           Country = user.Country,
                           PhoneNumber = user.PhoneNumber
                       })
                       .AsQueryable();

            if (!usersQuery.Any())
            {
                return new PaginatedResult<UserPaginatedListResponseDto>(new List<UserPaginatedListResponseDto>());
            }

            var usersPaginated = await usersQuery.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            foreach (var userDto in usersPaginated.Data)
            {
                var user = await _userManager.FindByIdAsync(userDto.Id.ToString());
                if (user != null)
                {
                    var Roles = await _userManager.GetRolesAsync(user);
                    userDto.roles = Roles.ToList();
                }
            }

            return usersPaginated;
        }
        public async Task<CusResponse<UserResponseDto>> GetUserByIdAsync(int Id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(Id.ToString());
                if (user == null)
                {
                    return new CusResponse<UserResponseDto>
                    {
                        IsSuccess = false,
                        Message = $"User with Id : {Id} not Found!",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var Roles = await _userManager.GetRolesAsync(user);
                var userResponseDto = new UserResponseDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Country = user.Country,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber,
                    roles = Roles.ToList()
                };

                return new CusResponse<UserResponseDto>
                {
                    IsSuccess = true,
                    Message = "Users retrieved successfully.",
                    Data = userResponseDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new CusResponse<UserResponseDto>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
        public async Task<CusResponse<string>> ChangePasswordAsync(ChangeUserPasswordRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.Id.ToString());
                if (user == null)
                {
                    return ErrorResponse($"User with Id : {request.Id} not Found!", HttpStatusCode.NotFound);
                }

                var IsChanged = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                if (!IsChanged.Succeeded)
                {
                    return ErrorResponse(GetErrors(IsChanged.Errors));
                }


                return new CusResponse<string>
                {
                    IsSuccess = true,
                    Message = "Changed Password Operation Successfully.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        private CusResponse<string> ErrorResponse(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new CusResponse<string>
            {
                IsSuccess = false,
                Message = message,
                Data = null,
                DataCount = 0,
                StatusCode = statusCode
            };
        }

        private string GetErrors(IEnumerable<IdentityError> errors)
        {
            return "An error occurred: " + string.Join(", ", errors.Select(e => e.Description));
        }
        #endregion

    }
}

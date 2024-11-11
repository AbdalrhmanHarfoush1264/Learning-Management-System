using LMSProject.Bussiness.Bases;
using LMSProject.Bussiness.Dtos.AuthorizationDTOS;
using LMSProject.Bussiness.Interfaces;
using LMSProject.Data.Data;
using LMSProject.Data.Data.Enities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace LMSProject.Bussiness.Implmentions
{
    public class CusAuthorizationService : ICusAuthorizationService
    {


        #region Fileds
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;

        #endregion

        #region Constructors
        public CusAuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, AppDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        #endregion


        #region Functions

        public async Task<CusResponse<string>> AddRole(AddRoleRequest request)
        {
            try
            {
                var IsExistRole = await _roleManager.RoleExistsAsync(request.roleName.ToLower());
                if (IsExistRole)
                    return ErrorResponse("This Role is Exist already.");


                var role = new Role();
                role.Name = request.roleName.ToLower();

                var IsAdded = await _roleManager.CreateAsync(role);

                if (!IsAdded.Succeeded)
                {
                    return ErrorResponse("Added Operation Failed.");
                }

                return new CusResponse<string>()
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
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CusResponse<string>> UpdateRole(UpdateRoleRequest request)
        {
            try
            {
                var IsExistRoleById = await _roleManager.FindByIdAsync(request.Id.ToString());
                if (IsExistRoleById == null)
                    return ErrorResponse($"Not found role with Id : {request.Id}", HttpStatusCode.NotFound);


                var IsExistRoleByName = await _roleManager.RoleExistsAsync(request.roleName.ToLower());
                if (IsExistRoleByName)
                    return ErrorResponse("This Role is Exist already.");



                IsExistRoleById.Name = request.roleName.ToLower();

                var IsUpdated = await _roleManager.UpdateAsync(IsExistRoleById);

                if (!IsUpdated.Succeeded)
                {
                    return ErrorResponse("Updated Operation Failed.");
                }

                return new CusResponse<string>()
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
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CusResponse<string>> DeleteRole(int roleId)
        {
            try
            {
                var IsExistRoleById = await _roleManager.FindByIdAsync(roleId.ToString());
                if (IsExistRoleById == null)
                    return ErrorResponse($"Not found role with Id : {roleId}", HttpStatusCode.NotFound);


                var IsDeleted = await _roleManager.DeleteAsync(IsExistRoleById);

                if (!IsDeleted.Succeeded)
                {
                    return ErrorResponse("Deleted Operation Failed.");
                }

                return new CusResponse<string>()
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
                return ErrorResponse($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<CusResponse<List<RoleResonseDto>>> GetRoleList()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var rolesDtos = new List<RoleResonseDto>();

                foreach (var role in roles)
                {
                    var roleDto = new RoleResonseDto();
                    roleDto.Id = role.Id;
                    roleDto.Name = role.Name!;

                    rolesDtos.Add(roleDto);
                }

                return new CusResponse<List<RoleResonseDto>>
                {
                    IsSuccess = true,
                    Message = roles.Any() ? "Roles retrieved successfully." : "No roles available.",
                    Data = rolesDtos,
                    DataCount = roles.Count,
                    StatusCode = roles.Any() ? HttpStatusCode.OK : HttpStatusCode.NoContent

                };
            }
            catch (Exception ex)
            {
                return new CusResponse<List<RoleResonseDto>>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError

                };
            }
        }

        public async Task<CusResponse<RoleResonseDto>> GetRoleById(int Id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(Id.ToString());
                if (role == null)
                {
                    return new CusResponse<RoleResonseDto>()
                    {
                        IsSuccess = false,
                        Message = $"Not found role with Id : {Id}",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var roleDto = new RoleResonseDto();
                roleDto.Id = role.Id;
                roleDto.Name = role.Name!;

                return new CusResponse<RoleResonseDto>
                {
                    IsSuccess = true,
                    Message = "Roles retrieved successfully.",
                    Data = roleDto,
                    DataCount = 1,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<RoleResonseDto>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError

                };
            }
        }

        public async Task<CusResponse<ManagerUserRolesResponse>> ManagerUserRoles(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new CusResponse<ManagerUserRolesResponse>
                    {
                        IsSuccess = false,
                        Message = $"Not found user with Id : {userId}",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var result = await GetManagerUserRolesData(user);

                return new CusResponse<ManagerUserRolesResponse>()
                {
                    IsSuccess = true,
                    Message = $"Operation Successfully.",
                    Data = result,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<ManagerUserRolesResponse>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<CusResponse<string>> UpdateManagerUserRoles(ManagerUserRolesResponse request)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                {
                    return new CusResponse<string>
                    {
                        IsSuccess = false,
                        Message = $"Not found user with Id : {request.UserId}",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var rolesForUser = await _userManager.GetRolesAsync(user);
                if (rolesForUser != null)
                {
                    var IsDeleted = await _userManager.RemoveFromRolesAsync(user, rolesForUser);
                    if (!IsDeleted.Succeeded)
                    {
                        return new CusResponse<string>
                        {
                            IsSuccess = false,
                            Message = GetErrors(IsDeleted.Errors),
                            Data = null,
                            DataCount = 0,
                            StatusCode = HttpStatusCode.BadRequest
                        };
                    }
                }


                var newRolesForUser = request.UserRoles.Where(x => x.HasRole == true).Select(x => x.Name);
                var IsAdded = await _userManager.AddToRolesAsync(user, newRolesForUser);
                if (!IsAdded.Succeeded)
                {
                    return new CusResponse<string>
                    {
                        IsSuccess = false,
                        Message = GetErrors(IsAdded.Errors),
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                await trans.CommitAsync();
                return new CusResponse<string>
                {
                    IsSuccess = false,
                    Message = $"Updated Operation Successfully.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return new CusResponse<string>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }


        public async Task<CusResponse<ManagerUserClaimsResponse>> ManagerUserClaimsData(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return new CusResponse<ManagerUserClaimsResponse>
                    {
                        IsSuccess = false,
                        Message = $"Not found user with Id : {userId}",
                        Data = null,
                        DataCount = 0,
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var result = await GetMangerUserClaimsData(user);
                return new CusResponse<ManagerUserClaimsResponse>
                {
                    IsSuccess = true,
                    Message = $"Operation Successfully.",
                    Data = result,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new CusResponse<ManagerUserClaimsResponse>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }


        public async Task<CusResponse<string>> UpdateMangerUserClaims(ManagerUserClaimsResponse request)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.userId.ToString());
                if (user == null)
                {
                    return ErrorResponse($"Not found user with Id : {request.userId}", HttpStatusCode.NotFound);
                }


                var claimsForUser = await _userManager.GetClaimsAsync(user);
                if (claimsForUser != null)
                {
                    var IsDeleted = await _userManager.RemoveClaimsAsync(user, claimsForUser);
                    if (!IsDeleted.Succeeded)
                    {
                        return ErrorResponse(GetErrors(IsDeleted.Errors));
                    }
                }

                var newClaims = request.UserClaims?.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var IsAdded = await _userManager.AddClaimsAsync(user, newClaims!);
                if (!IsAdded.Succeeded)
                {
                    return ErrorResponse(GetErrors(IsAdded.Errors));
                }

                await trans.CommitAsync();
                return new CusResponse<string>
                {
                    IsSuccess = false,
                    Message = $"Updated Operation Successfully.",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return new CusResponse<string>
                {
                    IsSuccess = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null,
                    DataCount = 0,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        #endregion


        #region Private-Methods

        public async Task<ManagerUserRolesResponse> GetManagerUserRolesData(User user)
        {
            var response = new ManagerUserRolesResponse();
            var userRoles = new List<UserRoles>();

            var rolesForUser = await _userManager.GetRolesAsync(user);
            var rolesInSystem = await _roleManager.Roles.ToListAsync();

            foreach (var role in rolesInSystem)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name!;
                userRole.HasRole = rolesForUser.Any(roleName => string.Equals(roleName, role.Name, StringComparison.OrdinalIgnoreCase));

                userRoles.Add(userRole);
            }

            response.UserId = user.Id;
            response.UserRoles = userRoles;

            return response;
        }

        public async Task<ManagerUserClaimsResponse> GetMangerUserClaimsData(User user)
        {
            var response = new ManagerUserClaimsResponse();
            var userClaimsList = new List<UserClaims>();


            //ClaimsForUser
            var claimsForUser = await _userManager.GetClaimsAsync(user);

            //ClaimsInSystem
            foreach (var item in ClaimsStore.Claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = item.Type;

                if (claimsForUser.Any(x => x.Type == item.Type))
                {
                    userClaim.Value = true;
                }
                else
                {
                    userClaim.Value = false;
                }

                userClaimsList.Add(userClaim);
            }

            response.userId = user.Id;
            response.UserClaims = userClaimsList;

            return response;
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

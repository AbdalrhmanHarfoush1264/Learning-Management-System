using LMSProject.Bussiness;
using LMSProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region Custom_Services
builder.Services.AddLMSDataDependencies(builder.Configuration)
    .AddLMSBussinessDependencies().AddServiceRegisteration(builder.Configuration);
#endregion

#region CORS
var CORS1 = "_cors1";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: CORS1,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors(CORS1);
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

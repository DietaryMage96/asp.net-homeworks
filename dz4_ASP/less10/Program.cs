using Bll.Services;
using Dll.Context;
using Dll.Repository;
using Domain.Models;
using FluentValidation;
using less10.MapperProfiles;
using less10.Validator;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("def")));

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;

    options.Lockout.AllowedForNewUsers = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
})
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<MovieCommentRepository>();
builder.Services.AddScoped<MovieCommentService>();
builder.Services.AddScoped<IdentityAuthService>();

builder.Services.AddValidatorsFromAssemblyContaining<IdentityRegisterRequestValidator>();

builder.Services.AddAutoMapper(opt => {
    opt.AddProfile<RequestResponceProfile>();
    opt.AddProfile<DtoMapperProfile>();
});
var app = builder.Build();

app.MapOpenApi();
app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "less10"));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

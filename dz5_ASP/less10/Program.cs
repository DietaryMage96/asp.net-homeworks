using Bll.Services;
using Dll.Context;
using Dll.Repository;
using Domain.Models;
using FluentValidation;
using less10.MapperProfiles;
using less10.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
})
    .AddSignInManager()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<IMovieCommentRepository, MovieCommentRepository>();
builder.Services.AddScoped<MovieCommentService>();
builder.Services.AddScoped<IdentityAuthService>();

builder.Services.AddValidatorsFromAssemblyContaining<IdentityRegisterRequestValidator>();

builder.Services.AddAutoMapper(opt => {
    opt.AddProfile<RequestResponceProfile>();
    opt.AddProfile<DtoMapperProfile>();
});

builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:issuer"],
            ValidAudience = builder.Configuration["JWT:audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]!)),

            NameClaimType = JwtRegisteredClaimNames.UniqueName
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors();

app.MapOpenApi();
app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "less10"));


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

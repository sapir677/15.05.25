using MyProject.Core.DTOs;
using MyProject.Core.Interface;
using MyProject.Data;
using MyProject.Service;
using AutoMapper;
using System.Text.Json.Serialization;
using MyProject.Api.MiddleWare;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
}); 
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHourService, HourService>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddAutoMapper(typeof(MapperProfile));


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero,//בודק האם לא פג התוקף
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});


//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.UseMiddleware<LogginMiddleWare>();
app.MapControllers();


////כתוב בקובץ וורד של המורה לא הבנתי למה צריך את זה 
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Hello World!");
//    });
//});

//כנ"ל
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello from the final middleware!");
//});
app.Run();
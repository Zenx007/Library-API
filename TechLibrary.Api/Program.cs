using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TechLibrary.Api.Filters;

const string AUTHENTICATION_TYPE = "Bearer"; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>

    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKey()
        };
    });
 builder.Services.AddSwaggerGen(options =>
    {
     options.AddSecurityDefinition(AUTHENTICATION_TYPE, new OpenApiSecurityScheme
     {
         Description = @"JWT Authorization header using the Bearer scheme.
                   Enter 'Bearer' [space] and then your token in the text input below.
                   Example: 'Bearer 12345abcdef'",
         Name = "Authorization",
         In = ParameterLocation.Header,
         Type = SecuritySchemeType.ApiKey,
          Scheme = AUTHENTICATION_TYPE
     });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                 new OpenApiSecurityScheme
                     {
                         Reference = new OpenApiReference
                         {
                         Type = ReferenceType.SecurityScheme,
                          Id = AUTHENTICATION_TYPE
                         },
                  Scheme = "oauth2",
            Name = AUTHENTICATION_TYPE,
            In = ParameterLocation.Header
        },
        new List<string>()
    }
});
        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        SymmetricSecurityKey SecurityKey()
        {
            var signinKey = "wMyJ530ueaBBqRksp8rpLQnzIafYZHUa";

            var symmetricKey = Encoding.UTF8.GetBytes(signinKey);

            return new SymmetricSecurityKey(symmetricKey);
        }
    }

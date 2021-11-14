using Api.ConfCMSReactDotNet.Configuration;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var authenticationConfiguration = new AuthenticationConfiguration();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.GetSection(nameof(AuthenticationConfiguration)).Bind(authenticationConfiguration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = authenticationConfiguration.JwtBearerConfiguration.Authority;
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authenticationConfiguration.JwtBearerConfiguration.TokenValidationConfiguration.Issuer,
            ValidateAudience = true,
            ValidAudience = authenticationConfiguration.JwtBearerConfiguration.TokenValidationConfiguration.Audience,
            ValidateLifetime = true
        };
    });

var pathToKeys = Path.Combine(Directory.GetCurrentDirectory(), "Keys", "confcmsreactdotnet-firebase-adminsdk.json");

if (builder.Environment.EnvironmentName == Environments.Development)
{
    pathToKeys = Path.Combine(Directory.GetCurrentDirectory(), "Keys", "confcmsreactdotnet-firebase-adminsdk-local.json");
}

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(pathToKeys)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

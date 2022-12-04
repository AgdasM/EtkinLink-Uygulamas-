using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    a =>
    {
        a.RequireHttpsMetadata=false;
        a.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidAudience="localhost",
            ValidIssuer ="localhost",
            ValidateAudience=true,
            ValidateIssuer=true,
            ValidateLifetime=true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("bohrbohrbohrbohrbohrbohrbohrbohr"))
        };

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(a =>
{
    a.AllowAnyOrigin();
    a.AllowAnyMethod();
    a.AllowAnyHeader();
});
app.UseAuthentication();

app.MapControllers();

app.UseAuthorization();


app.Run();

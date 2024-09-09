using System.Security.Claims;
using Keycloak;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/users/me",
        (ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal.Claims.ToDictionary(claim => claim.Type, claim => claim.Value))
    .RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
using PIva.Api.Endpoints.Internal;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices<Program>(builder.Configuration);

var app = builder.Build();
app.UseEndpoints<Program>();

app.Run();

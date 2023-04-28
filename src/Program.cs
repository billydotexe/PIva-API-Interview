using PIva.Api.Endpoints.Internal;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices<Program>(builder.Configuration);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

var app = builder.Build();
app.UseEndpoints<Program>();

app.Run();

using RealEstateBank.Extensions;
using RealEstateBank.Helpers;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi("v1", options => {
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseHttpLogging();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
    app.MapScalarApiReference(opts => {
        opts
            .WithTitle("Real Estate Bank API")
            .WithTheme(ScalarTheme.Kepler)
            .WithDefaultHttpClient(ScalarTarget.JavaScript, ScalarClient.Axios)
            .WithPersistentAuthentication();
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

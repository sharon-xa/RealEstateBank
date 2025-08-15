using RealEstateBank.Extensions;
using RealEstateBank.Helpers;
using RealEstateBank.Utils.Exceptions;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi("v1", options => {
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

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

app.UseExceptionHandler(_ => { });
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

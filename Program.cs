using github_practice;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseSwagger();

app.MapGet("/", async context =>
{
    var html = """
    <!DOCTYPE html>
    <html>
    <head>
        <title>API Docs</title>
        <script src="https://cdn.jsdelivr.net/npm/@scalar/api-reference"></script>
    </head>
    <body>
        <api-reference 
            url="/swagger/v1/swagger.json"
            theme="default"
        ></api-reference>
    </body>
    </html>
    """;

    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(html);
});

//app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();

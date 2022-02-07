using Autofac;
using Autofac.Extensions.DependencyInjection;
using OrderManagement.Service.DependencyResolvers.Autofac;
using OrderManagement.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
})
.UseServiceProviderFactory(new AutofacServiceProviderFactory())
.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacServiceModule());
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseServiceMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.Map("/", (HttpContext context) =>
    {
        context.Response.Redirect("swagger/index.html");
    });
}

app.Run();

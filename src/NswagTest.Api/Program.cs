using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddControllers()
	.AddJsonOptions(o =>
	{
		o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new () { Title = "AutorestTest.Api", Version = "v1" });
	
	c.CustomOperationIds(apiDesc =>
	{
		// use ControllerName_Method as operation id. That will group the methods in the generated client
		if (apiDesc.ActionDescriptor is ControllerActionDescriptor desc)
		{
			return $"{desc.ControllerName}_{desc.ActionName}";
		}
		// otherwise get the method name from the methodInfo
		var controller = apiDesc.ActionDescriptor.RouteValues["controller"];
		apiDesc.TryGetMethodInfo(out var methodInfo);
		var methodName = methodInfo?.Name ?? null;
		return $"{controller}_{methodName}";
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

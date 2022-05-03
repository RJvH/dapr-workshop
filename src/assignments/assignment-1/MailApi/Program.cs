using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/cronmail", async () =>
{
    var daprClient = new DaprClientBuilder().Build();
    var metadata = new Dictionary<string, string>
    {
        ["emailFrom"] = "noreply@dapr-workshop.local",
        ["emailTo"] = "john.doe@dapr-worksip.local",
        ["subject"] = $"Mail from cron job"
    };
    

    var result = await daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "weatherapi", "weatherforecast");

    string body = $"Wow, it's {result.FirstOrDefault().Summary}!";

    await daprClient.InvokeBindingAsync("dapr-smtp", "create", body, metadata);
})
.WithName("cronmail");

app.MapHealthChecks("/health");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
# Assignment 4

## Goal

In this assignment we will create a default WeatherForecast .NET6 minimal Web Api.

After this assignment we know

- how to create a new .NET6 minimal default WeatherForecast Web Api project
- how to run the Api from VS Code
- how to check the OpenApi swagger page and test the API

## Steps

### Prerequisite

- Open VS Code
- Open the assignment-4 VS Code workspace from the workspaces folder using "File > Open Workspace from File"

### Step 1.

Let's open a terminal. 
Right click in the 'src' folder and choose 'Open in Integrated Terminal'

A new terminal window (probably Powershell) will be opened with 'assignment-4' as the working directory.

### Step 2. Create new minimal ASP.NET WebApi Project

Create a new minimal ASP.NET WebApi Project with the name 'WeatherApi' using this command from the terminal:

```dotnet new webapi -minimal -o WeatherApi --kestrelHttpPort 5201```

The new project will be scaffolded.

Open the Program.cs class file and Remove this HSTS redirection line:
```c#
app.UseHttpsRedirection();
```

Add the health check service and map the health check in the Program.cs class:
- builder.Services.AddHealthChecks();
- app.MapHealthChecks("/health");

Your Program.cs class now looks like:
```c#
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapHealthChecks("/health");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

```

### Step 3. Run the WeatherApi

Run the WeatherApi using this command from the terminal:

```dotnet run --project WeatherApi```

Check that :
- the Health endpoint is working : http://localhost:5201/health
- the Swagger endpoint is working: http://localhost:5201/swagger/index.html

In the Swagger UI,
1. click on GET /weatherforecast
2. click the 'Try it out' button
3. click on 'Execute'
4. check that the response code is 200 and the body contains weather forecasts!'


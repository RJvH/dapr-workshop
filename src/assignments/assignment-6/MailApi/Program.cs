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
app.MapHealthChecks("/health");

app.MapPost("/cronmail", async () =>
{
    var daprClient = new DaprClientBuilder().Build();
    var metadata = new Dictionary<string, string>
    {
        ["emailFrom"] = "noreply@dapr-workshop.local",
        ["emailTo"] = "john.doe@dapr-worksip.local",
        ["subject"] = $"Mail from cron job"
    };

    string body = $"Wow, it's Working!";

    await daprClient.InvokeBindingAsync("dapr-smtp", "create", body, metadata);
})
.WithName("cronmail");

app.Run();


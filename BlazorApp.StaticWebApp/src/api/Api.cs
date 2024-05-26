using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace api;

public class ForecastsApi
{
  public static string[] summaries =
  ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

  private readonly ILogger _logger;

  public ForecastsApi(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<ForecastsApi>();

  [Function("Forecasts")]
  public WeatherForecast[] Run(
    [HttpTrigger(AuthorizationLevel.Function, "get")]
    HttpRequestData req)
  {
    _logger.LogInformation("C# HTTP trigger function processed a request.");
    DateOnly startDate = DateOnly.FromDateTime(DateTime.Now);
    WeatherForecast[] forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
      Date = startDate.AddDays(index),
      TemperatureC = Random.Shared.Next(-20, 55),
      Summary = summaries[Random.Shared.Next(summaries.Length)]
    }).ToArray();
    return forecasts;
  }
}

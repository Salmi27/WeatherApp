using WeatherApi.Dtos;

namespace WeatherApi.Endpoints;

public static class WeatherEndpoints
{
    private static readonly List<WeatherDto> weathers =
    [
        new (1, "Freezing", -10.0),
        new (2, "Bracing", 0.0),
        new (3, "Chilly", 5.0),
        new (4, "Cool", 10.0),
        new (5, "Mild", 15.0),
        new (6, "Warm", 20.0),
        new (7, "Balmy", 25.0),
        new (8, "Hot", 30.0),
        new (9, "Sweltering", 35.0),
        new (10, "Scorching", 40.0)
    ];

    public static RouteGroupBuilder MapWeatherEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("weather");

        // GET
        group.MapGet("/", () => weathers);

        // GET/1
        group.MapGet("/{id}", (int id) =>
        {
            var weather = weathers.FirstOrDefault(x => x.Id == id);
            return weather is null ? Results.NotFound() : Results.Ok(weather);
        });

        // POST
        group.MapPost("/", (CreateWeatherDto newWeather) => 
        {
            WeatherDto weather = new
            (
                weathers.Count + 1,
                newWeather.Name,
                newWeather.TemperatureC
            );

            weathers.Add(weather);

            Results.CreatedAtRoute();
        });

        // PUT/1
        group.MapPut("/{id}", (int id, UpdateGameDto updatedWeather) =>
        {
            var index = weathers.FindIndex(weather => weather.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            else
            {
                weathers[index] = new WeatherDto
                (
                    id,
                    updatedWeather.Name,
                    updatedWeather.TemperatureC
                );
            }

            return Results.NoContent();
        });

        // DELETE/1
        group.MapDelete("/{id}", (int id) => 
        {
            weathers.RemoveAll(weather => weather.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}

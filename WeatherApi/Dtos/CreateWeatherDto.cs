namespace WeatherApi.Dtos;

public record class CreateWeatherDto
(
    string Name,
    double TemperatureC
);

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController] 
// Enrutamiento a traves de controlador 
[Route("api/[controller]")] // Con el atributo route usamos una variable para hacer el enrutamiento de nuestro controlador, esta variable nos permite
                        // manejar un nombre dinamico. Podemos agregar antes la variable de api/ como fijo y dejarlo a controller como dinamica 
public class WeatherForecastController : ControllerBase // Si yo cambio el nombre de WeatherForecast automaticamente la ruta va a cambiar
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // Implementacion de login que luego de esto se puede utilizar donde sea
    private readonly ILogger<WeatherForecastController> _logger;

    // lista en memoria y podemos hacer lo que queramos dentro los metodos http
    private static List<WeatherForecast> ListWeatherForecast = new List<WeatherForecast>();


    // Constructor del controlador
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        // Para que perduren los datos en la lista y podamos hacer modificaciones y demas
        // El any es para preguntar si tiene algun registro, la negamos para decir si no tiene ni un registro
        if(ListWeatherForecast == null || !ListWeatherForecast.Any())
        {
            // En ves de poner el return ponemos la coleccion porque asignamos la coleccion que se genera a la lista
            ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToList(); // Para que genere una lista
        }
    }

    

    [HttpGet(Name = "GetWeatherForecast")] // Atributo para hacer el get
    // Enrutamiento a través de nuestras funciones dentro del controlador o actions
    //[Route("Get/weatherforecast")] // Tenemos que agregar esta ruta para acceder a este metodo
    // El metodo get retorna datos osea la lista de elementos
    // Podemos crear multiples rutas dependiendo de la accion que tengamos dentro del controlador
    //[Route("[action]")] // Podemos usar una palabra dinamica para los actions que es action, permite utilizar el nombre que tenga el metodo para poder hacer el llamado del endpoint
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Retornando la lista de weatherforecast"); // Cada mensaje que pongamos en el logger van a aparecer en la consola y van a ser leidos por diferentes servicios por ejemplo cdo pongamos la API en un contenedor en la nube. Nos va a servir mucho cuando queramos saber de erorres y demas.
        return ListWeatherForecast;
    }

    [HttpPost] // Atributo para hacer el post
    // Recibe un modelo tipo WF y cuando lo reciba, lo agrega a la lista
    public IActionResult Post(WeatherForecast weaterforecast)
    {
        ListWeatherForecast.Add(weaterforecast);
        return Ok();
    }

    // Con esta configuracion le decimos que va a ir un index en la url 
    [HttpDelete("{index}")] // Atributo para hacer el delete
    // Solo recibe el indice del elemento que queremos eliminar
    // La coleccion no tiene ids por eso no se hace el update, solo hacemos insercion y eliminacion
    public IActionResult Delete(int index)
    {
        ListWeatherForecast.RemoveAt(index);
        return Ok();
    }
}

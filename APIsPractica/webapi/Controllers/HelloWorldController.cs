namespace webapi.Controllers;

using CursoEntityFrameworkPractica;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;

[ApiController]
// Enrutamiento a traves de controlador 
[Route("api/[controller]")] // Con el atributo route usamos una variable para hacer el enrutamiento de nuestro controlador, esta variable nos permite
                              // manejar un nombre dinamico. Podemos agregar antes la variable de api/ como fijo y dejarlo a controller como dinamica 
public class HelloWorldController : ControllerBase // Tiene que heredar todos los controles de esto
{
    IHelloWorldService helloWorldService; // Recibimos la dependencia de la interfaz a traves del constructor

    // Cuando nos da error cuando ponemos la cadena de conexion sin la de windows, se hace lo siguiente:

    //TareasContext dbContext;


    // Recibimos a traves del constructor la dependencia 
    public HelloWorldController(IHelloWorldService helloWorld) //, TareasContext db) // Escribimos lo mismo para que podamos recibir la dependencia al momento que el inyector la pase 
    {
        // Recibimos la instancia que el inyector creo y paso por el constructor y lo guardamos en la propiedad creada dentro del controlador
        helloWorldService = helloWorld;
        //dbContext = db;
    }

    [HttpGet]  // Hacemos esto para cumplir con el estandar de open API que sigue swagger
    public IActionResult Get()
    {
        return Ok(helloWorldService.GetHelloWordl()); // Devuelve el mensaje de hello world luego de hacer el proceso de arriba
    }

    //[HttpGet]
    //[Route("createdb")]
    //public IActionResult CreateDataBase()
    //{
    //    dbContext.Database.EnsureCreated();

    //    return Ok();
    //}
} 


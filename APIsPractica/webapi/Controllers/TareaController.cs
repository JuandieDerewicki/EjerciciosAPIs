using CursoEntityFrameworkPractica;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")] // Estructura para los demas controladores

    public class TareaController : ControllerBase
    {
        // Usamos el servicio de categorias que tiene la logica que vamos a usar. Usamos la interfaz pq tiene la extraccion y es la q inyectamos   
        ITareasService tareaService;

        public TareaController(ITareasService service)
        {
            tareaService = service;
        }

        // Endpoints para devolver la informacion 
        [HttpGet] // Es importante agregar x buena practica el metodo al que se está accediendo que es get
        public IActionResult Get()
        {
            return Ok(tareaService.Get()); // la logica la tiene el servicio
        }

        [HttpPost]
        public IActionResult Post([FromBody] Tarea tarea) // Tiene como parametro el modelo de cat. Entonces desde el cuerpo del request debemos recibir la categoria
        {
            tareaService.Save(tarea); // El servicio en cat no devuelve nada, entonces si hay error ya no continua
            return Ok(); // la logica la tiene el servicio
        }

        // Este metodo actualiza
        [HttpPut("{id}")] // El id se recibe por la url por eso se pone eso
        public IActionResult Put(Guid id, [FromBody] Tarea tarea) // La dif es que el put tmb necesita tmb el id para actualizar y en el body solo lo mismo
        {
            tareaService.Update(id, tarea);
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(Guid id)
        {
            tareaService.Delete(id);
            return Ok();
        }
    }
}

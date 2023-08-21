using CursoEntityFrameworkPractica;
using Microsoft.AspNetCore.Mvc;
using webapi.Services;

namespace webapi.Controllers
{
    [Route("api/[controller]")] // Estructura para los demas controladores
    public class CategoriaController : ControllerBase // Tiene todos los componentes para devolver los resultados http que se devuelven dentro de un controlador 
    {
        // Usamos el servicio de categorias que tiene la logica que vamos a usar. Usamos la interfaz pq tiene la extraccion y es la q inyectamos   
        ICategoriaService categoriaService;

        public CategoriaController(ICategoriaService service)
        {
            categoriaService = service;
        }

        // Endpoints para devolver la informacion 
        [HttpGet] // Es importante agregar x buena practica el metodo al que se está accediendo que es get
        public IActionResult Get()
        {
            return Ok(categoriaService.Get()); // la logica la tiene el servicio
        }

        [HttpPost]
        public IActionResult Post([FromBody] Categoria categoria) // Tiene como parametro el modelo de cat. Entonces desde el cuerpo del request debemos recibir la categoria
        {     
            categoriaService.Save(categoria); // El servicio en cat no devuelve nada, entonces si hay error ya no continua
            return Ok(); // la logica la tiene el servicio
        }

        // Este metodo actualiza
        [HttpPut("{id}")] // El id se recibe por la url por eso se pone eso
        public IActionResult Put(Guid id, [FromBody] Categoria categoria) // La dif es que el put tmb necesita tmb el id para actualizar y en el body solo lo mismo
        {
            categoriaService.Update(id, categoria); 
            return Ok(); 
        }

        [HttpDelete]
        
        public IActionResult Delete(Guid id)
        {
            categoriaService.Delete(id);
            return Ok();
        }
    }
}

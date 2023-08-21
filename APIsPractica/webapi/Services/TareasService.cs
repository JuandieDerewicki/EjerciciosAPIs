using CursoEntityFrameworkPractica;

namespace webapi.Services
{
    public class TareasService : ITareasService
    {
        TareasContext context; // Para implementar el get necesitamos recibir esto, que es el contexto de ef

        // Recibimos el context dentro del constructor, igualmente las dependencias se pueden inyectar en cualquier parte 
        public TareasService(TareasContext dbcontext)
        {
            // Lo asignamos 
            context = dbcontext;
        }

        public IEnumerable<Tarea> Get() // Coleccion de tareas
        {
            return context.Tareas;
        }

        public async Task Save(Tarea tarea) // Metodo para guardar
        {
            context.Add(tarea);
            await context.SaveChangesAsync();
        }
        public async Task Update(Guid id, Tarea tarea) // Metodo para editar
        {
            // Realizamos la busqueda x el id para luego reemplazar los datos que vienen de categoria
            // Hay que especificar la coleccion donde se hace la busqueda
            var tareaActual = context.Tareas.Find(id);
            if (tareaActual != null) // siempre y cuando encuentre el elemento y sea diferente de null pq lo encontro
            {
                tareaActual.Titulo = tarea.Titulo;
                tareaActual.Descripcion = tarea.Descripcion;
                tareaActual.PrioridadTarea = tarea.PrioridadTarea;
                tareaActual.FechaCreacion = tarea.FechaCreacion;
                tareaActual.Categoria = tarea.Categoria;    
                tareaActual.CategoriaId = tarea.CategoriaId;    

                await context.SaveChangesAsync();
            }
        }

        /*
             public async Task Update(Guid id, Tarea tarea) {
        var tareaActual = context.Tareas.Find(id);
        if (tareaActual != null) {
            tareaActual.CategoriaId = tarea.CategoriaId;
            tareaActual.Titulo = tarea.Titulo;
            tareaActual.PrioridadTarea = tarea.PrioridadTarea;
            tareaActual.Descripcion = tarea.Descripcion;

            await context.SaveChangesAsync();
        }

         */

        public async Task Delete(Guid id) // Metodo para eliminar
        {
            var tareaActual = context.Tareas.Find(id);
            if (tareaActual != null) // siempre y cuando encuentre el elemento y sea diferente de null pq lo encontro
            {
                context.Remove(tareaActual);
                await context.SaveChangesAsync();
            }
        }
    }

    public interface ITareasService
    {
        // Creacion de servicio para categoria
        IEnumerable<Tarea> Get();

        Task Save(Tarea tarea);

        Task Update(Guid id, Tarea tarea);

        Task Delete(Guid id);
    }
}

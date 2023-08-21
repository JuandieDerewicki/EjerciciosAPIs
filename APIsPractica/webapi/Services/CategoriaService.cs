using CursoEntityFrameworkPractica;

namespace webapi.Services
{
    public class CategoriaService : ICategoriaService
    {
        TareasContext context; // Para implementar el get necesitamos recibir esto, que es el contexto de ef

        // Recibimos el context dentro del constructor, igualmente las dependencias se pueden inyectar en cualquier parte 
        public CategoriaService(TareasContext dbcontext)
        {
            // Lo asignamos 
            context = dbcontext;
        }
        public IEnumerable<Categoria> Get() // Vamos a devolver una lista de categorias
        {
            return context.Categorias;
        }
        public async Task Save(Categoria categoria) // Metodo para guardar
        {  
            categoria.CategoriaId= Guid.NewGuid();
            context.Add(categoria);
            await context.SaveChangesAsync();
        }
        public async Task Update(Guid id, Categoria categoria) // Metodo para editar
        {
            // Realizamos la busqueda x el id para luego reemplazar los datos que vienen de categoria
            // Hay que especificar la coleccion donde se hace la busqueda
            var categoriaActual = context.Categorias.Find(id);
            if (categoriaActual != null) // siempre y cuando encuentre el elemento y sea diferente de null pq lo encontro
            {
                categoriaActual.Nombre = categoria.Nombre;
                categoriaActual.Descripcion = categoria.Descripcion;
                categoriaActual.Peso = categoria.Peso;

                await context.SaveChangesAsync();   
            }
        }

        public async Task Delete(Guid id) // Metodo para eliminar
        {
            var categoriaActual = context.Categorias.Find(id);
            if (categoriaActual != null) // siempre y cuando encuentre el elemento y sea diferente de null pq lo encontro
            {
                context.Remove(categoriaActual);
                await context.SaveChangesAsync();
            }
        }

    }

    public interface ICategoriaService
    {
        // Creacion de servicio para categoria
        IEnumerable<Categoria> Get();

        Task Save(Categoria categoria);

        Task Update(Guid id, Categoria categoria);

        Task Delete(Guid id);

    }
}

namespace webapi.Middlewares
{
    // Es un middleware que devuelve la hora que hay en el servidor sin importar el request que se esté haciendo, solo analizando el parametro q venga dentro del request
    public class TimeMiddleware
    {
        readonly RequestDelegate next; // Nos ayuda a invocar el middleware que sigue dentro del ciclo y construir la logica de nuestro middleware de acuerdo a la secuencia de los middleware que es uno detras de otro

        public TimeMiddleware(RequestDelegate nextRequest) // Constructor para recibir la dependencia
        {
            next = nextRequest; // Ya tengo la info que necesito para hacer el llamado al siguiente middleware
        }

        // Metodo invoke que viene por defecto en todos los middleware
        public async Task Invoke(Microsoft.AspNetCore.Http.HttpContext context) // Va a recbir lo que representa el request 
        {
            // El metodo next es que invoca al mw que sigue y lo agrega al final la hora cuando hace la logica de abajo, pero se puede hacer lo contrario pero ya no va a devolver toda la otra informacion
            await next(context); // Llamado del siguiente middleware

            // Analisis del request, si dentro del query (los parametros que se ponen en la url) existe algun parametro que tenga una KEY igual a time y si existe vamos a escribir la hora actual  
            if(context.Request.Query.Any(p => p.Key == "time"))
            {
                await context.Response.WriteAsync(DateTime.Now.ToShortTimeString());
                // De esta manera capturamos y devolvemos la hora del servidor sobre el request
            }
        }
    }
    // Esta clase nos ayuda a hacer el usetimemiddleware dentro de la clase program
    // Clase que nos permite agregar el middleware dentro de la configuracion de la api
    // Recibimos como parametro el contexto actual del builder, tomamos el builder, agregamos el middleware de nosotros y retornamos para que siga con el siguiente middleware de la secuencia 
    public static class TimeMiddlewareExtension
    {
        public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeMiddleware>();
        }
    }
}

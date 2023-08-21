var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Ya no existe la carpeta controllers
app.MapGet("/", () => "Hello World!"); // De esta manera ya podemos hacer el mapeo de nuestra api 
// Aca simplemente hacemos el mapeo utilizamos funciones: app.MapPost()..
// Todo se haria sobre el archivo program

// Minimal API no está diseñado para proyectos muy largos o con muchos endpoints, para proyectos simples
app.Run();

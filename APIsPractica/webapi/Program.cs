using CursoEntityFrameworkPractica;
using Microsoft.AspNetCore.Hosting.Server;
using webapi.Middlewares;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Los servicios son las dependencias 
//builder.Services.AddSqlServer<TareasContext>("Data Source = localhost\\SQLEXPRESS;Initial Catalog=TareasDB;user id=COMPUCUERVO\\bocaj;TrustServerCertificate=True");
builder.Services.AddSqlServer<TareasContext>("Data Source=localhost\\SQLEXPRESS;Initial Catalog=TareasDB;Integrated Security=True;TrustServerCertificate=True");
// Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bocaj\OneDrive\Documentos\mibd.mdf;Integrated Security=True;Connect Timeout=30


// Inyeccion de la dependencia antes de hacer el build
// Si implmentamos addscope se va a crear una nueva instancia de la depedencia que estamos usando a nivel
// de controlador o clase, no importa si estamos inyectando varias veces y en dif partes la dependencia,
// dentro detodo el contexto del controlador o clase se va a inyectar la misma instancia que se creo para todo ese elemento
// Si implementamos addsingleton se va a crear una unica instancia de esa dependencia a nivel de toda la
// API, no recomendable pq se va a crear en memoria. La tendencia es implemtentar APIs que no manejen tipos
// de estados sino que con cada request se cree una nueva implementacion o instancia de la dependencia que hemos creado
builder.Services.AddScoped<IHelloWorldService, HelloWorldService>(); // Cada vez que se inyecte la interfaz se va a crear un objeto de la clase internamente y asi configuramos la dependencia
builder.Services.AddScoped<ICategoriaService, CategoriaService>();  
builder.Services.AddScoped<ITareasService, TareasService>(); // Configuracion de inyeccion de dependencias y se pueden usar dentro de un controlador 
//builder.Services.AddScoped<IHelloWorldService>(p => new HelloWorldService()); // Asi se inyecta la dependencia usando la clase y no la interfaz
// Cuando usamos una clase la interfaz se recibe en todos los controladores y en cualquier momento podemos cambiar la clase de HelloWorldService y luego de cambiarla en el inyector o configuracion se va a cambiar en todos los otros controaldores y tendria que cambiar en cada controlador por eso se hace el <Ihelloworldservice> entonces ese objeto representa esa dependencia   

// Cuando el proyecto empiece a crecer, se utiliza los servicios, separandolo de la logica para que los controladores solo llamen al servicio y devolver la logica que el servicio ejecuta   

var app = builder.Build();

// Configure the HTTP request pipeline.
// Cada uno de los que empieza con "Use" es un middleware y los agregamos despues del builder
// Cada request (get, post,etc) va a pasar por cada middleware y se tiene que respetar y agregar los middleware en el orden correcto
if (app.Environment.IsDevelopment()) // Esta en development y no debe estar en produccion pq un hacker puede estar dentro de la api y ver y utilzarla  
{
    app.UseSwagger(); // Las dependencias son usadas por estos MW
                      // Configuran swagger internamente para poder desplegar una pequeña pag web q incluye toda la definicion de la API y ademas poder interceptar la conf general de la API para poder diseñar esa documentacion 
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();  // Quien puede utilizar y quien no, es para la seguridad

app.UseAuthorization();

//app.UseWelcomePage(); // Nos permite agregar una pagina de bienvenida cada vez que un cliente ingrese a la API

//app.UseTimeMiddleware();    

app.MapControllers();

app.Run();

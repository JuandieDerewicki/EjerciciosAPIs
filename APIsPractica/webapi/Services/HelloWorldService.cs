namespace webapi.Services
{
    public class HelloWorldService : IHelloWorldService 
    {
        public string GetHelloWordl()
        {
            return "Hello World!"; // Metodo que retorna esto
        }
    }

    // Creacion de interfaz que nos va a ayudar a manejar un tipo abstracto que vamos a poder cambiar facilmenta y a inyectar mas facil las dependencias y podriamos agregar mas logica a la clase y no se veria pq la interfaz expone solamente los metodos que pueden ser utilizados   
    public interface IHelloWorldService
    {
        string GetHelloWordl(); 
    }
}

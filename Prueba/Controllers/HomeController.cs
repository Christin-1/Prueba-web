using Microsoft.AspNetCore.Mvc;
using Prueba.Models;
using System.Diagnostics;

namespace Prueba.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Usuarios()
        {
            // Pasamos datos de ejemplo para los usuarios
            var usuarios = new List<User>
            {
                new User { Id = 1, Nombre = "Cris", Apellido = "Soto", Correo = "cris@gmail.com", Telefono = "123456789", Rol = "Administrador" },
                new User { Id = 2, Nombre = "Tona", Apellido = "Aguilar", Correo = "tona@gmail.com", Telefono = "987654321", Rol = "Cliente" }
            };

            return View(usuarios);
        }

        // Acci�n para manejar el login
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Datos inv�lidos",
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            // Validaci�n de usuario 
            if (model.Email == "cris@gmail.com" && model.Password == "password123") 
            {
                return Ok(new { redirectUrl = "/Home/Usuarios" }); // Redirige a la p�gina de "Usuarios"
            }
            else
            {
                // Si las credenciales son incorrectas, devolver un mensaje de error
                return Unauthorized(new { message = "Credenciales incorrectas" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    // Modelo para el usuario
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Rol { get; set; }
    }

    // Modelo para el login
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

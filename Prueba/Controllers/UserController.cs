using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Prueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Nombre = "Cris", Apellido = "Soto", Correo = "cris@gmail.com", Telefono = "123456789", Rol = "Administrador" },
            new User { Id = 2, Nombre = "Tona", Apellido = "Aguilar", Correo = "tona@gmail.com", Telefono = "987654321", Rol = "Cliente" }
        };

        // Obtener todos los usuarios
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_users);
        }

        // Crear un nuevo usuario
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            // Asignar un nuevo Id 
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }

        // Actualizar un usuario existente
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Actualizamos los valores 
            existingUser.Nombre = user.Nombre;
            existingUser.Apellido = user.Apellido;
            existingUser.Correo = user.Correo;
            existingUser.Telefono = user.Telefono;
            existingUser.Rol = user.Rol;

            return NoContent();
        }

        // Eliminar un usuario
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _users.Remove(user);
            return NoContent();
        }
    }
}

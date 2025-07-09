using CoreApp;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //Indicamos que la direccion del controlador es "https://servidor:puertp/api/user"
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(User user)
        {

            try
            {

                var um = new UserManager();
                um.Create(user);
                return Ok(user); //Retorna el usuario creado con un codigo 200 OK
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);

            }

        }

        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {

                var um = new UserManager();
                var listResults = um.RetrieveAll();

                return Ok(listResults); //Retorna la lista de usuarios con un codigo 200 OK

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);

            }
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int id)
        {
            try
            {
                var um = new UserManager();
                var user = new User { Id = id };
                var result = um.RetrieveById(user);
                if (result == null)
                {
                    return NotFound("No se encontro el usuario");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);

            }
        }

        [HttpGet]
        [Route("RetrieveByEmail")]
        public ActionResult RetrieveByEmail(string email)
        {
            try
            {
                var um = new UserManager();
                var result = um.RetrieveByEmail(new DTOs.User { Email = email });
                if (result == null)
                {
                    return NotFound("No se encontro el usuario con ese email");
                }

                return Ok(result); // Return the retrieved user with a 200 OK status
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("RetrieveByUserCode")]
        public ActionResult RetrieveByUserCode(string userCode)
        {
            try
            {
                var um = new UserManager();
                var result = um.RetrieveByUserCode(userCode);
                if (result == null)
                {
                    return NotFound("No se encontro el usuario con ese codigo");
                }
                return Ok(result);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);

            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(User user)
        {
            try
            {
                var um = new UserManager();
                var existingUser = um.RetrieveById(user);
                if (existingUser == null) {
                    return NotFound("No se encontro el usuario a actualizar");
                }
                um.Update(user);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int Id)
        {
            try
            {
                var um = new UserManager();
                um.Delete(Id);
                return Ok($"Usuario con ID {Id} eliminado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

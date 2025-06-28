using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using CoreApp;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Movie movie)
        {

            try
            {

                var mm = new MovieManager();
                mm.Create(movie);
                return Ok(movie); //Retorna el usuario creado con un codigo 200 OK
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

                var mm = new MovieManager();
                var listResults = mm.RetrieveAll();

                return Ok(listResults); //Retorna la lista de usuarios con un codigo 200 OK

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);

            }
        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(Movie movie)
        {
            try
            {
                var mm = new MovieManager();
                var result = mm.RetrieveById(movie);
                if (result == null)
                {
                    return NotFound("No se encontro la pelicula");
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
        public ActionResult Update(Movie movie)
        {
            try
            {
                var mm = new MovieManager();
                mm.Update(movie);
                if (movie == null)
                {
                    return NotFound("No se encontro la pelicula a actualizar");
                }
                return Ok(movie);
            }
            catch (Exception ex)
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
                var mm= new MovieManager();
                var movie = new Movie { Id = Id };
                var existingMovie = mm.RetrieveById(movie);
                if (existingMovie == null)
                {
                    return NotFound("No se encontro la pelicula a eliminar");
                }
                mm.Delete(movie);
                return Ok("Pelicula eliminada correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

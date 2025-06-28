using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CoreApp
{
    public class MovieManager: BaseManager
    {
        /*metodo para crear una pelicula
         * Valida que el titulo no sea nulo o vacio
         * Valida que la fecha de lanzamiento no sea futura
         * Valida que el genero no sea nulo o vacio
         * Valida que el director no sea nulo o vacio
         */

        public void Create(Movie movie)
        {
            try
            {
                if (string.IsNullOrEmpty(movie.Title))
                {
                    throw new Exception("El titulo de la pelicula no puede ser nulo o vacio");
                }
                if (movie.ReleaseDate > DateTime.Now)
                {
                    throw new Exception("La fecha de lanzamiento no puede ser futura");
                }
                if (string.IsNullOrEmpty(movie.Genre))
                {
                    throw new Exception("El genero de la pelicula no puede ser nulo o vacio");
                }
                if (string.IsNullOrEmpty(movie.Director))
                {
                    throw new Exception("El director de la pelicula no puede ser nulo o vacio");
                }
                // Si todas las validaciones pasan, se procede a crear la pelicula
                var movieCrud = new DataAccess.CRUD.MovieCrudFactory();
                movieCrud.Create(movie);
                //se envia un correo a los usuarios registrados informando de la nueva pelicula
                // Enviar correos a los usuarios registrados
                SendNewMovieEmail(movie).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al crear la pelicula: " + ex.Message);
            }

            
            


        }


        //metodo para enviar un correo a los usuarios registrados informando de la nueva pelicula
        private async Task SendNewMovieEmail(Movie movie)
        {
            try
            {
                var userManager = new UserManager();
                var users = userManager.RetrieveAll();

                foreach (var user in users)
                {
                    var subject = "Nueva Película: " + movie.Title;
                    var body = $"Hola {user.Name},\n\nSe ha agregado una nueva película:\n\n" +
                               $"Título: {movie.Title}\n" +
                               $"Descripción: {movie.Description}\n" +
                               $"Fecha de lanzamiento: {movie.ReleaseDate.ToShortDateString()}\n" +
                               $"Género: {movie.Genre}\n" +
                               $"Director: {movie.Director}\n\n" +
                               "Saludos,\nEl equipo de Movie Manager";

                    await SendEmail(user.Email, subject, body);
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }

        // Método que envía un correo usando SendGrid
        private async Task SendEmail(string recipientEmail, string subject, string plainTextContent)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGridKey");

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("La API Key de SendGrid no está configurada.");

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("abarrantesc@ucenfotec.ac.cr", "CenfoCinemas");
            var to = new EmailAddress(recipientEmail);

            var htmlContent = $"<pre>{plainTextContent}</pre>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

            if ((int)response.StatusCode >= 400)
            {
                throw new Exception($"Error enviando el email: {response.StatusCode}");
            }
        }

    }
}

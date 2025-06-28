using DataAccess.CRUD;
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
    public class UserManager : BaseManager
    {
        /*
         *  Metodo para la creacion de un usuario
         *  Valida que el usuario sea mayor de 18 a;os
         *  Valida que el codigo de usuario este disponible
         *  Valida que el email no este registrado
         *  Envia un email de confirmacion al usuario
         */

        public void Create(User user)
        {
            try
            {
                //Validad edad
                if (IsOver18(user))
                {
                    var uCrud = new UserCrudFactory();

                    //comprobar si el usuario ya existe EN DB
                    var uExist = uCrud.RetrieveByUserCode<User>(user); //VER CLASE Y ARREGLAR
                    if (uExist == null)
                    {

                        //comprobar si el email ya existe EN DB
                        uExist = uCrud.RetrieveByEmail<User>(user);

                        if (uExist == null)
                        {
                            uCrud.Create(user);
                            //Ahora sigue el envio de corrreo de confirmacion
                            SendWelcomeEmail(user.Email, user.Name).GetAwaiter().GetResult(); //Llamada al metodo asincrono para enviar el correo


                        }
                        else
                        {
                            throw new Exception("Este correo electronico ya se encuentra registrado");
                        }
                    }
                    else
                    {
                        throw new Exception("El codigo de usuario no esta disponible");
                    }

                    //uCrud.Create(user);
                }
                else
                {
                    throw new Exception("Usuario no cumple con la edad minima");
                }
            }
            catch (Exception ex)
            {
                ManageException(ex);
            }
        }

        public List<User> RetrieveAll()
        {
            var uCrud = new UserCrudFactory();
            return uCrud.RetrieveAll<User>();
        }

        private bool IsOver18(User user)
        {
            var currentDate = DateTime.Now;
            int age = currentDate.Year - user.BirthDate.Year;

            if (user.BirthDate > currentDate.AddYears(-age).Date)
            {
                age--;
            }
            return age >= 18;
        }

        //Enviar un correo de bienvenida al usuario
        private async Task SendWelcomeEmail(string recipientEmail, string recipientName)
        {
            var apiKey = Environment.GetEnvironmentVariable("SendGridKey");

            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("abarrantesc@ucenfotec.ac.cr", "CenfoCinemas");
            var subject = "Bienvenido a CenfoCinemas";

            var to = new EmailAddress(recipientEmail, recipientName);

            var plainTextContent = $"Hola {recipientName}, gracias por registrarte.";
            var htmlContent = $"<strong>Hola {recipientName}, gracias por registrarte.</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            var response = await client.SendEmailAsync(msg);

        }
        }
}

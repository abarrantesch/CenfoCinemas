using DataAccess.CRUD;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class UserManager: BaseManager
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
                            //Ahora sigue el envio de reo de confirmacion
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

    }
}

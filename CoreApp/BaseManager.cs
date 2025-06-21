using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class BaseManager
    {
        protected void ManageException(Exception exception)
        {
            throw exception; // Aquí se lanza la excepción para que sea manejada por el llamador
            //TO DO>Implementacion de manejo de excepciones pendiente
        }
    }
    
}

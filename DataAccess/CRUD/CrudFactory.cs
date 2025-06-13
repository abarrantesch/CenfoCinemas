using DataAccess.DAO;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{   //clase madre abstracta de los crud
    //define como se hacen los crud en la arquitectura de la aplicacion
    public abstract class CrudFactory
    {

        protected SqlDao _sqlDao; //clases abstractas consitutyen un contrato. Al heredar de esa clase hay ciertas reglas que se deben cumplir.

        //Definir los metodos que deben ser implementados por las clases hijas (metodos que forman parte del contrato)
        // C=Create, R=Read, U=Update, D=Delete

        public abstract void Create(BaseDTO baseDTO);
        public abstract void Update(BaseDTO baseDTO);
        public abstract void Delete(BaseDTO baseDTO);

        public abstract T Retrieve<T>();

        public abstract T RetrieveById<T>();

        public abstract List<T> RetrieveAll<T>();




    }
}

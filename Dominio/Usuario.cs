using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        private string _correo;
        private string _contrasenia;

        public Usuario(string correo, string contrasenia)
        {
            _correo = correo;
            _contrasenia = contrasenia;
        }

        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(_contrasenia))
            {
                throw new Exception("Debes ingresar una contrasenia");
            }
            if (string.IsNullOrEmpty(_correo))
            {
                throw new Exception("No ingresaste email");
            }
        }


        public string Correo
        {
            get { return _correo; }
            
        }

        public string Contrasenia
        {
            get { return _contrasenia; }
            
        }

        // Método polimórfico
        public abstract string ObtenerInformacion();

        
    }

}

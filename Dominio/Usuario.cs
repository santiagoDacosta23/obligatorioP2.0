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

        //constructor 
        public Usuario(string correo, string contrasenia)
        {
            _correo = correo;
            _contrasenia = contrasenia;
        }

        // constructor vacio :
        public Usuario()
        {
        }


        // get y set : 
        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }

        public string Contrasenia
        {
            get { return _contrasenia; }
            set { _contrasenia = value; }

        }

        //Metodo de validacion: 
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
            if (Contrasenia.Length < 8 )
            throw new Exception("La contraseña debe tener al menos 8 caracteres y una letra mayúscula.");

        }


        // Método polimórfico
        public abstract string ObtenerInformacion();


        // equals
        public override bool Equals(object? obj)
        {
            Usuario us = obj as Usuario;
            return us != null && _correo == us.Correo;
        }

    }

}

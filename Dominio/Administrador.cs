using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Administrador : Usuario
    {
        private string _apodo;

        public Administrador(string correo, string contrasenia, string apodo)
            : base(correo, contrasenia)
        {
            Apodo = apodo;
        }

        public string Apodo
        {
            get { return _apodo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El apodo no puede estar vacío.");
                }
                _apodo = value;
            }
        }


        public override string ObtenerInformacion()
        {
            return $">> Administrador:\nCorreo: {Correo}, Apodo: {Apodo}";
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }


    }
}
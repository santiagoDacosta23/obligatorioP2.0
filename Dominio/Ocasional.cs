using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    

public class  Ocasional : Cliente
    {
        private bool _elegido;

        public bool elegido
        {
            get { return _elegido; }
            set { _elegido = value; }
        }

        // constructor vacio: 
        public Ocasional()
        {
        }

        public Ocasional(string nombre, string correo, string documento, string nacionalidad, string contrasenia)
            :base(nombre,correo,documento,nacionalidad,contrasenia)
        {
            // elegide de forma random si el ocacional, es elegido o no: 
            Random random = new Random();
            _elegido = random.Next(2) == 0;
        }

        // valida 
        public override void Validar()
        {
            // valida que el nombre y nacionalidad no pueden ser 0
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Nacionalidad))
                throw new Exception("Nombre y nacionalidad no pueden estar vacíos.");
            // docuemento menor a 6: 
            if (string.IsNullOrEmpty(Documento) || Documento.Length <6)
                throw new Exception("Documento inválido.");

        }
        
        // esto elegibilidad: 
        public void CambiarElegibilidad()
        {
            if(elegido)
            {
                _elegido = false;
            }
            else
            {
                _elegido = true;
            }
        }

        //muestra si es elejido o no en la tabla : 
        public string ObtenerEstadoElegibilidad()
        {
            return _elegido ? "Elegible" : "No elegible";
        }

        //Devuelve "selected" si el valor coincide con _elegido, de lo contrario devuelve una cadena vacía: 
        public string ObtenerSeleccion(bool valor)
        {
            return _elegido == valor ? "selected" : "";
        }
        //  mostrar la información del cliente ocasional

        public override string ObtenerInformacion()
        {
            return $">> Cliente Ocasional:\nNombre: {Nombre}, Correo: {Correo}, Estado: {elegido}";
        }

        //calcular precio pasaje : 
        public override double ObtenerDescuento(TipoEquipaje e)
        {

            double recargo = 1;
            switch (e)
            {
                case TipoEquipaje.CABINA:
                    recargo = 1.1;
                    break;
                case TipoEquipaje.BODEGA:
                    recargo = 1.2;
                    break;
                default:
                    break;
            }
            return recargo;
        }

        
    }
}

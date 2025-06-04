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

        public Ocasional(string nombre, string correo, string documento, string nacionalidad, string contrasenia, bool elegido)
            : base(nombre,correo,documento,nacionalidad,contrasenia)
        {
            _elegido = elegido;
            
        }

        public override void Validar()
        {
            base.Validar();

        }
        

        public void CambiarElegibilidad(bool elegido)
        {
            _elegido = elegido;
        }

        public string ObtenerEstadoElegibilidad()
        {
            return _elegido ? "Elegible" : "No elegible";
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

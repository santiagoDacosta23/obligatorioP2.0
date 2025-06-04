using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Premium : Cliente 
    {
        private int _puntos;

        public Premium(string nombre, string correo, string documento, string nacionalidad, string contrasenia, int puntos)
            : base(nombre,correo,documento,nacionalidad,contrasenia)
        {
            _puntos = puntos;
        }

        public override void Validar()
        {
            base.Validar();          
            if (_puntos < 0)
            {
                throw new Exception("Los puntos deben ser mayor a 0");
            }
        }

        public int Puntos
        {
            get { return _puntos; }
            set { _puntos = value; }
        }


        public void ModificarPuntos(int puntos)
        {
            _puntos = puntos;
        }

        public override string ObtenerInformacion()
        {
            return $">> Cliente Premium:\nNombre: {Nombre}, Correo: {Correo}, Puntos: {Puntos}";
        }
        //calcular precio pasaje : 
        public override double ObtenerDescuento(TipoEquipaje e)
        {
            double recargo = 1;
            switch (e)
            {
                case TipoEquipaje.BODEGA:
                    recargo = 1.05;
                    break;
                default:
                    break;
            }
            return recargo;
        }

    }

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ruta: IValidable
    {
        private static int _ultimoId = 0;

        private int _id;
        private double _distancia;
        private Aeropuerto _aeropuertoSalida;
        private Aeropuerto _aeropuertoLlegada;

        public string DevolverAeropuertos()
        {
            return $"{_aeropuertoSalida.CodigoIATA}-{_aeropuertoLlegada.CodigoIATA}";
        }

        public void Validar()
        {
            if (_distancia < 0)
            {
                throw new Exception("La distancia debe ser mayor a 0");
            }
        }


        public Ruta(Aeropuerto aeropuertoSalida, Aeropuerto aeropuertoLlegada, double distancia)
        {
            if (aeropuertoSalida == null || aeropuertoLlegada == null)
            {
                throw new ArgumentException("Los aeropuertos no pueden ser nulos.");
            }

            if (aeropuertoSalida == aeropuertoLlegada)
            {
                throw new ArgumentException("El aeropuerto de salida y llegada no pueden ser el mismo.");
            }

            if (distancia <= 0)
            {
                throw new ArgumentException("La distancia debe ser un valor positivo.");
            }

            _id = ++_ultimoId;
            _aeropuertoSalida = aeropuertoSalida;
            _aeropuertoLlegada = aeropuertoLlegada;
            _distancia = distancia;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public double Distancia
        {
            get { return _distancia; }
            set { _distancia = value; }
        }

        public Aeropuerto AeropuertoSalida
        {
            get { return _aeropuertoSalida; }
            set { _aeropuertoSalida = value;}
        }

        public Aeropuerto AeropuertoLlegada
        {
            get { return _aeropuertoLlegada; }
            set { _aeropuertoLlegada= value; }

        }

        public double CostoOpeSalida()
        {
            double costo = 0;
            costo = _aeropuertoSalida.CostoOperacion;
            return costo;
        }
        public double CostoOpeLlegada()
        {
            double costo = 0;
            costo = _aeropuertoLlegada.CostoOperacion;
            return costo;
        }
    }

}

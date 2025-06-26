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

        //get y set:
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
            set { _aeropuertoSalida = value; }
        }

        public Aeropuerto AeropuertoLlegada
        {
            get { return _aeropuertoLlegada; }
            set { _aeropuertoLlegada = value; }

        }
        public string DevolverAeropuertos()
        {
            return $"{_aeropuertoSalida.CodigoIATA}-{_aeropuertoLlegada.CodigoIATA}";
        }

        //constructor:
        public Ruta(Aeropuerto aeropuertoSalida, Aeropuerto aeropuertoLlegada, double distancia)
        {
           
            _id = ++_ultimoId;
            _aeropuertoSalida = aeropuertoSalida;
            _aeropuertoLlegada = aeropuertoLlegada;
            _distancia = distancia;
        }

        

        //Metodo validar: 
        public void Validar()
        {
            if (_aeropuertoSalida == null || _aeropuertoLlegada == null)
            {
                throw new Exception("Los aeropuertos no pueden ser nulos.");
            }

            if (_aeropuertoSalida == _aeropuertoLlegada)
            {
                throw new Exception("El aeropuerto de salida y llegada no pueden ser el mismo.");
            }

            if (_distancia <= 0)
            {
                throw new Exception("La distancia debe ser un valor positivo.");
            }
        }

        //Cosoto de operacion de salida: 
        public double CostoOpeSalida()
        {
            double costo = 0;
            costo = _aeropuertoSalida.CostoOperacion;
            return costo;
        }

        //Costo de operacion de llegada: 
        public double CostoOpeLlegada()
        {
            double costo = 0;
            costo = _aeropuertoLlegada.CostoOperacion;
            return costo;
        }
        // equals :
        public override bool Equals(object? obj)
        {
            Ruta otra = obj as Ruta;
            return this.Id == otra.Id;
        }
    }



}

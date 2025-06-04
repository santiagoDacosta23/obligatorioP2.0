using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Pasaje: IComparable<Pasaje>, IValidable
    {


        private static int _ultimoId = 0;

        private int _id;
        private DateTime _fecha;
        private Vuelo _vuelo;
        private Cliente _pasajero;
        private TipoEquipaje _equipaje;
        private double _precioPasaje;

        public string DevolverAeropuertos()
        {
            return _vuelo.DevolverAeropuertos();
        }
        public string DevolverNombrePasajero()
        {
            return _pasajero.Nombre;
        }


        public void Validar()
        {
            if (_precioPasaje < 0)
            {
                throw new Exception("El precio debe ser mayor a 0");
            }
        }


        public Pasaje(Vuelo vuelo, DateTime fecha, Cliente pasajero, TipoEquipaje equipaje, double precioPasaje)
        {
            if (vuelo == null)
            {
                throw new ArgumentException("El vuelo no puede ser nulo.");
            }

            if (pasajero == null)
            {
                throw new ArgumentException("El pasajero no puede ser nulo.");
            }

            //  el tipo de equipaje sea uno de los tres posibles
            if (!Enum.IsDefined(typeof(TipoEquipaje), equipaje))
            {
                throw new ArgumentException("El tipo de equipaje debe ser 'LIGHT', 'CABINA' o 'BODEGA'.");
            }

         
            _id = ++_ultimoId;
            _vuelo = vuelo;
            _fecha = fecha;
            _pasajero = pasajero;
            _equipaje = equipaje;
            _precioPasaje = precioPasaje;


        }

        public int Id
        {
            get { return _id; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
        }

        public Vuelo Vuelo
        {
            get { return _vuelo; }
        }

        public Cliente Pasajero
        {
            get { return _pasajero; }
        }

        public TipoEquipaje Equipaje
        {
            get { return _equipaje; }
        }

        public double PrecioPasaje
        {
            get { return _precioPasaje; }
        }

        // calcular costo pasaje: 
        public double calcularPrecioPasaje()
        { 
             double totalPasaje = 0;
             double costoPorAsiento = Vuelo.CalcularCostoPorAsiento();
             double descuento = Pasajero.ObtenerDescuento(Equipaje);
             double tasas = Vuelo.Ruta.AeropuertoSalida.tasa + Vuelo.Ruta.AeropuertoLlegada.tasa;
             totalPasaje = (costoPorAsiento * descuento)+ tasas;
                return totalPasaje;

        }

     
        public override string ToString()
        {
            return $"Id pasaje:{_id} \n" +
                $" Nombre pasajero:{_pasajero.Nombre}" +
                $" \n Precio pasje:{calcularPrecioPasaje()} \n Fecha:{_fecha} " +
                $"\n Numero de vuelo:{_vuelo.NumeroVuelo} \n  ";
        }

        public int CompareTo(Pasaje? other)
        {
            return _fecha.CompareTo(other.Fecha);
        }
    }



}


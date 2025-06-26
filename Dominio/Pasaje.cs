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

        //get y set : 
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
            get { return CalcularPrecioPasaje(); }
        }
        public string DevolverAeropuertos()
        {
            return _vuelo.DevolverAeropuertos();
        }
        public string DevolverNombrePasajero()
        {
            return _pasajero.Nombre;
        }

        //Constructor: 
        public Pasaje(Vuelo vuelo, DateTime fecha, Cliente pasajero, TipoEquipaje equipaje, double precioPasaje)
        {
       
            _id = ++_ultimoId;
            _vuelo = vuelo;
            _fecha = fecha;
            _pasajero = pasajero;
            _equipaje = equipaje;
            _precioPasaje = precioPasaje;
        }


        private DiaSemana ConvertirDia(DateTime fecha)
        {
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday: return DiaSemana.Lunes;
                case DayOfWeek.Tuesday: return DiaSemana.Martes;
                case DayOfWeek.Wednesday: return DiaSemana.Miercoles;
                case DayOfWeek.Thursday: return DiaSemana.Jueves;
                case DayOfWeek.Friday: return DiaSemana.Viernes;
                case DayOfWeek.Saturday: return DiaSemana.Sabado;
                case DayOfWeek.Sunday: return DiaSemana.Domingo;
                default: throw new ArgumentException("Día inválido");
            }
        }


        //Metodo Validar:
        public void Validar()
        {
            if (_vuelo == null)
            {
                throw new ArgumentException("El vuelo no puede ser nulo.");
            }

            if (_pasajero == null)
            {
                throw new ArgumentException("El pasajero no puede ser nulo.");
            }

            // Validar tipo de equipaje
            if (!Enum.IsDefined(typeof(TipoEquipaje), _equipaje))
            {
                throw new ArgumentException("El tipo de equipaje debe ser 'LIGHT', 'CABINA' o 'BODEGA'.");
            }

            if (_precioPasaje < 0)
            {
                throw new Exception("El precio debe ser mayor a 0");
            }

            // Validar frecuencia
            DiaSemana diaPasaje = ConvertirDia(_fecha);
            if (!_vuelo.Frecuencia.Contains(diaPasaje))
            {
                throw new Exception("La fecha ingresada no coincide con la frecuencia del vuelo.");
            }
        }


        // calcular costo pasaje: 
        public double CalcularPrecioPasaje()
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
                $" \n Precio pasje:{CalcularPrecioPasaje()} \n Fecha:{_fecha} " +
                $"\n Numero de vuelo:{_vuelo.NumeroVuelo} \n  ";
        }

        //Ordena los pasajes por fecha: 
        public int CompareTo(Pasaje? other)
        {
            return _fecha.CompareTo(other.Fecha);
        }
        //equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Pasaje otro = (Pasaje)obj;

            return _vuelo.NumeroVuelo == otro._vuelo.NumeroVuelo &&
                   _fecha.Date == otro._fecha.Date &&
                   _pasajero.Correo == otro._pasajero.Correo;
        }
    }



}


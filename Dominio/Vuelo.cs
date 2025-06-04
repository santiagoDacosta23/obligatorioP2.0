using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{


    public class Vuelo
    {
        private string _numeroVuelo;
        private Ruta _ruta;
        private Avion _avion;
        private List <DiaSemana> _frecuencia;

        public string DevolverAeropuertos()
        {
            return _ruta.DevolverAeropuertos();
        }


        public Vuelo(string numeroVuelo, Ruta ruta, Avion avion, List<DiaSemana> frecuencia)
        {
            if (!EsNumeroVueloValido(numeroVuelo))
            {
                throw new Exception("El número de vuelo no es válido.");
            }

            if (ruta == null)
            {
                throw new Exception("La ruta no puede ser nula.");
            }

            if (avion == null)
            {
                throw new Exception("El avión no puede ser nulo.");
            }

            if (frecuencia == null || frecuencia.Count == 0)
            {
                throw new Exception("Debe indicarse al menos un día de frecuencia.");
            }

            if (avion.Alcance < ruta.Distancia)
            {
                throw new Exception("El alcance del avión no permite cubrir la distancia de la ruta.");
            }

            _numeroVuelo = numeroVuelo;
            _ruta = ruta;
            _avion = avion;
            _frecuencia = frecuencia;
        }

        public string NumeroVuelo
        {
            get { return _numeroVuelo; }
            set { _numeroVuelo = value; }
        }

        public Ruta Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }

        public Avion Avion
        {
            get { return _avion; }
            set { _avion = value; }
        }

        public List<DiaSemana> Frecuencia
        {
            get { return _frecuencia; }
            set { _frecuencia = value; }
        }

        //METODO VALIDACION DE VUELO 
        //valida el vuelo a traves de su numero. 
        public static bool EsNumeroVueloValido(string numeroVuelo)
        {

            if (string.IsNullOrWhiteSpace(numeroVuelo) || numeroVuelo.Length < 3)
            {
                return false;
            }


            if (!char.IsUpper(numeroVuelo[0]) || !char.IsUpper(numeroVuelo[1]))
            {
                return false;
            }


            for (int i = 2; i < numeroVuelo.Length; i++)
            {
                if (!char.IsNumber(numeroVuelo[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool EsDiaValido(DateTime fecha)
        {
            DiaSemana dia = ConvertirADiaSemana(fecha);
            return _frecuencia.Contains(dia);
        }

    
        // covierte los dias de la semana en español: 
        private DiaSemana ConvertirADiaSemana(DateTime fecha)
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
                default: throw new Exception("Día no válido.");
            }
        }

        //devuelve  la frecuencia en LISTAR VUELOS 
        public string DevolverFrecuencia()
        {
            string resultado = "";
            foreach (DiaSemana dia in _frecuencia)
            {
                resultado += dia.ToString() + " ";
            }
            return resultado.Trim();
        }


        //Calcula el costo por asiento  
        public double CalcularCostoPorAsiento()
        {

            double costoPorKilometro = _avion.CostoPorKm;


            double distancia = _ruta.Distancia;


            double costoAeropuertoSalida = _ruta.CostoOpeSalida();
            double costoAeropuertoLlegada = _ruta.CostoOpeLlegada();

            // costo total (costo de operación por km * distancia)
            double costoTotal = (costoPorKilometro * distancia) + costoAeropuertoSalida + costoAeropuertoLlegada;

            // costo por asiento
            double costoPorAsiento = costoTotal / _avion.CantidadAsientos;

            return costoPorAsiento;
        }
    }

}




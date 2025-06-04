using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Aeropuerto: IValidable
    {
        private string _IATA;
        private string _ciudad;
        private double _costoOperacion;
        private double _tasa;

        public string CodigoIATA
        {
            get { return _IATA; }
            set { _IATA = value; }
        }

        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
        }

        public double CostoOperacion
        {
            get { return _costoOperacion; }
            set { _costoOperacion = value; }

        }

        public double tasa
        {
            get { return _tasa; }
            set { _tasa = value; }
        }
        public Aeropuerto(string IATA, string ciudad, double costoOperacion, double tasa)
        {
            _IATA = IATA.ToUpper(); 
            _ciudad = ciudad;
            _costoOperacion = costoOperacion;
            _tasa = tasa;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_IATA) && _IATA.Length == 3)
            {
                throw new Exception("Debes ingresar un codigo IATA con 3 letras");
            }

            if (string.IsNullOrWhiteSpace(_ciudad))
            {
                throw new Exception("La ciudad no puede estar vacía.");
            }

            if (_costoOperacion < 0)
            {
                throw new Exception("El costo de operación no puede ser negativo.");
            }

            if (_tasa < 0)
            {
                throw new Exception("El costo de las tasas no puede ser negativo.");
            }
        }

       
    }

}

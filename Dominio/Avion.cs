using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Dominio
{
    public class Avion : IValidable
    {
        private string _fabricante;
        private string _modelo;
        private int _cantidadAsientos;
        private double _costoPorKm;
        private int _alcance;

        public string Fabricante
        {
            get { return _fabricante; }
            set { _fabricante = value; }
        }

        public string Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }

        public int CantidadAsientos
        {
            get { return _cantidadAsientos; }
            set { _cantidadAsientos = value; }
        }

        public double CostoPorKm
        {
            get { return _costoPorKm; }
            set { _costoPorKm = value; }
        }

        public int Alcance
        {
            get { return _alcance; }
            set { _alcance = value; }
        }
        public Avion(string fabricante, string modelo, int cantidadAsientos, double costoPorKm, int alcance)
        {
            _fabricante = fabricante;
            _modelo = modelo;
            _cantidadAsientos = cantidadAsientos;
            _costoPorKm = costoPorKm;
            _alcance = alcance;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_fabricante))
            {
                throw new Exception("Debes ingresar un fabricante");
            }
            if (string.IsNullOrEmpty(_modelo))
            {
                throw new Exception("Debes ingresar un modelo");
            }
            if (_cantidadAsientos < 0)
            {
                throw new Exception("La cantidad de asientos debe ser mayor a 0");
            }
            if (_alcance < 0)
            {
                throw new Exception("El alcance debe ser mayor a 0");
            }
            if (_costoPorKm < 0)
            {
                throw new Exception("El costo por KM debe ser mayor a 0");
            }
        }



        // equals de Avion : 
        public override bool Equals(object? obj)
        {
            Avion avion = obj as Avion;
            return avion != null && _fabricante == avion.Fabricante && _modelo == avion.Modelo;
        }


    }
}

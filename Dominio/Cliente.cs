using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Cliente : Usuario, IComparable <Cliente>
    {
        private string _documento;
        private string _nombre;
        private string _nacionalidad;
        public string Documento
        {
            get { return _documento; }
            set { _documento = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Nacionalidad
        {
            get { return _nacionalidad; }
            set { _nacionalidad = value; }
        }

        // el constructor vacio recibe objetos completos: ahre 
        public Cliente()
        {
        }

        public Cliente(string nombre, string correo, string documento, string nacionalidad, string contrasenia)
           : base(correo, contrasenia)
        {
            _documento = documento;
            _nombre = nombre;
            _nacionalidad = nacionalidad;
        }



        public override void Validar()
        {
            base.Validar();
            if (string.IsNullOrEmpty(_documento))
            {
                throw new Exception("Debes ingresar documento");
            }
            if (string.IsNullOrEmpty(_nombre))
            {
                throw new Exception("Debes ingresar nombre");
            }
            if (string.IsNullOrEmpty(_nacionalidad))
            {
                throw new Exception("Debes ingresar nacionalidad");
            }

        }


        public override string ToString()
        {
            return $"Nombre: {Nombre}\nCorreo: {Correo}\nDocumento: {Documento}\nNacionalidad: {Nacionalidad}";
        }
        public abstract double ObtenerDescuento(TipoEquipaje e);

        //ordena los clientes por Documento: 
        public int CompareTo(Cliente other)
        {
            return this.Documento.CompareTo(other.Documento);
        }

        public override bool Equals(object? obj)
        {
            Cliente cli = obj as Cliente;
            return cli != null && _documento == cli.Documento;
        }



    }



}

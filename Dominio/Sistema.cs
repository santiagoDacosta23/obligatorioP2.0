using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    public class Sistema
    {

        private static Sistema instancia = null;


        private List<Usuario> usuarios;
        private List<Avion> aviones;
        private List<Aeropuerto> aeropuertos;
        private List<Ruta> rutas;
        private List<Vuelo> vuelos;
        private List<Pasaje> pasajes;
        private List<Cliente> clientes;//borrar 
      

        //listar pasaje 
        public List<Pasaje> Pasajes { get { return new List<Pasaje>(pasajes); } }

        //get de la lista de usuarios
        public List<Usuario> Usuarios { get { return new List<Usuario>(usuarios); } }
        // listar vuelo 
        public List<Vuelo> Vuelos { get { return new List<Vuelo>(vuelos); } }

        private Sistema()
        {
            usuarios = new List<Usuario>();
            aviones = new List<Avion>();
            aeropuertos = new List<Aeropuerto>();
            rutas = new List<Ruta>();
            vuelos = new List<Vuelo>();
            pasajes = new List<Pasaje>();
            clientes = new List<Cliente>();
          
            PrecargarAdministradores();
            PrecargarClientesPremium();
            PrecargarClientesOcasionales();
            PrecargarAviones();
            PrecargarAeropuertos();
            PrecargarRutas();
            PrecargarVuelos();
            PrecargarPasajes();
        }
        public static Sistema Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new Sistema();
                }
                return instancia;
            }
        }




        // CASE 1. Obtener la info de todos los clientes 

        public List<string> ObtenerInformacionClientes()
        {
            List<string> informacionClientes = new List<string>();

            foreach (Usuario usuario in clientes)
            {
                informacionClientes.Add(usuario.ObtenerInformacion()); // POLIMORFISMO
            }

            return informacionClientes;
        }

       

        //para actualizar los puntos de los clientes premium 
        public void ModificarPuntosPremium(string correo, int nuevosPuntos)
        {
            foreach (Usuario usuario in clientes)
            {
                if (usuario is Premium && usuario.Correo == correo)
                {
                    Premium clientePremium = (Premium)usuario;
                    clientePremium.ModificarPuntos(nuevosPuntos);
                }
            }
        }


        //cambia la elegibilidad del cliente Ocasioanl 
        public void CambiarElegibilidadOcasional(string correo, bool nuevaElegibilidad)
        {
            foreach (Usuario usuario in clientes)
            {
                if (usuario is Ocasional && usuario.Correo == correo)
                {
                    Ocasional clienteOcasional = (Ocasional)usuario;
                    clienteOcasional.CambiarElegibilidad(nuevaElegibilidad);
                }
            }
        }


        // agregar al usuario
        //METODO
        public void AgregarUsuario(Usuario usuario)
        {
            this.usuarios.Add(usuario);
        }


        //ACA TERMINA CASE 1_______________________________________________________________________
        //__________________________________________________________________________________________



        // case 2: listar los vuelos el codigo de AEROPUERTO: ______________________________________

        //METODO
        public List<string> ListarVuelosPorAeropuerto(string codigoIATA)
        {
            //valida que el codigo pasado tenga 3 LETRAS
            if (codigoIATA.Length != 3 || !codigoIATA.All(char.IsLetter))
            {
                throw new Exception("El código IATA debe tener exactamente 3 letras.");
            }

            List<string> vuelosPorAeropuerto = new List<string>();


            string codigoIATAMayusculas = codigoIATA.ToUpper();

            // Recorrer todos los vuelos en el sistema
            foreach (Vuelo vuelo in vuelos)
            {
                //que sean en mayusuculas
                string codigoSalidaMayusculas = vuelo.Ruta.AeropuertoSalida.CodigoIATA.ToUpper();
                string codigoLlegadaMayusculas = vuelo.Ruta.AeropuertoLlegada.CodigoIATA.ToUpper();

                //  el código IATA ingresado coincide con los códigos de los aeropuertos de salida o llegada
                if (codigoSalidaMayusculas.Equals(codigoIATAMayusculas) || codigoLlegadaMayusculas.Equals(codigoIATAMayusculas))
                {

                    string frecuencias = "";
                    foreach (var frecuencia in vuelo.Frecuencia)
                    {
                        if (frecuencias != "")
                        {
                            frecuencias += ", ";
                        }
                        frecuencias += frecuencia.ToString();
                    }

                    // el código IATA coincide, agregamos el vuelo a la lista
                    vuelosPorAeropuerto.Add($"Vuelo: {vuelo.NumeroVuelo}, " +
                        $"Ruta: {vuelo.Ruta.AeropuertoSalida.CodigoIATA}" +
                        $" -> {vuelo.Ruta.AeropuertoLlegada.CodigoIATA}," +
                        $" Frecuencia: {frecuencias}");
                }
            }


            if (vuelosPorAeropuerto.Count == 0)
            {
                vuelosPorAeropuerto.Add("No se encontraron vuelos para este código IATA.");
            }

            return vuelosPorAeropuerto;
        }






        // CASE 3: __________________________________________________________________________________________________
        //Alta cliente Ocasional 
        //METODO
        public string AltaClienteOcasional(string nombre, string correo, string documento, string nacionalidad, string contrasenia)
        {
            bool existe = false;

            for (int i = 0; i < usuarios.Count; i++)
            {
                if (usuarios[i].Correo.ToUpper() == correo.ToUpper())
                {
                    existe = true;
                }
            }

            if (existe)
            {
                return "Ya existe un cliente con ese correo.";
            }
            else
            {
                Random rnd = new Random();
                bool estado = rnd.Next(2) == 0;

                Ocasional nuevo = new Ocasional(nombre, correo, documento, nacionalidad, contrasenia, estado);
                usuarios.Add(nuevo);

                if (estado)
                {
                    return "Cliente ocasional agregado correctamente. Estado: Elegible";
                }
                else
                {
                    return "Cliente ocasional agregado correctamente. Estado: No elegible";
                }
            }
        }






        //listar los pasajes ordenados por fecha : 
        public List<Pasaje> ListarPasajesOrdenadosPorFecha()
        {
            List<Pasaje> listaPasajes = Pasajes;
            listaPasajes.Sort();
            return listaPasajes;
        }

        // obtener vuelo: 
        public List<Vuelo> ObtenerVuelos()
        {
            return new List<Vuelo>(vuelos);
        }

        //Buscar usuario
        public Usuario BusarUsuario(string correo)
        {
            foreach (Usuario u in usuarios)
            {
                if (u.Correo == correo)
                {
                    return u;
                }
            }
            return null;
        }






        // comienza la precarga : 

        private void PrecargarAdministradores()
        {
            usuarios.Add(new Administrador("admin1", "admin1@mail.com", "1234"));
            usuarios.Add(new Administrador("admin2", "admin2@mail.com", "1234"));
        }

        private void PrecargarClientesPremium()
        {
            clientes.Add(new Premium("Juan Perez", "facu@gmail.com", "1-123-563-1", "uruguaya", "contraseña1", 100));
            clientes.Add(new Premium("Nicolas Lagrof", "nicolaslagroft@gmail.com", "4-123-567-1", "peruna", "contraseña2", 150));
            clientes.Add(new Premium("Camila Sanchez", "camilasanchez@gmail.com", "2-324-123-1", "argentina", "contraseña3", 80));
            clientes.Add(new Premium("Federica Rodriguez", "federicarodriguez@gmail.com", "1-123-321-1", "brasilera", "contraseña4", 120));
            clientes.Add(new Premium("Sofia Balbi", "sofiabalbi@gmail.com", "1-432-125-1", "francesa", "contraseña5", 90));
        }

        private void PrecargarClientesOcasionales()
        {
            clientes.Add(new Ocasional("Lucia Florencio", "eli@gmail.com", "1-457-987-1", "uruguaya", "contraseña6", true));
            usuarios.Add(new Ocasional("Alejandra Presentado", "alejandrapresentado@gmail.com", "3-213-879-1", "peruana", "contraseña6", false));
            clientes.Add(new Ocasional("Laura La Cruz", "lauralacruz@gmail.com", "3-333-122-3", "uruguaya", " contraseña7", true));
            clientes.Add(new Ocasional("Rafael Gigena", "rafaelgigena@gmail.com", "4-444-444-4", "brasilero", "contraseña8", false));
            clientes.Add(new Ocasional("Rita Alida", "ritaalida@gmail.com", "5-555-555-5", "uruguaya", "contraseña9", true));
        }

        private void PrecargarAviones()
        {
            aviones.Add(new Avion("Airbus", "A320", 180, 6000, 4123));
            aviones.Add(new Avion("Boeing", "737", 160, 5500, 5123));
            aviones.Add(new Avion("Airbus", "A330", 250, 11000, 6640));
            aviones.Add(new Avion("Boeing", "777", 300, 15000, 7456));
        }

        private void PrecargarAeropuertos()
        {
            aeropuertos.Add(new Aeropuerto("MVD", "Montevideo", 80, 120));
            aeropuertos.Add(new Aeropuerto("EZE", "Buenos Aires", 90, 110));
            aeropuertos.Add(new Aeropuerto("SCL", "Santiago", 95, 105));
            aeropuertos.Add(new Aeropuerto("GRU", "São Paulo", 85, 115));
            aeropuertos.Add(new Aeropuerto("LIM", "Lima", 100, 130));
            aeropuertos.Add(new Aeropuerto("BOG", "Bogotá", 110, 120));
            aeropuertos.Add(new Aeropuerto("LAX", "Los Angeles", 200, 150));
            aeropuertos.Add(new Aeropuerto("JFK", "New York", 180, 140));
            aeropuertos.Add(new Aeropuerto("MAD", "Madrid", 150, 160));
            aeropuertos.Add(new Aeropuerto("CDG", "París", 160, 170));
            aeropuertos.Add(new Aeropuerto("LHR", "Londres", 170, 180));
            aeropuertos.Add(new Aeropuerto("FCO", "Roma", 130, 140));
            aeropuertos.Add(new Aeropuerto("IST", "Estambul", 140, 150));
            aeropuertos.Add(new Aeropuerto("SFO", "San Francisco", 210, 160));
            aeropuertos.Add(new Aeropuerto("SYD", "Sídney", 220, 180));
            aeropuertos.Add(new Aeropuerto("BOM", "Mumbai", 130, 120));
            aeropuertos.Add(new Aeropuerto("PEK", "Beijing", 140, 130));
            aeropuertos.Add(new Aeropuerto("DXB", "Dubái", 150, 140));
            aeropuertos.Add(new Aeropuerto("SEA", "Seattle", 160, 150));
            aeropuertos.Add(new Aeropuerto("MEX", "Ciudad de México", 90, 110));
        }

        private void PrecargarRutas()
        {
            rutas.Add(new Ruta(aeropuertos[0], aeropuertos[1], 200));
            rutas.Add(new Ruta(aeropuertos[1], aeropuertos[2], 300));
            rutas.Add(new Ruta(aeropuertos[2], aeropuertos[3], 350));
            rutas.Add(new Ruta(aeropuertos[3], aeropuertos[4], 400));
            rutas.Add(new Ruta(aeropuertos[4], aeropuertos[5], 500));
            rutas.Add(new Ruta(aeropuertos[5], aeropuertos[6], 600));
            rutas.Add(new Ruta(aeropuertos[6], aeropuertos[7], 700));
            rutas.Add(new Ruta(aeropuertos[7], aeropuertos[8], 800));
            rutas.Add(new Ruta(aeropuertos[8], aeropuertos[9], 900));
            rutas.Add(new Ruta(aeropuertos[9], aeropuertos[10], 1000));
            rutas.Add(new Ruta(aeropuertos[10], aeropuertos[11], 1100));
            rutas.Add(new Ruta(aeropuertos[11], aeropuertos[12], 1200));
            rutas.Add(new Ruta(aeropuertos[12], aeropuertos[13], 1300));
            rutas.Add(new Ruta(aeropuertos[13], aeropuertos[14], 1400));
            rutas.Add(new Ruta(aeropuertos[14], aeropuertos[15], 1500));
            rutas.Add(new Ruta(aeropuertos[15], aeropuertos[16], 1600));
            rutas.Add(new Ruta(aeropuertos[16], aeropuertos[17], 1700));
            rutas.Add(new Ruta(aeropuertos[17], aeropuertos[18], 1800));
            rutas.Add(new Ruta(aeropuertos[18], aeropuertos[19], 1900));
            rutas.Add(new Ruta(aeropuertos[19], aeropuertos[0], 2000));
            rutas.Add(new Ruta(aeropuertos[1], aeropuertos[8], 2200));
            rutas.Add(new Ruta(aeropuertos[2], aeropuertos[6], 2400));
            rutas.Add(new Ruta(aeropuertos[3], aeropuertos[7], 2600));
            rutas.Add(new Ruta(aeropuertos[4], aeropuertos[9], 2800));
            rutas.Add(new Ruta(aeropuertos[5], aeropuertos[10], 3000));
            rutas.Add(new Ruta(aeropuertos[6], aeropuertos[11], 3200));
            rutas.Add(new Ruta(aeropuertos[7], aeropuertos[12], 3400));
            rutas.Add(new Ruta(aeropuertos[8], aeropuertos[13], 3600));
            rutas.Add(new Ruta(aeropuertos[9], aeropuertos[14], 3800));
            rutas.Add(new Ruta(aeropuertos[10], aeropuertos[15], 4000));
        }
        private void PrecargarVuelos()
        {
            vuelos.Add(new Vuelo("VU001", rutas[0], aviones[0], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Miercoles }));
            vuelos.Add(new Vuelo("VU002", rutas[1], aviones[1], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Jueves }));
            vuelos.Add(new Vuelo("VU003", rutas[2], aviones[2], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Jueves, DiaSemana.Domingo }));
            vuelos.Add(new Vuelo("VU004", rutas[3], aviones[3], new List<DiaSemana> { DiaSemana.Miercoles, DiaSemana.Viernes, DiaSemana.Domingo }));
            vuelos.Add(new Vuelo("VU005", rutas[4], aviones[0], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Sabado }));
            vuelos.Add(new Vuelo("VU006", rutas[5], aviones[1], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Miercoles }));
            vuelos.Add(new Vuelo("VU007", rutas[6], aviones[2], new List<DiaSemana> { DiaSemana.Jueves, DiaSemana.Domingo }));
            vuelos.Add(new Vuelo("VU008", rutas[7], aviones[3], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Jueves }));
            vuelos.Add(new Vuelo("VU009", rutas[8], aviones[0], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Sabado }));
            vuelos.Add(new Vuelo("VU010", rutas[9], aviones[1], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Miercoles }));
            vuelos.Add(new Vuelo("VU011", rutas[10], aviones[2], new List<DiaSemana> { DiaSemana.Domingo, DiaSemana.Martes }));
            vuelos.Add(new Vuelo("VU012", rutas[11], aviones[3], new List<DiaSemana> { DiaSemana.Sabado, DiaSemana.Jueves }));
            vuelos.Add(new Vuelo("VU013", rutas[12], aviones[0], new List<DiaSemana> { DiaSemana.Miercoles, DiaSemana.Viernes }));
            vuelos.Add(new Vuelo("VU014", rutas[13], aviones[1], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Jueves }));
            vuelos.Add(new Vuelo("VU015", rutas[14], aviones[2], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Sabado }));
            vuelos.Add(new Vuelo("VU016", rutas[15], aviones[3], new List<DiaSemana> { DiaSemana.Miercoles, DiaSemana.Domingo }));
            vuelos.Add(new Vuelo("VU017", rutas[16], aviones[0], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Lunes }));
            vuelos.Add(new Vuelo("VU018", rutas[17], aviones[1], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Miercoles }));
            vuelos.Add(new Vuelo("VU019", rutas[18], aviones[2], new List<DiaSemana> { DiaSemana.Jueves, DiaSemana.Sabado }));
            vuelos.Add(new Vuelo("VU020", rutas[19], aviones[3], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Lunes }));
            vuelos.Add(new Vuelo("VU021", rutas[20], aviones[0], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Jueves }));
            vuelos.Add(new Vuelo("VU022", rutas[21], aviones[1], new List<DiaSemana> { DiaSemana.Miercoles, DiaSemana.Domingo }));
            vuelos.Add(new Vuelo("VU023", rutas[22], aviones[2], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Sabado }));
            vuelos.Add(new Vuelo("VU024", rutas[23], aviones[3], new List<DiaSemana> { DiaSemana.Jueves, DiaSemana.Martes }));
            vuelos.Add(new Vuelo("VU025", rutas[24], aviones[0], new List<DiaSemana> { DiaSemana.Domingo, DiaSemana.Miercoles }));
            vuelos.Add(new Vuelo("VU026", rutas[25], aviones[1], new List<DiaSemana> { DiaSemana.Sabado, DiaSemana.Lunes }));
            vuelos.Add(new Vuelo("VU027", rutas[26], aviones[2], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Miercoles }));
            vuelos.Add(new Vuelo("VU028", rutas[27], aviones[3], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Jueves }));
            vuelos.Add(new Vuelo("VU029", rutas[28], aviones[0], new List<DiaSemana> { DiaSemana.Sabado, DiaSemana.Domingo }));
            vuelos.Add(new Vuelo("VU030", rutas[29], aviones[1], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Lunes }));
        }

        private void PrecargarPasajes()
        {
            pasajes.Add(new Pasaje(vuelos[0], new DateTime(2025, 7, 7), clientes[0], TipoEquipaje.LIGHT, 100)); // lunes
            pasajes.Add(new Pasaje(vuelos[1], new DateTime(2025, 7, 8), clientes[1], TipoEquipaje.CABINA, 100)); // martes
            pasajes.Add(new Pasaje(vuelos[2], new DateTime(2025, 7, 10), clientes[2], TipoEquipaje.BODEGA, 100)); // jueves
            pasajes.Add(new Pasaje(vuelos[3], new DateTime(2025, 7, 9), clientes[3], TipoEquipaje.LIGHT, 100)); // miércoles
            pasajes.Add(new Pasaje(vuelos[4], new DateTime(2025, 7, 5), clientes[4], TipoEquipaje.CABINA, 10560)); // sábado
            pasajes.Add(new Pasaje(vuelos[5], new DateTime(2025, 7, 7), clientes[0], TipoEquipaje.BODEGA, 500)); // lunes
            pasajes.Add(new Pasaje(vuelos[6], new DateTime(2025, 7, 6), clientes[1], TipoEquipaje.LIGHT, 100)); // domingo
            pasajes.Add(new Pasaje(vuelos[7], new DateTime(2025, 7, 8), clientes[2], TipoEquipaje.CABINA, 15400)); // martes
            pasajes.Add(new Pasaje(vuelos[8], new DateTime(2025, 7, 4), clientes[3], TipoEquipaje.BODEGA, 1600)); // viernes
            pasajes.Add(new Pasaje(vuelos[9], new DateTime(2025, 7, 7), clientes[4], TipoEquipaje.LIGHT, 1700)); // lunes
            pasajes.Add(new Pasaje(vuelos[10], new DateTime(2025, 7, 8), clientes[0], TipoEquipaje.CABINA, 100)); // martes
            pasajes.Add(new Pasaje(vuelos[11], new DateTime(2025, 7, 10), clientes[1], TipoEquipaje.BODEGA, 100)); // jueves
            pasajes.Add(new Pasaje(vuelos[12], new DateTime(2025, 7, 9), clientes[2], TipoEquipaje.LIGHT, 100)); // miércoles
            pasajes.Add(new Pasaje(vuelos[13], new DateTime(2025, 7, 10), clientes[3], TipoEquipaje.CABINA, 100)); // jueves
            pasajes.Add(new Pasaje(vuelos[14], new DateTime(2025, 7, 5), clientes[4], TipoEquipaje.BODEGA, 100)); // sábado
            pasajes.Add(new Pasaje(vuelos[15], new DateTime(2025, 7, 6), clientes[0], TipoEquipaje.LIGHT, 100)); // domingo
            pasajes.Add(new Pasaje(vuelos[16], new DateTime(2025, 7, 7), clientes[1], TipoEquipaje.CABINA, 100)); // lunes
            pasajes.Add(new Pasaje(vuelos[17], new DateTime(2025, 7, 8), clientes[2], TipoEquipaje.BODEGA, 100)); // martes
            pasajes.Add(new Pasaje(vuelos[18], new DateTime(2025, 7, 5), clientes[3], TipoEquipaje.LIGHT, 100)); // sábado
            pasajes.Add(new Pasaje(vuelos[19], new DateTime(2025, 7, 4), clientes[4], TipoEquipaje.CABINA, 100)); // viernes
            pasajes.Add(new Pasaje(vuelos[20], new DateTime(2025, 7, 7), clientes[0], TipoEquipaje.BODEGA, 100)); // lunes
            pasajes.Add(new Pasaje(vuelos[21], new DateTime(2025, 7, 9), clientes[1], TipoEquipaje.LIGHT, 100)); // miércoles
            pasajes.Add(new Pasaje(vuelos[22], new DateTime(2025, 7, 5), clientes[2], TipoEquipaje.CABINA, 100)); // sábado
            pasajes.Add(new Pasaje(vuelos[23], new DateTime(2025, 7, 8), clientes[3], TipoEquipaje.BODEGA, 100)); // martes
            pasajes.Add(new Pasaje(vuelos[24], new DateTime(2025, 7, 6), clientes[4], TipoEquipaje.LIGHT, 100)); // domingo
            pasajes.Add(new Pasaje(vuelos[25], new DateTime(2025, 7, 7), clientes[0], TipoEquipaje.CABINA, 100)); // lunes
            pasajes.Add(new Pasaje(vuelos[26], new DateTime(2025, 7, 9), clientes[1], TipoEquipaje.BODEGA, 100)); // miércoles
            pasajes.Add(new Pasaje(vuelos[27], new DateTime(2025, 7, 8), clientes[2], TipoEquipaje.LIGHT, 100)); // martes
            pasajes.Add(new Pasaje(vuelos[28], new DateTime(2025, 7, 5), clientes[3], TipoEquipaje.CABINA, 100)); // sábado
            pasajes.Add(new Pasaje(vuelos[29], new DateTime(2025, 7, 7), clientes[4], TipoEquipaje.BODEGA, 100)); // lunes
        }






    }

}


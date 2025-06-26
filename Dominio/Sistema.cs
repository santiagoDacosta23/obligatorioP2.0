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
       
        //listar pasaje 
        public List<Pasaje> Pasajes { get { return new List<Pasaje>(pasajes); } }

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
        private Sistema()
        {
            usuarios = new List<Usuario>();
            aviones = new List<Avion>();
            aeropuertos = new List<Aeropuerto>();
            rutas = new List<Ruta>();
            vuelos = new List<Vuelo>();
            pasajes = new List<Pasaje>();
            PrecargaSistema();
         
        }

        private void PrecargaSistema()
        {
            PrecargarAdministradores();
            PrecargarClientesPremium();
            PrecargarClientesOcasionales();
            PrecargarAviones();
            PrecargarAeropuertos();
            PrecargarRutas();
            PrecargarVuelos();
            PrecargarPasajes();
        }

        // alta usuario
        public void AgregarUsuario(Usuario usuario)
        {
            usuario.Validar();
      
           //Nos manda una excepcion de que ya existe el usuario 
           if (usuarios.Contains(usuario))
           {
               throw new Exception("Ya existe un usuario con ese nombre");
           }
           usuarios.Add(usuario);
        }


        // alta cliente
        public void AgregarCliente(Cliente cliente)
        {
            cliente.Validar();
           if (usuarios.Contains(cliente))
           {
               throw new Exception("Ya existe un cliente con ese nombre");
           }
           usuarios.Add(cliente);
        }

        // alta avion
        public void AgregarAvion(Avion avion)

        {
            avion.Validar();
            // si el avion ya existe nos manda una excepcion: 
            if (aviones.Contains(avion))
            {
                throw new Exception("Ya existe un avion con esos datos");
            }
            aviones.Add(avion);
        }

        // alta aeropuerto
        public void AgregarAeropuerto(Aeropuerto aeropuerto)
        {

            // Validar datos del aeropuerto
            aeropuerto.Validar();

            // Verificar si ya existe un aeropuerto con el mismo código IATA
            if (aeropuertos.Contains(aeropuerto))
            {
                throw new Exception("Ya existe un aeropuerto con ese código IATA.");
            }

            // Agregar aeropuerto a la lista
            aeropuertos.Add(aeropuerto);
        }

        //alta ruta
        public void AgregarRuta(Ruta ruta)
        {
            // Validar datos de la ruta
            ruta.Validar();
            // Verificar si ya existe una ruta con los mismos aeropuertos
            if (rutas.Contains(ruta))
            {
                throw new Exception("Ya existe una ruta con esos aeropuertos.");
            }
            // Agregar ruta a la lista
            rutas.Add(ruta);
        }

      

        public void AgregarVuelo(Vuelo vuelo)
        {
            vuelo.Validar();
            if (vuelos.Contains(vuelo))
            {
                throw new Exception("Ya existe un vuelo con esos datos");
            }
            vuelos.Add(vuelo);
        }

        public void AgregarPasaje(Pasaje nuevo)
        {
            nuevo.Validar();
            ExistePasaje(nuevo);
            pasajes.Add(nuevo);
        }

        private void ExistePasaje(Pasaje nuevo)
        {
            if (this.pasajes.Contains(nuevo))
            {
                throw new Exception("Ya existe el pasaje ");
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

        // lista lo clientes : // ver clientes : 
        public List<Cliente> ObtenerClientes()
        {

            List<Cliente> clientes = new List<Cliente>();

            foreach (Usuario usuario in usuarios)
            {
                if (usuario is Cliente)
                {
                    Cliente cliente = (Cliente)usuario;
                    clientes.Add(cliente);
                }
            }
            //ordena los clientes por cedula
            clientes.Sort();
            return clientes;


        }

        //Buscar usuario
        public Usuario BusarUsuario(string correo)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Correo == correo)
                {
                    return usuario;
                }
            }
            return null;
        }

        //Registrar cliente Ocasional:
        public bool ExisteUsuario(Usuario u)
        {
            return usuarios.Contains(u);
        }
        //registro
        public bool RegistrarCliente(Ocasional nuevo)
        {

            nuevo.Validar();

            if (!ExisteUsuario(nuevo))
            {
                usuarios.Add(nuevo);
                return true;
            }

            return false;
        }

        // ordena los pasajes por precio: 
        public class OrdenPasajesPorPrecio : IComparer<Pasaje>
        {
            public int Compare(Pasaje? p1, Pasaje? p2)
            {
                return p1.PrecioPasaje.CompareTo(p2.PrecioPasaje);
            }
        }
        
        // le muestra al cliente el pasaje que compro: 
        public List<Pasaje> ObtenerPasajesDelClientePorPrecio(string correo)
        {
            List<Pasaje> resultado = new List<Pasaje>();

            foreach (Pasaje pasaje in pasajes)
            {
                if (pasaje.Pasajero != null && pasaje.Pasajero.Correo == correo)
                {
                    resultado.Add(pasaje);
                }
            }

            resultado.Sort(new OrdenPasajesPorPrecio());
            return resultado;
        }

        //obetener los vuelos por la ruta: 
        public List<Vuelo> ObtenerVuelosPorRuta(string iataSalida, string iataLlegada)
        {
            List<Vuelo> filtrados = new List<Vuelo>();

            foreach (Vuelo vuelo in vuelos)

            {
                string salida = vuelo.ObtenerIataSalida();
                string llegada = vuelo.ObtenerIataLlegada();


                // Ambos campos vacíos: devolver todos
                if (string.IsNullOrEmpty(iataSalida) && string.IsNullOrEmpty(iataLlegada))
                {
                    filtrados.Add(vuelo);
                }
                // Ambos completos: coinciden ambos
                else if (!string.IsNullOrEmpty(iataSalida) && !string.IsNullOrEmpty(iataLlegada))
                {
                    if (salida == iataSalida.ToUpper() && llegada == iataLlegada.ToUpper())
                    {
                        filtrados.Add(vuelo);
                    }
                }
                // Solo uno de los dos
                else if (!string.IsNullOrEmpty(iataSalida))
                {
                    if (salida == iataSalida.ToUpper() || llegada == iataSalida.ToUpper())
                    {
                        filtrados.Add(vuelo);
                    }
                }
                else {
                    if (salida == iataLlegada.ToUpper() || llegada == iataLlegada.ToUpper())
                    {
                        filtrados.Add(vuelo);
                    }
                }
            }

            return filtrados;
        }


        // Obtener vuelos por id: 
        public Vuelo ObtenerVueloPorId(string idVuelo)
        {
            foreach (Vuelo vuelo in vuelos)
            {
                if (vuelo.NumeroVuelo == idVuelo)
                {
                    return vuelo;
                }
            }
            return null;
        }

        // clientes por cedula. 
        public Cliente BuscarClientePorDocumento(string documento)
        {
            foreach (Cliente cliente in usuarios.OfType<Cliente>())
            {
                if (cliente.Documento == documento)
                {
                    return cliente;
                }
            }
            return null;
        }


        // comienza la precarga : 

        //PRECARGA DE ADMINISTRADORES: 
        private void PrecargarAdministradores()
        {
            Administrador a1 = new Administrador("admin1@gmail.com", "administrador1", "1234");
            Administrador a2 = new Administrador("admin2@gmail.com", "administradores2", "1234");
            AgregarUsuario(a1);
            AgregarUsuario(a2);
        }


        //PRECARGA DE CLIENTES PREMIUM : 
        private void PrecargarClientesPremium()
        { 
            Premium p1 = new Premium ("Juan Perez", "facu@gmail.com", "1-123-563-1", "uruguaya", "contraseña1", 100);
            Premium p2 = new Premium("Nicolas Lagrof", "nicolaslagroft@gmail.com", "4-123-567-1", "peruana", "contraseña2", 150);
            Premium p3 = new Premium("Camila Sanchez", "camilasanchez@gmail.com", "2-324-123-1", "argentina", "contraseña3", 80);
            Premium p4 = new Premium("Federica Rodriguez", "federicarodriguez@gmail.com", "1-123-321-1", "brasilera", "contraseña4", 120);
            Premium p5 = new Premium("Sofia Balbi", "sofiabalbi@gmail.com", "1-432-125-1", "francesa", "contraseña5", 90);

            AgregarCliente(p1);
            AgregarCliente(p2);
            AgregarCliente(p3);
            AgregarCliente(p4);
            AgregarCliente(p5);


        }

        //PRECARGA DE CLIENTE OCACIONAL : 
        private void PrecargarClientesOcasionales()
        {
           Ocasional o1 = new Ocasional("Lucia Florencio", "eli@gmail.com", "1-457-987-1", "uruguaya", "contraseña6");
           Ocasional o2 = new Ocasional("Alejandra Presentado", "alejandrapresentado@gmail.com", "3-213-879-1", "peruana", "contraseña6");
           Ocasional o3 = new Ocasional("Laura La Cruz", "lauralacruz@gmail.com", "3-333-122-3", "uruguaya", "contraseña7");
           Ocasional o4 = new Ocasional("Rafael Gigena", "rafaelgigena@gmail.com", "4-444-444-4", "brasilero", "contraseña8");
           Ocasional o5 = new Ocasional("Rita Alida", "ritaalida@gmail.com", "5-555-555-5", "uruguaya", "contraseña9");

           AgregarCliente(o1);
           AgregarCliente(o2);
           AgregarCliente(o3);
           AgregarCliente(o4);
           AgregarCliente(o5);
        }
       //PRECARGA DE AVIONES : 
        private void PrecargarAviones()
        {

            Avion avion1 = new Avion("Airbus", "A320", 180, 6000, 10000);
            Avion avion2 = new Avion("Boeing", "737", 160, 5500, 12000);
            Avion avion3 = new Avion("Airbus", "A330", 250, 11000, 11000);
            Avion avion4 = new Avion("Boeing", "777", 300, 15000, 90000);

               AgregarAvion(avion1);
               AgregarAvion(avion2);
               AgregarAvion(avion3);
               AgregarAvion(avion4);

        }
        //Precarga Aeropuerto: 
        private void PrecargarAeropuertos()
        {
            Aeropuerto ad1 = new Aeropuerto("MVD", "Montevideo", 80, 120);
            Aeropuerto ad2 = new Aeropuerto("EZE", "Buenos Aires", 90, 110);
            Aeropuerto ad3 = new Aeropuerto("SCL", "Santiago", 95, 105);
            Aeropuerto ad4 = new Aeropuerto("GRU", "São Paulo", 85, 115);
            Aeropuerto ad5 = new Aeropuerto("LIM", "Lima", 100, 130);
            Aeropuerto ad6 = new Aeropuerto("BOG", "Bogotá", 110, 120);
            Aeropuerto ad7 = new Aeropuerto("LAX", "Los Angeles", 200, 150);
            Aeropuerto ad8 = new Aeropuerto("JFK", "New York", 180, 140);
            Aeropuerto ad9 = new Aeropuerto("MAD", "Madrid", 150, 160);
            Aeropuerto ad10 = new Aeropuerto("CDG", "París", 160, 170);
            Aeropuerto ad11 = new Aeropuerto("LHR", "Londres", 170, 180);
            Aeropuerto ad12 = new Aeropuerto("FCO", "Roma", 130, 140);
            Aeropuerto ad13 = new Aeropuerto("IST", "Estambul", 140, 150);
            Aeropuerto ad14 = new Aeropuerto("SFO", "San Francisco", 210, 160);
            Aeropuerto ad15 = new Aeropuerto("SYD", "Sídney", 220, 180);
            Aeropuerto ad16 = new Aeropuerto("BOM", "Mumbai", 130, 120);
            Aeropuerto ad17 = new Aeropuerto("PEK", "Beijing", 140, 130);
            Aeropuerto ad18 = new Aeropuerto("DXB", "Dubái", 150, 140);
            Aeropuerto ad19 = new Aeropuerto("SEA", "Seattle", 160, 150);
            Aeropuerto ad20 = new Aeropuerto("MEX", "Ciudad de México", 90, 110);

            AgregarAeropuerto(ad1);
            AgregarAeropuerto(ad2);
            AgregarAeropuerto(ad3);
            AgregarAeropuerto(ad4);
            AgregarAeropuerto(ad5);
            AgregarAeropuerto(ad6);
            AgregarAeropuerto(ad7);
            AgregarAeropuerto(ad8);
            AgregarAeropuerto(ad9);
            AgregarAeropuerto(ad10);
            AgregarAeropuerto(ad11);
            AgregarAeropuerto(ad12);
            AgregarAeropuerto(ad13);
            AgregarAeropuerto(ad14);
            AgregarAeropuerto(ad15);
            AgregarAeropuerto(ad16);
            AgregarAeropuerto(ad17);
            AgregarAeropuerto(ad18);
            AgregarAeropuerto(ad19);
            AgregarAeropuerto(ad20);
        }

        //Precraga de ruta :
        private void PrecargarRutas()
        {
            Ruta r1 = new Ruta(aeropuertos[0], aeropuertos[1], 200);
            Ruta r2 = new Ruta(aeropuertos[1], aeropuertos[2], 300);
            Ruta r3 = new Ruta(aeropuertos[2], aeropuertos[3], 350);
            Ruta r4 = new Ruta(aeropuertos[3], aeropuertos[4], 400);
            Ruta r5 = new Ruta(aeropuertos[4], aeropuertos[5], 500);
            Ruta r6 = new Ruta(aeropuertos[5], aeropuertos[6], 600);
            Ruta r7 = new Ruta(aeropuertos[6], aeropuertos[7], 700);
            Ruta r8 = new Ruta(aeropuertos[7], aeropuertos[8], 800);
            Ruta r9 = new Ruta(aeropuertos[8], aeropuertos[9], 900);
            Ruta r10 = new Ruta(aeropuertos[9], aeropuertos[10], 1000);
            Ruta r11 = new Ruta(aeropuertos[10], aeropuertos[11], 1100);
            Ruta r12 = new Ruta(aeropuertos[11], aeropuertos[12], 1200);
            Ruta r13 = new Ruta(aeropuertos[12], aeropuertos[13], 1300);
            Ruta r14 = new Ruta(aeropuertos[13], aeropuertos[14], 1400);
            Ruta r15 = new Ruta(aeropuertos[14], aeropuertos[15], 1500);
            Ruta r16 = new Ruta(aeropuertos[15], aeropuertos[16], 1600);
            Ruta r17 = new Ruta(aeropuertos[16], aeropuertos[17], 1700);
            Ruta r18 = new Ruta(aeropuertos[17], aeropuertos[18], 1800);
            Ruta r19 = new Ruta(aeropuertos[18], aeropuertos[19], 1900);
            Ruta r20 = new Ruta(aeropuertos[19], aeropuertos[0], 2000);
            Ruta r21 = new Ruta(aeropuertos[1], aeropuertos[8], 2200);
            Ruta r22 = new Ruta(aeropuertos[2], aeropuertos[6], 2400);
            Ruta r23 = new Ruta(aeropuertos[3], aeropuertos[7], 2600);
            Ruta r24 = new Ruta(aeropuertos[4], aeropuertos[9], 2800);
            Ruta r25 = new Ruta(aeropuertos[5], aeropuertos[10], 3000);
            Ruta r26 = new Ruta(aeropuertos[6], aeropuertos[11], 3200);
            Ruta r27 = new Ruta(aeropuertos[7], aeropuertos[12], 3400);
            Ruta r28 = new Ruta(aeropuertos[8], aeropuertos[13], 3600);
            Ruta r29 = new Ruta(aeropuertos[9], aeropuertos[14], 3800);
            Ruta r30 = new Ruta(aeropuertos[10], aeropuertos[15], 4000);

            AgregarRuta(r1);
            AgregarRuta(r2);
            AgregarRuta(r3);
            AgregarRuta(r4);
            AgregarRuta(r5);
            AgregarRuta(r6);
            AgregarRuta(r7);
            AgregarRuta(r8);
            AgregarRuta(r9);
            AgregarRuta(r10);
            AgregarRuta(r11);
            AgregarRuta(r12);
            AgregarRuta(r13);
            AgregarRuta(r14);
            AgregarRuta(r15);
            AgregarRuta(r16);
            AgregarRuta(r17);
            AgregarRuta(r18);
            AgregarRuta(r19);
            AgregarRuta(r20);
            AgregarRuta(r21);
            AgregarRuta(r22);
            AgregarRuta(r23);
            AgregarRuta(r24);
            AgregarRuta(r25);
            AgregarRuta(r26);
            AgregarRuta(r27);
            AgregarRuta(r28);
            AgregarRuta(r29);
            AgregarRuta(r30);
        }

        //Precraga de Vuelos: 
        private void PrecargarVuelos()
        {
            Vuelo v1 = new Vuelo("VU001", rutas[0], aviones[0], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Miercoles });
            Vuelo v2 = new Vuelo("VU002", rutas[1], aviones[1], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Jueves });
            Vuelo v3 = new Vuelo("VU003", rutas[2], aviones[2], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Jueves, DiaSemana.Domingo });
            Vuelo v4 = new Vuelo("VU004", rutas[3], aviones[3], new List<DiaSemana> { DiaSemana.Miercoles, DiaSemana.Viernes, DiaSemana.Domingo });
            Vuelo v5 = new Vuelo("VU005", rutas[4], aviones[0], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Sabado });
            Vuelo v6 = new Vuelo("VU006", rutas[5], aviones[1], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Miercoles });
            Vuelo v7 = new Vuelo("VU007", rutas[6], aviones[2], new List<DiaSemana> { DiaSemana.Jueves, DiaSemana.Domingo });
            Vuelo v8 = new Vuelo("VU008", rutas[7], aviones[3], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Jueves });
            Vuelo v9 = new Vuelo("VU009", rutas[8], aviones[0], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Sabado });
            Vuelo v10 = new Vuelo("VU010", rutas[9], aviones[1], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Miercoles });
            Vuelo v11 = new Vuelo("VU011", rutas[10], aviones[2], new List<DiaSemana> { DiaSemana.Domingo, DiaSemana.Martes });
            Vuelo v12 = new Vuelo("VU012", rutas[11], aviones[3], new List<DiaSemana> { DiaSemana.Sabado, DiaSemana.Jueves });
            Vuelo v13 = new Vuelo("VU013", rutas[12], aviones[0], new List<DiaSemana> { DiaSemana.Miercoles, DiaSemana.Viernes });
            Vuelo v14 = new Vuelo("VU014", rutas[13], aviones[1], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Jueves });
            Vuelo v15 = new Vuelo("VU015", rutas[14], aviones[2], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Sabado });
            Vuelo v16 = new Vuelo("VU016", rutas[15], aviones[3], new List<DiaSemana> { DiaSemana.Miercoles, DiaSemana.Domingo });
            Vuelo v17 = new Vuelo("VU017", rutas[16], aviones[0], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Lunes });
            Vuelo v18 = new Vuelo("VU018", rutas[17], aviones[1], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Miercoles });
            Vuelo v19 = new Vuelo("VU019", rutas[18], aviones[2], new List<DiaSemana> { DiaSemana.Jueves, DiaSemana.Sabado });
            Vuelo v20 = new Vuelo("VU020", rutas[19], aviones[3], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Lunes });
            Vuelo v21 = new Vuelo("VU021", rutas[20], aviones[0], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Jueves });
            Vuelo v22 = new Vuelo("VU022", rutas[21], aviones[1], new List<DiaSemana> { DiaSemana.Miercoles, DiaSemana.Domingo });
            Vuelo v23 = new Vuelo("VU023", rutas[22], aviones[2], new List<DiaSemana> { DiaSemana.Lunes, DiaSemana.Sabado });
            Vuelo v24 = new Vuelo("VU024", rutas[23], aviones[3], new List<DiaSemana> { DiaSemana.Jueves, DiaSemana.Martes });
            Vuelo v25 = new Vuelo("VU025", rutas[24], aviones[0], new List<DiaSemana> { DiaSemana.Domingo, DiaSemana.Miercoles });
            Vuelo v26 = new Vuelo("VU026", rutas[25], aviones[1], new List<DiaSemana> { DiaSemana.Sabado, DiaSemana.Lunes });
            Vuelo v27 = new Vuelo("VU027", rutas[26], aviones[2], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Miercoles });
            Vuelo v28 = new Vuelo("VU028", rutas[27], aviones[3], new List<DiaSemana> { DiaSemana.Martes, DiaSemana.Jueves });
            Vuelo v29 = new Vuelo("VU029", rutas[28], aviones[0], new List<DiaSemana> { DiaSemana.Sabado, DiaSemana.Domingo });
            Vuelo v30 = new Vuelo("VU030", rutas[29], aviones[1], new List<DiaSemana> { DiaSemana.Viernes, DiaSemana.Lunes });

            AgregarVuelo(v1);
            AgregarVuelo(v2);
            AgregarVuelo(v3);
            AgregarVuelo(v4);
            AgregarVuelo(v5);
            AgregarVuelo(v6);
            AgregarVuelo(v7);
            AgregarVuelo(v8);
            AgregarVuelo(v9);
            AgregarVuelo(v10);
            AgregarVuelo(v11);
            AgregarVuelo(v12);
            AgregarVuelo(v13);
            AgregarVuelo(v14);
            AgregarVuelo(v15);
            AgregarVuelo(v16);
            AgregarVuelo(v17);
            AgregarVuelo(v18);
            AgregarVuelo(v19);
            AgregarVuelo(v20);
            AgregarVuelo(v21);
            AgregarVuelo(v22);
            AgregarVuelo(v23);
            AgregarVuelo(v24);
            AgregarVuelo(v25);
            AgregarVuelo(v26);
            AgregarVuelo(v27);
            AgregarVuelo(v28);
            AgregarVuelo(v29);
            AgregarVuelo(v30);
        }



           
       

       
           private void PrecargarPasajes()
        {
            Pasaje p1 = new Pasaje(vuelos[0], new DateTime(2025, 7, 7), usuarios[6] as Cliente, TipoEquipaje.LIGHT, 100);   // lunes
            Pasaje p2 = new Pasaje(vuelos[1], new DateTime(2025, 7, 8), usuarios[7] as Cliente, TipoEquipaje.CABINA, 100);  // martes
            Pasaje p3 = new Pasaje(vuelos[2], new DateTime(2025, 7, 10), usuarios[2] as Cliente, TipoEquipaje.BODEGA, 100); // jueves
            Pasaje p4 = new Pasaje(vuelos[3], new DateTime(2025, 7, 9), usuarios[3] as Cliente, TipoEquipaje.LIGHT, 100);   // miércoles
            Pasaje p5 = new Pasaje(vuelos[4], new DateTime(2025, 7, 5), usuarios[4] as Cliente, TipoEquipaje.CABINA, 10560); // sábado
            Pasaje p6 = new Pasaje(vuelos[5], new DateTime(2025, 7, 7), usuarios[6] as Cliente, TipoEquipaje.BODEGA, 500);   // lunes
            Pasaje p7 = new Pasaje(vuelos[6], new DateTime(2025, 7, 6), usuarios[7] as Cliente, TipoEquipaje.LIGHT, 100);    // domingo
            Pasaje p8 = new Pasaje(vuelos[7], new DateTime(2025, 7, 8), usuarios[2] as Cliente, TipoEquipaje.CABINA, 15400); // martes
            Pasaje p9 = new Pasaje(vuelos[8], new DateTime(2025, 7, 4), usuarios[3] as Cliente, TipoEquipaje.BODEGA, 1600);  // viernes
            Pasaje p10 = new Pasaje(vuelos[9], new DateTime(2025, 7, 7), usuarios[4] as Cliente, TipoEquipaje.LIGHT, 1700);  // lunes
            Pasaje p11 = new Pasaje(vuelos[10], new DateTime(2025, 7, 8), usuarios[7] as Cliente, TipoEquipaje.CABINA, 100); // martes
            Pasaje p12 = new Pasaje(vuelos[11], new DateTime(2025, 7, 10), usuarios[7] as Cliente, TipoEquipaje.BODEGA, 100); // jueves
            Pasaje p13 = new Pasaje(vuelos[12], new DateTime(2025, 7, 9), usuarios[2] as Cliente, TipoEquipaje.LIGHT, 100);  // miércoles
            Pasaje p14 = new Pasaje(vuelos[13], new DateTime(2025, 7, 10), usuarios[3] as Cliente, TipoEquipaje.CABINA, 100); // jueves
            Pasaje p15 = new Pasaje(vuelos[14], new DateTime(2025, 7, 5), usuarios[4] as Cliente, TipoEquipaje.BODEGA, 100); // sábado
            Pasaje p16 = new Pasaje(vuelos[15], new DateTime(2025, 7, 6), usuarios[6] as Cliente, TipoEquipaje.LIGHT, 100);  // domingo
            Pasaje p17 = new Pasaje(vuelos[16], new DateTime(2025, 7, 7), usuarios[6] as Cliente, TipoEquipaje.CABINA, 100); // lunes
            Pasaje p18 = new Pasaje(vuelos[17], new DateTime(2025, 7, 8), usuarios[2] as Cliente, TipoEquipaje.BODEGA, 100); // martes
            Pasaje p19 = new Pasaje(vuelos[18], new DateTime(2025, 7, 5), usuarios[3] as Cliente, TipoEquipaje.LIGHT, 100);  // sábado
            Pasaje p20 = new Pasaje(vuelos[19], new DateTime(2025, 7, 4), usuarios[4] as Cliente, TipoEquipaje.CABINA, 100); // viernes
            Pasaje p21 = new Pasaje(vuelos[20], new DateTime(2025, 7, 7), usuarios[6] as Cliente, TipoEquipaje.BODEGA, 100); // lunes
            Pasaje p22 = new Pasaje(vuelos[21], new DateTime(2025, 7, 9), usuarios[7] as Cliente, TipoEquipaje.LIGHT, 100);  // miércoles
            Pasaje p23 = new Pasaje(vuelos[22], new DateTime(2025, 7, 5), usuarios[2] as Cliente, TipoEquipaje.CABINA, 100); // sábado
            Pasaje p24 = new Pasaje(vuelos[23], new DateTime(2025, 7, 8), usuarios[3] as Cliente, TipoEquipaje.BODEGA, 100); // martes
            Pasaje p25 = new Pasaje(vuelos[24], new DateTime(2025, 7, 6), usuarios[4] as Cliente, TipoEquipaje.LIGHT, 100);  // domingo
            Pasaje p26 = new Pasaje(vuelos[25], new DateTime(2025, 7, 7), usuarios[7] as Cliente, TipoEquipaje.CABINA, 100); // lunes
            Pasaje p27 = new Pasaje(vuelos[26], new DateTime(2025, 7, 9), usuarios[6] as Cliente, TipoEquipaje.BODEGA, 100); // miércoles
            Pasaje p28 = new Pasaje(vuelos[27], new DateTime(2025, 7, 8), usuarios[2] as Cliente, TipoEquipaje.LIGHT, 100);  // martes
            Pasaje p29 = new Pasaje(vuelos[28], new DateTime(2025, 7, 5), usuarios[3] as Cliente, TipoEquipaje.CABINA, 100); // sábado
            Pasaje p30 = new Pasaje(vuelos[29], new DateTime(2025, 7, 7), usuarios[4] as Cliente, TipoEquipaje.BODEGA, 100); // lunes

            AgregarPasaje(p1); AgregarPasaje(p2); AgregarPasaje(p3); AgregarPasaje(p4); AgregarPasaje(p5);
            AgregarPasaje(p6); AgregarPasaje(p7); AgregarPasaje(p8); AgregarPasaje(p9); AgregarPasaje(p10);
            AgregarPasaje(p11); AgregarPasaje(p12); AgregarPasaje(p13); AgregarPasaje(p14); AgregarPasaje(p15);
            AgregarPasaje(p16); AgregarPasaje(p17); AgregarPasaje(p18); AgregarPasaje(p19); AgregarPasaje(p20);
            AgregarPasaje(p21); AgregarPasaje(p22); AgregarPasaje(p23); AgregarPasaje(p24); AgregarPasaje(p25);
            AgregarPasaje(p26); AgregarPasaje(p27); AgregarPasaje(p28); AgregarPasaje(p29); AgregarPasaje(p30);
        

    }


}

}


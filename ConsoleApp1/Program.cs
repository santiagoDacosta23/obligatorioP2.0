
using Dominio;

namespace ConsoleApp1
{
    internal  class Program
    {
        static void Main(string[] args)
        {
            string opcion = "0"; 
            Sistema sistema = Sistema.Instancia;
           
            
            while (opcion != "5")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== MENÚ PRINCIPAL =====");
                Console.ResetColor();

                Console.WriteLine("1 - Listado de todos los clientes");
                Console.WriteLine("2 - Listar vuelos por código de aeropuerto");
                Console.WriteLine("3 - Alta de cliente ocasional");
                Console.WriteLine("4 - Listar pasajes entre dos fechas");
                Console.WriteLine("5 - Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();

                try
                {
                    if (!sistema.EsOpcionValida(opcion))
                    {
                        Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar....");
                        Console.ReadKey();
                        continue;
                    }

                    Console.Clear(); 

                    switch (opcion)
                    {
                        case "1":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Listado de Todos los Clientes\n");
                            Console.ResetColor();

                            List<string> lista = sistema.ObtenerInformacionClientes();

                            foreach (string info in lista)
                            {
                                Console.WriteLine(info);
                                Console.WriteLine("----------------------------------------");
                            }
                            break;

                        case "2":
                            
                            Console.Write("Ingrese el código IATA del aeropuerto: ");
                            string codigo = Console.ReadLine();
                            List<string> vuelos = sistema.ListarVuelosPorAeropuerto(codigo);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"\nVuelos disponibles para el aeropuerto con código {codigo}:\n");
                            Console.ResetColor();
                            foreach (string vuelo in vuelos)
                            {
                                Console.WriteLine(vuelo);
                            }
                            break;

                        case "3":
                         
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("Alta de Cliente Ocasional\n");
                            Console.ResetColor();

                            Console.Write("Ingrese correo: ");
                            string correo = Console.ReadLine();

                            Console.Write("Ingrese contraseña: ");
                            string contrasenia = Console.ReadLine();

                            Console.Write("Ingrese documento sin guiones: ");
                            string documento = Console.ReadLine();

                            Console.Write("Ingrese nombre: ");
                            string nombre = Console.ReadLine();

                            Console.Write("Ingrese nacionalidad: ");
                            string nacionalidad = Console.ReadLine();

                            string resultado = sistema.AltaClienteOcasional( nombre, correo,  documento,  nacionalidad,  contrasenia);
                            Console.WriteLine("\n" + resultado);
                            break;

                        case "4":
                            Console.WriteLine("Listar pasajes entre dos fechas\n");
                            Console.Write("Ingrese la fecha desde (yyyy-mm-dd): ");
                            DateTime desde = DateTime.Parse(Console.ReadLine());
                            Console.Write("Ingrese la fecha hasta (yyyy-mm-dd): ");
                            DateTime hasta = DateTime.Parse(Console.ReadLine());

                           
                            List<string> pasajes = sistema.ListarPasajesEntreFechas(desde, hasta);

                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"\nPasajes disponibles entre {desde.ToString("yyyy-MM-dd")} y " +
                                $"{hasta.ToString("yyyy-MM-dd")}:\n");
                            Console.ResetColor();

                            foreach (string pasaje in pasajes)
                            {
                                Console.WriteLine(pasaje);
                            }
                            break;


                        case "5":
                            // Salir
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("¡Gracias por utilizar nuestro sistema de vuelos! Saliendo...");
                            Console.ResetColor();
                            break;

                        default:
                            Console.WriteLine("\nOpción no válida. Presione cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                }

                Console.WriteLine();
            }
            if (opcion != "5")
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }

        
    }

  
}


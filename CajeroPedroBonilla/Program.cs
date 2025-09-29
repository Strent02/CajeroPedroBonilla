using CajeroPedroBonilla.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroPedroBonilla
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Usuario usuario = new Usuario();
            Acciones acciones = new Acciones();
            bool bandera = true;

            while (true)
            {
                Console.WriteLine("Ingrese su nombre de usuario:");
                usuario.Nombre = Console.ReadLine();
                Console.WriteLine("Ingrese su pin:");
                usuario.Pin = Console.ReadLine();
                if (usuario.Nombre == "Pedro" && usuario.Pin == "1234")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Usuario o pin incorrecto, intente de nuevo.");
                }
            }
            while (bandera)
            {
                Console.Clear();
                Console.WriteLine($"Bienvenido {usuario.Nombre}");
                Console.WriteLine("Seleccione una opcion:");
                Console.WriteLine("1. Depositar");
                Console.WriteLine("2. Retirar");
                Console.WriteLine("3. Consultar saldo");
                Console.WriteLine("4. Cambiar pin");
                Console.WriteLine("5. Ver movimientos");
                Console.WriteLine("6. Salir");
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        acciones.Depositar(usuario);
                        break;
                    case "2":
                        acciones.Retirar(usuario);
                        break;
                    case "3":
                        acciones.ConsultarSaldo(usuario);
                        break;
                    case "4":
                        acciones.CambiarPin(usuario);
                        break;
                    case "5":
                        acciones.VerMovimientos(usuario);
                        break;
                    case "6":
                        bandera = false;
                        break;
                    default:
                        Console.WriteLine("Opcion no valida");
                        break;
                }
                if (bandera)
                {
                    Console.WriteLine("Presione una tecla para continuar...");
                    Console.ReadKey(true);
                }
            }
        }
    }
}

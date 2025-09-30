using CajeroPedroBonilla.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace CajeroPedroBonilla
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string rutaArchivoUsuarios = @"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Usuarios.txt";
            List<Usuario> usuarios = Usuario.CargarUsuariosDesdeArchivo(rutaArchivoUsuarios);
            if (usuarios.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados. Verifique el archivo Usuarios.txt.");
                return;
            }
            Usuario usuario = null;
            Acciones acciones = new Acciones();
            bool bandera = true;
            // Login
            while (true)
            {
                Console.WriteLine("Ingrese su nombre de usuario:");
                string nombreIngresado = Console.ReadLine();
                Console.WriteLine("Ingrese su pin:");
                string pinIngresado = Console.ReadLine();
                usuario = usuarios.Find(u => u.Nombre == nombreIngresado && u.Pin == pinIngresado);
                if (usuario != null)
                {
                    break; // Login exitoso
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace CajeroPedroBonilla.Objetos
{
    internal class Acciones
    {
        public string rutaArchivoUsuarios = @"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Usuarios.txt";

        public void Depositar(Usuario usuario)
        {

            Console.WriteLine("Ingrese cantidad a depositar:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad inválida. Operación cancelada.");
                usuario.RegistrarMovimientos("Depósito", 0, false);
                return;
            }
            usuario.Saldo += cantidad;

            usuario.RegistrarMovimientos("Depósito", cantidad, true);
            usuario.GuardarMovimientoEnArchivo(usuario.Movimientos.Last());

            Console.WriteLine($"Ha depositado {cantidad}.");
            Console.WriteLine($"Su nuevo saldo es {usuario.Saldo}.");

            ActualizarSaldoEnArchivo(usuario);


        }
        public void Retirar(Usuario usuario)
        {
            Console.WriteLine("Ingrese cantidad a retirar:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad inválida. Operación cancelada.");

                usuario.RegistrarMovimientos("Depósito", cantidad, true);
                usuario.GuardarMovimientoEnArchivo(usuario.Movimientos.Last());

                return;
            }
            if (cantidad > usuario.Saldo)
            {
                Console.WriteLine("Saldo insuficiente.");
                usuario.RegistrarMovimientos("Depósito", cantidad, true);
                usuario.GuardarMovimientoEnArchivo(usuario.Movimientos.Last());
            }
            else
            {
                usuario.Saldo -= cantidad;
                usuario.RegistrarMovimientos("Depósito", cantidad, true);
                usuario.GuardarMovimientoEnArchivo(usuario.Movimientos.Last());

                Console.WriteLine($"Ha retirado {cantidad}.");
                Console.WriteLine($"Su nuevo saldo es {usuario.Saldo}.");

                ActualizarSaldoEnArchivo(usuario);
            }
        }

        public void ConsultarSaldo(Usuario usuario)
        {
            Console.WriteLine($"Su saldo es {usuario.Saldo}");
        }

        public void CambiarContraseña(Usuario usuario)
        {
            string nuevaContrasena;
            do
            {
                Console.WriteLine("Ingrese su nueva contraseña:");
                nuevaContrasena = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nuevaContrasena))
                {
                    Console.WriteLine("La contraseña no puede estar vacía. Intente de nuevo.");
                }
            } while (string.IsNullOrWhiteSpace(nuevaContrasena));

            usuario.Contraseña = nuevaContrasena;

            var lineas = File.ReadAllLines(rutaArchivoUsuarios).ToList();
            for (int i = 0; i < lineas.Count; i++)
            {
                var partes = lineas[i].Split('|');
                if (partes.Length == 3 && partes[0] == usuario.Nombre)
                {
                    lineas[i] = $"{partes[0]}|{nuevaContrasena}|{partes[2]}";
                    break;
                }
            }
            File.WriteAllLines(rutaArchivoUsuarios, lineas);

            Console.WriteLine("Contraseña cambiada con éxito.");
        }


        public void VerMovimientos(Usuario usuario)
        {
            Console.WriteLine("=== Últimos 5 Movimientos ===");

            if (usuario.Movimientos.Count == 0)
            {
                Console.WriteLine("No tiene movimientos registrados.");
                return;
            }

            int TotalMovimientos = usuario.Movimientos.Count;
            int CantidadAMostrar = 5;
            if (TotalMovimientos < 5)
            {
                CantidadAMostrar = TotalMovimientos;
            }
            int IndiceInicio = TotalMovimientos - CantidadAMostrar;
            for (int i = IndiceInicio; i < TotalMovimientos; i++)
            {
                Movimientos mov = usuario.Movimientos[i];
                Console.WriteLine(mov);
            }
        }
        private void ActualizarSaldoEnArchivo(Usuario usuario)
        {
            try
            {
                var lineas = File.ReadAllLines(rutaArchivoUsuarios).ToList();
                for (int i = 0; i < lineas.Count; i++)
                {
                    var partes = lineas[i].Split('|');
                    if (partes.Length == 3 && partes[0] == usuario.Nombre)
                    {
                        partes[2] = usuario.Saldo.ToString("F2");
                        lineas[i] = string.Join("|", partes);
                        break;
                    }
                }
                File.WriteAllLines(rutaArchivoUsuarios, lineas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error actualizando saldo en archivo: " + ex.Message);
            }
        }
    }
}


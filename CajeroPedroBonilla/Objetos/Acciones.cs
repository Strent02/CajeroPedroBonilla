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
        string rutaArchivoUsuarios = @"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Usuarios.txt";

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
                usuario.RegistrarMovimientos("Retiro", 0, false);
                return;
            }
            if (cantidad > usuario.Saldo)
            {
                Console.WriteLine("Saldo insuficiente.");
                usuario.RegistrarMovimientos("Retiro", cantidad, false);
            }
            else
            {
                usuario.Saldo -= cantidad;
                usuario.RegistrarMovimientos("Retiro", cantidad, true);
                Console.WriteLine($"Ha retirado {cantidad}.");
                Console.WriteLine($"Su nuevo saldo es {usuario.Saldo}.");

                ActualizarSaldoEnArchivo(usuario);
            }
        }

        public void ConsultarSaldo(Usuario usuario)
        {
            Console.WriteLine($"Su saldo es {usuario.Saldo}");
        }
        public void CambiarPin(Usuario usuario)
        {
            Console.WriteLine("Ingrese su nuevo pin");
            string nuevoPin = Console.ReadLine();
            usuario.Pin = nuevoPin;
            Console.WriteLine("Pin cambiado con exito");
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
                // Leer todas las líneas
                var lineas = File.ReadAllLines(rutaArchivoUsuarios).ToList();
                // Buscar la línea del usuario y actualizar saldo
                for (int i = 0; i < lineas.Count; i++)
                {
                    var partes = lineas[i].Split('|');
                    if (partes.Length == 3 && partes[0] == usuario.Nombre)
                    {
                        // Actualizar saldo con formato InvariantCulture para evitar problemas con coma/punto
                        partes[2] = usuario.Saldo.ToString("F2", CultureInfo.InvariantCulture);
                        lineas[i] = string.Join("|", partes);
                        break;
                    }
                }
                // Sobrescribir archivo con líneas actualizadas
                File.WriteAllLines(rutaArchivoUsuarios, lineas);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error actualizando saldo en archivo: " + ex.Message);
            }
        }
    }
}


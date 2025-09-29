using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroPedroBonilla.Objetos
{
    internal class Acciones
    {    

        public void Depositar(Usuario usuario)
        {   
            
            Console.WriteLine("Ingrese Cantidad a depositar");
            decimal cantidad = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine($"Ha depositado {cantidad}");

            usuario.Saldo += cantidad;

            Console.WriteLine($"Su nuevo saldo es {usuario.Saldo}");
        }
        public void Retirar(Usuario usuario)
        {
            Console.WriteLine("Ingrese Cantidad a retirar");
            decimal cantidad = Convert.ToDecimal(Console.ReadLine());
            if (cantidad > usuario.Saldo)
            {
                Console.WriteLine("Saldo insuficiente");
            }
            else
            {
                usuario.Saldo -= cantidad;
                Console.WriteLine($"Ha retirado {cantidad}");
                Console.WriteLine($"Su nuevo saldo es {usuario.Saldo}");
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

        
    }
}

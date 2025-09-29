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
            Console.WriteLine("hola mundo");
            Usuario usuario = new Usuario();
            usuario.MostrarMensaje();
        }
    }
}

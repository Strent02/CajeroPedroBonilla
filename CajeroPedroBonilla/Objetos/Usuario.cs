using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroPedroBonilla.Objetos
{
    internal class Usuario
    {
        public string Nombre { get; set; }
        public string Pin { get; set; }
        public decimal Saldo { get; set; }
        public List<Movimientos> Movimientos { get; private set; }
        public Usuario()
        {
            Movimientos = new List<Movimientos>();
        }

        public void RegistrarMovimientos(string tipo, decimal monto, bool exitoso)
        {
            Movimientos.Add(new Movimientos(tipo, monto, Saldo, exitoso));
        }
    }
}

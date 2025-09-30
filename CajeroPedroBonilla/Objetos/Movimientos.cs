using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace CajeroPedroBonilla.Objetos
{
    internal class Movimientos
    {
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public decimal SaldoRestante { get; set; }
        bool ProcesoExitoso { get; set; }

        public Movimientos() { }
        public Movimientos( string tipo, decimal monto, decimal saldoRestante, bool procesoExitoso)
        {
            Fecha = DateTime.Now;
            Tipo = tipo;
            Monto = monto;
            SaldoRestante = saldoRestante;
            ProcesoExitoso = procesoExitoso;
        }
        public override string ToString()
        {
            string status = ProcesoExitoso ? "Éxito" : "Fallido";
            return $"{Tipo}: {Monto} (Nuevo saldo: {SaldoRestante}) - {status} - {Fecha:dd/MM/yyyy HH:mm}.";
        }
    }
}

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
        public decimal Cantidad { get; set; }
        public decimal SaldoRestante { get; set; }
        public bool ProcesoExitoso { get; set; }

        public Movimientos() { }
        public Movimientos( string tipo, decimal cantidad, decimal saldoRestante, bool procesoExitoso)
        {
            Fecha = DateTime.Now;
            Tipo = tipo;
            Cantidad = cantidad;
            SaldoRestante = saldoRestante;
            ProcesoExitoso = procesoExitoso;
        }
        public override string ToString()
        {
            string status = ProcesoExitoso ? "Éxito" : "Fallido";
            return $"{Tipo}: {Cantidad} (Nuevo saldo: {SaldoRestante}) - {status} - {Fecha:dd/MM/yyyy HH:mm}.";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;

namespace CajeroPedroBonilla.Objetos
{
    internal class Usuario
    {
        string rutaArchivoUsuarios = @"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Usuarios.txt";

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

        //Metodo para cargar usuarios desde archivo
        public static List<Usuario> CargarUsuariosDesdeArchivo(string rutaArchivo)
        {
            var usuarios = new List<Usuario>();
            if (!File.Exists(rutaArchivo))
            {
                Console.WriteLine("Archivo de usuarios no encontrado: " + rutaArchivo);
                return usuarios;
            }
            try
            {
                using (var sr = new StreamReader(rutaArchivo))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(linea)) continue;
                        var partes = linea.Split('|');
                        if (partes.Length != 3) continue;
                        string nombre = partes[0];
                        string pin = partes[1];
                        if (!decimal.TryParse(partes[2], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal saldo))
                            saldo = 0;
                        usuarios.Add(new Usuario()
                        {
                            Nombre = nombre,
                            Pin = pin,
                            Saldo = saldo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error leyendo archivo de usuarios: " + ex.Message);
            }
            return usuarios;
        }
    }
}

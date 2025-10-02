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
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public decimal Saldo { get; set; }
        public List<Movimientos> Movimientos { get; private set; }

        string rutaArchivoUsuarios = @"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Usuarios.txt";

        public Usuario()
        {
            Movimientos = new List<Movimientos>();
        }

        public void RegistrarMovimientos(string tipo, decimal monto, bool exitoso)
        {
            Movimientos.Add(new Movimientos(tipo, monto, Saldo, exitoso));
        }

        //Metodo para registrar movimientos
        public void GuardarMovimientoEnArchivo(Movimientos movimiento)
        {
            string ruta = $@"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Logs\Log{Nombre}.txt";

            // Crear carpeta "Logs" si no existe
            string carpeta = Path.GetDirectoryName(ruta);
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            using (StreamWriter sw = new StreamWriter(ruta, true))
            {
                sw.WriteLine($"{movimiento.Fecha:yyyy-MM-dd HH:mm}|{movimiento.Tipo}|{movimiento.Cantidad}|{movimiento.SaldoRestante}|{(movimiento.ProcesoExitoso ? "Éxito" : "Fallido")}");
            }
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
                        string contraseña = partes[1];

                        if (!decimal.TryParse(partes[2],  out decimal saldo))
                            saldo = 0;

                        usuarios.Add(new Usuario()
                        {
                            Nombre = nombre,
                            Contraseña = contraseña,
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

        public void CargarMovimientosDesdeArchivo()
        {
            string ruta = $@"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Logs\Log{Nombre}.txt";
            Movimientos.Clear();

            if (!File.Exists(ruta))
            {
                using (File.Create(ruta)) { }
                return;
            }

            using (StreamReader sr = new StreamReader(ruta))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    var partes = linea.Split('|');
                    if (partes.Length != 5) continue;

                    Movimientos.Add(new Movimientos(
                        partes[1],
                        decimal.Parse(partes[2]),
                        decimal.Parse(partes[3]),
                        partes[4] == "Éxito"
                    )
                    {
                        Fecha = DateTime.Parse(partes[0])
                    });
                }
            }
        }
    }
}

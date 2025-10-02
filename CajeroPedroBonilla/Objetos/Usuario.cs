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
            Movimientos nuevo = new Movimientos(tipo, monto, Saldo, exitoso);
            Movimientos.Add(nuevo);

            using (StreamWriter sw = new StreamWriter($@"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Logs\Log{Nombre}.txt", true))
            {
                sw.WriteLine($"{tipo}|{monto}|{Saldo}|{(exitoso ? "Éxito" : "Fallido")}|{nuevo.Fecha:yyyy-MM-dd HH:mm}");
            }
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
                sw.WriteLine($"{movimiento.Tipo}|{movimiento.Cantidad}|{movimiento.SaldoRestante}|{(movimiento.ProcesoExitoso ? "Éxito" : "Fallido")}|{movimiento.Fecha:yyyy-MM-dd HH:mm}");
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
            Movimientos.Clear();
            string ruta = $@"C:\Users\DickRider\source\repos\CajeroPedroBonilla\CajeroPedroBonilla\Objetos\Logs\Log{Nombre}.txt";

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
                        partes[0],
                        decimal.Parse(partes[1]),
                        decimal.Parse(partes[2]),
                        partes[3] == "Éxito"
                    )
                    {
                        Fecha = DateTime.Parse(partes[4])
                    });
                }
            }
        }
    }
}

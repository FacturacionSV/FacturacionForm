using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Threading.Tasks;



namespace FacturacionForm.BaseDeDatos
{

    public class ManejadorBD
    {
        private string _rutaBaseDatos;
        private SQLiteConnection _conexion;

        public ManejadorBD()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, "GestionVentas");
            Directory.CreateDirectory(appFolder); // ¡Crear la carpeta si no existe!
            _rutaBaseDatos = Path.Combine(appFolder, "GestionVentas.db");

            InicializarBaseDatos();
        }

        private void InicializarBaseDatos()
        {
            bool existeBaseDatos = File.Exists(_rutaBaseDatos);

            // Crear la cadena de conexión
            string cadenaCon = $"Data Source={_rutaBaseDatos};Version=3;";
            _conexion = new SQLiteConnection(cadenaCon);

            // Abrir la conexión
            _conexion.Open();

            // Si la base de datos no existe, crear las tablas
            if (!existeBaseDatos)
            {
                CrearTablas();
            }
        }

        private void CrearTablas()
        {
            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                // Script para crear la tabla Ventas
                comando.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Ventas (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        DocumentoJson TEXT NOT NULL,
                        NumeroControl TEXT NOT NULL,
                        CodigoGeneracion TEXT NOT NULL,
                        SelloRecepcion TEXT,
                        Fecha TEXT NOT NULL,
                        TipoDTE TEXT NOT NULL,
                          SelloRecepcionAnulacion TEXT,
                          jsonAnulacion TEXT,
                        cliente TEXT
                    );";
                comando.ExecuteNonQuery();

                // Crear tabla de Correlativos
                comando.CommandText = @"
        CREATE TABLE IF NOT EXISTS Correlativos (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            TipoDTE TEXT NOT NULL UNIQUE,
            NumeroActual INTEGER NOT NULL DEFAULT 0,
            FechaCreacion TEXT NOT NULL,
            FechaUltimaActualizacion TEXT NOT NULL
        );";
                comando.ExecuteNonQuery();

                // Insertar valores iniciales para los correlativos
                comando.CommandText = @"
        INSERT OR IGNORE INTO Correlativos (TipoDTE, NumeroActual, FechaCreacion, FechaUltimaActualizacion)
        VALUES 
            ('CF', 0, datetime('now'), datetime('now')),
            ('CFF', 0, datetime('now'), datetime('now'));";
                comando.ExecuteNonQuery();

            }

        }

        public void CerrarConexion()
        {
            if (_conexion != null && _conexion.State == System.Data.ConnectionState.Open)
            {
                _conexion.Close();
                _conexion.Dispose();
            }
        }

        public long InsertarVenta(string documentoJson, string numeroControl, string codigoGeneracion,
                                 string selloRecepcion, DateTime fecha, string tipoDTE,string cliente)
        {
            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = @"
                    INSERT INTO Ventas (DocumentoJson, NumeroControl, CodigoGeneracion, SelloRecepcion, Fecha, TipoDTE,cliente)
                    VALUES (@documentoJson, @numeroControl, @codigoGeneracion, @selloRecepcion, @fecha, @tipoDTE,@cliente);
                    SELECT last_insert_rowid();";

                comando.Parameters.AddWithValue("@documentoJson", documentoJson);
                comando.Parameters.AddWithValue("@numeroControl", numeroControl);
                comando.Parameters.AddWithValue("@codigoGeneracion", codigoGeneracion);
                comando.Parameters.AddWithValue("@selloRecepcion", selloRecepcion);
                comando.Parameters.AddWithValue("@fecha", fecha.ToString("yyyy-MM-dd HH:mm:ss"));
                comando.Parameters.AddWithValue("@tipoDTE", tipoDTE);
                comando.Parameters.AddWithValue("@cliente", cliente);

                return (long)comando.ExecuteScalar();
            }
        }

        public List<VentaDTO> ObtenerTodasLasVentas()
        {
            List<VentaDTO> ventas = new List<VentaDTO>();

            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Ventas ORDER BY Id DESC;";

                using (SQLiteDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        VentaDTO venta = new VentaDTO
                        {
                            Id = Convert.ToInt64(lector["Id"]),
                            DocumentoJson = lector["DocumentoJson"].ToString(),
                            NumeroControl = lector["NumeroControl"].ToString(),
                            CodigoGeneracion = lector["CodigoGeneracion"].ToString(),
                            SelloRecepcion = lector["SelloRecepcion"].ToString(),
                            Fecha = DateTime.Parse(lector["Fecha"].ToString()),
                            TipoDTE = lector["TipoDTE"].ToString(),
                            cliente = lector["cliente"].ToString()
                        };

                        ventas.Add(venta);
                    }
                }
            }

            return ventas;
        }

        public VentaDTO ObtenerVentaPorId(long id)
        {
            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Ventas WHERE Id = @id;";
                comando.Parameters.AddWithValue("@id", id);

                using (SQLiteDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new VentaDTO
                        {
                            Id = Convert.ToInt64(lector["Id"]),
                            DocumentoJson = lector["DocumentoJson"].ToString(),
                            NumeroControl = lector["NumeroControl"].ToString(),
                            CodigoGeneracion = lector["CodigoGeneracion"].ToString(),
                            SelloRecepcion = lector["SelloRecepcion"].ToString(),
                            Fecha = DateTime.Parse(lector["Fecha"].ToString()),
                            TipoDTE = lector["TipoDTE"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public List<VentaDTO> BuscarVentasPorTipoDTE(string tipoDTE)
        {
            List<VentaDTO> ventas = new List<VentaDTO>();

            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Ventas WHERE TipoDTE = @tipoDTE ORDER BY Fecha DESC;";
                comando.Parameters.AddWithValue("@tipoDTE", tipoDTE);

                using (SQLiteDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        VentaDTO venta = new VentaDTO
                        {
                            Id = Convert.ToInt64(lector["Id"]),
                            DocumentoJson = lector["DocumentoJson"].ToString(),
                            NumeroControl = lector["NumeroControl"].ToString(),
                            CodigoGeneracion = lector["CodigoGeneracion"].ToString(),
                            SelloRecepcion = lector["SelloRecepcion"].ToString(),
                            Fecha = DateTime.Parse(lector["Fecha"].ToString()),
                            TipoDTE = lector["TipoDTE"].ToString()
                        };

                        ventas.Add(venta);
                    }
                }
            }

            return ventas;
        }
        public List<VentaDTO> Todos()
        {
            List<VentaDTO> ventas = new List<VentaDTO>();

            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Ventas  ORDER BY Fecha DESC;";
                

                using (SQLiteDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        VentaDTO venta = new VentaDTO
                        {
                            Id = Convert.ToInt64(lector["Id"]),
                            DocumentoJson = lector["DocumentoJson"].ToString(),
                            NumeroControl = lector["NumeroControl"].ToString(),
                            CodigoGeneracion = lector["CodigoGeneracion"].ToString(),
                            SelloRecepcion = lector["SelloRecepcion"].ToString(),
                            Fecha = DateTime.Parse(lector["Fecha"].ToString()),
                            TipoDTE = lector["TipoDTE"].ToString(),
                            cliente = lector["cliente"].ToString()
                        };

                        ventas.Add(venta);
                    }
                }
            }

            return ventas;
        }
        public List<VentaDTO> BuscarVentasPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            List<VentaDTO> ventas = new List<VentaDTO>();

            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = "SELECT * FROM Ventas WHERE Fecha BETWEEN @fechaInicio AND @fechaFin ORDER BY Fecha DESC;";
                comando.Parameters.AddWithValue("@fechaInicio", fechaInicio.ToString("yyyy-MM-dd HH:mm:ss"));
                comando.Parameters.AddWithValue("@fechaFin", fechaFin.ToString("yyyy-MM-dd HH:mm:ss"));

                using (SQLiteDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        VentaDTO venta = new VentaDTO
                        {
                            Id = Convert.ToInt64(lector["Id"]),
                            DocumentoJson = lector["DocumentoJson"].ToString(),
                            NumeroControl = lector["NumeroControl"].ToString(),
                            CodigoGeneracion = lector["CodigoGeneracion"].ToString(),
                            SelloRecepcion = lector["SelloRecepcion"].ToString(),
                            Fecha = DateTime.Parse(lector["Fecha"].ToString()),
                            TipoDTE = lector["TipoDTE"].ToString(),
                            cliente = lector["cliente"].ToString()
                        };

                        ventas.Add(venta);
                    }
                }
            }

            return ventas;
        }

        public bool ActualizarSelloRecepcion(long id, string selloRecepcion)
        {
            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = "UPDATE Ventas SET SelloRecepcion = @selloRecepcion WHERE Id = @id;";
                comando.Parameters.AddWithValue("@selloRecepcion", selloRecepcion);
                comando.Parameters.AddWithValue("@id", id);

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }
        public void ActualizarAnulacion(int id, string selloRecepcionAnulacion, string jsonAnulacion)
        {
            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = @"
            UPDATE Ventas 
            SET SelloRecepcionAnulacion = @selloRecepcionAnulacion,
                jsonAnulacion = @jsonAnulacion
            WHERE Id = @id";

                // Parámetros para evitar SQL injection
                comando.Parameters.AddWithValue("@id", id);
                comando.Parameters.AddWithValue("@selloRecepcionAnulacion", selloRecepcionAnulacion);
                comando.Parameters.AddWithValue("@jsonAnulacion", jsonAnulacion);

                comando.ExecuteNonQuery();
            }
        }

        public bool EliminarVenta(long id)
        {
            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                comando.CommandText = "DELETE FROM Ventas WHERE Id = @id;";
                comando.Parameters.AddWithValue("@id", id);

                int filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

       

        // Método para obtener el correlativo actual sin incrementar
        public int ObtenerSiguienteCorrelativo(string tipoDTE)
        {
            using (SQLiteCommand comando = _conexion.CreateCommand())
            {
                // Primero obtenemos el número actual
                comando.CommandText = @"SELECT NumeroActual FROM Correlativos WHERE TipoDTE = @tipoDTE";
                comando.Parameters.AddWithValue("@tipoDTE", tipoDTE);

                var numeroActual = Convert.ToInt32(comando.ExecuteScalar() ?? 0);
                var siguienteNumero = numeroActual + 1;

                // Actualizamos el correlativo
                comando.CommandText = @"
            UPDATE Correlativos 
            SET NumeroActual = @nuevoNumero, 
                FechaUltimaActualizacion = datetime('now')
            WHERE TipoDTE = @tipoDTE";

                comando.Parameters.Clear();
                comando.Parameters.AddWithValue("@nuevoNumero", siguienteNumero);
                comando.Parameters.AddWithValue("@tipoDTE", tipoDTE);
                comando.ExecuteNonQuery();

                return siguienteNumero;
            }
        }
    }

    public class VentaDTO
    {
        public long Id { get; set; }
        public string DocumentoJson { get; set; }
        public string NumeroControl { get; set; }
        public string CodigoGeneracion { get; set; }
        public string SelloRecepcion { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoDTE { get; set; }
        public string cliente { get; set; }
    }
}


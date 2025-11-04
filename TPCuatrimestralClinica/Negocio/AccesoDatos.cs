// Archivo: Negocio/AccesoDatos.cs

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class AccesoDatos
    {
        private SqlCommand comando;
        private SqlDataReader lector;
        private string connectionString;

        // Propiedad para el lector
        public SqlDataReader Lector
        {
            get { return lector; }
        }

        // Constructor
        public AccesoDatos()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ClinicaConnection"].ConnectionString;
            comando = new SqlCommand();
        }

        // Método para setear consulta
        public void SetearConsulta(string consulta)
        {
            comando.CommandType = CommandType.Text;
            comando.CommandText = consulta;
        }

        // Método para setear Stored Procedure
        public void SetearSP(string sp)
        {
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        
        public void EjecutarLectura()
        {
            SqlConnection conexion = new SqlConnection(connectionString);
            comando.Connection = conexion;
            try
            {
                conexion.Open();

                lector = comando.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
                throw ex;
            }
        }

        
        public void EjecutarAccion()
        {
   
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                comando.Connection = conexion;
                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } 
        }

        public object EjecutarEscalar()
        {
            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                comando.Connection = conexion;
                try
                {
                    conexion.Open();
                    return comando.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } 
        }

        public void SetearParametros(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void LimpiarParametros()
        {
            comando.Parameters.Clear();
        }

        
        public void CerrarConexion()
        {
            if (lector != null && !lector.IsClosed)
                lector.Close();

        }
    }
}
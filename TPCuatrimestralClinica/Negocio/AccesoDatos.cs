using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Negocio
{
    public class AccesoDatos
    {

        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        //propiedad para el lector
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        //constructor
        public AccesoDatos()
        {
            string cadena = ConfigurationManager.ConnectionStrings["ClinicaConnection"].ConnectionString;
            //conexion = new SqlConnection("server = .\\SQLEXPRESS02; database = CLINICA_DB_TEST; integrated security =true ;");
          
            conexion = new SqlConnection(cadena);
            comando = new SqlCommand();

        }
        //metodo para setear consulta
        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void SetearSP(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }
        //metodo para ejecutar lectura
        public void EjecutarLectura()
        {

            comando.Connection = conexion;
            try
            {
                conexion.Open();

                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //metodo para ejecutar accion
        public void EjecutarAccion()
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
        //metodo para ejecutar escalar
        public object EjecutarEscalar()
        {
            comando.Connection = conexion;
            conexion.Open();
            return comando.ExecuteScalar();
        }
        //metodo para setear parametro

        public void SetearParametros(string nombre, object valor)
        {

            comando.Parameters.AddWithValue(nombre, valor);

        }
        //metodo para cerrar conexion
        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();

        }

    }
}

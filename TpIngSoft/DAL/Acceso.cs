using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Acceso
    {
        private SqlConnection conexion;
        private SqlTransaction transaccion;


        public void Abrir()
        {
            conexion = new SqlConnection
            {
                ConnectionString = "Data Source=localhost;Initial Catalog=tpingsoft;Persist Security Info=True;User ID=sa;Password=MaxiNachoIngSoft26;"
            };
            conexion.Open();
        }

        public void Cerrar()
        {
            conexion.Close();
            conexion = null;
            GC.Collect();
        }

        public void IniciarTx()
        {
            transaccion = conexion.BeginTransaction();
        }

        public void ConfirmarTX()
        {
            transaccion.Commit();
            transaccion = null;
        }

        public void DeshacerTX()
        {
            transaccion.Rollback();
            transaccion = null;
        }


        private SqlCommand CrearComando(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = sql,
                Connection = conexion
            };
            if (transaccion != null)
            {
                cmd.Transaction = transaccion;
            }
            if (parametros != null)
            {
                cmd.Parameters.AddRange(parametros.ToArray());
            }
            return cmd;
        }

        public int Escribir(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand cmd = CrearComando(sql, parametros);
            int filas;
            try
            {
                filas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                filas = -1;
            }
            cmd.Parameters.Clear();
            return filas;
        }


        public DataTable Leer(string sql, List<SqlParameter> parametros = null)
        {
            SqlDataAdapter adaptador = new SqlDataAdapter
            {
                SelectCommand = CrearComando(sql, parametros)
            };
            DataTable tabla = new DataTable();

            adaptador.Fill(tabla);

            return tabla;

        }

        public SqlParameter CrearParametro(string nombre, object valor)
        {
            SqlParameter p = new SqlParameter
            {
                ParameterName = nombre,
                Value = valor ?? DBNull.Value
            };

            return p;
        }

        public SqlParameter CrearParametroOut(string nombre)
        {
            SqlParameter p = new SqlParameter
            {
                ParameterName = nombre,
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };
            return p;
        }

    }
}

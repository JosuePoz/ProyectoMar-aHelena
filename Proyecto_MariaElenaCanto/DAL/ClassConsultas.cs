using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class ClassConsultas
    {

        public DataTable Consulta(string comandosql, MySqlParameter[] parametros)
        {
           
            envioEntities db = new envioEntities();
            using (DbDataAdapter adapador = new MySqlDataAdapter())
            {
                adapador.SelectCommand = db.Database.Connection.CreateCommand();
                adapador.SelectCommand.CommandText = comandosql;
                if (parametros != null)
                    adapador.SelectCommand.Parameters.AddRange(parametros);
                DataTable tablarespuesta = new DataTable();
               adapador.Fill(tablarespuesta);
               return tablarespuesta;
            }
        }//fin Consulta
        public string Acciones(string comandosql, params object[] parametros)
        {
            try
            {
                envioEntities BD = new envioEntities();
                BD.Database.ExecuteSqlCommand(comandosql, parametros);
                return "Éxito de ejecución";
            }
            catch (Exception err)
            {
                return "Error: " + err.Message;
            }
        }//fin acciones
    }

}








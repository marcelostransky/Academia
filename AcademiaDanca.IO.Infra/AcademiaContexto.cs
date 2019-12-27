using AcademiaDanca.IO.Compartilhado;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AcademiaDanca.IO.Infra
{
   public  class AcademiaContexto
    {
        public MySqlConnection Connection { get; set; }

        public AcademiaContexto()
        {
          
            Connection = new MySqlConnection(Settings.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
                Connection.Close();
        }
    }
}

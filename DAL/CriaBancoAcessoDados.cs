using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace DAL
{
    public class CriaBancoAcessoDados
    {

        private MySqlConnection conn;
        private DataTable data;
        private MySqlDataAdapter da;
        private MySqlCommandBuilder cb;

        private String server = "localhost";
        private String user = "root";
        private String password = "";
        private String database = "controlefrota";
        //conectar ao BD
        public void Conectar()
        {
            if (conn != null)
                conn.Close();
            //string de conexao
            string connStr = String.Format("server={0}; user id={1}; password={2}; database={3}; pooling=false", server, user, password, database);

            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
            }
            catch (MySqlException)
            {
                MessageBox.Show("Falha ao conectar ao BD", "Erro 0002");
            }
        }

        public void ExecutarComandoSQL(string comandoSql)
        {
            MySqlCommand comando = new MySqlCommand(comandoSql, conn);
            comando.ExecuteReader();
            conn.Close();
        }
        //evneto para retornar um dataTable
        public DataTable RetDataTable(string sql)
        {
            data = new DataTable();
            da = new MySqlDataAdapter(sql, conn);
            cb = new MySqlCommandBuilder(da);
            da.Fill(data);

            return data;
        }
        //evento para retornar um dataTableReader
        public MySqlDataReader RetDataReader(string sql)
        {
            MySqlCommand comando = new MySqlCommand();
            MySqlDataReader dr = comando.ExecuteReader();
            dr.Read();
            data.Load(dr);

            return dr;
        }
    }
}

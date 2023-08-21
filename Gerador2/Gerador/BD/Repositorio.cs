using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Gerador.BD
{
    public class Repositorio
    {
        private string _connectionStrings;

        public Repositorio(IConfiguration configuration)
        {
            //IConfiguration configuration = new ConfigurationBuilder()
            // .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            // .AddEnvironmentVariables()
            // .Build();

            // _connectionStrings = $"{configuration.GetValue<string>("ConnectionStrings:webged")}";
            _connectionStrings = "Data Source=D800BCORP;Initial Catalog=dtb_hub;Integrated Security=True;TrustServerCertificate=yes";
        }

        public IList<string> ListTable()
        {
            var sql = @"
                        SELECT TABLE_NAME
                            FROM INFORMATION_SCHEMA.TABLES
                        WHERE TABLE_NAME <> '__MigrationHistory' and TABLE_NAME <> 'Indices'
                        ORDER BY TABLE_NAME
                        ";
            IList<string> tabelas = new List<string>();
            using (SqlConnection connection = new SqlConnection(_connectionStrings))
            {

                SqlCommand cmd = new(sql, connection);
                cmd.CommandType = CommandType.Text;

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    tabelas.Add(reader["TABLE_NAME"].ToString());

                }
                connection.Close();
            }
            return tabelas;

        }

        public Table ListTableAndField(string tabela)
        {
            var tbl = new Table();
            tbl.Name = tabela;
            var sql = @"

SELECT 
    c.TABLE_NAME,
    c.COLUMN_NAME, 

    c.DATA_TYPE, 
    c.CHARACTER_MAXIMUM_LENGTH,
     CASE WHEN c.IS_NULLABLE = 'NO' THEN 'False' ELSE 'True' END AS IS_NULLABLE,
    CASE WHEN fk.COLUMN_NAME IS NULL THEN 'False' ELSE 'True' END AS IS_FOREIGN_KEY
FROM INFORMATION_SCHEMA.COLUMNS c
LEFT JOIN (
    SELECT 
        ku.COLUMN_NAME, 
        ku.TABLE_NAME
    FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
    INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc ON ku.CONSTRAINT_NAME = tc.CONSTRAINT_NAME
    WHERE tc.CONSTRAINT_TYPE = 'FOREIGN KEY'
) fk ON c.TABLE_NAME = fk.TABLE_NAME AND c.COLUMN_NAME = fk.COLUMN_NAME
WHERE c.TABLE_NAME = @Tabela
order by IS_FOREIGN_KEY

";


            using (SqlConnection connection = new SqlConnection(_connectionStrings))
            {

                SqlCommand cmd = new(sql, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Tabela", tabela);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    var campo = new Filde();
                    campo.Collum = reader["COLUMN_NAME"].ToString();
                    campo.IsNull = Convert.ToBoolean(reader["IS_NULLABLE"].ToString());
                    campo.Type = reader["DATA_TYPE"].ToString();
                    campo.MaximumCharacters = string.IsNullOrWhiteSpace(reader["CHARACTER_MAXIMUM_LENGTH"].ToString()) ? null : Convert.ToInt32(reader["CHARACTER_MAXIMUM_LENGTH"].ToString());
                    campo.IsForenKey = Convert.ToBoolean(reader["IS_FOREIGN_KEY"].ToString());

                    if (campo.IsForenKey)
                    {
                        campo.TableForemKey = ListTable(tabela, campo.Collum);
                    }
                    tbl.Fildes.Add(campo);
                }
                connection.Close();
            }
            //tbl.Collection = GetCollections(tabela);
            return tbl;
        }


        public string ListTable(string tabela, string campo)
        {
            var sql = @"
SELECT 
    OBJECT_NAME(fkc.parent_object_id) AS TabelaAtual,
    COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS ColunaAtual,
    OBJECT_NAME(fkc.referenced_object_id) AS TabelaReferenciada,
    COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS ColunaReferenciada
FROM sys.foreign_key_columns fkc
WHERE OBJECT_NAME(fkc.parent_object_id) = @Tabela
    AND COL_NAME(fkc.parent_object_id, fkc.parent_column_id) = @Campo;
                        ";
            string tabelas = "";
            using (SqlConnection connection = new SqlConnection(_connectionStrings))
            {

                SqlCommand cmd = new(sql, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Tabela", tabela);
                cmd.Parameters.AddWithValue("@Campo", campo);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    tabelas =  reader["TabelaReferenciada"].ToString();

                }
                connection.Close();
            }
            return tabelas;

        }


        public IList<string> GetCollections(string tabela)
        {
            var sql = @"
SELECT 
    OBJECT_NAME(fkc.parent_object_id) AS TabelaReferenciada,
    COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS ColunaReferenciada,
    OBJECT_NAME(fkc.referenced_object_id) AS TabelaAtual,
    COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS ColunaAtual
FROM sys.foreign_key_columns fkc
WHERE OBJECT_NAME(fkc.referenced_object_id) = @Tabela
order by TabelaReferenciada
                        ";
            IList<string> collections = new List<string>();
            IList<string> campos = new List<string>();
            using (SqlConnection connection = new SqlConnection(_connectionStrings))
            {

                SqlCommand cmd = new(sql, connection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Tabela", tabela);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    campos.Add(reader["TabelaReferenciada"].ToString());

                }
                connection.Close();
            }

            foreach (var c in campos)
            {
                if (!collections.Contains(c))
                {
                    var table = this.ListTableAndField(c);

                    if (table.Fildes.Any(f => f.Collum == "Id"))
                    {
                        collections.Add(c);
                    }
                }

            }

            return collections;

        }

    }



    
}

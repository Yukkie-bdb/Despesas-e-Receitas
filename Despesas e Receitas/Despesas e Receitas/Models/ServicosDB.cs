using System.Data.SqlClient;
using System.Drawing;

namespace Despesas_e_Receitas.Models
{
    public class ServicosDB
    {
        private readonly string _stringDeConexao;

        public ServicosDB()
        {
            _stringDeConexao = "Data Source=SB-1491297\\SQLSENAI;Initial Catalog=dbCrudMVC;Integrated Security=True";
        }

        public List<Servico> getList()
        {
            List<Servico> aux = new List<Servico>();

            // Criando um objeto de conexão com o banco de dados
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                // Criando a instrução Sql e o objeto command para executá-la
                string query = "Select * From tbReceitasDespesas";
                SqlCommand _cmd = new SqlCommand(query, _conn);

                // Abrir a conexão com o banco de dados
                _conn.Open();

                // Usando objeto DataReader para ler os dados
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    // Enquanto conseguir ler algum registro da tabela...
                    while (reader.Read())
                    {
                        Servico servico = new Servico
                        {
                            Id = new Guid(reader["Id"].ToString()),
                            Nome = reader["Nome"].ToString(),
                            Descricao = reader["Descricao"].ToString(),
                            Categoria = Char.Parse(reader["Categoria"].ToString()),
                            Valor = Decimal.Parse(reader["Valor"].ToString()),
                            CapexOpex = Char.Parse(reader["CapexOpex"].ToString())
                        };

                        aux.Add(servico);
                    }
                }
                _conn.Close();
            }

            return aux;
        }

        public List<Servico> filter(string criterio)
        {
            criterio = criterio == null ? "" : criterio.Trim().ToLower();

            List<Servico> aux = getList();
            return aux.Where(
                        p => p.Nome.ToLower().Contains(criterio)
                   ).ToList();
        }

        public void insert(Servico servico)
        {
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                // Criando instrução de insert
                string instrucao = "INSERT INTO tbReceitasDespesas(Id, Nome, Categoria, Descricao, Valor, CapexOpex) " +
                                   "VALUES(@Id, @Nome, @Categ, @Descri, @Valor, @CapexOpex)";

                // Criando o objeto command
                SqlCommand _cmd = new SqlCommand(instrucao, _conn);

                // Preenchendo os parâmetros do Insert
                _cmd.Parameters.AddWithValue("@Id", servico.Id);
                _cmd.Parameters.AddWithValue("@Nome", servico.Nome);
                _cmd.Parameters.AddWithValue("@Categ", servico.Categoria);
                _cmd.Parameters.AddWithValue("@Descri", servico.Descricao);
                _cmd.Parameters.AddWithValue("@Valor", servico.Valor);
                _cmd.Parameters.AddWithValue("@CapexOpex", servico.CapexOpex);

                


                // Abrindo a conexão com o banco de dados
                _conn.Open();

                // Executando o comando de insert
                _cmd.ExecuteNonQuery();

                // Fechando a conexão com o banco de dados
                _conn.Close();
            }
        }

        public Servico getById(string id)
        {
            Servico aux = new Servico();

            // Criar conexão com o banco
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                // Criando a instrução SQL
                string query = "Select * From tbReceitasDespesas Where Id=@Id";
                // Criando o objeto command para executar
                SqlCommand _cmd = new SqlCommand(query, _conn);
                // Preenchendo o parâmetro @Id
                _cmd.Parameters.AddWithValue("@Id", id);

                // Abrir a conexão
                _conn.Open();

                // Criando objeto DataReader para ler informações
                using (SqlDataReader reader = _cmd.ExecuteReader())
                {
                    // Ler enquanto tiver dados
                    while (reader.Read())
                    {
                        aux.Id = new Guid(reader["Id"].ToString());
                        aux.Nome = reader["Nome"].ToString();
                        aux.Descricao = reader["Descricao"].ToString();
                        aux.Categoria = Char.Parse(reader["Categoria"].ToString());
                        aux.Valor = Decimal.Parse(reader["Valor"].ToString());
                        aux.CapexOpex = Char.Parse(reader["CapexOpex"].ToString());
                    }
                }

                // Fechar a conexão
                _conn.Close();
            }

            return aux;
        }

        public void update(Servico servico)
        {
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                // Criando instrução de insert
                string instrucao = "UPDATE tbReceitasDespesas " +
                                   "SET Nome = @Nome, Categoria = @Categ, Descricao = @Descri, Valor = @Valor, CapexOpex = @CapexOpex " +
                                    " WHERE Id = @Id;";                                   
                                                                                         
                // Criando o objeto command                                              
                SqlCommand _cmd = new SqlCommand(instrucao, _conn);                      
                                                                                         
                // Preenchendo os parâmetros do Insert
                _cmd.Parameters.AddWithValue("@Id", servico.Id);
                _cmd.Parameters.AddWithValue("@Nome", servico.Nome);
                _cmd.Parameters.AddWithValue("@Categ", servico.Categoria);
                _cmd.Parameters.AddWithValue("@Descri", servico.Descricao);
                _cmd.Parameters.AddWithValue("@Valor", servico.Valor);
                _cmd.Parameters.AddWithValue("@CapexOpex", servico.CapexOpex);

                // Abrindo a conexão com o banco de dados
                _conn.Open();

                // Executando o comando de insert
                _cmd.ExecuteNonQuery();

                // Fechando a conexão com o banco de dados
                _conn.Close();
            }
        }

        public void delete(string id)
        {
            using (SqlConnection _conn = new SqlConnection(_stringDeConexao))
            {
                // Criando instrução de insert
                string instrucao = "DELETE FROM tbReceitasDespesas " +
                                    "WHERE Id = @Id;";

                // Criando o objeto command
                SqlCommand _cmd = new SqlCommand(instrucao, _conn);

                // Preenchendo os parâmetros do Insert
                _cmd.Parameters.AddWithValue("@Id", id);

                // Abrindo a conexão com o banco de dados
                _conn.Open();

                // Executando o comando de insert
                _cmd.ExecuteNonQuery();

                // Fechando a conexão com o banco de dados
                _conn.Close();
            }
        }

    }
}

using MySql.Data.MySqlClient;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace PostoGasolinaAPI
{
    public static class BancoDeDados
    {
        private static string stringDeConexao = "server=localhost;Port=3306;User ID=root;Database=ti_113_uc13";
        public static List<Combustivel> Combustiveis = new List<Combustivel>() 
        {
            new Combustivel 
            { 
                CodigoDoProduto = 1, 
                Descricao = "Gasolina Comum", 
                PrecoLitro = 5.99 
            },
            new Combustivel 
            { 
                CodigoDoProduto = 2, 
                Descricao = "Etanol Comum", 
                PrecoLitro = 3.99 
            },
            new Combustivel 
            { 
                CodigoDoProduto = 3, 
                Descricao = "Diesel", 
                PrecoLitro = 6.99 
            }
        };

        public static List<Compra> Compras = new List<Compra>();

        public static List<Combustivel> ListarCombustiveis() 
        {
            //Abrindo conexao com o meu banco de dados
            MySqlConnection connection = new MySqlConnection(stringDeConexao);
            connection.Open();

            // Definindo a query que será executada
            string query = "SELECT * FROM Combustivel";

            // Criando um comando MySql com a query e a string de conexão
            MySqlCommand command = new MySqlCommand(query, connection);

            // Estou executando o comando o método de leitura de execução
            // Vai me trazer o resultado da minha busca
            MySqlDataReader reader = command.ExecuteReader();

            // Lista para armazenar os valores buscados no meu banco de dados
            List<Combustivel> combustiveis = new List<Combustivel>();

            // Enquanto houver linhas para serem lidas
            while (reader.Read()) {
                // Adiciona esse item na lista de combustíveis
                combustiveis.Add(new Combustivel
                {
                    // Preenche os campos do combustível com os valores do banco de dados
                    CodigoDoProduto = reader.GetInt32("IdCodigoProduto"),
                    Descricao = reader.GetString("Descricao"),
                    PrecoLitro = reader.GetDouble("Preco")
                });
            }
            connection.Close();

            return combustiveis;
        }

        public static Combustivel? BuscaCombustivelEspecifico(int codigoProduto)
        {
            MySqlConnection connection = new MySqlConnection(stringDeConexao);
            connection.Open();

            string query = "SELECT * FROM Combustivel where IdCodigoProduto = @codigoProduto";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@codigoProduto", codigoProduto);

            MySqlDataReader reader = command.ExecuteReader();

            Combustivel? combustivel = null;

            while (reader.Read())
            {
                combustivel = new Combustivel
                {
                    CodigoDoProduto = reader.GetInt32("IdCodigoProduto"),
                    Descricao = reader.GetString("Descricao"),
                    PrecoLitro = reader.GetDouble("Preco")
                };
            }
            connection.Close();

            if (combustivel == null)
                return null;
            else
                return combustivel;
        }

        public static void AtualizarPreco(int codigoProduto, double novoPreco) 
        {
            MySqlConnection connection = new MySqlConnection(stringDeConexao);
            connection.Open();

            string query = "update Combustivel set Preco = @novoPreco where IdCodigoProduto = @codigoProduto";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@novoPreco", novoPreco);
            command.Parameters.AddWithValue("@codigoProduto", codigoProduto);

            command.ExecuteNonQuery();
        }
    }
}

using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // Garante a conexão com o MySQL do XAMPP

namespace EasyProdutos
{
    public partial class Form1 : Form
    {
        // O seu link do banco de dados continua exatamente o mesmo!
        private string linkBanco = "Server=localhost;Database=easyprodutos;Uid=root;Pwd=;";

        public Form1()
        {
            InitializeComponent();
        }

        // 1. O SEU BACK-END DE SALVAR (Adaptado para ler da Interface Gráfica)
        public void SalvarNoBanco(string nome, int qtd, double preco)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(linkBanco))
                {
                    conexao.Open();
                    string query = "INSERT INTO produtos (nome, quantidade, preco) VALUES (@nome, @qtd, @preco)";

                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", nome);
                        comando.Parameters.AddWithValue("@qtd", qtd);
                        comando.Parameters.AddWithValue("@preco", preco);

                        comando.ExecuteNonQuery();
                    }
                }

                // Em vez de Console.WriteLine, usamos uma caixinha de aviso na tela!
                MessageBox.Show($"Produto '{nome}' salvo no banco de dados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar no banco: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. O SEU BACK-END DE CONSULTAR (Adaptado para a Interface Gráfica)
        // Esta função vai retornar 'true' se achar o produto ou 'false' se não achar
        public bool ConsultarProduto(string nomeBusca)
        {
            try
            {
                using (MySqlConnection conexao = new MySqlConnection(linkBanco))
                {
                    conexao.Open();
                    string query = "SELECT nome, quantidade, preco FROM produtos WHERE nome = @nome";

                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", nomeBusca);

                        using (MySqlDataReader dados = comando.ExecuteReader())
                        {
                            if (dados.Read()) // Se o MySQL encontrar o produto
                            {
                                string nomeRetornado = dados["nome"].ToString();
                                int qtdRetornada = Convert.ToInt32(dados["quantidade"]);
                                double precoRetornado = Convert.ToDouble(dados["preco"]);

                                // Mostra os dados em uma janela limpa para o Tião
                                MessageBox.Show($"--- Produto Encontrado! ---\n\nNome: {nomeRetornado}\nQuantidade: {qtdRetornada}\nPreço: R$ {precoRetornado:F2}", "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                return true; // Achou!
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false; // Se chegou aqui, não encontrou
        }
    }
}

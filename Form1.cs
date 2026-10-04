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

                // Caixinha de aviso de sucesso na tela
                MessageBox.Show($"Produto '{nome}' salvo no banco de dados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar no banco: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. O SEU BACK-END DE CONSULTAR (Adaptado para a Interface Gráfica)
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

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // CORRIGIDO: Agora usa o nome correto 'textNome' que o seu designer criou
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string nomePesquisa = textNome.Text;

            if (string.IsNullOrEmpty(nomePesquisa))
            {
                MessageBox.Show("Por favor, digite o nome do produto que deseja buscar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ConsultarProduto(nomePesquisa);
        }

        // CORRIGIDO: Mapeado para ler 'textNome', 'textQuantidade' e 'textPreço' sem dar erro
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string nome = textNome.Text;

            if (string.IsNullOrEmpty(nome))
            {
                MessageBox.Show("Por favor, digite o nome do produto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(textQuantidade.Text, out int quantidade);
            double.TryParse(textPreço.Text, out double preco);

            SalvarNoBanco(nome, quantidade, preco);

            // Limpa os campos da tela usando os nomes certos
            textNome.Clear();
            textQuantidade.Clear();
            textPreço.Clear();
        }
    }
}

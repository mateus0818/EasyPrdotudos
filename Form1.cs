using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EasyProdutos
{
    public class Form1 : Form
    {
        // Linha de conexão com o banco de dados do XAMPP
        private string linkBanco = "Server=localhost;Database=easyprodutos;Uid=root;Pwd=;";

        // Elementos visuais (Campos de texto)
        private TextBox txtNome;
        private TextBox txtQuantidade;
        private TextBox txtPreco;

        public Form1()
        {
            // 1. Configurações da Janela
            this.Text = "Sistema EasyProdutos - Menu do Tião";
            this.Size = new Size(450, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 240, 240);

            // COLOCAMOS A LINHA DO ÍCONE NO LUGAR CORRETO (Dentro do Form1):
            try
            {
                if (System.IO.File.Exists("logo.ico"))
                {
                    this.Icon = new Icon("logo.ico");
                }
            }
            catch (Exception)
            {
                // Se der problema no formato do arquivo, o app abre normal sem quebrar
            }

            // 2. Título Principal
            Label lblTitulo = new Label();
            lblTitulo.Text = "Cadastro & Consulta de Produtos";
            lblTitulo.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitulo.Location = new Point(20, 20);
            lblTitulo.AutoSize = true;
            this.Controls.Add(lblTitulo);

            // 3. Campo: Nome do Produto
            Label lblNome = new Label() { Text = "Nome do Produto:", Location = new Point(20, 70), AutoSize = true };
            txtNome = new TextBox() { Location = new Point(20, 90), Width = 380 };
            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);

            // 4. Campo: Quantidade
            Label lblQuantidade = new Label() { Text = "Quantidade em Estoque:", Location = new Point(20, 130), AutoSize = true };
            txtQuantidade = new TextBox() { Location = new Point(20, 150), Width = 180 };
            this.Controls.Add(lblQuantidade);
            this.Controls.Add(txtQuantidade);

            // 5. Campo: Preço
            Label lblPreco = new Label() { Text = "Preço Unitário (R$):", Location = new Point(220, 130), AutoSize = true };
            txtPreco = new TextBox() { Location = new Point(220, 150), Width = 180 };
            this.Controls.Add(lblPreco);
            this.Controls.Add(txtPreco);

            // 6. Botão Cadastrar (Salvar no Banco)
            Button btnSalvar = new Button();
            btnSalvar.Text = "Cadastrar Produto";
            btnSalvar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnSalvar.Location = new Point(20, 210);
            btnSalvar.Size = new Size(180, 40);
            btnSalvar.BackColor = Color.FromArgb(40, 167, 69); // Verde
            btnSalvar.ForeColor = Color.White;
            btnSalvar.Click += BtnSalvar_Click;
            this.Controls.Add(btnSalvar);

            // 7. Botão Consultar (Buscar no Banco)
            Button btnConsultar = new Button();
            btnConsultar.Text = "Consultar por Nome";
            btnConsultar.Font = new Font("Arial", 10, FontStyle.Bold);
            btnConsultar.Location = new Point(220, 210);
            btnConsultar.Size = new Size(180, 40);
            btnConsultar.BackColor = Color.FromArgb(0, 123, 255); // Azul
            btnConsultar.ForeColor = Color.White;
            btnConsultar.Click += BtnConsultar_Click;
            this.Controls.Add(btnConsultar);
        }

        // AÇÃO DO BOTÃO CADASTRAR
        private void BtnSalvar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por favor, digite o nome do product!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int.TryParse(txtQuantidade.Text, out int qtd);
            double.TryParse(txtPreco.Text, out double preco);

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(linkBanco))
                {
                    conexao.Open();
                    string query = "INSERT INTO produtos (nome, quantidade, preco) VALUES (@nome, @qtd, @preco)";

                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", txtNome.Text);
                        comando.Parameters.AddWithValue("@qtd", qtd);
                        comando.Parameters.AddWithValue("@preco", preco);

                        comando.ExecuteNonQuery();
                    }
                }

                MessageBox.Show($"Produto '{txtNome.Text}' cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNome.Clear();
                txtQuantidade.Clear();
                txtPreco.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar no banco: {ex.Message}", "Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // AÇÃO DO BOTÃO CONSULTAR
        private void BtnConsultar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Digite o nome do produto no campo de cima para poder buscar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conexao = new MySqlConnection(linkBanco))
                {
                    conexao.Open();
                    string query = "SELECT nome, quantidade, preco FROM produtos WHERE nome = @nome";

                    using (MySqlCommand comando = new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", txtNome.Text);

                        using (MySqlDataReader dados = comando.ExecuteReader())
                        {
                            if (dados.Read())
                            {
                                txtQuantidade.Text = dados["quantidade"].ToString();
                                txtPreco.Text = Convert.ToDouble(dados["preco"]).ToString("F2");

                                MessageBox.Show($"Produto encontrado!\n\nEstoque atual: {dados["quantidade"]}\nPreço: R$ {Convert.ToDouble(dados["preco"]):F2}", "Busca Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"O produto '{txtNome.Text}' não foi localizado no estoque.", "Não Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao consultar banco: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

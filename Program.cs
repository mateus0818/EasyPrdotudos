
using System;
using MySql.Data.MySqlClient;

namespace MeuProjeto
{
    // Molde do produto
    public struct Produto
    {
        public string Nome;
        public int Quantidade;
        public double Preco;
    }

    public static class Program
    {
        private static string linkBanco =
            "Server=localhost;Database=easyprodutos;Uid=root;Pwd=;";

        // Cadastra os dados do produto
        public static Produto CriarNovoProduto()
        {
            Produto pdt = new Produto();

            Console.Write("Digite o nome do produto: ");
            pdt.Nome = Console.ReadLine() ?? "";

            // Validação da quantidade
            while (true)
            {
                Console.Write("Digite a quantidade do produto: ");
                if (int.TryParse(Console.ReadLine(), out int quantidade))
                {
                    pdt.Quantidade = quantidade;
                    break;
                }

                Console.WriteLine("Quantidade inválida. Tente novamente.");
            }

            // Validação do preço
            while (true)
            {
                Console.Write("Digite o preço do produto: ");
                if (double.TryParse(Console.ReadLine(), out double preco))
                {
                    pdt.Preco = preco;
                    break;
                }

                Console.WriteLine("Preço inválido. Tente novamente.");
            }

            return pdt;
        }

        // Consulta um produto no banco de dados
        public static void ConsultarProduto()
        {
            Console.Write("Digite o nome do produto: ");
            string nomeBusca = Console.ReadLine() ?? "";

            bool encontrado = false;

            try
            {
                using (MySqlConnection conexao =
                    new MySqlConnection(linkBanco))
                {
                    conexao.Open();

                    string query = "SELECT nome, quantidade, preco " +
                                   "FROM produtos WHERE nome = @nome";

                    using (MySqlCommand comando =
                        new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", nomeBusca);

                        using (MySqlDataReader dados =
                            comando.ExecuteReader())
                        {
                            if (dados.Read())
                            {
                                Console.WriteLine(
                                    "\n--- Produto Encontrado ---");

                                Console.WriteLine($"Nome: {dados["nome"]}");
                                Console.WriteLine(
                                    $"Quantidade em Estoque: {dados["quantidade"]}");
                                Console.WriteLine(
                                    $"Preço: R$ {Convert.ToDouble(dados["preco"]):F2}");

                                Console.WriteLine("--------------------------");

                                encontrado = true;
                            }
                        }
                    }
                }

                if (!encontrado)
                {
                    Console.WriteLine(
                        $"\nO produto '{nomeBusca}' não foi encontrado.");

                    Console.Write(
                        "Deseja cadastrar um produto agora? (S/N): ");

                    string resposta = Console.ReadLine() ?? "";

                    if (resposta.Equals(
                        "S", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Clear();

                        Produto novo = CriarNovoProduto();
                        SalvarNoBanco(novo);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"\nErro ao consultar o banco: {ex.Message}");
            }

            if (!encontrado)
            {
                Console.WriteLine(
                    "\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(
                    "\nPressione qualquer tecla para voltar ao menu...");
                Console.ReadKey();
            }
        }

        // Salva o produto no banco de dados
        public static void SalvarNoBanco(Produto p)
        {
            try
            {
                using (MySqlConnection conexao =
                    new MySqlConnection(linkBanco))
                {
                    conexao.Open();

                    string query =
                        "INSERT INTO produtos (nome, quantidade, preco) " +
                        "VALUES (@nome, @qtd, @preco)";

                    using (MySqlCommand comando =
                        new MySqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@nome", p.Nome);
                        comando.Parameters.AddWithValue("@qtd", p.Quantidade);
                        comando.Parameters.AddWithValue("@preco", p.Preco);

                        comando.ExecuteNonQuery();
                    }
                }

                Console.WriteLine(
                    $"\nProduto '{p.Nome}' salvo com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"\nErro ao salvar o produto: {ex.Message}");
            }

            Console.WriteLine(
                "Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        // Menu principal
        static int Main()
        {
            Console.WriteLine("=== Cadastro de Produto ===");

            Console.Write("Digite seu nome: ");
            string nome = Console.ReadLine() ?? "Usuário";

            Console.WriteLine($"Olá, {nome}!");

            bool continuar = true;

            while (continuar)
            {
                try
                {
                    Console.Clear();
                }
                catch (System.IO.IOException)
                {
                    // Ignora erros ao limpar o console
                }

                Console.WriteLine("=== EASYPRODUTOS ===");
                Console.WriteLine("O que deseja fazer?");
                Console.WriteLine("1 - Cadastrar um produto");
                Console.WriteLine("2 - Consultar um produto");
                Console.WriteLine("3 - Sair");
                Console.Write("Escolha uma opção: ");

                int.TryParse(Console.ReadLine(), out int opcao);

                if (opcao == 1)
                {
                    Produto p1 = CriarNovoProduto();
                    SalvarNoBanco(p1);
                }
                else if (opcao == 2)
                {
                    ConsultarProduto();
                }
                else if (opcao == 3)
                {
                    Console.WriteLine("Saindo do programa...");
                    continuar = false;
                }
                else
                {
                    Console.WriteLine(
                        "Opção inválida. Tente novamente.");
                    Console.WriteLine(
                        "Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }

            return 0;
        }
    }
}
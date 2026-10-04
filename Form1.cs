using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace MeuProjeto
{
    // 1. Definição da Struct (O "Molde")
    public struct produto
    {
        public string nome;
        public int quantidade;
        public double preco;
    }

    public static class Program
    {
        private static string linkBanco = "Server=localhost;Database=easyprodutos;Uid=root;Pwd=;";


        // 2. Função responsável por ler os dados e retornar o produto
        public static produto CriarNovoProduto()
        {
            // Criamos a instância da struct que vai guardar os dados
            produto pdt = new produto();

            // Leitura do Nome
            Console.WriteLine("Digite o nome do produto: ");
            pdt.nome = (Console.ReadLine() ?? "nome");

            // Leitura e conversão da Quantidade (string -> int)
            Console.WriteLine("Digite a quantidade do produto: ");
            pdt.quantidade = int.Parse(Console.ReadLine() ?? "0");

            // Leitura e conversão do Preço (string -> double)
            Console.WriteLine("Digite o preço do produto: ");
            pdt.preco = double.Parse(Console.ReadLine() ?? "0");

            // Retorna o produto preenchido
            return pdt;
        }

        public static void ConsultarProduto() // <-- Sem parênteses cheios de listas!
        {
            // O início continua igual: pede o nome para o Tião
            Console.Write("Digite o nome do produto: ");
            string nomeBusca = Console.ReadLine() ?? "";

            bool encontrado = false;

            // 🛑 DAQUI PARA BAIXO TUDO MUDOU: Entra o código do Banco de Dados
            using (MySqlConnection conexao = new MySqlConnection(linkBanco))
            {
                conexao.Open(); // Abre o banco

                // Criamos a pergunta em SQL para o MySQL buscar
                string query = "SELECT nome, quantidade, preco FROM produtos WHERE nome = @nome";

                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@nome", nomeBusca); // Passa o nome digitado

                    // O DataReader é o leitor que pega a resposta do MySQL
                    using (MySqlDataReader dados = comando.ExecuteReader())
                    {
                        // Se o MySQL responder que achou a linha:
                        if (dados.Read())
                        {
                            Console.WriteLine("\n--- Produto Encontrado no Banco! ---");
                            // Puxamos os dados direto das colunas da tabela do XAMPP:
                            Console.WriteLine($"Nome: {dados["nome"]}");
                            Console.WriteLine($"Quantidade em Estoque: {dados["quantidade"]}");
                            Console.WriteLine($"Preço: R$ {Convert.ToDouble(dados["preco"]):F2}");
                            Console.WriteLine("----------------------------");

                            encontrado = true;

                            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                            Console.ReadKey();
                        }
                    }
                }
            } // O bloco 'using' fecha a conexão com o banco automaticamente aqui

            // Se o MySQL vasculhou e não achou nada (encontrado continuou false)
            if (!encontrado)
            {
                Console.WriteLine($"\nO produto '{nomeBusca}' não foi encontrado.");
                Console.Write("Deseja cadastrar um produto agora? (S/N): ");
                string resposta = Console.ReadLine() ?? "";

                // Se o Tião digitar 'S' ou 's'
                if (resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();

                    // 1. Roda a SUA função original de fazer as perguntas na tela
                    produto novo = CriarNovoProduto();

                    // 2. Chama a nova função para gravar esse produto no MySQL do XAMPP
                    SalvarNoBanco(novo);
                }
                else
                {
                    Console.WriteLine("\nVoltando ao menu principal...");
                    Console.ReadKey();
                }
            }
        }


        public static void SalvarNoBanco(produto p)
        {
            // 1. Cria a ponte com o XAMPP usando a nossa string de conexão
            using (MySqlConnection conexao = new MySqlConnection(linkBanco))
            {
                // 2. Abre a porta do banco de dados
                conexao.Open();

                // 3. Escreve o comando SQL de inserção (o mesmo que se usa no phpMyAdmin)
                string query = "INSERT INTO produtos (nome, quantidade, preco) VALUES (@nome, @qtd, @preco)";

                // 4. Prepara o comando para ser enviado de forma segura
                using (MySqlCommand comando = new MySqlCommand(query, conexao))
                {
                    // 5. Vincula os dados da sua struct 'produto p' aos parâmetros do banco
                    comando.Parameters.AddWithValue("@nome", p.nome);
                    comando.Parameters.AddWithValue("@qtd", p.quantidade);
                    comando.Parameters.AddWithValue("@preco", p.preco);

                    // 6. Dá o "raiozinho" (executa o comando) para gravar no HD de verdade!
                    comando.ExecuteNonQuery();
                }
            }

            // 7. Avisa o Tião que deu certo
            Console.WriteLine($"\nProduto '{p.nome}' salvo no banco de dados com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
        }

        static int Main() { 


            List<produto> listaDeProdutos = new List<produto>();
        

            Console.WriteLine("=== Cadastro de Produto ===");

            Console.WriteLine("digite seu nome ");
            string nome = (Console.ReadLine() ?? "nome");

            Console.WriteLine($"Ola {nome}!");

            bool continuar = true;

            // O loop 'while' mantém o console aberto até que a opção 3 seja digitada

            while (continuar)
            {
                Console.Clear();

                Console.WriteLine("O que deseja fazer?");
                Console.WriteLine("1 - Cadastrar um produto");
                Console.WriteLine("2 - Consultar um produto");
                Console.WriteLine("3 - Sair");

                int.TryParse(Console.ReadLine(), out int opcao);

                if (opcao == 1)
                {
                    // 3. Chamando a função para criar um novo produto we e armazenando o resultado na variável p1

                    produto p1 = CriarNovoProduto();
                   
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
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

            }
            return 0;
        }
    }
}

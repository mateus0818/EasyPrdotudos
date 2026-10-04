using System;
using System.Collections.Specialized;
using System.Collections.Generic;

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

        // Função responsável por buscar o produto pelo nome
        public static void ConsultarProduto(List<produto> produtos)
        {
            Console.Clear();
            Console.WriteLine("=== CONSULTA DE PRODUTO ===");
            Console.Write("Digite o nome do produto que deseja pesquisar: ");
            string nomeBusca = Console.ReadLine() ?? "";

            bool encontrado = false;

            foreach (produto p in produtos)
            {
                if (p.nome.Equals(nomeBusca, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\n--- Produto Encontrado! ---");
                    Console.WriteLine($"Nome: {p.nome}");
                    Console.WriteLine($"Quantidade em Estoque: {p.quantidade}");
                    Console.WriteLine($"Preço: R$ {p.preco:F2}");
                    Console.WriteLine("----------------------------");
                    encontrado = true;

                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine($"\nO produto '{nomeBusca}' não foi encontrado.");
                Console.Write("Deseja cadastrar um produto agora? (S/N): ");
                string resposta = Console.ReadLine() ?? "";

                if (resposta.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();

                    // CHAMANDO A SUA FUNÇÃO ORIGINIAL AQUI DENTRO!
                    produto novo = CriarNovoProduto();
                    produtos.Add(novo);

                    Console.WriteLine($"\nProduto cadastrado com sucesso!");
                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\nVoltando ao menu principal...");
                    Console.ReadKey();
                }
            }
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
                Console.WriteLine("O que deseja fazer?");
                Console.WriteLine("1 - Cadastrar um produto");
                Console.WriteLine("2 - Consultar um produto");
                Console.WriteLine("3 - Sair");

                int.TryParse(Console.ReadLine(), out int opcao);

                if (opcao == 1)
                {
                    // 3. Chamando a função para criar um novo produto we e armazenando o resultado na variável p1

                    produto p1 = CriarNovoProduto();
                    listaDeProdutos.Add(p1);
                }
                else if (opcao == 2)
                {
                    ConsultarProduto(listaDeProdutos);
                }
                else if (opcao == 3)
                {
                    Console.WriteLine("Saindo do programa...");
                    continuar = false;
                }
                else
                {
                    Console.WriteLine("Opção inválida. Tente novamente.");
                }

            }
            return 0;
        }
    }
}

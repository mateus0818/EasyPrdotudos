using System;
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

        static int Main()
        {
            Console.WriteLine("=== Cadastro de Produto ===");

            Console.WriteLine("digite seu nome ");
            string nome = (Console.ReadLine() ?? "nome");

            Console.WriteLine($"Ola {nome}!");

            bool continuar = true;

            Console.WriteLine("O que deseja fazer?");
            Console.WriteLine("1 - Cadastrar um produto");
            Console.WriteLine("2 - Consultar um produto");
            Console.WriteLine("3 - Sair");
            int.TryParse(Console.ReadLine(), out int opcao);

            if (opcao == 1)
            {
                // 3. Chamando a função para criar um novo produto we e armazenando o resultado na variável p1

                produto p1 = CriarNovoProduto();
            }
            else if (opcao == 2)
            {
                Console.WriteLine("Consulta de produto ainda não implementada.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");

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


            return 0;
        }
    }
}

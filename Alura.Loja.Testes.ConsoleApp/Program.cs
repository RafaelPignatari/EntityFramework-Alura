using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GravarUsandoEntity();
            VisualizarUsandoEntity();
            AlterarUsandoEntity();
            VisualizarUsandoEntity();
            DeletarUsandoEntity();
            VisualizarUsandoEntity();
            Console.ReadKey();
        }

        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var repo = new ProdutoDAOEntity())
            {
                repo.Adicionar(p);
            }
        }
        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var context = new ProdutoDAOEntity())
            {
                context.Adicionar(p);
            }
        }        
        private static void VisualizarUsandoEntity()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = contexto.Listar();
                Console.WriteLine("Foram encontrados {0} produtos", produtos.Count);
                foreach (var produto in produtos)
                    Console.WriteLine(produto.Nome);
            }
        }
        private static void DeletarUsandoEntity()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> produtos = contexto.Listar();
                foreach (var produto in produtos)
                {
                    contexto.Remover(produto);
                }
            }
        }
        private static void AlterarUsandoEntity()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                Produto primeiro = contexto.Listar().First();
                primeiro.Nome = "testeee";
                contexto.Atualizar(primeiro);
            }
        }
    }
}

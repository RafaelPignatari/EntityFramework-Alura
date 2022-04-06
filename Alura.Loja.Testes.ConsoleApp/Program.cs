using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
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
            using (var contexto = new LojaContext())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Queima Total Janeiro 2022";
                promocao.DataInicio = new DateTime(2022, 1, 1);
                promocao.DataTermino = new DateTime(2022, 1, 31);

                var produtos = contexto.Produtos.Where(p => p.Categoria == "Bebidas").ToList();
                foreach (var produto in produtos)
                {
                    promocao.IncluiProduto(produto);
                }

                contexto.Add(promocao);
                contexto.SaveChanges();
            }

            using (var contexto = new LojaContext())
            {
                var promocao = contexto
                    .Promocoes
                    .Include(p => p.Produtos)
                    .ThenInclude(pp => pp.Produto)
                    .Last();
                foreach (var produto in promocao.Produtos)
                {
                    Console.WriteLine(produto.Produto);
                }
                Console.ReadLine();
            }
        }
        static void UmParaUm()
        {
            var fulano = new Cliente();
            fulano.Nome = "Fulaninho";
            fulano.EnderecoEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua dos Inválidos",
                Complemento = "sobrado",
                Bairro = "Centro",
                Cidade = "Cidade"
            };

            using (var contexto = new LojaContext())
            {
                contexto.Add(fulano);
                contexto.SaveChanges();
            }
        }
        static void UmParaMuitos()
        {
            var paoFrances = new Produto();
            paoFrances.Nome = "Pão Francês";
            paoFrances.PrecoUnitario = 0.40;
            paoFrances.Unidade = "Unidade";
            paoFrances.Categoria = "Padaria";

            var compra = new Compra();
            compra.Produto = paoFrances;
            compra.Quantidade = 6;
            compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

            //Note que adicionamos a compra e ela puxa o produto junto.
            using (var contexto = new LojaContext())
            {
                contexto.Add(compra);
                contexto.SaveChanges();
            }
        }
        static void MuitosParaMuitos()
        {
            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.45, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Suco de Laranja", Categoria = "Alimentos", PrecoUnitario = 5, Unidade = "Litros" };

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {
                //Adiciona a promoção e todos os seus dependentes da tabela PromocaoProduto
                //Nota: Não remove os produtos adicionados da tabela Produto.
                contexto.Add(promocaoDePascoa);
                contexto.SaveChanges();

                //Remove o primeiro item da tabela Promocao.
                //var promocao = contexto.Promocoes.FirstOrDefault();
                //contexto.Remove(promocao);
                //contexto.SaveChanges();
            }
        }
    }
}

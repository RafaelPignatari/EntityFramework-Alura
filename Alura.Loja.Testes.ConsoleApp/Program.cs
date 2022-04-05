﻿using Microsoft.EntityFrameworkCore.Infrastructure;
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
            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros"};
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

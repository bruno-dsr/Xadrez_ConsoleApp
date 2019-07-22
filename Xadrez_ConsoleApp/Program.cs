using System;
using Model;
using Model.Enums;
using Controller;

namespace Xadrez_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro T = new Tabuleiro(8, 8);

            T.ColocarPeca(new Torre(T, Cor.Preto), new Posicao(3, 3));

            Tela.ImprimirTabuleiro(T);




            Console.ReadKey();
        }
    }
}

using System;
using Model;
using Model.Enums;

namespace Xadrez_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro T = new Tabuleiro(8, 8);

            Tela.ImprimirTabuleiro(T);
        }
    }
}

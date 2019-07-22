using System;
using Model;
using Model.Enums;

namespace Xadrez_ConsoleApp
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        ImprimirPeca(tab.Peca(i, j));
                    }

                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h".ToUpper());
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca.Cor == Cor.Branco)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }

        public static void ImprimirLegendaPecas()
        {
            Console.WriteLine("* Peças Brancas *");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("* Peças Pretas *");
            Console.ForegroundColor = aux;
        }
    }
}

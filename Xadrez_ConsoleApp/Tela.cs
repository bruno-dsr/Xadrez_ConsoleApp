using System;
using Model;
using Model.Enums;
using Controller;
using System.Collections.Generic;

namespace Xadrez_ConsoleApp
{
    class Tela
    {
        public static void ImprimirPartida(Partida partida)
        {
            ImprimirTabuleiro(partida.Tabuleiro);
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            Console.WriteLine("Aguardando jogador: " + partida.JogadorAtual);
        }

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.Peca(i, j));
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPecasCapturadas(Partida partida)
        {
            Console.WriteLine();
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjuntoPecas(partida.GetPecasCapturadas(Cor.Branco));
            Console.WriteLine();

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Pretas:  ");
            ImprimirConjuntoPecas(partida.GetPecasCapturadas(Cor.Preto));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ImprimirConjuntoPecas(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Console.Write(peca + " ");
            }
            Console.Write("]");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] movimentosPossiveis)
        {
            ConsoleColor ogColor = Console.BackgroundColor;
            ConsoleColor altColor = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (movimentosPossiveis[i, j] == true)
                    {
                        Console.BackgroundColor = altColor;
                    }
                    else Console.BackgroundColor = ogColor;

                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = ogColor;
                }
                Console.WriteLine();
                Console.BackgroundColor = ogColor;
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }

            else
            {
                if (peca.Cor == Cor.Branco)
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
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}

using System;
using Model;
using Model.Enums;
using Model.ModelException;
using Controller;

namespace Xadrez_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Partida partida = new Partida();

                while (!partida.Finalizada)
                {
                    Tela.ImprimirTabuleiro(partida.Tabuleiro);
                    Console.WriteLine();
                    Tela.ImprimirLegendaPecas();
                    Console.WriteLine();

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                    Console.Clear();
                    bool[,] movimentosPossiveis = partida.Tabuleiro.Peca(origem).MovimentosPossiveis();
                    Tela.ImprimirTabuleiro(partida.Tabuleiro, movimentosPossiveis);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                    partida.MoverPeca(origem, destino);

                    Console.Clear();
                }
            }

            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}

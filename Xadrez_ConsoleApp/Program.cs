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
                    PosicaoXadrez origem = Tela.LerPosicaoXadrez();
                    Console.Write("Destino: ");
                    PosicaoXadrez destino = Tela.LerPosicaoXadrez();
                    partida.MoverPeca(origem.ToPosicao(), destino.ToPosicao());

                    Tela.ImprimirTabuleiro(partida.Tabuleiro);

                    Console.ReadKey();
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

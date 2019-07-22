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

                Tela.ImprimirTabuleiro(partida.Tabuleiro);
                Console.WriteLine();
                Tela.ImprimirLegendaPecas();
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

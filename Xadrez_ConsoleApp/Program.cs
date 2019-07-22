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
                Tabuleiro t = new Tabuleiro(8, 8);

                t.ColocarPeca(new Torre(t, Cor.Branco), new Posicao(3, 3));
                t.ColocarPeca(new Dama(t, Cor.Preto), new Posicao(3, 5));

                Tela.ImprimirTabuleiro(t);
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

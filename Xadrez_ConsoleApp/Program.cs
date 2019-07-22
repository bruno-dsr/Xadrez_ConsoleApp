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
                Tabuleiro T = new Tabuleiro(8, 8);

                T.ColocarPeca(new Torre(T, Cor.Preto), new Posicao(3, 3));

                Tela.ImprimirTabuleiro(T);
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

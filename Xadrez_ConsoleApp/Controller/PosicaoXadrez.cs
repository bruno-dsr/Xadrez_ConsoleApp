using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public PosicaoXadrez()
        {

        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }
    }
}

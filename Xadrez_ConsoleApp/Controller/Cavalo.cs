using Model;
using Model.Enums;

namespace Controller
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {

        }

        public override string ToString()
        {
            return "C ";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao p = new Posicao(Posicao.Linha, Posicao.Coluna);
            //ACIMA-DIREITA
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(Posicao.Linha - 2, Posicao.Coluna + 1);
            if(Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //ACIMA-ESQUERDA
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //ABAIXO-DIREITA
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //ABAIXO-ESQUERDA
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //DIREITA-ACIMA
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //DIREITA-ABAIXO
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //ESQUERDA-ACIMA
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //ESQUERDA-ABAIXO
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            return movimentos;
        }
    }
}

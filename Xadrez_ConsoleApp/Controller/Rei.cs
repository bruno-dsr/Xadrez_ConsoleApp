using Model;
using Model.Enums;

namespace Controller
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {

        }

        public override string ToString()
        {
            return "R ";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao inicial = Posicao;
            Posicao p = new Posicao(Posicao.Linha, Posicao.Coluna);

            //N
            p.SetPosicao(p.Linha - 1, p.Coluna);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            ////NE
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha - 1, p.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //L
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha, p.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //SE
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha + 1, p.Coluna + 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //S
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha + 1, p.Coluna);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //SO
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha + 1, p.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //O
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha, p.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            //NO
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha - 1, p.Coluna - 1);
            if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
            }

            return movimentos;
        }
    }
}

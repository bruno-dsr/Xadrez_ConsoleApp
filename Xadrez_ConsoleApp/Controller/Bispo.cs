using Model;
using Model.Enums;

namespace Controller
{
    class Bispo : Peca
    {
        public Bispo(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {

        }

        public override string ToString()
        {
            return "B ";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];
            Posicao p = new Posicao(Posicao.Linha, Posicao.Coluna);

            //NE
            p.SetPosicao(p.Linha - 1, p.Coluna + 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
                if (Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor != this.Cor)
                {
                    break;
                }
                p.Linha -= 1;
                p.Coluna += 1;
            }

            //SE
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha + 1, p.Coluna + 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
                if (Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor != this.Cor)
                {
                    break;
                }
                p.Linha += 1;
                p.Coluna += 1;
            }

            //SO
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha + 1, p.Coluna - 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
                if (Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor != this.Cor)
                {
                    break;
                }
                p.Linha += 1;
                p.Coluna -= 1;
            }

            //NO
            p.SetPosicao(Posicao.Linha, Posicao.Coluna);
            p.SetPosicao(p.Linha - 1, p.Coluna - 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
                if (Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor != this.Cor)
                {
                    break;
                }
                p.Linha -= 1;
                p.Coluna -= 1;
            }

            return movimentos;
        }
    }
}

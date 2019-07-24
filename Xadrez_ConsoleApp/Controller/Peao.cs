using Model;
using Model.Enums;

namespace Controller
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {

        }

        public override string ToString()
        {
            return "P ";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao p = new Posicao(Posicao.Linha, Posicao.Coluna);

            //ACIMA(PEÇAS BRANCAS)
            if (Cor == Cor.Branco)
            {
                p.SetPosicao(Posicao.Linha, Posicao.Coluna);
                p.SetPosicao(p.Linha - 1, p.Coluna);
                if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                {
                    movimentos[p.Linha, p.Coluna] = true;

                    p.SetPosicao(p.Linha - 1, p.Coluna);
                    if (Tabuleiro.PosicaoValida(p) && PodeMover(p) && QtdeMovimentos == 0)
                    {
                        movimentos[p.Linha, p.Coluna] = true;
                    }
                }
            }

            //ABAIXO(PEÇAS PRETAS)
            else if (Cor == Cor.Preto)
            {
                p.SetPosicao(Posicao.Linha, Posicao.Coluna);
                p.SetPosicao(p.Linha + 1, p.Coluna);
                if (Tabuleiro.PosicaoValida(p) && PodeMover(p))
                {
                    movimentos[p.Linha, p.Coluna] = true;

                    p.SetPosicao(p.Linha + 1, p.Coluna);
                    if (Tabuleiro.PosicaoValida(p) && PodeMover(p) && QtdeMovimentos == 0)
                    {
                        movimentos[p.Linha, p.Coluna] = true;
                    }
                }
            }

            return movimentos;
        }
    }
}

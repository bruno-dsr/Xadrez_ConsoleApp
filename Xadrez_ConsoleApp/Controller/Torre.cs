using Model;
using Model.Enums;

namespace Controller
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {

        }

        public override string ToString()
        {
            return "T ";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] movimentos = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao p = new Posicao(Posicao.Linha, Posicao.Coluna);

            //ACIMA
            p.SetPosicao(p.Linha - 1, p.Coluna);
            while(Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
                if (Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor != this.Cor)
                {
                    break;
                }
                p.Linha -= 1;
            }

            //ABAIXO
            p = Posicao;
            p.SetPosicao(p.Linha + 1, p.Coluna);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
                if (Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor != this.Cor)
                {
                    break;
                }
                p.Linha += 1;
            }

            //DIREITA
            p = Posicao;
            p.SetPosicao(p.Linha, p.Coluna + 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
                if (Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor != this.Cor)
                {
                    break;
                }
                p.Coluna += 1;
            }

            //ESQUERDA
            p = Posicao;
            p.SetPosicao(p.Linha, p.Coluna - 1);
            while (Tabuleiro.PosicaoValida(p) && PodeMover(p))
            {
                movimentos[p.Linha, p.Coluna] = true;
                if (Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor != this.Cor)
                {
                    break;
                }
                p.Coluna -= 1;
            }


            return movimentos;
        }
    }
}

using Model;
using Model.Enums;

namespace Controller
{
    class Peao : Peca
    {
        Partida partida;

        public Peao(Tabuleiro tabuleiro, Cor cor, Partida partida) : base(cor, tabuleiro)
        {
            this.partida = partida;
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

                p.SetPosicao(Posicao.Linha, Posicao.Coluna);
                p.SetPosicao(p.Linha - 1, p.Coluna - 1);
                if (Tabuleiro.PosicaoValida(p) && Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor == Cor.Preto && PodeMover(p))
                {
                    movimentos[p.Linha, p.Coluna] = true;
                }

                p.SetPosicao(Posicao.Linha, Posicao.Coluna);
                p.SetPosicao(p.Linha - 1, p.Coluna + 1);
                if (Tabuleiro.PosicaoValida(p) && Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor == Cor.Preto && PodeMover(p))
                {
                    movimentos[p.Linha, p.Coluna] = true;
                }

                //En Passant Branca

                p.SetPosicao(Posicao.Linha, Posicao.Coluna);
                if (p.Linha == 3)
                {
                    Posicao esquerda = new Posicao(p.Linha, p.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && Tabuleiro.ExistePeca(esquerda) && Tabuleiro.Peca(esquerda).Cor == Cor.Preto
                        && Tabuleiro.Peca(esquerda) == partida.EnPassant)
                    {
                        movimentos[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(p.Linha, p.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && Tabuleiro.ExistePeca(direita) && Tabuleiro.Peca(direita).Cor == Cor.Preto
                        && Tabuleiro.Peca(direita) == partida.EnPassant)
                    {
                        movimentos[direita.Linha - 1, direita.Coluna] = true;
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

                p.SetPosicao(Posicao.Linha, Posicao.Coluna);
                p.SetPosicao(p.Linha + 1, p.Coluna - 1);
                if (Tabuleiro.PosicaoValida(p) && Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor == Cor.Branco && PodeMover(p))
                {
                    movimentos[p.Linha, p.Coluna] = true;
                }

                p.SetPosicao(Posicao.Linha, Posicao.Coluna);
                p.SetPosicao(p.Linha + 1, p.Coluna + 1);
                if (Tabuleiro.PosicaoValida(p) && Tabuleiro.ExistePeca(p) && Tabuleiro.Peca(p).Cor == Cor.Branco && PodeMover(p))
                {
                    movimentos[p.Linha, p.Coluna] = true;
                }

                //En Passant Preta

                p.SetPosicao(Posicao.Linha, Posicao.Coluna);
                if (p.Linha == 4)
                {
                    Posicao esquerda = new Posicao(p.Linha, p.Coluna - 1);
                    if (Tabuleiro.PosicaoValida(esquerda) && Tabuleiro.ExistePeca(esquerda) && Tabuleiro.Peca(esquerda).Cor == Cor.Branco
                        && Tabuleiro.Peca(esquerda) == partida.EnPassant)
                    {
                        movimentos[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(p.Linha, p.Coluna + 1);
                    if (Tabuleiro.PosicaoValida(direita) && Tabuleiro.ExistePeca(direita) && Tabuleiro.Peca(direita).Cor == Cor.Branco
                        && Tabuleiro.Peca(direita) == partida.EnPassant)
                    {
                        movimentos[direita.Linha + 1, direita.Coluna] = true;
                    }
                }
            }
            return movimentos;
        }
    }
}

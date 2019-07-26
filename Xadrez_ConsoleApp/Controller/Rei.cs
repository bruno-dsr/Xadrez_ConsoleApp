using Model;
using Model.Enums;

namespace Controller
{
    class Rei : Peca
    {
        private Partida partida;
        public Rei(Tabuleiro tabuleiro, Cor cor, Partida partida) : base(cor, tabuleiro)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R ";
        }

        private bool TorreElegivelRoque(Posicao P)
        {
            Peca T = Tabuleiro.Peca(P);
            return T != null && T is Torre && T.QtdeMovimentos == 0 && T.Cor == Cor;
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

            //ROQUE PEQUENO
            if (QtdeMovimentos == 0 && !partida.JogadorEmXeque)
            {
                Posicao T1 = new Posicao(this.Posicao.Linha, this.Posicao.Coluna + 3);
                if (Tabuleiro.PosicaoValida(T1) && TorreElegivelRoque(T1))
                {
                    Posicao P1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao P2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tabuleiro.PosicaoValida(P1) && Tabuleiro.PosicaoValida(P2))
                    {
                        if (Tabuleiro.Peca(P1) == null && Tabuleiro.Peca(P2) == null)
                        {
                            movimentos[Posicao.Linha, Posicao.Coluna + 2] = true;
                        }
                    }
                }
            }

            ////ROQUE GRANDE
            if (QtdeMovimentos == 0 && !partida.JogadorEmXeque)
            {
                Posicao T2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (Tabuleiro.PosicaoValida(T2) && TorreElegivelRoque(T2))
                {
                    Posicao P1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao P2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao P3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tabuleiro.PosicaoValida(P1) && Tabuleiro.PosicaoValida(P2) && Tabuleiro.PosicaoValida(P3))
                    {
                        if (Tabuleiro.Peca(P1) == null && Tabuleiro.Peca(P2) == null && Tabuleiro.Peca(P3) == null)
                        {
                            movimentos[Posicao.Linha, Posicao.Coluna - 2] = true;
                        }
                    }
                }
            }

            return movimentos;
        }
    }
}

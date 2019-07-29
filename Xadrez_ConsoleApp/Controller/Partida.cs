using Model;
using Model.Enums;
using Model.ModelException;
using System.Collections.Generic;
using System;

namespace Controller
{
    class Partida
    {
        public Tabuleiro Tabuleiro { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Finalizada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas;
        public bool JogadorEmXeque { get; private set; }
        public Peca EnPassant { get; private set; }

        public Partida()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Finalizada = false;
            JogadorEmXeque = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            EnPassant = null;
            IniciarPecas();
        }

        public Peca MoverPeca(Posicao origem, Posicao destino)
        {
            Peca P = Tabuleiro.RetirarPeca(origem);
            P.IncrementarMovimentos();
            Peca Captura = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(P, destino);
            if (Captura != null)
            {
                pecasCapturadas.Add(Captura);
            }

            //ROQUE PEQUENO
            if (P is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementarMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            //ROQUE GRANDE
            if (P is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(origemT);
                T.IncrementarMovimentos();
                Tabuleiro.ColocarPeca(T, destinoT);
            }

            // EN PASSANT
            if (P is Peao && destino.Coluna != origem.Coluna && Captura == null)
            {
                Posicao enPassantPosisao;
                if (P.Cor == Cor.Branco)
                {
                    enPassantPosisao = new Posicao(P.Posicao.Linha + 1, P.Posicao.Coluna);
                }
                else
                {
                    enPassantPosisao = new Posicao(P.Posicao.Linha - 1, P.Posicao.Coluna);
                }

                if (Tabuleiro.Peca(enPassantPosisao) == EnPassant)
                {
                    Captura = Tabuleiro.RetirarPeca(enPassantPosisao);
                    pecasCapturadas.Add(Captura);
                }
            }

            return Captura;
        }

        public HashSet<Peca> GetPecasCapturadas(Cor cor)
        {
            HashSet<Peca> capturadas = new HashSet<Peca>();

            foreach (Peca peca in pecasCapturadas)
            {
                if (peca.Cor == cor)
                {
                    capturadas.Add(peca);
                }
            }
            return capturadas;
        }

        public HashSet<Peca> GetPecasEmJogo(Cor cor)
        {
            HashSet<Peca> emJogo = new HashSet<Peca>();

            foreach (Peca peca in pecas)
            {
                if (peca.Cor == cor)
                {
                    emJogo.Add(peca);
                }
            }
            emJogo.ExceptWith(GetPecasCapturadas(cor));

            return emJogo;
        }

        private Cor GetCorAdversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca GetRei(Cor cor)
        {
            foreach (Peca peca in GetPecasEmJogo(cor))
            {
                if (peca is Rei)
                {
                    return peca;
                }
            }
            return null;
        }

        public bool EmXeque(Cor cor)
        {
            Peca rei = GetRei(cor);

            if (rei == null)
            {
                throw new TabuleiroException("Não existe rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca p in GetPecasEmJogo(GetCorAdversaria(rei.Cor)))
            {
                bool[,] movimentos = p.MovimentosPossiveis();

                if (movimentos[rei.Posicao.Linha, rei.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool XequeMate(Cor cor)
        {
            if (!EmXeque(cor))
            {
                return false;
            }

            foreach (Peca p in GetPecasEmJogo(cor))
            {
                bool[,] movimentos = p.MovimentosPossiveis();
                for (int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for (int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if (movimentos[i, j])
                        {
                            Posicao origem = p.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca captura = MoverPeca(p.Posicao, destino);
                            bool emXeque = EmXeque(cor);
                            ReverterJogada(origem, destino, captura);

                            if (!emXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void Jogada(Posicao origem, Posicao destino)
        {
            Peca captura = MoverPeca(origem, destino);

            if (EmXeque(JogadorAtual))
            {
                ReverterJogada(origem, destino, captura);
                throw new TabuleiroException("Impossível se colocar em cheque!");
            }

            Peca p = Tabuleiro.Peca(destino);

            //PROMOÇÃO DE PEÇA
            if (p is Peao)
            {
                if (p.Cor == Cor.Branco && destino.Linha == 0 || p.Cor == Cor.Preto && destino.Linha == 7)
                {
                    Console.WriteLine();
                    Console.WriteLine("Possível promoção!");
                    Console.Write("Escolha [T]Torre, [B]Bispo, [C]Cavalo ou [D]Dama: ");
                    string promo = Console.ReadLine().ToLower();

                    switch (promo)
                    {
                        case "t":
                            Peca T = new Torre(Tabuleiro, p.Cor);
                            Tabuleiro.RetirarPeca(destino);
                            pecas.Remove(p);
                            Tabuleiro.ColocarPeca(T, destino);
                            pecas.Add(T);
                            break;

                        case "b":
                            Peca B = new Bispo(Tabuleiro, p.Cor);
                            Tabuleiro.RetirarPeca(destino);
                            pecas.Remove(p);
                            Tabuleiro.ColocarPeca(B, destino);
                            pecas.Add(B);
                            break;

                        case "c":
                            Peca C = new Cavalo(Tabuleiro, p.Cor);
                            Tabuleiro.RetirarPeca(destino);
                            pecas.Remove(p);
                            Tabuleiro.ColocarPeca(C, destino);
                            pecas.Add(C);
                            break;

                        case "d":
                            Peca D = new Dama(Tabuleiro, p.Cor);
                            Tabuleiro.RetirarPeca(destino);
                            pecas.Remove(p);
                            Tabuleiro.ColocarPeca(D, destino);
                            pecas.Add(D);
                            break;
                    }
                }
            }

            if (EmXeque(GetCorAdversaria(JogadorAtual)))
            {
                JogadorEmXeque = true;
            }

            else
            {
                JogadorEmXeque = false;
            }

            if (XequeMate(GetCorAdversaria(JogadorAtual)))
            {
                Finalizada = true;
            }

            else
            {
                Turno++;
                MudarJogador();
            }

            //EN PASSANT
            if (p is Peao && (destino.Linha == origem.Linha + 2 || destino.Linha == origem.Linha - 2))
            {
                EnPassant = p;
            }
            else
            {
                EnPassant = null;
            }
        }

        public void ReverterJogada(Posicao origem, Posicao destino, Peca captura)
        {
            Peca p = Tabuleiro.RetirarPeca(destino);
            p.DecrementarMovimentos();
            if (captura != null)
            {
                Tabuleiro.ColocarPeca(captura, destino);
                pecasCapturadas.Remove(captura);
            }
            Tabuleiro.ColocarPeca(p, origem);

            //ROQUE PEQUENO
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.DecrementarMovimentos();
                Tabuleiro.ColocarPeca(T, origemT);
            }

            //ROQUE GRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(destinoT);
                T.DecrementarMovimentos();
                Tabuleiro.ColocarPeca(T, origemT);
            }

            //EN PASSANT
            if (p is Peao)
            {
                if (destino.Coluna != origem.Coluna && captura == EnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao pos;
                    if (p.Cor == Cor.Branco)
                    {
                        pos = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        pos = new Posicao(4, destino.Coluna);
                    }
                }
            }
        }

        public void ValidarOrigem(Posicao origem)
        {
            if (!Tabuleiro.ExistePeca(origem))
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (JogadorAtual != Tabuleiro.Peca(origem).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!Tabuleiro.Peca(origem).ExistemMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existem movimentos possíveis para a peça escolhida!");
            }
        }

        public void ValidarDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).DestinoValido(destino))
            {
                throw new TabuleiroException("Impossível mover para o destino escolhido!");
            }
        }

        private void MudarJogador()
        {
            if (JogadorAtual == Cor.Branco)
            {
                JogadorAtual = Cor.Preto;
            }
            else
            {
                JogadorAtual = Cor.Branco;
            }
        }

        private void NovaPeca(Peca peca, PosicaoXadrez posicao)
        {
            Tabuleiro.ColocarPeca(peca, posicao.ToPosicao());
            pecas.Add(peca);
        }

        private void IniciarPecas()
        {
            // TESTE XEQUEMATE
            //NovaPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('c', 1));
            //NovaPeca(new Rei(Tabuleiro, Cor.Branco), new PosicaoXadrez('d', 1));
            //NovaPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('h', 7));

            //NovaPeca(new Rei(Tabuleiro, Cor.Preto), new PosicaoXadrez('a', 8));
            //NovaPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('b', 8));

            NovaPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('a', 1));
            NovaPeca(new Cavalo(Tabuleiro, Cor.Branco), new PosicaoXadrez('b', 1));
            NovaPeca(new Bispo(Tabuleiro, Cor.Branco), new PosicaoXadrez('c', 1));
            NovaPeca(new Rei(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('e', 1));
            NovaPeca(new Dama(Tabuleiro, Cor.Branco), new PosicaoXadrez('d', 1));
            NovaPeca(new Bispo(Tabuleiro, Cor.Branco), new PosicaoXadrez('f', 1));
            NovaPeca(new Cavalo(Tabuleiro, Cor.Branco), new PosicaoXadrez('g', 1));
            NovaPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('h', 1));

            NovaPeca(new Peao(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('a', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('b', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('c', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('d', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('e', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('f', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('g', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco, this), new PosicaoXadrez('h', 2));

            NovaPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('a', 8));
            NovaPeca(new Cavalo(Tabuleiro, Cor.Preto), new PosicaoXadrez('b', 8));
            NovaPeca(new Bispo(Tabuleiro, Cor.Preto), new PosicaoXadrez('c', 8));
            NovaPeca(new Rei(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('e', 8));
            NovaPeca(new Dama(Tabuleiro, Cor.Preto), new PosicaoXadrez('d', 8));
            NovaPeca(new Bispo(Tabuleiro, Cor.Preto), new PosicaoXadrez('f', 8));
            NovaPeca(new Cavalo(Tabuleiro, Cor.Preto), new PosicaoXadrez('g', 8));
            NovaPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('h', 8));

            NovaPeca(new Peao(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('a', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('b', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('c', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('d', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('e', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('f', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('g', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto, this), new PosicaoXadrez('h', 7));
        }
    }
}

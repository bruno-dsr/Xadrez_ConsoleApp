using Model;
using Model.Enums;
using Model.ModelException;
using System.Collections.Generic;

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

        public Partida()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Finalizada = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            IniciarPecas();
        }

        public void MoverPeca(Posicao origem, Posicao destino)
        {
            Peca P = Tabuleiro.RetirarPeca(origem);
            P.IncrementarMovimentos();
            Peca Captura = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(P, destino);
            if (Captura != null)
            {
                pecasCapturadas.Add(Captura);
            }
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

        public void Jogada(Posicao origem, Posicao destino)
        {
            MoverPeca(origem, destino);
            Turno++;
            MudarJogador();
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
            NovaPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('a', 1));
            NovaPeca(new Cavalo(Tabuleiro, Cor.Branco), new PosicaoXadrez('b', 1));
            NovaPeca(new Bispo(Tabuleiro, Cor.Branco), new PosicaoXadrez('c', 1));
            NovaPeca(new Rei(Tabuleiro, Cor.Branco), new PosicaoXadrez('d', 1));
            NovaPeca(new Dama(Tabuleiro, Cor.Branco), new PosicaoXadrez('e', 1));
            NovaPeca(new Bispo(Tabuleiro, Cor.Branco), new PosicaoXadrez('f', 1));
            NovaPeca(new Cavalo(Tabuleiro, Cor.Branco), new PosicaoXadrez('g', 1));
            NovaPeca(new Torre(Tabuleiro, Cor.Branco), new PosicaoXadrez('h', 1));

            NovaPeca(new Peao(Tabuleiro, Cor.Branco), new PosicaoXadrez('a', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco), new PosicaoXadrez('b', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco), new PosicaoXadrez('c', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco), new PosicaoXadrez('d', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco), new PosicaoXadrez('e', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco), new PosicaoXadrez('f', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco), new PosicaoXadrez('g', 2));
            NovaPeca(new Peao(Tabuleiro, Cor.Branco), new PosicaoXadrez('h', 2));

            NovaPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('a', 8));
            NovaPeca(new Cavalo(Tabuleiro, Cor.Preto), new PosicaoXadrez('b', 8));
            NovaPeca(new Bispo(Tabuleiro, Cor.Preto), new PosicaoXadrez('c', 8));
            NovaPeca(new Rei(Tabuleiro, Cor.Preto), new PosicaoXadrez('d', 8));
            NovaPeca(new Dama(Tabuleiro, Cor.Preto), new PosicaoXadrez('e', 8));
            NovaPeca(new Bispo(Tabuleiro, Cor.Preto), new PosicaoXadrez('f', 8));
            NovaPeca(new Cavalo(Tabuleiro, Cor.Preto), new PosicaoXadrez('g', 8));
            NovaPeca(new Torre(Tabuleiro, Cor.Preto), new PosicaoXadrez('h', 8));

            NovaPeca(new Peao(Tabuleiro, Cor.Preto), new PosicaoXadrez('a', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto), new PosicaoXadrez('b', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto), new PosicaoXadrez('c', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto), new PosicaoXadrez('d', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto), new PosicaoXadrez('e', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto), new PosicaoXadrez('f', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto), new PosicaoXadrez('g', 7));
            NovaPeca(new Peao(Tabuleiro, Cor.Preto), new PosicaoXadrez('h', 7));
        }
    }
}

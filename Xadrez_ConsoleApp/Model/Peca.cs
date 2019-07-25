using Model.Enums;

namespace Model
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdeMovimentos = 0;
        }

        public void IncrementarMovimentos()
        {
            QtdeMovimentos++;
        }

        public void DecrementarMovimentos()
        {
            QtdeMovimentos--;
        }

        public virtual bool PodeMover(Posicao posicao)
        {
            if (!Tabuleiro.ExistePeca(posicao) || Tabuleiro.Peca(posicao).Cor != this.Cor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DestinoValido(Posicao posicao) {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();

        public bool ExistemMovimentosPossiveis()
        {
            bool[,] movimentos = MovimentosPossiveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if(movimentos[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

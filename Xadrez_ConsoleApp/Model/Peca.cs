using System;
using System.Collections.Generic;
using System.Text;
using Model;
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

        public virtual bool PodeMover(Posicao posicao)
        {
            if(!Tabuleiro.ExistePeca(posicao) || Tabuleiro.Peca(posicao).Cor != this.Cor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}

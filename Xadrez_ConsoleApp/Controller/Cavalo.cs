using Model;
using Model.Enums;

namespace Controller
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {

        }

        public override string ToString()
        {
            return "C ";
        }
    }
}

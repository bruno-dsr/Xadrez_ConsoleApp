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
    }
}

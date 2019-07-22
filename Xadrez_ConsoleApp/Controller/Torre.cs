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
    }
}

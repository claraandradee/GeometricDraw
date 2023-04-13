using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apGrafico
{
    public class Polilinha : Ponto //herdada da classe ponto
    {
        private Ponto pontoFinal; //lista de pontos

        public Polilinha(int x1, int y1, int x2, int y2, Color novaCor) :
                    base(x1, y1, novaCor)
        {
            pontoFinal = new Ponto(x2, y2, novaCor);
        }


        //metodo desenhar
        public override void desenhar(Color corDesenho, Graphics g)
        {
            Pen pen = new Pen(corDesenho);
            g.DrawLine(pen, base.X, base.Y, pontoFinal.X, pontoFinal.Y);
            
        }

        //ToString da classe Polilinha
        public String transformaString(int valor, int quantasPosicoes)
        {
            String cadeia = valor + "";
            while (cadeia.Length < quantasPosicoes)
                cadeia = " " + cadeia;

            return cadeia.Substring(0, quantasPosicoes); //corta no tamanho maximo se necessario
        }

        public String transformaString(String valor, int quantasPosicoes)
        {
            String cadeia = valor + "";
            while (cadeia.Length < quantasPosicoes)
                cadeia = cadeia + " ";

            return cadeia.Substring(0, quantasPosicoes);
        }

        public override string ToString()
        {
      return transformaString("o", 5) +
             transformaString(X, 5) +
             transformaString(Y, 5) +
             transformaString(Cor.R, 5) +
             transformaString(Cor.G, 5) +
             transformaString(Cor.B, 5) +
             transformaString(pontoFinal.X, 5) + 
             transformaString(pontoFinal.Y, 5);
        }

        string FormatoDeRegistro()
        {
            return X + " " + Y + " " + Cor;
        }
    }
}

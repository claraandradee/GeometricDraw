using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace apGrafico
{
    class Elipse : Ponto //herdada da classe ponto
    {
        int raio1, raio2; //utilizaremos dois raios diferentes

        //getters e setters dos raios
        public int Raio1 { get { return raio1; } set { raio1 = value; } }
        public int Raio2 { get { return raio2; } set { raio2 = value; } }

        //construtor
        public Elipse(int xCentro, int yCentro, int novoRaio1, int novoRaio2, Color novaCor) :
               base(xCentro, yCentro, novaCor)
        {
            raio1 = novoRaio1;
            raio2 = novoRaio2;
        }

        //setters dos novos raios
        public void setRaio1(int novoRaio1)
        {
            raio1 = novoRaio1;
        }

        public void setRaio2(int novoRaio2)
        {
            raio2 = novoRaio2;
        }

        //sobreposição do metodo desenhar
        public override void desenhar(Color corDesenho, Graphics g)
        {
            Pen pen = new Pen(corDesenho);
            g.DrawEllipse(pen, base.X - raio1, base.Y - raio2, 2 * raio1, 2 * raio2);
        }

        //ToString da classe Elipse
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
          return transformaString("e", 5) +
                 transformaString(X, 5) +
                 transformaString(Y, 5) +
                 transformaString(Cor.R, 5) +
                 transformaString(Cor.G, 5) +
                 transformaString(Cor.B, 5) +
                 transformaString(raio1, 5) +
                 transformaString(raio2, 5);
        }

        string FormatoDeRegistro()
        {
            return X + " " + Y + " " + Cor;
        }
    }
}

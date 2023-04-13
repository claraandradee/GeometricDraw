using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace apGrafico
{
    class Circulo : Ponto //herdada da classe ponto
    {
        private int raio; //vamos utilizar mais uma variável

    //get e set
    public int Raio { get => raio; set => raio = value; }

    public void setRaio1(int novoRaio1)
    {
      raio = novoRaio1;
    }

    //contrutor
    public Circulo(int xCentro, int yCentro, int novoRaio, Color novaCor) : 
               base(xCentro, yCentro, novaCor)
    {
       raio = novoRaio;
    }   

    //sobreposição da metodo desenhar
    public override void desenhar(Color corDesenho, Graphics g)
        {
          Pen pen = new Pen(corDesenho);
          g.DrawEllipse(pen, base.X - raio, base.Y - raio,
            2 * raio, 2 * raio);
        }

        //ToString da classe Circulo
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
            return transformaString("c", 5) +
                   transformaString(X, 5) +
                   transformaString(Y, 5) +
                   transformaString(Cor.R, 5) +
                   transformaString(Cor.G, 5) +
                   transformaString(Cor.B, 5) +
                   transformaString(raio, 5);
        }

        string FormatoDeRegistro()
        {
            return X + " " + Y + " " + Cor;
        }
    }
}

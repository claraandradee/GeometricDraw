using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; //declarações e classes necessárias para desenho gráfico
using System.Windows.Forms;

namespace apGrafico
{
    public class Ponto : IComparable<Ponto>, IRegistro, ICriterioDeSeparacao<Ponto>
    {
        private int x, y; //indicam as posições do plano cartesiano 
        private Color cor;

        //compareTo
        public int CompareTo(Ponto other)
        {
            int diferencaX = X - other.X;
            if (diferencaX == 0)
                return Y - other.Y;
            return diferencaX;
        }

        //contrutor
        public Ponto(int cX, int cY, Color qualCor)
        {
            x = cX;
            y = cY;
            cor = qualCor;

            //quando chamar esse objeto vamos usar:
            //Ponto p = new Ponto(3, 2, Color.red)
        }

        //getters e setters
        public int X { get { return x; } set { x = value; }  }
        public int Y { get { return y; } set { y = value; }  }

        public void setX(int X)
        {
          x = X;
        }

        public void setY(int Y)
        {
          y = Y;
        }

        public void setCor(Color novaCor) 
        { 
            cor = novaCor;  
        }

    public Color Cor { get { return cor; } set { cor = value; } }

        //metodo para desenhar
        public virtual void desenhar(Color cor, Graphics g)
        {
          Brush brush = new SolidBrush(cor);
          g.FillEllipse(brush, X, Y, 2, 2);
        }

        //ToString da classe Ponto
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
            return transformaString("p", 5)   +
                   transformaString(X, 5)     +
                   transformaString(Y, 5)     +
                   transformaString(Cor.R, 5) +
                   transformaString(Cor.G, 5) +
                   transformaString(Cor.B, 5);
        }

        string IRegistro.FormatoDeRegistro()
        {
            return X + " " + Y + " " + Cor;
        }

        bool ICriterioDeSeparacao<Ponto>.PodeSeparar()
        {
          int xPonto = x;
          if(xPonto == 0) 
            return true;

          return false;
        }
    }
}


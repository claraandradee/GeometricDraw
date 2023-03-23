using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace apGrafico
{
    class Elipse : Ponto
    {
        int raio1, raio2;

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
    }
}

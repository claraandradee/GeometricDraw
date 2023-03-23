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
        int raio; //vamos utilizar mais uma variável

        //get e set
        public int Raio { get { return raio; } set { raio = value; } }

        //contrutor
        public Circulo(int xCentro, int yCentro, int novoRaio, Color novaCor) : 
               base(xCentro, yCentro, novaCor)
        {
            raio = novoRaio;
        }

        //set no do novoRaio
        public void setRaio(int novoRaio)
        {
            raio = novoRaio;
        }

        //sobreposição da metodo desenhar
        public override void desenhar(Color corDesenho, Graphics g)
        {
            Pen pen = new Pen(corDesenho);
            g.DrawEllipse(pen, base.X - raio, base.Y - raio,  //centro - raio
                          2 * raio, 2 * raio);                //centro + raio 
        }

    }
}

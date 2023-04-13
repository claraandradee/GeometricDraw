using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apGrafico
{
  public class Retangulo : Ponto //herdada da classe ponto
  {
    int xponto, yponto, altura, largura; //precisaremos desses pontos para fazer 
                                         //a altura e a largura do retangulo

    //getters e setters
    public int XPonto { get { return xponto; } set { xponto = value; } }
    public int YPonto { get { return yponto; } set { yponto = value; } }

    public int Altura { get { return altura; } set { altura = value; } }
    public int Largura { get { return largura; } set { largura = value; } }

    //contrutor
    public Retangulo(int novoXPonto, int novoYPonto, int novaAltura, int novaLargura, Color novaCor) :
        base(novoXPonto, novoYPonto, novaCor)
    {
      xponto = novoXPonto;
      yponto = novoYPonto;
      altura = novaAltura;
      largura = novaLargura;
    }

    //metodo desenhar
    public override void desenhar(Color corDesenho, Graphics g)
    {
      Pen pen = new Pen(corDesenho);
      int alturaDesenho = 0, larguraDesenho = 0;

      //verificação
      if (largura < 0)
      {
        larguraDesenho = -largura;
        XPonto = base.X + largura;
      }
      else
      {
        XPonto = base.X;
        larguraDesenho = largura;
      }

      if (altura < 0)
      {
        alturaDesenho = -altura;
        YPonto = base.Y + altura;
      }
      else
      {
        YPonto = base.Y;
        alturaDesenho = altura;
      }

      Rectangle rect = new Rectangle(XPonto, YPonto, larguraDesenho, alturaDesenho);
      g.DrawRectangle(pen, rect);
    }

    //ToString da classe Retangulo
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
      return transformaString("r", 5) +
             transformaString(X, 5) +
             transformaString(Y, 5) +
             transformaString(Cor.R, 5) +
             transformaString(Cor.G, 5) +
             transformaString(Cor.B, 5) +
             transformaString(altura, 5) +
             transformaString(largura, 5);
    }

    string FormatoDeRegistro()
    {
      return X + " " + Y + " " + Cor;
    }
  }
}

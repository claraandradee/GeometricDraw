using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//PROJETO DESENVOLVIDO POR: 
//Clara Andrade Sant´anna Santos  RA: 22124
//Julia Enriquetto de Brito       RA: 22139

namespace apGrafico
{
  public partial class FrmGrafico : Form
  {
    //variaveis globais que iramos utilizar durante a execusão
    bool esperaPonto = false;

    bool esperaInicioReta = false;
    bool esperaFimReta = false;

    bool esperarInicioCirculo = false;
    bool esperarFimCirculo = false;

    bool esperarInicioElipse = false;
    bool esperarFimElipse = false;

    bool esperarInicioRetangulo = false;
    bool esperarFimRetangulo = false;

    bool esperarInicioPolilinha = false;
    bool esperarMeioPolilinha = false;
    bool esperarFimPolilinha = false;

    //lista simples que vai ser utilizada para guardar os dados
    private ListaSimples<Ponto> figuras = new ListaSimples<Ponto>();
    Color corAtual = Color.Red;
    private static Ponto p1 = new Ponto(0, 0, Color.Black);

    public FrmGrafico()
    {
      InitializeComponent();
    }

    private void pbAreaDesenho_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics; // acessa contexto gráfico
      figuras.Atual = figuras.Primeiro;
      while (figuras.Atual != null)
      {
          Ponto figuraAtual = figuras.Atual.Info;
          figuraAtual.desenhar(figuraAtual.Cor, g);
          figuras.Atual = figuras.Atual.Prox;
      }
    }

    private void btnAbrir_Click(object sender, EventArgs e)
    {
      if (dlgAbrir.ShowDialog() == DialogResult.OK)
      {
        try
        {
          StreamReader arqFiguras = new StreamReader(dlgAbrir.FileName);
          String linha = "";
          while ((linha = arqFiguras.ReadLine()) != null)
          {
            String tipo = linha.Substring(0, 5).Trim();
            int xBase = Convert.ToInt32(linha.Substring(5, 5).Trim());
            int yBase = Convert.ToInt32(linha.Substring(10, 5).Trim());
            int corR = Convert.ToInt32(linha.Substring(15, 5).Trim());
            int corG = Convert.ToInt32(linha.Substring(20, 5).Trim());
            int corB = Convert.ToInt32(linha.Substring(25, 5).Trim());
            Color cor = new Color();
            cor = Color.FromArgb(255, corR, corG, corB);
            switch (tipo[0])
            {
              case 'p': // figura é um ponto
                figuras.InserirAposFim(
                new NoLista<Ponto>(new Ponto(xBase, yBase, cor), null));
                break;
              case 'l': // figura é uma reta
                int xFinal = Convert.ToInt32(linha.Substring(30, 5).Trim());
                int yFinal = Convert.ToInt32(linha.Substring(35, 5).Trim());
                figuras.InserirAposFim(new NoLista<Ponto>(
                new Reta(xBase, yBase, xFinal, yFinal, cor), null));
                break;
              case 'c': // figura é um círculo
                int raio = Convert.ToInt32(linha.Substring(30, 5).Trim());
                figuras.InserirAposFim(
                new NoLista<Ponto>(new Circulo(xBase, yBase, raio, cor), null));
                break;
              case 'e' : //figura é uma elipse
                int raio1 = Convert.ToInt32(linha.Substring(30, 5).Trim());
                int raio2 = Convert.ToInt32(linha.Substring(35, 5).Trim());
                figuras.InserirAposFim(new NoLista<Ponto>(
                  new Elipse(xBase, yBase, raio1, raio2, cor), null));
                break;
              case 'r': //figura é um retangulo
                int altura = Convert.ToInt32(linha.Substring(30, 5).Trim());
                int largura = Convert.ToInt32(linha.Substring(35, 5).Trim());
                figuras.InserirAposFim(new NoLista<Ponto>(
                  new Retangulo(xBase, yBase, altura, largura, cor), null));
                break;
              case 'o': //figura é uma polilinha
                int xBase2 = Convert.ToInt32(linha.Substring(30, 5).Trim());
                int yBase2 = Convert.ToInt32(linha.Substring(35, 5).Trim());
                figuras.InserirAposFim(new NoLista<Ponto>(
                  new Polilinha(xBase, yBase, xBase2, yBase2, cor), null));
                break;
            }
          }

          arqFiguras.Close();
          this.Text = dlgAbrir.FileName;
          pbAreaDesenho.Invalidate();

        }
        catch (IOException)
        {
          Console.WriteLine("Erro de leitura no arquivo");
        }
      }
    }
     private void btnSalvar_Click(object sender, EventArgs e)
     {
        if(dlgSalvar.ShowDialog() == DialogResult.OK)
        {
            try
            {
                StreamWriter arquivoFig = new StreamWriter(dlgSalvar.FileName); //abrir um novo arquivo
                figuras.IniciarPercursoSequencial();                            //iniciar a gravação
                while(figuras.PodePercorrer())
                {
                    arquivoFig.Flush();                                         //limpa os buffers
                    arquivoFig.WriteLine(figuras.Atual.Info.ToString());        //gravar dados
                }
                arquivoFig.Close();                                             //fechar o arquivo
            }
            catch(IOException)
            {
                Console.WriteLine("Erro na hora de salvar o arquivo");
            }
        }
     }

    private void pbAreaDesenho_MouseMove(object sender, MouseEventArgs e)
    {
      stMensagem.Items[3].Text = e.X + "," + e.Y;  //mostrar as coordenadass
    }

    private void limparEsperas() //limpa tudo->deixa tudo false
    {
      esperaPonto = false;
      esperaInicioReta = false;
      esperaFimReta = false;
      esperarInicioCirculo = false;
      esperarFimCirculo = false;
      esperarInicioElipse = false;
      esperarFimElipse = false;
      esperarInicioRetangulo = false;
      esperarFimRetangulo = false;
      esperarInicioPolilinha = false;
      esperarMeioPolilinha = false;
      esperarFimPolilinha = false;
    }

    private void pbAreaDesenho_MouseClick(object sender, MouseEventArgs e)
    {
      //para desenhar o ponto
      if (esperaPonto)
      {
        Ponto novoPonto = new Ponto(e.X, e.Y, corAtual);
        figuras.InserirAposFim(new NoLista<Ponto>(novoPonto, null));
        novoPonto.desenhar(novoPonto.Cor, pbAreaDesenho.CreateGraphics());
        esperaPonto = true;
        stMensagem.Items[1].Text = "";
      }
      //para desenhar o circulo
      else if (esperarInicioCirculo)
      {
        p1.setCor(corAtual);
        p1.setX(e.X);
        p1.setY(e.Y);
        esperarInicioCirculo = false;
        esperarFimCirculo = true;
        stMensagem.Items[1].Text = "Clique no proximo ponto do raio do circulo";
      }
      else if (esperarFimCirculo)
      {
        esperarInicioCirculo = false;
        esperarFimCirculo = false;
        Circulo novoCirculo = new Circulo(p1.X, p1.Y, p1.X - e.X, corAtual);
        novoCirculo.desenhar(novoCirculo.Cor, pbAreaDesenho.CreateGraphics());
        stMensagem.Items[1].Text = "";
        figuras.InserirAposFim(novoCirculo);
      }
      //para desenhar a elipse
      else if (esperarInicioElipse)
      {
        p1.setCor(corAtual);
        p1.setX(e.X);
        p1.setY(e.Y);
        esperarInicioElipse = false;
        esperarFimElipse = true;
        stMensagem.Items[1].Text = "Clique no proximo da elipse";
      }
      else if(esperarFimElipse)
      {
        esperarInicioElipse = false;
        esperarFimElipse = false;
        Elipse novaElipse = new Elipse(p1.X, p1.Y, p1.X - e.X, p1.Y - e.Y, corAtual);
        novaElipse.desenhar(novaElipse.Cor, pbAreaDesenho.CreateGraphics());
        stMensagem.Items[1].Text = "";
        figuras.InserirAposFim(novaElipse);
      }
      //para desemhar o retagulo
      else if(esperarInicioRetangulo)
      {
        p1.setCor(corAtual);
        p1.setX(e.X);
        p1.setY(e.Y);
        esperarInicioRetangulo = false;
        esperarFimRetangulo = true;
        stMensagem.Items[1].Text = "Clique no proximo do retangulo";
      }
      else if(esperarFimRetangulo)
      {
        esperarInicioRetangulo = false;
        esperarFimRetangulo = false;
        Retangulo novoRetangulo = new Retangulo(
          p1.X,
          p1.Y,
          e.Y - p1.Y,
          e.X - p1.X,
          corAtual
        );
        novoRetangulo.desenhar(novoRetangulo.Cor, pbAreaDesenho.CreateGraphics());
        stMensagem.Items[1].Text = "";
        figuras.InserirAposFim(novoRetangulo);
      }
      //para desenhar a polilinha
      else if(esperarInicioPolilinha)
      {
                if(!esperarMeioPolilinha)
                {
                    p1.setCor(corAtual);
                    p1.setY(e.Y);
                    p1.setX(e.X);
                }
                esperarInicioPolilinha= false;
                esperarFimPolilinha = true;
                stMensagem.Items[1].Text = "Clique no proximo ponto da polilinha"; 
      }
      else if(esperarFimPolilinha)
            {
                esperarFimPolilinha = true;
                esperarMeioPolilinha = true;
                Polilinha novaPolilinha = new Polilinha(p1.X, p1.Y, e.X, e.Y, corAtual);
                novaPolilinha.desenhar(novaPolilinha.Cor, pbAreaDesenho.CreateGraphics());
                p1.setX(e.X);
                p1.setY(e.Y);
                stMensagem.Items[1].Text = "Clique no proximo ponto da polilinha";
                figuras.InserirAposFim(novaPolilinha);
            }
    }

    //ação dos botões de desenhar
    private void btnPonto_Click(object sender, EventArgs e)
    {
      stMensagem.Items[1].Text = "clique no local do ponto:";
      limparEsperas();
      esperaPonto = true;
    }

    private void btnReta_Click(object sender, EventArgs e)
    {
      stMensagem.Items[1].Text = "Clique no local do ponto inicial da reta:";
      limparEsperas();
      esperaInicioReta = true;
    }

    private void btnCirculo_Click(object sender, EventArgs e)
    {
      stMensagem.Items[1].Text = "Clique no local do ponto inicial do circulo:";
      limparEsperas();
      esperarInicioCirculo = true;
    }

    private void btnElipse_Click(object sender, EventArgs e)
    {
      stMensagem.Items[1].Text = "Clique no local do ponto inicial da elipse:";
      limparEsperas();
      esperarInicioElipse = true;
    }

    private void btnRetangulo_Click(object sender, EventArgs e)
    {
      stMensagem.Items[1].Text = "Clique no local do ponto inicial do retangulo: ";
      limparEsperas();
      esperarInicioRetangulo = true;
    }

    private void btnPolilinha_Click(object sender, EventArgs e)
    {
      stMensagem.Items[1].Text = "Clique no local do ponto inicial da polilinha:";
      limparEsperas();
      esperarInicioPolilinha = true;
    }

    private void FrmGrafico_MouseClick(object sender, MouseEventArgs e)
    {
   
    }

    private void btnSair_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void pbAreaDesenho_Click(object sender, EventArgs e)
    {
      MouseEventArgs me = (MouseEventArgs)e;
      int X = me.X;
      int Y = me.Y;
      if (esperaPonto)
      {
        Ponto pontoInicial = new Ponto(X, Y, corAtual);
        figuras.InserirAposFim(pontoInicial);
        esperaPonto = false;
        pbAreaDesenho.Refresh();
      }
      else
            if (esperaInicioReta)
      {
        p1.Cor = corAtual;
        p1.X = X;
        p1.Y = Y;
        esperaInicioReta = false;
        esperaFimReta = true;
      }
      else if (esperaFimReta)
      {
        esperaInicioReta = false;
        esperaFimReta = false;
        Reta novaLinha = new Reta(p1.X, p1.Y, X, Y, corAtual);
        figuras.InserirAposFim(new NoLista<Ponto>(novaLinha, null));
        novaLinha.desenhar(novaLinha.Cor, pbAreaDesenho.CreateGraphics());
        stMensagem.Items[1].Text = "Mensagem: clique o ponto final da reta";
      }
    }

    //usuario poder escolher a cor
    private void btnCor_Click(object sender, EventArgs e)
    {
      ColorDialog cor = new ColorDialog();
      if(cor.ShowDialog() == DialogResult.OK) 
      {
        corAtual = cor.Color;
      }
    }

        private void pbAreaDesenho_DoubleClick(object sender, EventArgs e)
        {
            if(esperarMeioPolilinha)
            {
                limparEsperas();
                stMensagem.Items[1].Text = "";
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

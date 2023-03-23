using System;

namespace apGrafico
{
    public class NoLista<Ponto> where Ponto : IComparable<Ponto>
    {
        Ponto info;
        NoLista<Ponto> prox;

        public NoLista(Ponto novaInfo, NoLista<Ponto> proximo)
        {
            Info = novaInfo;
            Prox = proximo;
        }

        public NoLista(Ponto novaInfo)
        {
            Info = novaInfo;
            Prox = null;
        }
        public Ponto Info
        {
            get => info;
            set
            {
                if (value != null)
                    info = value;
            }
        }

        public NoLista<Ponto> Prox
        {
            get => prox;
            set => prox = value;
        }
    }

}

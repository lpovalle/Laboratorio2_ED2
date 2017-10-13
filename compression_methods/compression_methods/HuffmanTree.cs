using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compression_methods
{
    class HuffmanTree
    {

        public string Dato;
        public int Frecuencia;

        public HuffmanTree Padre;
        public HuffmanTree Izquierda;
        public HuffmanTree Derecha;

        public HuffmanTree(string dato, int frecuencia)
        {
            Dato = dato;
            Frecuencia = frecuencia;
            Izquierda = null;
            Derecha = null;
        }

        public HuffmanTree Emparejar(HuffmanTree arbolDerecho)
        {
            HuffmanTree arbolPadre = new HuffmanTree("*", Frecuencia + arbolDerecho.Frecuencia);
            arbolPadre.Izquierda = this;
            arbolPadre.Derecha = arbolDerecho;
            this.Padre = arbolPadre;
            arbolDerecho.Padre = arbolPadre;

            return arbolPadre;
        }
        public bool EsHoja()
        {
            if (Derecha == null && Izquierda == null)
            {
                return true;
            }
            else return false;
        }


    }
    public static class ComprimirHuffman
    {
        public static Dictionary<string, string> Diccionario;

        static HuffmanTree Raiz;

        public static int length { get; set; }

        //comprimir
        public static string Comprimir(string datoComprimir)
        {
            Diccionario = new Dictionary<string, string>();
            GenerarArbol(datoComprimir);
            ObtenerClave();
            return MensajeEncriptado(datoComprimir);
        }

        static string MensajeEncriptado(string texto)
        {
            char[] caracteres = texto.ToCharArray();
            string mensaje = "";
            foreach (var item in caracteres)
            {
                mensaje += Diccionario[item.ToString()];
            }

            return mensaje;
        }

        
        static void GenerarArbol(string texto)
        {
            char[] Caracteres = texto.ToCharArray();
            Dictionary<string, int> Frecuencias = new Dictionary<string, int>();

            foreach (char caracter in Caracteres)
            {
                if (Frecuencias.ContainsKey(caracter.ToString()))
                {
                    Frecuencias[caracter.ToString()]++;
                }
                else
                {
                    Frecuencias[caracter.ToString()] = 1;
                }
            }

            List<HuffmanTree> Lista = new List<HuffmanTree>();
            foreach (KeyValuePair<string, int> item in Frecuencias)
            {
                Lista.Add(new HuffmanTree(item.Key, item.Value));
            }
            Lista = Lista.OrderBy(e => e.Frecuencia).ToList();
            while (Lista.Count > 1)
            {
                Lista.Add(Lista[0].Emparejar(Lista[1]));
                Lista.RemoveRange(0, 2);
                Lista = Lista.OrderBy(e => e.Frecuencia).ToList();
            }

            Raiz = Lista[0];
        }

        //descomprimir
        static Dictionary<string, string> ObtenerClave()
        {
            Dictionary<string, List<byte>> Clave = new Dictionary<string, List<byte>>();
            if (Raiz.EsHoja())
            {
                Diccionario.Add(Raiz.Dato, "1");
            }
            RecorrerArbol(Raiz, "");
            return Diccionario;

        }

        private static void RecorrerArbol(HuffmanTree arbolito, string recorrido)
        {
            if (arbolito.EsHoja())
            {
                Diccionario.Add(arbolito.Dato, recorrido);
                return;
            }
            else
            {
                if (!(arbolito.Izquierda == null))
                {
                    string irIzquierda = recorrido + "0";
                    RecorrerArbol(arbolito.Izquierda, irIzquierda);
                }
                if (!(arbolito.Derecha == null))
                {
                    string irDerecha = recorrido + "1";
                    RecorrerArbol(arbolito.Derecha, irDerecha);
                }
            }
        }

        static string ObtenerMensaje(Dictionary<string, string> clave, string codigo)
        {
            Dictionary<string, string> Clave = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in clave)
            {
                Clave.Add(item.Value, item.Key);
            }

            List<string> mensaje = new List<string>();
            int inicio = 0;
            int fin = 1;
            for (int i = 0; i < codigo.Length; i++)
            {
                if (Clave.ContainsKey(codigo.Substring(inicio, fin)))
                {
                    mensaje.Add(Clave[codigo.Substring(inicio, fin)]);
                    inicio = inicio + fin;
                    fin = 1;
                }
                else
                {
                    fin++;
                }
            }
            return String.Join("", mensaje.ToArray());

        }

        public static string descomprimir(Dictionary<string, string> clave, string codigo)
        {
            return ObtenerMensaje(clave, codigo);
        }

    }
}

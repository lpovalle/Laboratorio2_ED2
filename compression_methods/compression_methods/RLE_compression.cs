using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compression_methods
{
    public class RLE_compression
    {
        int contador = 0;
        int tamaño = 0;
        char[] cadena = null;
        string resultado = "";
        string dcadena = "";


        public RLE_compression()
        {

        }

        public string Encode(int lenght, char[] caracteres)
        {
            int i = 0;
            cadena = caracteres;
            tamaño = lenght;

            contador++;

            while (i < tamaño)
            {

                if (i == tamaño - 1)
                {
                    resultado += contador.ToString() + cadena[i];
                    contador = 0;
                }
                else if (cadena[i] == cadena[i + 1])
                {
                    contador++;
                }
                else
                {
                    resultado += contador.ToString() + cadena[i];
                    contador = 1;
                }

                i++;
            }


                return resultado;
        }

        public string Decode(string cadena)
        {
            resultado = null;
            dcadena = cadena;
            int radix = 0;

            for (int i = 0; i < dcadena.Length; i++)
            {
                if (Char.IsNumber(dcadena[i]))
                {
                    radix++;
                }
                else
                {
                    if (radix > 0)
                    {
                        int value_repeat = Convert.ToInt32(dcadena.Substring(i - radix, radix));

                        for (int j = 0; j < value_repeat; j++)
                        {
                            resultado += dcadena[i];
                        }

                        radix = 0;
                    }
                    else if (radix == 0)
                    {
                        resultado += dcadena[i];
                    }
                }
            }

            if (resultado == null || !TieneChar(ref resultado))
            {
                throw new Exception("\r\n No se puede decodificar, error en la cadena. No existen letras, unicamente numeros\r\n");
            }
            return resultado;
        }

        private bool TieneChar(ref string entrada)
        {
            bool estado = false;

            for (int i = 0; i < entrada.Length; i++)
            {
                if (Char.IsLetter(entrada[i]))
                {
                    estado = true;
                    break;
                }
            }

            return estado;
        }
    }
}

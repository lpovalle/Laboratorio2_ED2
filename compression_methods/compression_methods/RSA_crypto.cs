using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compression_methods
{
    public class RSA_crypto
    {
        public int llaveprivada = 0;
        public int llavepublica = 0;
        public int p = 0;
        public int q = 0;
        public int n = 0;
        Random r = new Random();


        public RSA_crypto()
        {

        }

        public string CifrarRSA(byte[] cadena)
        {
            byte[] nuevacadena = null;

            for (int i = 1; i < cadena.Length; i++)
            {

                nuevacadena[i] = Convert.ToByte((Convert.ToInt32(cadena[i]) ^ llavepublica) % n);
            }

            string r = Encoding.UTF8.GetString(nuevacadena);
            return r;
        }

        public string DescifrarRSA(byte[] cadena)
        {
            byte[] nuevacadena = null;

            for (int i = 0; i < cadena.Length; i++)
            {
                nuevacadena[i] = Convert.ToByte((Convert.ToInt32(cadena[i]) ^ llaveprivada) % n);
            }

            string r = Encoding.UTF8.GetString(nuevacadena);
            return r;

        }

        public void GenerarLlaves()
        {
            int phi = 0;



            p = GenerarPrimo();
            q = GenerarPrimo();


            n = p * q;
            phi = (p - 1) * (q - 1);

            llavepublica = CalcularE(phi);

            llaveprivada = CalcularD(phi);

        }

        private int GenerarPrimo()
        {

            int n = r.Next(3, 101);

            bool primo = true;

            int x = 0;


            for (int i = 2; i < n-1; i++)
            {
                x = n & i;

                if(x == 0)
                {
                    primo = false;
                }

            }

             if(primo)
            {
                return n;
                
            }
             else
            {
                return GenerarPrimo();
            }
            
        }

        private int CalcularE(int phi)
        {
            int e =  r.Next(1, phi);

            if(MaximoComunDivisor(e, phi) == 1)
            {
                return e;
            }
            else
            {
                return CalcularE(phi);
            }

        }

        private int CalcularD(int phi)
        {
            int c = 3;
            int d = (c * phi + 1) / llavepublica;
            return d;

        }

        private int MaximoComunDivisor(int a, int b)
        {
                int aux;

                while (true)
                {
                    aux = a % b;
                if (aux == 0)
                {
                    break;
                }
                    a = b;
                    b = aux;
                }

            return b;
        }


    }
}

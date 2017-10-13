using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab2_cifrado
{
    class Program
    {
        static void Main(string[] args)
        {
             
            Console.WriteLine("Inicia el programa: \nEl comando -c se utiliza para cifrar un archivo y -d para descifrarlo. \ne.g: -c -f (entre comillas)ruta del archivo");

            string LeerLinea = Console.ReadLine();


            operacion(LeerLinea);




            Console.ReadKey();



        }

        static void operacion(string linea)
        {
            try
            {
                string cmd = linea.Substring(0, 2);
                string[] lineaseparada = linea.Split('"');
                compression_methods.RSA_crypto rsa = new compression_methods.RSA_crypto();

                string path = lineaseparada[1];

                if(cmd == "-c")
                {

                    BinaryReader br = new BinaryReader(File.OpenRead(path));
                    StreamReader sr = new StreamReader(File.OpenRead(path));

                    string archivocompleto = sr.ReadToEnd();
                    byte[] charsarchivo = br.ReadBytes(archivocompleto.Length);

                    

                    rsa.GenerarLlaves();
                    int llavepu = rsa.llavepublica;
                    int llavepr = rsa.llaveprivada;

                    Console.WriteLine("Se generaron las siguientes claves: \nPublica: " + Convert.ToString(llavepu) + "\nPrivada (Por fines prácticos): " + Convert.ToString(llavepr));

                    string cifrado = rsa.CifrarRSA(charsarchivo);

                    Console.WriteLine(cifrado);

                    StreamWriter sw = new StreamWriter(path + ".cif");
                    sw.WriteLine(cifrado);

                    sw.Close();




                }
                else if(cmd == "-d")
                {

                    Console.WriteLine("Ingrese la llave publica.");
                    rsa.llavepublica = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Ingrese la llave privada.");
                    rsa.llaveprivada = Convert.ToInt32(Console.ReadLine());


                    BinaryReader br = new BinaryReader(File.OpenRead(path));
                    StreamReader sr = new StreamReader(File.OpenRead(path));

                    string archivocompleto = sr.ReadToEnd();
                    byte[] charsarchivo = br.ReadBytes(archivocompleto.Length);

                    string descifrado = rsa.DescifrarRSA(charsarchivo);

                    Console.WriteLine(descifrado);

                    StreamWriter sw = new StreamWriter(path +".des");
                    sw.WriteLine(descifrado);

                    sw.Close();


                }
                else
                {
                    Console.WriteLine("Ingrese un comando válido");
                    operacion(Console.ReadLine());
                }





            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                Console.WriteLine("Escriba el comando de nuevo: ");
                operacion(Console.ReadLine());

            }





        }
    }
}

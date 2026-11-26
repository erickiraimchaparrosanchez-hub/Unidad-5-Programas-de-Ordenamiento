using System;

namespace Programa6_Organizacion_de_numeros_de_matricula
{
    class Program
    {
        const int MINIMO = 15;

        static void Main(string[] args)
        {
            Console.Title = "Organización de matrículas - ShellSort";
            string repetir;

            do
            {
                Console.Clear();
                Console.WriteLine("===== ORGANIZACIÓN DE NÚMEROS DE MATRÍCULA =====");

                int n = PedirCantidad();
                int[] matriculas = new int[n];
                CapturarMatriculas(matriculas);

                Console.WriteLine("\nMatrículas en orden de captura:");
                MostrarMatriculas(matriculas);

                ShellSort(matriculas);

                Console.WriteLine("\nMatrículas ORDENADAS (ascendente):");
                MostrarMatriculas(matriculas);

                Console.Write("\n¿Desea ordenar otra secuencia de datos? (S/N): ");
                repetir = Console.ReadLine().Trim().ToUpper();
            } while (repetir == "S");

            Console.WriteLine("Fin del programa.");
        }

        static int PedirCantidad()
        {
            int n;
            do
            {
                Console.Write($"¿Cuántas matrículas capturará? (mínimo {MINIMO}): ");
                if (!int.TryParse(Console.ReadLine(), out n) || n < MINIMO)
                {
                    Console.WriteLine($"Ingrese un entero mayor o igual a {MINIMO}.");
                    n = 0;
                }
            } while (n < MINIMO);
            return n;
        }

        static void CapturarMatriculas(int[] arreglo)
        {
            for (int i = 0; i < arreglo.Length; i++)
            {
                int valor;
                Console.Write($"Matrícula {i + 1}: ");
                while (!int.TryParse(Console.ReadLine(), out valor) || valor <= 0)
                {
                    Console.Write("Matrícula no válida, ingrese un entero positivo: ");
                }
                arreglo[i] = valor;
            }
        }
        static void ShellSort(int[] arreglo)
        {
            int n = arreglo.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = arreglo[i];
                    int j = i;

                    while (j >= gap && arreglo[j - gap] > temp)
                    {
                        arreglo[j] = arreglo[j - gap];
                        j -= gap;
                    }

                    arreglo[j] = temp;
                }
            }
        }

        static void MostrarMatriculas(int[] arreglo)
        {
            for (int i = 0; i < arreglo.Length; i++)
            {
                Console.WriteLine($"{i + 1,3}. {arreglo[i]}");
            }
        }
    }
}

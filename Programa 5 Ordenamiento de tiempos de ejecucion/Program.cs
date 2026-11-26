using System;
using System.Globalization;

namespace Programa5_Ordenamiento_de_tiempos_de_ejecucion
{
    class Program
    {
        static long comparaciones = 0;
        static long movimientos = 0;

        static void Main(string[] args)
        {
            Console.Title = "Tiempos de ejecución - ShellSort";
            Console.WriteLine("===== ORDENAMIENTO DE TIEMPOS DE EJECUCIÓN =====");

            int n;
            do
            {
                Console.Write("¿Cuántas mediciones? (mínimo 2): ");
                if (!int.TryParse(Console.ReadLine(), out n) || n < 2)
                {
                    Console.WriteLine("Ingrese un entero mayor o igual a 2.");
                    n = 0;
                }
            } while (n < 2);

            double[] tiempos = new double[n];

            Console.WriteLine("\n1. Capturar los tiempos manualmente");
            Console.WriteLine("2. Generar los tiempos aleatoriamente");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine();

            if (opcion == "1")
                CapturarTiempos(tiempos);
            else
                GenerarTiempos(tiempos);

            Console.WriteLine("\nLista ORIGINAL (ms):");
            MostrarLista(tiempos);

            comparaciones = 0;
            movimientos = 0;

            ShellSort(tiempos);

            Console.WriteLine("\nLista ORDENADA (ms, de menor a mayor):");
            MostrarLista(tiempos);

            Console.WriteLine("\n--- Estadísticas ---");
            Console.WriteLine($"Comparaciones: {comparaciones}");
            Console.WriteLine($"Movimientos:   {movimientos}");
            Console.WriteLine($"Tiempo más rápido: {tiempos[0]:0.00} ms");
            Console.WriteLine($"Tiempo más lento:  {tiempos[n - 1]:0.00} ms");

            Console.WriteLine("\nPresione una tecla para salir...");
            Console.ReadKey();
        }

        static void CapturarTiempos(double[] tiempos)
        {
            for (int i = 0; i < tiempos.Length; i++)
            {
                double valor;
                while (true)
                {
                    Console.Write($"Tiempo {i + 1} (ms): ");
                    string texto = Console.ReadLine().Replace(',', '.');
                    if (double.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out valor) && valor >= 0)
                        break;
                    Console.WriteLine("Valor no válido (número mayor o igual a 0).");
                }
                tiempos[i] = valor;
            }
        }

        static void GenerarTiempos(double[] tiempos)
        {
            Random rnd = new Random();
            for (int i = 0; i < tiempos.Length; i++)
            {
                tiempos[i] = Math.Round(1 + rnd.NextDouble() * 499, 2);
            }
        }

        static void ShellSort(double[] arreglo)
        {
            int n = arreglo.Length;

            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                Console.WriteLine($"\nPasada con gap = {gap}");

                for (int i = gap; i < n; i++)
                {
                    double temp = arreglo[i];
                    int j = i;

                    while (j >= gap)
                    {
                        comparaciones++; 
                        if (arreglo[j - gap] > temp)
                        {
                            arreglo[j] = arreglo[j - gap];
                            movimientos++; 
                            j -= gap;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (j != i)
                    {
                        arreglo[j] = temp;
                        movimientos++;
                    }
                }

                Console.Write("  Resultado: ");
                MostrarLista(arreglo);
            }
        }

        static void MostrarLista(double[] arreglo)
        {
            Console.WriteLine("[ " + string.Join(", ", Array.ConvertAll(arreglo, t => t.ToString("0.00", CultureInfo.InvariantCulture))) + " ]");
        }
    }
}

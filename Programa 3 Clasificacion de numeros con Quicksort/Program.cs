using System;

namespace Programa_3_Clasificacion_de_numeros_con_Quicksort
{
    class Program
    {
        const int TOTAL = 20;

        static int comparaciones = 0;
        static int llamadasRecursivas = 0;

        static void Main(string[] args)
        {
            Console.Title = "Clasificación de números con Quicksort";
            int[] numeros = new int[TOTAL];

            Console.WriteLine("===== QUICKSORT =====");
            Console.WriteLine("1. Capturar los 20 números manualmente");
            Console.WriteLine("2. Generar los 20 números aleatoriamente");
            Console.Write("Opción: ");

            string opcion = Console.ReadLine();
            if (opcion == "1")
                CapturarNumeros(numeros);
            else
                GenerarNumeros(numeros);

            Console.WriteLine("\nDatos ANTES del ordenamiento:");
            MostrarArreglo(numeros);

            comparaciones = 0;
            llamadasRecursivas = 0;

            Quicksort(numeros, 0, numeros.Length - 1);

            Console.WriteLine("\nDatos DESPUÉS del ordenamiento:");
            MostrarArreglo(numeros);

            Console.WriteLine("\n--- Estadísticas ---");
            Console.WriteLine($"Comparaciones realizadas: {comparaciones}");
            Console.WriteLine($"Llamadas recursivas:      {llamadasRecursivas}");

            Console.WriteLine("\nPresione una tecla para salir...");
            Console.ReadKey();
        }

        static void CapturarNumeros(int[] arreglo)
        {
            for (int i = 0; i < arreglo.Length; i++)
            {
                int valor;
                Console.Write($"Número {i + 1}: ");
                while (!int.TryParse(Console.ReadLine(), out valor))
                {
                    Console.Write("Entrada no válida, ingrese un entero: ");
                }
                arreglo[i] = valor;
            }
        }

        static void GenerarNumeros(int[] arreglo)
        {
            Random rnd = new Random();
            for (int i = 0; i < arreglo.Length; i++)
            {
                arreglo[i] = rnd.Next(1, 101); 
            }
        }

        static void Quicksort(int[] arreglo, int inicio, int fin)
        {
            llamadasRecursivas++;

            if (inicio < fin)
            {
                int posPivote = Particion(arreglo, inicio, fin);

                Quicksort(arreglo, inicio, posPivote - 1);
                Quicksort(arreglo, posPivote + 1, fin);
            }
        }

        static int Particion(int[] arreglo, int inicio, int fin)
        {
            int pivote = arreglo[fin];
            int i = inicio - 1;

            for (int j = inicio; j < fin; j++)
            {
                comparaciones++; 
                if (arreglo[j] <= pivote)
                {
                    i++;
                    Intercambiar(arreglo, i, j);
                }
            }

            Intercambiar(arreglo, i + 1, fin);
            return i + 1;
        }

        static void Intercambiar(int[] arreglo, int a, int b)
        {
            int aux = arreglo[a];
            arreglo[a] = arreglo[b];
            arreglo[b] = aux;
        }

        static void MostrarArreglo(int[] arreglo)
        {
            Console.WriteLine("[ " + string.Join(", ", arreglo) + " ]");
        }
    }
}

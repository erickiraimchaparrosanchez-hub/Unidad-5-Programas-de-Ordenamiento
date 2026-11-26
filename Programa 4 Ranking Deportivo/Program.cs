using System;
using System.Globalization;

namespace Programa4_RankingDeportivo
{
    
    class Participante
    {
        public string Nombre;
        public double Puntuacion;

        public Participante(string nombre, double puntuacion)
        {
            Nombre = nombre;
            Puntuacion = puntuacion;
        }
    }

    class Program
    {
        static int comparaciones = 0;
        static int intercambios = 0;

        static void Main(string[] args)
        {
            Console.Title = "Ranking deportivo - Quicksort";
            Console.WriteLine("===== RANKING DEPORTIVO =====");

            int n;
            do
            {
                Console.Write("¿Cuántos participantes? (mínimo 2): ");
                if (!int.TryParse(Console.ReadLine(), out n) || n < 2)
                {
                    Console.WriteLine("Ingrese un entero mayor o igual a 2.");
                    n = 0;
                }
            } while (n < 2);

            Participante[] participantes = new Participante[n];
            CapturarParticipantes(participantes);

            Console.WriteLine("\n--- Participantes en orden de captura ---");
            MostrarLista(participantes);

            comparaciones = 0;
            intercambios = 0;

            Quicksort(participantes, 0, participantes.Length - 1);

            Console.WriteLine("\n===== RANKING FINAL (mayor a menor) =====");
            MostrarRanking(participantes);

            Console.WriteLine("\n--- Estadísticas ---");
            Console.WriteLine($"Comparaciones: {comparaciones}");
            Console.WriteLine($"Intercambios:  {intercambios}");

            Console.WriteLine("\nPresione una tecla para salir...");
            Console.ReadKey();
        }

        static void CapturarParticipantes(Participante[] lista)
        {
            for (int i = 0; i < lista.Length; i++)
            {
                Console.WriteLine($"\nParticipante {i + 1}:");

                string nombre;
                do
                {
                    Console.Write("  Nombre: ");
                    nombre = Console.ReadLine().Trim();
                } while (nombre == "");

                double puntos;
                while (true)
                {
                    Console.Write("  Puntuación: ");
                    string texto = Console.ReadLine().Replace(',', '.');
                    if (double.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out puntos) && puntos >= 0)
                        break;
                    Console.WriteLine("  Puntuación no válida (número mayor o igual a 0).");
                }

                lista[i] = new Participante(nombre, puntos);
            }
        }

        static void Quicksort(Participante[] lista, int inicio, int fin)
        {
            if (inicio < fin)
            {
                int posPivote = Particion(lista, inicio, fin);
                Quicksort(lista, inicio, posPivote - 1);
                Quicksort(lista, posPivote + 1, fin);
            }
        }

        static int Particion(Participante[] lista, int inicio, int fin)
        {
            double pivote = lista[fin].Puntuacion;
            int i = inicio - 1;

            for (int j = inicio; j < fin; j++)
            {
                comparaciones++;
                if (lista[j].Puntuacion >= pivote)
                {
                    i++;
                    Intercambiar(lista, i, j);
                }
            }

            Intercambiar(lista, i + 1, fin);
            return i + 1;
        }

        static void Intercambiar(Participante[] lista, int a, int b)
        {
            
            if (a != b)
            {
                Participante aux = lista[a];
                lista[a] = lista[b];
                lista[b] = aux;
                intercambios++;
            }
        }

        static void MostrarLista(Participante[] lista)
        {
            Console.WriteLine($"{"Participante",-25}{"Puntos",10}");
            Console.WriteLine(new string('-', 35));
            foreach (Participante p in lista)
            {
                Console.WriteLine($"{p.Nombre,-25}{p.Puntuacion,10:0.##}");
            }
        }
        static void MostrarRanking(Participante[] lista)
        {
            Console.WriteLine($"{"Pos.",-6}{"Participante",-25}{"Puntos",10}");
            Console.WriteLine(new string('-', 41));

            int posicion = 1;
            for (int i = 0; i < lista.Length; i++)
            {
                if (i > 0 && lista[i].Puntuacion != lista[i - 1].Puntuacion)
                    posicion = i + 1;

                Console.WriteLine($"{posicion,-6}{lista[i].Nombre,-25}{lista[i].Puntuacion,10:0.##}");
            }
        }
    }
}

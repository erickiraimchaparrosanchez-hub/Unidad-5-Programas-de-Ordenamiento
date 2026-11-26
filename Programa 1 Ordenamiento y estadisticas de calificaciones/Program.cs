using System;

namespace Programa1_Ordenamiento_y_estadisticas_de_calificaciones
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Ordenamiento y estadísticas de calificaciones ===\n");

            int n = LeerEntero("¿Cuántos estudiantes? ", 1, 1000);
            string[] nombres = new string[n];
            double[] calificaciones = new double[n];

            Console.WriteLine("\n1. Capturar nombres y calificaciones");
            Console.WriteLine("2. Generar calificaciones aleatorias");
            int modo = LeerEntero("Elige una opción: ", 1, 2);

            if (modo == 1)
                Capturar(nombres, calificaciones);
            else
                Generar(nombres, calificaciones);

            Console.WriteLine("\n1. Ascendente (menor a mayor)");
            Console.WriteLine("2. Descendente (mayor a menor)");
            bool ascendente = LeerEntero("Elige el orden: ", 1, 2) == 1;

            string[] nombresOrd = (string[])nombres.Clone();
            double[] calOrd = (double[])calificaciones.Clone();

            int comparaciones, intercambios;
            Burbuja(nombresOrd, calOrd, ascendente, out comparaciones, out intercambios);

            Console.WriteLine("\n--- LISTA ORIGINAL ---");
            Mostrar(nombres, calificaciones);

            Console.WriteLine("\n--- LISTA ORDENADA (" + (ascendente ? "ascendente" : "descendente") + ") ---");
            Mostrar(nombresOrd, calOrd);

            Console.WriteLine("\n--- ESTADÍSTICAS ---");
            Console.WriteLine("Comparaciones : " + comparaciones);
            Console.WriteLine("Intercambios  : " + intercambios);
            MostrarEstadisticas(calificaciones);

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }

        static void Burbuja(string[] nombres, double[] cal, bool ascendente,
                            out int comparaciones, out int intercambios)
        {
            comparaciones = 0;
            intercambios = 0;
            int n = cal.Length;

            for (int i = 0; i < n - 1; i++)
            {
                bool huboIntercambio = false;

                for (int j = 0; j < n - 1 - i; j++)
                {
                    comparaciones++;
                    bool debeIntercambiar = ascendente ? cal[j] > cal[j + 1]
                                                       : cal[j] < cal[j + 1];

                    if (debeIntercambiar)
                    {
                        double tempCal = cal[j];
                        cal[j] = cal[j + 1];
                        cal[j + 1] = tempCal;

                        string tempNom = nombres[j];
                        nombres[j] = nombres[j + 1];
                        nombres[j + 1] = tempNom;

                        intercambios++;
                        huboIntercambio = true;
                    }
                }

                if (!huboIntercambio) break;
            }
        }

        static void Capturar(string[] nombres, double[] cal)
        {
            Console.WriteLine();
            for (int i = 0; i < nombres.Length; i++)
            {
                string nombre;
                do
                {
                    Console.Write("Nombre del estudiante " + (i + 1) + ": ");
                    nombre = Console.ReadLine().Trim();
                } while (nombre == "");

                nombres[i] = nombre;
                cal[i] = LeerDouble("Calificación de " + nombre + " (0-100): ", 0, 100);
            }
        }

        static void Generar(string[] nombres, double[] cal)
        {
            Random rnd = new Random();
            for (int i = 0; i < nombres.Length; i++)
            {
                nombres[i] = "Estudiante " + (i + 1);
                cal[i] = rnd.Next(0, 101);
            }
        }

        static void Mostrar(string[] nombres, double[] cal)
        {
            Console.WriteLine("{0,-4} {1,-25} {2,10}", "No.", "Nombre", "Calif.");
            for (int i = 0; i < nombres.Length; i++)
                Console.WriteLine("{0,-4} {1,-25} {2,10:0.##}", i + 1, nombres[i], cal[i]);
        }

        static void MostrarEstadisticas(double[] cal)
        {
            double suma = 0, max = cal[0], min = cal[0];
            int aprobados = 0;

            foreach (double c in cal)
            {
                suma += c;
                if (c > max) max = c;
                if (c < min) min = c;
                if (c >= 70) aprobados++;
            }

            Console.WriteLine("Promedio      : " + (suma / cal.Length).ToString("0.00"));
            Console.WriteLine("Calif. máxima : " + max);
            Console.WriteLine("Calif. mínima : " + min);
            Console.WriteLine("Aprobados (>=70): " + aprobados + " de " + cal.Length);
        }

        static int LeerEntero(string mensaje, int min, int max)
        {
            int valor;
            while (true)
            {
                Console.Write(mensaje);
                if (int.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max)
                    return valor;
                Console.WriteLine("Valor inválido. Ingresa un número entre " + min + " y " + max + ".");
            }
        }

        static double LeerDouble(string mensaje, double min, double max)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                if (double.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max)
                    return valor;
                Console.WriteLine("Valor inválido. Ingresa un número entre " + min + " y " + max + ".");
            }
        }
    }
}

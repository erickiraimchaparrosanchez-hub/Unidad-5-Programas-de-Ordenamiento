using System;
using System.Globalization;

namespace Ordenamiento_de_inventario_por_precio
{
    class Program
    {
        
        static string[] nombres;
        static double[] precios;
        static int cantidad = 0;
        static bool ordenado = false;

        static void Main(string[] args)
        {
            Console.Title = "Inventario - Ordenamiento Burbuja";
            RegistrarProductos();

            int opcion;
            do
            {
                Console.WriteLine("\n===== INVENTARIO POR PRECIO =====");
                Console.WriteLine("1. Mostrar inventario (orden actual)");
                Console.WriteLine("2. Ordenar precios ascendentemente (Burbuja)");
                Console.WriteLine("3. Mostrar producto más caro y más barato");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                switch (opcion)
                {
                    case 1:
                        MostrarInventario();
                        break;
                    case 2:
                        Console.WriteLine("\n--- Antes de ordenar ---");
                        MostrarInventario();
                        Burbuja();
                        ordenado = true;
                        Console.WriteLine("\n--- Después de ordenar (ascendente) ---");
                        MostrarInventario();
                        break;
                    case 3:
                        MostrarExtremos();
                        break;
                    case 0:
                        Console.WriteLine("Fin del programa.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            } while (opcion != 0);
        }

        static void RegistrarProductos()
        {
            int n;
            do
            {
                Console.Write("¿Cuántos productos registrará? (mínimo 10): ");
                if (!int.TryParse(Console.ReadLine(), out n) || n < 10)
                {
                    Console.WriteLine("Debe ingresar un número entero mayor o igual a 10.");
                    n = 0;
                }
            } while (n < 10);

            nombres = new string[n];
            precios = new double[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nProducto {i + 1}:");

                string nombre;
                do
                {
                    Console.Write("  Nombre: ");
                    nombre = Console.ReadLine().Trim();
                } while (nombre == "");

                double precio;
                while (true)
                {
                    Console.Write("  Precio: $");
                    string texto = Console.ReadLine().Replace(',', '.');
                    if (double.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out precio) && precio > 0)
                        break;
                    Console.WriteLine("  Precio no válido, ingrese un número mayor a 0.");
                }

                nombres[i] = nombre;
                precios[i] = precio;
                cantidad++;
            }
        }

        static void Burbuja()
        {
            for (int i = 0; i < cantidad - 1; i++)
            {
                bool huboCambio = false;

                for (int j = 0; j < cantidad - 1 - i; j++)
                {
                    if (precios[j] > precios[j + 1])
                    {
                       
                        double auxPrecio = precios[j];
                        precios[j] = precios[j + 1];
                        precios[j + 1] = auxPrecio;

                        string auxNombre = nombres[j];
                        nombres[j] = nombres[j + 1];
                        nombres[j + 1] = auxNombre;

                        huboCambio = true;
                    }
                }

                if (!huboCambio)
                    break;
            }
        }

        static void MostrarInventario()
        {
            Console.WriteLine($"\n{"No.",-5}{"Producto",-25}{"Precio",10}");
            Console.WriteLine(new string('-', 40));
            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"{i + 1,-5}{nombres[i],-25}{precios[i],10:C2}");
            }
        }

        static void MostrarExtremos()
        {
            if (!ordenado)
            {
                Console.WriteLine("\nPrimero debe ordenar el inventario (opción 2).");
                return;
            }

            Console.WriteLine("\n--- Resultados ---");
            Console.WriteLine($"Producto más barato: {nombres[0]} - {precios[0]:C2}");
            Console.WriteLine($"Producto más caro:   {nombres[cantidad - 1]} - {precios[cantidad - 1]:C2}");
        }
    }
}

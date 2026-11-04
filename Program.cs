List<Titulo> catalogo = new List<Titulo>();

// Datos: ISBN, nombre, ejemplares, préstamos históricos.
catalogo.Add(new Titulo("978-0001", "Don Quijote", 5, 40));
catalogo.Add(new Titulo("978-0002", "Cien años de soledad", 4, 35));
catalogo.Add(new Titulo("978-0003", "El principito", 5, 35));
catalogo.Add(new Titulo("978-0004", "Pedro Páramo", 2, 20));
catalogo.Add(new Titulo("978-0005", "La odisea", 1, 12));
catalogo.Add(new Titulo("978-0006", "Rayuela", 3, 12));
catalogo.Add(new Titulo("978-0007", "Fahrenheit 451", 2, 8));
catalogo.Add(new Titulo("978-0008", "El diario de Ana Frank", 1, 3));

// El catálogo se mantiene ordenado por préstamos.
OrdenarPorInsercion(catalogo);

// Copia ordenada por ISBN para la búsqueda binaria.
// Contiene los mismos libros, así que los préstamos se reflejan en ambas listas.
List<Titulo> porIsbn = new List<Titulo>(catalogo);
OrdenarPorIsbn(porIsbn);

bool salir = false;
while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("===== BIBLIOTECA ESCOLAR =====");
    Console.WriteLine("1. Ver catálogo (más prestados primero)");
    Console.WriteLine("2. Consultar disponibilidad por título");
    Console.WriteLine("3. Buscar por ISBN");
    Console.WriteLine("4. Registrar un préstamo");
    Console.WriteLine("5. Prueba de escritorio (3 préstamos)");
    Console.WriteLine("6. Medir comparaciones (propia vs Array.BinarySearch)");
    Console.WriteLine("0. Salir");
    Console.Write("Elige una opción: ");
    string opcion = Console.ReadLine() ?? "";

    if (opcion == "1")
    {
        MostrarCatalogo(catalogo);
    }
    else if (opcion == "2")
    {
        Console.Write("Título del libro: ");
        string nombre = Console.ReadLine() ?? "";
        ConsultarDisponibilidad(catalogo, nombre);
    }
    else if (opcion == "3")
    {
        Console.Write("ISBN (ejemplo 978-0003): ");
        string isbn = Console.ReadLine() ?? "";
        BuscarPorIsbn(porIsbn, isbn);
    }
    else if (opcion == "4")
    {
        Console.Write("Título del libro a prestar: ");
        string nombre = Console.ReadLine() ?? "";
        RegistrarPrestamo(catalogo, nombre);
        OrdenarPorInsercion(catalogo);   // los préstamos cambian el orden
    }
    else if (opcion == "5")
    {
        PruebaDeEscritorio();
    }
    else if (opcion == "6")
    {
        MedirComparaciones();
    }
    else if (opcion == "0")
    {
        salir = true;
        Console.WriteLine("Hasta luego.");
    }
    else
    {
        Console.WriteLine("Opción no válida.");
    }
}


// ===== FUNCIONES =====

void PruebaDeEscritorio()
{
    Console.WriteLine();
    Console.WriteLine("=== CATÁLOGO ANTES DE LOS PRÉSTAMOS ===");
    MostrarCatalogo(catalogo);

    Console.WriteLine();
    Console.WriteLine("=== REGISTRANDO 3 PRÉSTAMOS ===");
    RegistrarPrestamo(catalogo, "Don Quijote");
    RegistrarPrestamo(catalogo, "Rayuela");
    RegistrarPrestamo(catalogo, "La odisea");
    OrdenarPorInsercion(catalogo);

    Console.WriteLine();
    Console.WriteLine("=== CATÁLOGO DESPUÉS DE LOS PRÉSTAMOS ===");
    MostrarCatalogo(catalogo);
}

// Compara tu búsqueda binaria con Array.BinarySearch usando 5 consultas.
void MedirComparaciones()
{
    // Arreglo con los ISBN ya ordenados, para usar Array.BinarySearch.
    string[] isbns = new string[porIsbn.Count];
    for (int i = 0; i < porIsbn.Count; i++)
    {
        isbns[i] = porIsbn[i].Isbn;
    }

    string[] consultas = { "978-0001", "978-0003", "978-0005", "978-0008", "978-9999" };

    Console.WriteLine();
    Console.WriteLine("ISBN | Comparaciones (propia) | Comparaciones (Array.BinarySearch) | Resultado");
    foreach (string c in consultas)
    {
        int posPropia = BusquedaBinaria(porIsbn, c, out int propia);

        ContadorOrdinal contador = new ContadorOrdinal();
        int posArray = Array.BinarySearch(isbns, c, contador);

        string coincide = (posPropia >= 0) == (posArray >= 0) ? "coinciden" : "DIFERENTE";
        Console.WriteLine(c + " | " + propia + " | " + contador.Cuenta + " | " + coincide);
    }
}

void MostrarCatalogo(List<Titulo> lista)
{
    foreach (Titulo t in lista)
    {
        Console.WriteLine(t.Isbn + " | " + t.Nombre + " | Ejemplares: " + t.Ejemplares
            + " | Disponibles: " + t.Disponibles + " | Préstamos: " + t.Prestamos);
    }
}

// Devuelve true si "a" debe ir ANTES que "b".
// Va primero el que tiene más préstamos. Si empatan, va primero el alfabético.
bool VaAntes(Titulo a, Titulo b)
{
    if (a.Prestamos != b.Prestamos)
    {
        return a.Prestamos > b.Prestamos;
    }
    return string.Compare(a.Nombre, b.Nombre, StringComparison.CurrentCulture) < 0;
}

// Ordenamiento por inserción por préstamos (hecho a mano).
void OrdenarPorInsercion(List<Titulo> lista)
{
    for (int i = 1; i < lista.Count; i++)
    {
        Titulo actual = lista[i];
        int j = i - 1;

        while (j >= 0 && VaAntes(actual, lista[j]))
        {
            lista[j + 1] = lista[j];
            j--;
        }
        lista[j + 1] = actual;
    }
}

// Ordenamiento por inserción por ISBN (hecho a mano), de menor a mayor.
void OrdenarPorIsbn(List<Titulo> lista)
{
    for (int i = 1; i < lista.Count; i++)
    {
        Titulo actual = lista[i];
        int j = i - 1;

        while (j >= 0 && string.CompareOrdinal(actual.Isbn, lista[j].Isbn) < 0)
        {
            lista[j + 1] = lista[j];
            j--;
        }
        lista[j + 1] = actual;
    }
}

// Búsqueda binaria hecha a mano. La lista DEBE estar ordenada por ISBN.
// Devuelve la posición del libro, o -1 si no existe.
int BusquedaBinaria(List<Titulo> lista, string isbn, out int comparaciones)
{
    int inicio = 0;
    int fin = lista.Count - 1;
    comparaciones = 0;

    while (inicio <= fin)
    {
        int medio = (inicio + fin) / 2;
        comparaciones++;
        int resultado = string.CompareOrdinal(isbn, lista[medio].Isbn);

        if (resultado == 0)
        {
            return medio;
        }
        else if (resultado < 0)
        {
            fin = medio - 1;
        }
        else
        {
            inicio = medio + 1;
        }
    }
    return -1;
}

void BuscarPorIsbn(List<Titulo> lista, string isbn)
{
    int pos = BusquedaBinaria(lista, isbn, out int comparaciones);
    if (pos == -1)
    {
        Console.WriteLine(isbn + ": NO existe en el catálogo (" + comparaciones + " comparaciones).");
    }
    else
    {
        Titulo t = lista[pos];
        Console.WriteLine(isbn + ": " + t.Nombre + " - quedan " + t.Disponibles
            + " de " + t.Ejemplares + " copias (" + comparaciones + " comparaciones).");
    }
}

// Búsqueda secuencial por nombre (revisa uno por uno).
Titulo? BuscarPorNombre(List<Titulo> lista, string nombre)
{
    for (int i = 0; i < lista.Count; i++)
    {
        if (lista[i].Nombre == nombre)
        {
            return lista[i];
        }
    }
    return null;
}

void ConsultarDisponibilidad(List<Titulo> lista, string nombre)
{
    Titulo? libro = BuscarPorNombre(lista, nombre);
    if (libro == null)
    {
        Console.WriteLine("No existe el libro: " + nombre);
    }
    else
    {
        Console.WriteLine(libro.Nombre + ": quedan " + libro.Disponibles
            + " de " + libro.Ejemplares + " copias.");
    }
}

void RegistrarPrestamo(List<Titulo> lista, string nombre)
{
    Titulo? libro = BuscarPorNombre(lista, nombre);
    if (libro == null)
    {
        Console.WriteLine("No existe el libro: " + nombre);
    }
    else if (libro.Disponibles == 0)
    {
        Console.WriteLine("No hay copias disponibles de: " + nombre);
    }
    else
    {
        libro.Disponibles--;
        libro.Prestamos++;
        Console.WriteLine("Préstamo registrado: " + nombre);
    }
}

class Titulo
{
    public string Isbn;
    public string Nombre;
    public int Ejemplares;
    public int Disponibles;
    public int Prestamos;

    public Titulo(string isbn, string nombre, int ejemplares, int prestamos)
    {
        Isbn = isbn;
        Nombre = nombre;
        Ejemplares = ejemplares;
        Disponibles = ejemplares;
        Prestamos = prestamos;
    }
}

// Comparador que cuenta cuántas comparaciones hace Array.BinarySearch.
class ContadorOrdinal : IComparer<string>
{
    public int Cuenta = 0;

    public int Compare(string? x, string? y)
    {
        Cuenta++;
        return string.CompareOrdinal(x, y);
    }
}
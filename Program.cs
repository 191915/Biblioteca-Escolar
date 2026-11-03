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

// Desordenamos a propósito para comprobar que el ordenamiento funciona.
Titulo temp = catalogo[0];
catalogo[0] = catalogo[7];
catalogo[7] = temp;
temp = catalogo[2];
catalogo[2] = catalogo[5];
catalogo[5] = temp;

Console.WriteLine("=== CATÁLOGO DESORDENADO ===");
MostrarCatalogo(catalogo);

OrdenarPorInsercion(catalogo);

Console.WriteLine();
Console.WriteLine("=== CATÁLOGO ORDENADO (más préstamos primero) ===");
MostrarCatalogo(catalogo);

// ===== PRÉSTAMOS (Día 3) =====
Console.WriteLine();
Console.WriteLine("=== REGISTRANDO 3 PRÉSTAMOS ===");
RegistrarPrestamo(catalogo, "Don Quijote");
RegistrarPrestamo(catalogo, "Rayuela");
RegistrarPrestamo(catalogo, "La odisea");
RegistrarPrestamo(catalogo, "La odisea");   // ya no hay copias

OrdenarPorInsercion(catalogo);

Console.WriteLine();
Console.WriteLine("=== CATÁLOGO ORDENADO DESPUÉS DE LOS PRÉSTAMOS ===");
MostrarCatalogo(catalogo);

Console.WriteLine();
Console.WriteLine("=== CONSULTA DE DISPONIBILIDAD (búsqueda secuencial por título) ===");
ConsultarDisponibilidad(catalogo, "Don Quijote");
ConsultarDisponibilidad(catalogo, "Libro inexistente");

// ===== BÚSQUEDA BINARIA POR ISBN (Día 5) =====
// Hacemos una COPIA de la lista y la ordenamos por ISBN.
// El catálogo original sigue ordenado por préstamos.
List<Titulo> porIsbn = new List<Titulo>(catalogo);
OrdenarPorIsbn(porIsbn);

Console.WriteLine();
Console.WriteLine("=== COPIA ORDENADA POR ISBN ===");
MostrarCatalogo(porIsbn);

Console.WriteLine();
Console.WriteLine("=== BÚSQUEDA BINARIA POR ISBN ===");
BuscarPorIsbn(porIsbn, "978-0003");
BuscarPorIsbn(porIsbn, "978-0008");
BuscarPorIsbn(porIsbn, "978-0001");
BuscarPorIsbn(porIsbn, "978-0005");
BuscarPorIsbn(porIsbn, "978-9999");   // no existe


// ===== FUNCIONES =====

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
// "comparaciones" cuenta cuántas veces comparamos (lo usaremos en el Día 7).
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
            return medio;          // lo encontramos
        }
        else if (resultado < 0)
        {
            fin = medio - 1;       // descartamos la mitad derecha
        }
        else
        {
            inicio = medio + 1;    // descartamos la mitad izquierda
        }
    }
    return -1;                     // no existe
}

// Busca por ISBN e informa si existe y cuántas copias quedan.
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
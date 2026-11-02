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

// ===== PRÉSTAMOS (del Día 3) =====
Console.WriteLine();
Console.WriteLine("=== REGISTRANDO 3 PRÉSTAMOS ===");
RegistrarPrestamo(catalogo, "Don Quijote");
RegistrarPrestamo(catalogo, "Rayuela");
RegistrarPrestamo(catalogo, "La odisea");
RegistrarPrestamo(catalogo, "La odisea");   // ya no hay copias

// Después de prestar, los préstamos cambiaron: hay que volver a ordenar.
OrdenarPorInsercion(catalogo);

Console.WriteLine();
Console.WriteLine("=== CATÁLOGO ORDENADO DESPUÉS DE LOS PRÉSTAMOS ===");
MostrarCatalogo(catalogo);

Console.WriteLine();
Console.WriteLine("=== CONSULTA DE DISPONIBILIDAD ===");
ConsultarDisponibilidad(catalogo, "Don Quijote");
ConsultarDisponibilidad(catalogo, "Libro inexistente");


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

// Ordenamiento por inserción, hecho a mano (sin Sort).
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
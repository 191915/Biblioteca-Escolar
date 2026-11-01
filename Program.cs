// Lista que guarda todos los libros de la biblioteca.
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

// ===== PRUEBA DE ESCRITORIO =====
Console.WriteLine("=== CATÁLOGO ANTES DE LOS PRÉSTAMOS ===");
MostrarCatalogo(catalogo);

Console.WriteLine();
Console.WriteLine("=== REGISTRANDO 3 PRÉSTAMOS ===");
RegistrarPrestamo(catalogo, "Don Quijote");
RegistrarPrestamo(catalogo, "Rayuela");
RegistrarPrestamo(catalogo, "La odisea");

Console.WriteLine();
Console.WriteLine("=== PRUEBA EXTRA: La odisea ya no tiene copias ===");
RegistrarPrestamo(catalogo, "La odisea");

Console.WriteLine();
Console.WriteLine("=== CATÁLOGO DESPUÉS DE LOS PRÉSTAMOS ===");
MostrarCatalogo(catalogo);

Console.WriteLine();
Console.WriteLine("=== CONSULTA DE DISPONIBILIDAD ===");
ConsultarDisponibilidad(catalogo, "Don Quijote");
ConsultarDisponibilidad(catalogo, "Libro inexistente");


// ===== FUNCIONES =====

// Muestra todos los libros en pantalla.
void MostrarCatalogo(List<Titulo> lista)
{
    foreach (Titulo t in lista)
    {
        Console.WriteLine(t.Isbn + " | " + t.Nombre + " | Ejemplares: " + t.Ejemplares
            + " | Disponibles: " + t.Disponibles + " | Préstamos: " + t.Prestamos);
    }
}

// Busca un libro por su nombre (búsqueda secuencial: revisa uno por uno).
Titulo? BuscarPorNombre(List<Titulo> lista, string nombre)
{
    for (int i = 0; i < lista.Count; i++)
    {
        if (lista[i].Nombre == nombre)
        {
            return lista[i];
        }
    }
    return null; // no se encontró
}

// Dice cuántas copias quedan de un libro.
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

// Registra un préstamo: baja las disponibles y sube el contador histórico.
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
        libro.Disponibles--;   // queda una copia menos
        libro.Prestamos++;     // se suma un préstamo al historial
        Console.WriteLine("Préstamo registrado: " + nombre);
    }
}

// Esta es la "ficha" de un libro.
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
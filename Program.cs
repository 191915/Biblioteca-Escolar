// Lista que guarda todos los libros de la biblioteca.
List<Titulo> catalogo = new List<Titulo>();

// Cargamos 8 libros con distinta cantidad de ejemplares.
// Datos: ISBN, nombre, ejemplares, préstamos históricos.
catalogo.Add(new Titulo("978-0001", "Don Quijote", 5, 40));
catalogo.Add(new Titulo("978-0002", "Cien años de soledad", 4, 35));
catalogo.Add(new Titulo("978-0003", "El principito", 5, 35));
catalogo.Add(new Titulo("978-0004", "Pedro Páramo", 2, 20));
catalogo.Add(new Titulo("978-0005", "La odisea", 1, 12));
catalogo.Add(new Titulo("978-0006", "Rayuela", 3, 12));
catalogo.Add(new Titulo("978-0007", "Fahrenheit 451", 2, 8));
catalogo.Add(new Titulo("978-0008", "El diario de Ana Frank", 1, 3));

// Mostramos el catálogo en pantalla.
Console.WriteLine("=== CATÁLOGO DE LA BIBLIOTECA ===");
foreach (Titulo t in catalogo)
{
    Console.WriteLine(t.Isbn + " | " + t.Nombre + " | Ejemplares: " + t.Ejemplares
        + " | Disponibles: " + t.Disponibles + " | Préstamos: " + t.Prestamos);
}

// Esta es la "ficha" de un libro.
class Titulo
{
    public string Isbn;          // código del libro
    public string Nombre;        // título del libro
    public int Ejemplares;       // copias totales que tiene la biblioteca
    public int Disponibles;      // copias que quedan para prestar
    public int Prestamos;        // veces que se ha prestado en total

    // Esto es el "constructor": sirve para llenar la ficha al crearla.
    public Titulo(string isbn, string nombre, int ejemplares, int prestamos)
    {
        Isbn = isbn;
        Nombre = nombre;
        Ejemplares = ejemplares;
        Disponibles = ejemplares;   // al inicio, todas las copias están disponibles
        Prestamos = prestamos;
    }
}
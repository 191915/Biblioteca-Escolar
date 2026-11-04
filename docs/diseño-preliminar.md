\# Documento de diseño preliminar



Para guardar los libros comparé tres opciones:



\## 1. Arreglo escalonado

Cada libro tiene su fila con el tamaño exacto de sus ejemplares, sin celdas vacías.

Pros: rápido y sin espacio desperdiciado.

Contras: tamaño fijo, y no guarda el título ni el ISBN junto con los datos.



\## 2. List<int\[]>

Es como el escalonado, pero se pueden agregar libros nuevos.

Pros: es flexible para agregar libros.

Contras: cada fila sigue siendo de tamaño fijo, y tampoco guarda título ni ISBN.



\## 3. List<Titulo>

Cada libro es un objeto que guarda ISBN, título, ejemplares y préstamos.

Pros: todo está junto, es fácil de leer, ordenar y buscar, y no deja celdas vacías.

Contras: hay que crear una clase y es un poco más de código.



\## Decisión

Usaré List<Titulo>, porque agrupa todos los datos de cada libro y soporta títulos con distinto número de ejemplares.


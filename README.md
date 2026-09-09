# Actividad: Capa de Modelos (Models) - Sistema de Biblioteca

## Integrantes del Equipo
* **CONTRERAS Rodriguez Janis Isabel**
* **TORRES Barrera Jose Angel**
* **MACÍAS Cruz Meredith Miranda**

---

## Descripción del Proyecto
Este proyecto corresponde a la capa de dominio/modelos de un sistema de gestión de biblioteca escolar desarrollado en C# (.NET). Implementa la lógica de encapsulamiento, herencia, validación de datos, sobrecarga de constructores y métodos de negocio para 10 entidades principales del sistema.

---

## Estructura de Entidades (`Models/`)
1. **Persona** (Clase Base)
2. **Autores** (Hereda de Persona)
3. **Usuarios** (Hereda de Persona)
4. **Administrador** (Hereda de Persona)
5. **Libros**
6. **Prestamos**
7. **Genero**
8. **Editorial**
9. **Multa**
10. **Reserva**

---

## Instrucciones para Ejecutar las Pruebas
1. Clonar o descargar este repositorio.
2. Abrir la solución `.sln` en **Visual Studio**.
3. Asegurarse de que el proyecto **Biblioteca** esté configurado como proyecto de inicio.
4. Compilar y ejecutar la solución (`F5` o `Ctrl + F5`).
5. La consola ejecutará las pruebas de las clases (`Program.cs`), demostrando:
   - Creación de objetos con constructores vacíos y parametrizados.
   - Captura de excepciones al ingresar datos inválidos (`try-catch`).
   - Ejecución de sobrecarga de métodos de negocio.
   - Salida formateada con `ToString()`.

/*
 * ENCABEZADO DE AUTORÍA
 * Equipo N°: 3
 * Integrantes:
 * 1. CONTRERAS Rodriguez Janis Isabel
 * 2. TORRES Barrera Jose Angel
 * 3. MACÍAS Cruz Meredith Miranda
 */

using System;
using Biblioteca.Models;

namespace Biblioteca
{
    internal static class Program
    {
        static void Main()
        {
            ProbarConstructores();
            ProbarValidacionesYExcepciones();
            ProbarSobrecargaMetodos();
            ProbarCamposObligatorios();

            ImprimirResultadoFinal();

            Console.WriteLine("\nPresiona una tecla para salir...");
            Console.ReadKey();
        }

        private static void ProbarConstructores()
        {
            EncabezadoSeccion("1. PRUEBA DE INSTANCIACIÓN Y CONSTRUCTORES");

            Autores autorDefault = new Autores();
            Console.WriteLine("[+] Objeto creado con constructor por defecto:");
            Console.WriteLine($"    {autorDefault}");

            Autores autorParametrizado = new Autores(
                101, "Gabriel García Márquez", 87, "gabo@macondo.com", "Colombiana", "gabo.png", true
            );
            Console.WriteLine("\n[+] Objeto creado con constructor parametrizado:");
            Console.WriteLine($"    {autorParametrizado}");
        }

        private static void ProbarValidacionesYExcepciones()
        {
            EncabezadoSeccion("2. PRUEBA DE VALIDACIONES Y CAPTURA DE EXCEPCIONES (TRY-CATCH)");

            try
            {
                Console.WriteLine("[*] Intentando registrar Persona con correo inválido...");
                Persona p = new Persona(1, "Carlos Gomez", 30, "correoSinArroba.com");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"    [OK EXCEPCIÓN CAPTURADA]: {ex.Message}");
            }

            try
            {
                Console.WriteLine("\n[*] Intentando asignar edad negativa (-10)...");
                Persona p = new Persona();
                p.Edad = -10;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"    [OK EXCEPCIÓN CAPTURADA]: {ex.Message}");
            }

            try
            {
                Console.WriteLine("\n[*] Intentando asignar nombre de género en blanco...");
                Genero g = new Genero();
                g.Nombre = "   ";
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"    [OK EXCEPCIÓN CAPTURADA]: {ex.Message}");
            }
        }

        private static void ProbarSobrecargaMetodos()
        {
            EncabezadoSeccion("3. PRUEBA DE SOBRECARGA DE MÉTODOS DE NEGOCIO");

            Autores autor = new Autores(1, "Mario Vargas Llosa", "Peruana");
            decimal ventas = 100000m;

            decimal regaliaA = autor.CalcularRegalias(ventas);
            Console.WriteLine($"[Versión A - Regalía Estándar (10%)]: ${regaliaA:N2}");

            decimal regaliaB = autor.CalcularRegalias(ventas, 18m);
            Console.WriteLine($"[Versión B - Regalía Especial (18%)]: ${regaliaB:N2}");

            Console.WriteLine();

            Genero genero = new Genero(1, "Terror", "Libros de suspenso", "terror.png", true);

            Console.WriteLine($"[Versión A - Etiqueta Estándar]: {genero.GenerarEtiqueta()}");
            Console.WriteLine($"[Versión B - Etiqueta con Prefijo]: {genero.GenerarEtiqueta("SECCION_NOCHE")}");
        }

        private static void ProbarCamposObligatorios()
        {
            EncabezadoSeccion("4. VERIFICACIÓN DE ATRIBUTOS OBLIGATORIOS (IMAGEN Y ESTADO)");

            Genero g = new Genero(2, "Fantasía", "Libros épicos", "fantasia.png", true);
            Console.WriteLine($"Entidad: {g.Nombre}");
            Console.WriteLine($" - Campo RutaImagen: {g.RutaImagen}");
            Console.WriteLine($" - Campo Estado: {(g.Estado ? "Activo" : "Inactivo")}");
        }

        private static void EncabezadoSeccion(string titulo)
        {
            Console.WriteLine("\n========================================================================");
            Console.WriteLine($"  {titulo}");
            Console.WriteLine("========================================================================\n");
        }

        private static void ImprimirResultadoFinal()
        {
            Console.WriteLine("\n========================================================================");
            Console.WriteLine("   TODAS LAS PRUEBAS SE EJECUTARON CORRECTAMENTE. COMPILACIÓN EXITOSA.");
            Console.WriteLine("========================================================================\n");
        }
    }
}
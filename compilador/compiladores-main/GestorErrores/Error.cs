using Compilador_22023.AnalisisLexico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_22023.GestorErrores
{
    public class Error
    {
        private int numeroLinea;
        private int posicionInicial;
        private int posicionFinal;
        private string lexema;
        private string falla;
        private string causa;
        private string solucion;
        private TipoError tipo;
        private CategoriaError categoria;

        public Error(int numeroLinea, int posicionInicial, int posicionFinal, string lexema, string falla, string causa, string solucion, TipoError tipo, CategoriaError categoria)
        {
            NumeroLinea = numeroLinea;
            PosicionInicial = posicionInicial;
            PosicionFinal = posicionFinal;
            Lexema = lexema;
            Falla = falla;
            Causa = causa;
            Solucion = solucion;
            Tipo = tipo;
            Categoria = categoria;
            NumeroLinea = numeroLinea;
            PosicionInicial = posicionInicial;
            PosicionFinal = posicionFinal;
            Lexema = lexema;
            Falla = falla;
            Causa = causa;
            Solucion = solucion;
            Tipo = tipo;
            Categoria = categoria;
        }

        public int NumeroLinea { get => numeroLinea; set => numeroLinea = value; }
        public int PosicionInicial { get => posicionInicial; set => posicionInicial = value; }
        public int PosicionFinal { get => posicionFinal; set => posicionFinal = value; }
        public string Lexema { get => lexema; set => lexema = value; }
        public string Falla { get => falla; set => falla = value; }
        public string Causa { get => causa; set => causa = value; }
        public string Solucion { get => solucion; set => solucion = value; }
        public TipoError Tipo { get => tipo; set => tipo = value; }
        public CategoriaError Categoria { get => categoria; set => categoria = value; }

        public static Error CrearErrorLexicoRecuperable(int numeroLinea, int posicionInicial, string lexema, string falla, string causa, string solucion)
        {

            return new Error(numeroLinea, posicionInicial, posicionInicial + lexema.Length, lexema, falla, causa, solucion, TipoError.LEXICO, CategoriaError.RECUPERABLE);

        }
        public static Error CrearErrorLexicoStopper(int numeroLinea, int posicionInicial, string lexema, string falla, string causa, string solucion)
        {

            return new Error(numeroLinea, posicionInicial, posicionInicial + lexema.Length, lexema, falla, causa, solucion, TipoError.LEXICO, CategoriaError.STOPPER);

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("-------------------------------INICIO-------------------------------\r\n");
            sb.Append("Tipo: ").Append(Tipo).Append("\r\n");
            sb.Append("Categoria: ").Append(Categoria).Append("\r\n");
            sb.Append("Lexema: ").Append(Lexema).Append("\r\n");
            sb.Append("Número de línea: ").Append(NumeroLinea).Append("\r\n");
            sb.Append("Posicion Inicia: ").Append(PosicionInicial).Append("\r\n");
            sb.Append("Posicion Final: ").Append(PosicionFinal).Append("\r\n");
            sb.Append("Falla: ").Append(Falla).Append("\r\n");
            sb.Append("Causa: ").Append(Causa).Append("\r\n")
            sb.Append("Solución Final: ").Append(Solucion).Append("\r\n");

            sb.Append("-------------------------------FIN-------------------------------\r\n");
            return sb.ToString();
        }
    }
}

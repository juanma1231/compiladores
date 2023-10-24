using Compiladores_Clase.AnalisisLexico;
using Compiladores_Clase.GestorErrores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_Clase.AnalisisSintactico
{
    public class AnalizadorSintactico
    {
        private AnalizadorLexico AnaLex = new AnalizadorLexico();
        private ComponenteLexico Componente;
        private string falla = "";
        private string causa = "";
        private string solucion = "";
        public string Analizar()
        {
            string resultado = "";
            DevolverSiguienteComponenteLexico();
            Expresion();



            if (ManejadorErrores.ObtenerManejadorDeErrores().HayErroresAnalisis())
            {
                resultado = "El proceso de compilación finalizó con errores";
            }
            else if (!CategoriaGramatical.FIN_DE_ARCHIVO.Equals(Componente.Categoria))
            {
                resultado = "Aunque el programa no presenta errores, faltaron componentes por evaluar...";
            }
            else
            {
                resultado = "El programa se encuentra bien escrito";
            }



            return resultado;
        }
        private void DevolverSiguienteComponenteLexico()
        {
            Componente = AnaLex.DevolverSiguienteComponente();
        }
        private void Expresion()
        {
            Termino();
            ExpresionPrima();
        }
        private void ExpresionPrima()
        {
            if (EsCategoriaValida(CategoriaGramatical.SUMA))
            {
                DevolverSiguienteComponenteLexico();
                Expresion();
            }
            else if (EsCategoriaValida(CategoriaGramatical.RESTA))
            {
                DevolverSiguienteComponenteLexico();
                Expresion();
            }
            else
            {
                //NADA
            }



        }
        private void Termino()
        {
            Factor();
            TerminoPrima();
        }
        private void TerminoPrima()
        {
            if (EsCategoriaValida(CategoriaGramatical.MULTIPLICACION))
            {
                DevolverSiguienteComponenteLexico();
                Termino();
            }
            else if (EsCategoriaValida(CategoriaGramatical.DIVISION))
            {
                DevolverSiguienteComponenteLexico();
                Termino();
            }
            else
            {
                //NADA
            }
        }
        private void Factor()
        {
            if (EsCategoriaValida(CategoriaGramatical.NUMERO_ENTERO))
            {
                DevolverSiguienteComponenteLexico();
            }
            else if (EsCategoriaValida(CategoriaGramatical.NUMERO_DECIMAL))
            {
                DevolverSiguienteComponenteLexico();
            }
            else if (EsCategoriaValida(CategoriaGramatical.PARENTESIS_ABRE))
            {
                DevolverSiguienteComponenteLexico();
                Expresion();
                if (EsCategoriaValida(CategoriaGramatical.PARENTESIS_CIERRA))
                {
                    DevolverSiguienteComponenteLexico();

                }
                else
                {
                    falla = "Categoría gramática inválida...";
                    causa = "Se esperaba PARÉNTESIS CIERRA, se recibió '" + Componente.Categoria;
                    solucion = "Asegúrese que en la posición esperada se encuentre PARÉNTESIS CIERRA...";
                    ReportarErrorSintacticoStopper();
                }
            }
            else
            {
                falla = "Categoría gramática inválida...";
                causa = "Se esperaba NUMERO ENTERO, NUMERO DECIMAL o PARÉNTESIS ABRE, se recibió '" + Componente.Categoria;
                solucion = "Asegúrese que en la posición esperada se encuentre NUMERO ENTERO, NUMERO DECIMAL o PARÉNTESIS ABRE...";
                ReportarErrorSintacticoStopper();
            }
        }



        private bool EsCategoriaValida(CategoriaGramatical categoria)
        {
            return categoria.Equals(Componente.Categoria);
        }



        private void ReportarErrorSintacticoStopper()
        {
            Error error = Error.CrearErrorSintacticoStopper(Componente.NumeroLinea, Componente.PosicionInicial, Componente.Lexema, falla, causa, solucion);
            ManejadorErrores.ObtenerManejadorDeErrores().ReportarError(error);
        }
    }
}

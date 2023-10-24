using Compilador_22023.AnalisisLexico;
using Compilador_22023.GestorErrores;
using Compiladores_Clase.AnalisisLexico;
using Compiladores_Clase.GestorErrores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_22023.AnalisisSintactico
{
    internal class AnalizadorSintactico
    {

        private AnalizadorLexico Analex = new AnalizadorLexico();
        private ComponenteLexico Componente;
        private string falla = "";
        private string causa = "";
        private string solucion = "";
        private Stack<double> pila = new Stack<double>();

        public string Analizar()
        {
            string resultado = "";
            DevolverSiguienteComponenteLexico();
            Expresion();

            if (ManejadorErrores.ObtenerManejadorDeErrores().HayErroresAnalisis())
            {
                resultado = "El proceso de compilación terminó con errores.\r\n";
            }
            else if (!CategoriaGramatical.FIN_DE_ARCHIVO.Equals(Componente.Categoria) || pila.Count > 1)
            {
                resultado = "Aunque el programa no tiene errores, faltaron componentes por evaluar.\r\n";
            }
            else
            {
                resultado = "El programa se encuentra bien escrito. El resultado de la expresión es:\r\n" pila.Pop();
            }

            return resultado;
        }

        private void DevolverSiguienteComponenteLexico()
        {
            Componente = Analex.DevolverSiguienteComponente();
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
                EvaluarExpresion(CategoriaGramatical.SUMA);
            }
            else if (EsCategoriaValida(CategoriaGramatical.RESTA))
            {
                DevolverSiguienteComponenteLexico();
                Expresion();
                EvaluarExpresion(CategoriaGramatical.RESTA);
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
                EvaluarExpresion(CategoriaGramatical.MULTIPLICACION);
            }
            else if (EsCategoriaValida(CategoriaGramatical.DIVISION))
            {
                DevolverSiguienteComponenteLexico();
                Termino();
                EvaluarExpresion(CategoriaGramatical.DIVISION);
            }

        }
        private void Factor()
        {
            if (EsCategoriaValida(CategoriaGramatical.NUMERO_ENTERO))
            {
                pila.Push()
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
                    falla = "Categoria gramatical inválida";
                    causa = "Se esperaba PARENTESIS_CIERRA y se recibió " + Componente.Categoria;
                    solucion = "Asegurese que en la posición esperada se encuentre un PARENTESIS_CIERRA";
                    ReportarErrorSintacticoStopper();
                }
            }
            else
            {
                falla = "Categoria gramatical inválida";
                causa = "Se esperaba NUMERO_ENTERO, NUMERO_DECIMAL o PARENTESIS_ABRE y se recibió " + Componente.Categoria;
                solucion = "Asegurese que en la posición esperada se encuentre un NUMERO_ENTERO, NUMERO_DECIMAL o PARENTESIS_ABRE";
                ReportarErrorSintacticoStopper();
            }
        }
        private bool EsCategoriaValida(CategoriaGramatical categoria)
        {
            return categoria.Equals(Componente.Categoria);
        }
        private void ReportarErrorSintacticoStopper()
        {
            Error error = Error.CrearErrorSintacticoStopper(Componente.NumeroLinea, Componente.PosicionInicial,
                Componente.Lexema, falla, causa, solucion);
            ManejadorErrores.ObtenerManejadorDeErrores().ReportarError(error);
        }
        private void ReportarErrorSemanticoRecuperable()
        {
            Error error = Error.CrearErrorSemanticoRecuperable(Componente.NumeroLinea, Componente.PosicionInicial,
                Componente.Lexema, falla, causa, solucion);
            ManejadorErrores.ObtenerManejadorDeErrores().ReportarError(error);
        }
        private void EvaluarExpresion(CategoriaGramatical categoria)
        {
            if (!ManejadorErrores.ObtenerManejadorDeErrores().HayErroresAnalisis())
            {
                double operandoDerecha = pila.Pop();
                double operandoIzquierda = pila.Pop();
                if (CategoriaGramatical.SUMA.Equals(categoria))
                {
                    pila.Push(operandoIzquierda + operandoDerecha);
                }
                else if (CategoriaGramatical.RESTA.Equals(categoria))
                {
                    pila.Push(operandoIzquierda - operandoDerecha);
                }
                else if (CategoriaGramatical.MULTIPLICACION.Equals(categoria))
                {
                    pila.Push(operandoIzquierda * operandoDerecha);
                }
                else if (CategoriaGramatical.DIVISION.Equals(categoria))
                {
                    if (operandoDerecha == 0)
                    {
                        falla = "Categoria gramatical inválida";
                        causa = "Se esperaba un número diferente de cero para realizar la división" + Componente.Categoria;
                        solucion = "Asegurese que en la posición esperada se encuentre un número diferente de cero";
                        ReportarErrorSintacticoStopper();
                    }
                    else
                    {
                        pila.Push(operandoIzquierda / operandoDerecha);
                    }
                }
            }

        }
    }
}
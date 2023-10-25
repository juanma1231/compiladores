using Compilador_22023.AnalisisLexico;
using Compilador_22023.GestorErrores;
using Compiladores_Clase.AnalisisLexico;
using Compiladores_Clase.GestorErrores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private StringBuilder trazaDepuracion= new StringBuilder();


        public string Analizar()
        {
            string resultado = "";
            DevolverSiguienteComponenteLexico();
            Expresion();

            if (ManejadorErrores.ObtenerManejadorDeErrores().HayErroresAnalisis())
            {
                resultado = "El proceso de compilaci�n termin� con errores.\r\n";
            }
            else if (!CategoriaGramatical.FIN_DE_ARCHIVO.Equals(Componente.Categoria) || pila.Count > 1)
            {
                resultado = "Aunque el programa no tiene errores, faltaron componentes por evaluar.\r\n";
            }
            else
            {
                resultado = "El programa se encuentra bien escrito. El resultado de la expresi�n es:\r\n" pila.Pop();
            }

            return resultado;
        }

        private void DevolverSiguienteComponenteLexico()
        {
            Componente = Analex.DevolverSiguienteComponente();
        }
        private void Expresion(int jerarquia)
        {
            FormarInicio("Expresion",jerarquia);
            Termino(jerarquia + 1);
            ExpresionPrima(jerarquia + 1);
            FormarFin("EXPRESION", jerarquia);
        }
        private void ExpresionPrima(int jerarquia)
           
        {
            FormarInicio("EXPRESION", jerarquia);
            if (EsCategoriaValida(CategoriaGramatical.SUMA))
            {
                DevolverSiguienteComponenteLexico();
                Expresion(jerarquia+1);
                EvaluarExpresion(CategoriaGramatical.SUMA);
            }
            else if (EsCategoriaValida(CategoriaGramatical.RESTA))
            {
                DevolverSiguienteComponenteLexico();
                Expresion(jerarquia +1);
                EvaluarExpresion(CategoriaGramatical.RESTA);
            }
            FormarFin("EXPRESION", jerarquia);
        }
        private void Termino(int jerarquia)
        {
            FormarInicio("EXPRESION", jerarquia);
            Factor(jerarquia + 1);
            TerminoPrima(jerarquia + 1);
            FormarFin("EXPRESION", jerarquia);

        }
        private void TerminoPrima(int jerarquia)
        {
            FormarInicio("EXPRESION", jerarquia)
            if (EsCategoriaValida(CategoriaGramatical.MULTIPLICACION))
            {
                DevolverSiguienteComponenteLexico();
                Termino(jerarquia + 1);
                EvaluarExpresion(CategoriaGramatical.MULTIPLICACION);
            }
            else if (EsCategoriaValida(CategoriaGramatical.DIVISION))
            {
                DevolverSiguienteComponenteLexico();
                Termino(jerarquia + 1);
                EvaluarExpresion(CategoriaGramatical.DIVISION);
            }
            FormarFin("EXPRESION", jerarquia);

        }
        private void Factor(int jerarquia)
        {
            FormarInicio("EXPRESION", jerarquia);
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
                Expresion(jerarquia+1);
                if (EsCategoriaValida(CategoriaGramatical.PARENTESIS_CIERRA))
                {
                    DevolverSiguienteComponenteLexico();
                }
                else
                {
                    falla = "Categoria gramatical inv�lida";
                    causa = "Se esperaba PARENTESIS_CIERRA y se recibi� " + Componente.Categoria;
                    solucion = "Asegurese que en la posici�n esperada se encuentre un PARENTESIS_CIERRA";
                    ReportarErrorSintacticoStopper();
                }
            }
            else
            {
                falla = "Categoria gramatical inv�lida";
                causa = "Se esperaba NUMERO_ENTERO, NUMERO_DECIMAL o PARENTESIS_ABRE y se recibi� " + Componente.Categoria;
                solucion = "Asegurese que en la posici�n esperada se encuentre un NUMERO_ENTERO, NUMERO_DECIMAL o PARENTESIS_ABRE";
                ReportarErrorSintacticoStopper();
            }
            FormarFin("EXPRESION", jerarquia);
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
                        falla = "Categoria gramatical inv�lida";
                        causa = "Se esperaba un n�mero diferente de cero para realizar la divisi�n" + Componente.Categoria;
                        solucion = "Asegurese que en la posici�n esperada se encuentre un n�mero diferente de cero";
                        ReportarErrorSintacticoStopper();
                    }
                    else
                    {
                        pila.Push(operandoIzquierda / operandoDerecha);
                    }
                }
            }

        }
        private void FormarInicio(String nombreRegla, int jerarquia)
        {
            StringBuilder indentador = new StringBuilder;
            for (int indice = 1; indice <= jerarquia; indice++)
            {
                indentador.Append("--------");
            }
            indentador.Append("INICIO REGLA").Append(nombreRegla);
            indentador.Append(" con componente").Append(Componente.Lexema).Append("\n");

            trazaDepuracion.Append(indentador.ToString());

        }
    }
   
}
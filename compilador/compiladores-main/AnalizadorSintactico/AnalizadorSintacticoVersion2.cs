using Compiladores_Clase.AnalisisLexico;
using Compiladores_Clase.GestorErrores;

namespace Compiladores_Clase.AnalisisSintactico
{
    public class AnalizadorSintacticoVersion2
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


            Factor();
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
            else if (EsCategoriaValida(CategoriaGramatical.MULTIPLICACION))
            {
                DevolverSiguienteComponenteLexico();
                Termino();
                if (EsCategoriaValida(CategoriaGramatical.SUMA))
                {
                    Expresion();
                }
                else if (EsCategoriaValida(CategoriaGramatical.RESTA))
                {
                    Expresion();
                }
                else if (!EsCategoriaValida(CategoriaGramatical.FIN_DE_ARCHIVO))
                 {
                    falla = "Categoria gramatical inválida";
                    causa = "Se esperaba SUMA o RESTA y se recibió " + Componente.Categoria;
                    solucion = "Asegurese que en la posición esperada se encuentre un SUMA o RESTA";
                    ReportarErrorSintacticoStopper();
                }
            }
            else if (EsCategoriaValida(CategoriaGramatical.DIVISION))
            {
                DevolverSiguienteComponenteLexico();
                Termino();
                if (EsCategoriaValida(CategoriaGramatical.SUMA))
                {
                    Expresion();
                }
                else if (EsCategoriaValida(CategoriaGramatical.RESTA))
                {
                    Expresion();
                }
                else
                {
                    if (!EsCategoriaValida(CategoriaGramatical.FIN_DE_ARCHIVO))
                    {
                        falla = "Categoria gramatical inválida";
                        causa = "Se esperaba SUMA o RESTA y se recibió " + Componente.Categoria;
                        solucion = "Asegurese que en la posición esperada se encuentre un SUMA o RESTA";
                        ReportarErrorSintacticoStopper();
                    }
                }

            }
            else
            {
                if (!EsCategoriaValida(CategoriaGramatical.FIN_DE_ARCHIVO))
                {
                    falla = "Categoria gramatical inválida";
                    causa = "Se esperaba SUMA, RESTA DIVISIÓN o MULTIPLICACION y se recibió " + Componente.Categoria;
                    solucion = "Asegurese que en la posición esperada se encuentre un SUMA, RESTA DIVISIÓN o MULTIPLICACIO";
                    ReportarErrorSintacticoStopper();
                }
            }


        }


        private void Termino()
        {


            Factor();
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
                if (!EsCategoriaValida(CategoriaGramatical.FIN_DE_ARCHIVO))
                {

                    falla = "Categoria gramatical inválida";
                    causa = "Se esperaba DIVISIÓN o MULTIPLICACION y se recibió " + Componente.Categoria;
                    solucion = "Asegurese que en la posición esperada se encuentre un DIVISIÓN o MULTIPLICACIO";
                    ReportarErrorSintacticoStopper();
                }
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



        private void ReportarErrorSintacticoStopper()
        {
            Error error = Error.CrearErrorSintacticoStopper(Componente.NumeroLinea, Componente.PosicionInicial, Componente.Lexema, falla, causa, solucion);
            ManejadorErrores.ObtenerManejadorDeErrores().ReportarError(error);
        }


        private bool EsCategoriaValida(CategoriaGramatical categoria)
        {
            return categoria.Equals(Componente.Categoria);
        }
    }
}


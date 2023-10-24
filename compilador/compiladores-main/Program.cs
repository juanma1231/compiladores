using Compiladores_Clase.AnalisisLexico;
using Compiladores_Clase.AnalisisSintactico;
using Compiladores_Clase.GestorErrores;
using Compiladores_Clase.TablaCompenetes;
using Compiladores_Clase.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Compiladores_Clase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
                DataCache.AgregarLinea("");
                DataCache.AgregarLinea("");
                DataCache.AgregarLinea("5 +5 +3-4/20*100");

                AnalizadorSintactico anaSin = new AnalizadorSintactico();



                try
                {
                    string respuesta = anaSin.Analizar();
                    Console.WriteLine(respuesta);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                ImprimirComponentes(TipoComponente.SIMBOLO);
                ImprimirComponentes(TipoComponente.LITERAL);
                ImprimirComponentes(TipoComponente.DUMMY);
                ImprimirComponentes(TipoComponente.PALABRA_RESERVADA);
                ImprimirErrores(TipoError.LEXICO);
                ImprimirErrores(TipoError.SEMANTICO);
                ImprimirErrores(TipoError.SINTACTICO);
                ImprimirErrores(TipoError.GENERADOR_CODIGO_INTERMEDIO);
                ImprimirErrores(TipoError.OPTIMIZACION);
                ImprimirErrores(TipoError.GENERADOR_CODIGO_FINAL);


                Thread.Sleep(20000);
            }
            private static void ImprimirComponentes(TipoComponente tipo)
            {
                Console.WriteLine("+++++++++++++++ Inicio Componentes" + tipo.ToString() + " +++++++++++++++");
                List<ComponenteLexico> componentes = TablaMaestra.ObtenerTablaMaestra().ObtenerTodosSimbolos(tipo);
                foreach (ComponenteLexico componente in componentes)
                {
                    Console.WriteLine(componente.ToString());
                }
                Console.WriteLine("+++++++++++++++ Fin Componentes    " + tipo.ToString() + "+++++++++++++++\r\n");
            }
            private static void ImprimirErrores(TipoError tipo)
            {
                Console.WriteLine("+++++++++++++++ Inicio Errores" + tipo.ToString() + " +++++++++++++++");
                List<Error> errores = ManejadorErrores.ObtenerManejadorDeErrores().ObtenerErrores(tipo);
                foreach (Error error in errores)
                {
                    Console.WriteLine(error.ToString());
                }
                Console.WriteLine("+++++++++++++++ Fin Errores    " + tipo.ToString() + "+++++++++++++++\r\n");
            }
        }

    
}

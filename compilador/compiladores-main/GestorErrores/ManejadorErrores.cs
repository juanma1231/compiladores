using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_Clase.GestorErrores
{
    public class ManejadorErrores
    {
        private Dictionary<TipoError, List<Error>> errores = new Dictionary<TipoError, List<Error>>();
        private static readonly ManejadorErrores INSTANCIA = new ManejadorErrores();



        private ManejadorErrores()
        {
            Limpiar();
        }



        public static ManejadorErrores ObtenerManejadorDeErrores()
        {
            return INSTANCIA;
        }



        public void Limpiar()
        {
            errores.Add(TipoError.LEXICO, new List<Error>());
            errores.Add(TipoError.SINTACTICO, new List<Error>());
            errores.Add(TipoError.SEMANTICO, new List<Error>());
            errores.Add(TipoError.GENERADOR_CODIGO_INTERMEDIO, new List<Error>());
            errores.Add(TipoError.OPTIMIZACION, new List<Error>());
            errores.Add(TipoError.GENERADOR_CODIGO_FINAL, new List<Error>());



        }



        public void ReportarError(Error error)
        {
            if (error != null)
            {
                errores[error.Tipo].Add(error);
                if (CategoriaErrror.STOPPER.Equals(error.Categoria))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("++++++++++++++ ERROR TIPO STOPPER ++++++++++++++\r\n");
                    sb.Append("Se ha presentado un error de tipo STOPPER dentro del proceso: \r\n").Append(error.Tipo).Append("\r\n");
                    sb.Append("Por favor solucione el problema para llevar a cabo nuevamente el proceso de compilación.\r\n");
                    sb.Append("Verifique la tabla de errores para tener mayor detalle.\r\n");
                    throw new Exception(sb.ToString());
                }
            }
        }
        public bool HayErrores(TipoError tipo)
        {
            return errores[tipo].Count > 0;
        }
        public bool HayErroresAnalisis()
        {
            return HayErrores(TipoError.LEXICO) || HayErrores(TipoError.SINTACTICO) || HayErrores(TipoError.SEMANTICO);
        }
        public bool HayErroresSintesis()
        {
            return HayErrores(TipoError.GENERADOR_CODIGO_INTERMEDIO) || HayErrores(TipoError.OPTIMIZACION) || HayErrores(TipoError.GENERADOR_CODIGO_FINAL);
        }
        public bool HayErroresCompilación()
        {
            return HayErroresAnalisis() || HayErroresSintesis();
        }
        public List<Error> ObtenerErrores(TipoError tipo)
        {
            return errores[tipo];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_22023.cache
{
    public class DataCache
    {
        private static Dictionary<int, Linea> programaFuente
            = new Dictionary<int, Linea>();
        public static void limpiar()
        {
            programaFuente.Clear();
        }
        public static void AgregarLinea(string linea)
        {
            if(linea != null)
            {
                int numeroLinea = ObtenetProximaLinea();
                programaFuente.Add(numeroLinea, Linea.Crear(numeroLinea, linea));
            }
        }

        public static void AgregarLineas(List<string> lineas)
        {
            foreach(string linea in lineas)
            {
                AgregarLinea(linea);
            }
        }

        private static int ObtenetProximaLinea()
        {
            return programaFuente.Count + 1;
        }
        public static Linea ObtenerLinea(int numeroLinea)
        {
            int numeroUltimaLinea = ObtenetProximaLinea();
            Linea lineaRetorno = Linea.Crear(numeroUltimaLinea, "@EOF@");

            if (numeroLinea <= 0)
            {
                throw new Exception("Número de línea inválido");
            }
            else if(numeroLinea <= programaFuente.Count)
            {
                lineaRetorno = programaFuente[numeroLinea];
            }
            return lineaRetorno;
        }
            
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiladores_Clase.GestorErrores
{
    public enum TipoError
    {
        LEXICO,SINTACTICO,SEMANTICO, GENERADOR_INTERMEDIO, OPTIMIZACION, GENERADOR_CODIGO_FINAL,GENERADOR_CODIGO_INTERMEDIO
    }
}

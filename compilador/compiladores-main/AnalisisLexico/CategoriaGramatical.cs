using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_22023.AnalisisLexico
{
    public enum CategoriaGramatical
    {
        IDENTIFICADOR, NUMERO_ENTERO, NUMERO_DECIMAL, SUMA, RESTA, MULTIPLICACION, DIVISION, MODULO, ASIGNACION, PARENTESIS_ABRE, PARENTESIS_CIERRA,
        MAYOR_QUE, MENOR_QUE, IGUAL_QUE, MAYOR_IGUAL_QUE, MENOR_IGUAL_QUE, DIFERENTE_QUE, FIN_DE_ARCHIVO, DEFECTO, NO_DEFINIDA, PALABRA_RESERVADA_CLASS,
        PALABRA_RESERVADA_iF
    }
}

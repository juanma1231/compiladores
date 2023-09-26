using Compilador_22023.AnalisisLexico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador_22023.TablaComponentes
{
    public class TablaPalabrasReservadas
    {
        private Dictionary<string, List<ComponenteLexico>> tabla = new Dictionary<string, List<ComponenteLexico>>();
        private Dictionary<string, ComponenteLexico> palabrasReservadas = new Dictionary<string, ComponenteLexico>();

        public TablaPalabrasReservadas()
        {
            llenarPalabrasReservadas();
        }

        private void llenarPalabrasReservadas()
        {
            palabrasReservadas.Add("class", ComponenteLexico.CrearPalabraReservada(0, 0, "class", CategoriaGramatical.PALABRA_RESERVADA_CLASS));
            palabrasReservadas.Add("if", ComponenteLexico.CrearPalabraReservada(0, 0, "if", CategoriaGramatical.PALABRA_RESERVADA_iF));

        }


        public void Limpiar()
        {
            tabla.Clear();
        }

        public void ComprobarPalabraReservada(ComponenteLexico componete)
        {
            if(componete != null && palabrasReservadas.ContainsKey(componete.Lexema))
            {
                componete.Categoria = palabrasReservadas[componete.Lexema].Categoria;
                componete.Tipo = TipoComponente.PALABRA_RESERVADA;
            }
        }

        public void Agregar(ComponenteLexico componente)
        {
            if (componente != null && TipoComponente.PALABRA_RESERVADA.Equals(componente.Tipo))
            {
                ObtenerSimbolo(componente.Lexema).Add(componente);
            }
        }

        public List<ComponenteLexico> ObtenerSimbolo(string lexema)
        {
            if (!tabla.ContainsKey(lexema))
            {
                tabla.Add(lexema, new List<ComponenteLexico>());
            }

            return tabla[lexema];
        }

        public List<ComponenteLexico> ObtenerTodosSimbolos()
        {
            List<ComponenteLexico> listaRetorno = new List<ComponenteLexico>();
            foreach (List<ComponenteLexico> lista in tabla.Values)
            {
                listaRetorno.AddRange(lista);
            }
            return listaRetorno;
        }
    }
}

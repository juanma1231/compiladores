using Compilador_22023.AnalisisLexico;
using Compilador_22023.cache;
using Compilador_22023.TablaComponentes;

namespace Compilador_22023
{
    class Program
    {
        static void Main(string[] args)
        {
            DataCache.AgregarLinea("");
            DataCache.AgregarLinea("Segunda Línea");
            DataCache.AgregarLinea("5 + 3 + 2 + 1");

            AnalizadorLexico analex = new AnalizadorLexico();
            ComponenteLexico componente = analex.DevolverSiguienteComponente();

            do
            {
                componente = analex.DevolverSiguienteComponente();

            } while (!CategoriaGramatical.FIN_DE_ARCHIVO.Equals(componente.Categoria));

            Imprimir(TipoComponente.SIMBOLO);
            Imprimir(TipoComponente.LITERAL);
            Imprimir(TipoComponente.DUMMY);
            Imprimir(TipoComponente.PALABRA_RESERVADA);

            Thread.Sleep(20000);
        }
        private static void Imprimir(TipoComponente tipo)
        {
            Console.WriteLine("+++++++++++++++ Inicio Componentes" + tipo.ToString() + " +++++++++++++++");
            List<ComponenteLexico> componentes = TablaMaestra.ObtenerTablaMaestra().ObtenerTodosSimbolos(tipo);
            foreach (ComponenteLexico componente in componentes)
            {
                Console.WriteLine(componente.ToString());
            }
            Console.WriteLine("+++++++++++++++ Fin Componentes    " + tipo.ToString() + "+++++++++++++++");
        }
    }
}
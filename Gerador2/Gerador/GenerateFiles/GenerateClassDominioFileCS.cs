using System.Text;

namespace Gerador.GenerateFiles
{
    public class GenerateClassDominioFileCS
    {
        public bool Generate(Table table, Dictionary<string, DirectoryInfo> directorys)
        {

            var directory = directorys.GetValueOrDefault("WebGed.Core.Dominio");


            if (table.Fildes.Count == 2)
            {
                if (table.Fildes.All(f => f.IsForenKey) && table.Fildes.All(f => f.Collum != "Id"))
                {
                    return false;
                }

            }

            GenerateClass(table, directory);
            GenerateIRepositorios(table, directory);
            return true;
        }

        private string GeradorPropriedades(Table table)
        {
            var toReturn = new StringBuilder();

            var filedes = table.Fildes.Where(f => !f.IsForenKey).ToList();
            var filedesForenkey = table.Fildes.Where(f => f.IsForenKey).ToList();


            foreach (var campo in filedes)
            {
                if (campo.Collum != "Id")
                {
                    var stringBase = @$"        public {campo.TypeCshap} {campo.Collum} {{ get; set; }}";

                    toReturn.Append(stringBase);
                    toReturn.Append(Environment.NewLine);
                }
            }

            foreach (var campo in filedesForenkey)
            {
               

                    var stringBase = @$"        public {campo.TypeCshap} {campo.CollumForemKey}Id {{ get; set; }}
        public {campo.TableClassForemKey} {campo.CollumForemKey} {{ get; set; }}";

                    toReturn.Append(stringBase);
                    toReturn.Append(Environment.NewLine);
            }


            foreach (var campo in table.Collection)
            {
                if (campo != "Indices")
                {
                    string? stringBase = @$"        public virtual IList<{table.GetNameClass(campo)}> {campo} {{ get; set; }} = new List<{table.GetNameClass(campo)}>();";


                    toReturn.Append(stringBase);
                    toReturn.Append(Environment.NewLine);
                }
            }

            return toReturn.ToString();
        }
        private void GenerateClass(Table table, DirectoryInfo? directory)
        {
            var propertys = GeradorPropriedades(table);
            var conteudo = @$"
using WebGed.Dominio.Core.Base;

namespace WebGed.Dominio.Core
{{

    public class {table.NameClass} : Entidade
    {{
{propertys}
    }}
}}
";

            StreamWriter file = new(directory.FullName + "\\Core\\"+ table.NameClass +".cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();

        }

        private void GenerateIRepositorios(Table table, DirectoryInfo? directory)
        {

            var conteudo = @$"
using WebGed.Dominio.Core;
using WebGed.Dominio.IRepositorio.Base;

namespace WebGed.Dominio.IRepositorio
{{
    public interface IRepositorio{table.NameClass} : IRepositorio<{table.NameClass}>
    {{
    }}
}}
";

            StreamWriter file = new(directory.FullName + "\\IRepositorio\\" + "IRepositorio" +  table.NameClass + ".cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();
        }


    }
}

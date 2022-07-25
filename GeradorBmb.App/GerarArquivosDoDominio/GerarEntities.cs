namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarEntities
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";

        public GerarEntities(DirectoryInfo directoryInfo, string nameClass)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
        }


        public void Gerar()
        {
            DirectoryInfo directoryDeleteCommand = new(@$"{_directory.FullName}\{_nameClass}\Entities\v1");
            if (!directoryDeleteCommand.Exists)
            {
                directoryDeleteCommand.Create();
            }

            Repository(directoryDeleteCommand);

          

        }


        private void Repository(DirectoryInfo directoryDeleteCommand)
        {
            StreamWriter file = new(@$"{directoryDeleteCommand.FullName}\I{_nameClass}Repository.cs");
            string linhas = @$"
using Bmb.Core.Domain.Entities;

namespace {gerarNamespace(directoryDeleteCommand)};

public class {_nameClass} : Entity
{abre}
    public {_nameClass}()
    {abre}
    {fecha}


{fecha}
";
             file.WriteLine(linhas.Trim());
            file.Close();


        }


        private string gerarNamespace(DirectoryInfo directoryDeleteCommand)
        {
            var split = directoryDeleteCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}

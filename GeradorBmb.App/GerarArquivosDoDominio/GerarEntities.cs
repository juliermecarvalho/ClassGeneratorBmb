namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarEntities
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        private readonly IDictionary<string, string> _propertys;
        const string abre = "{";
        const string fecha = "}";
        private string _nameUsing;


        public GerarEntities(DirectoryInfo directoryInfo, string nameClass, IDictionary<string, string> propertys, string nameUsing)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _propertys = propertys;
            _nameUsing = nameUsing;
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
            StreamWriter file = new(@$"{directoryDeleteCommand.FullName}\{_nameClass}.cs");

            Assistant assistant = new();
            var p = assistant.GerarPropertys(_propertys, true);
            var c = assistant.GerarConstrutor(_nameClass, _propertys);

            string linhas = @$"
using Bmb.Core.Domain.Entities;

namespace {gerarNamespace(directoryDeleteCommand)};

public class {_nameClass} : Entity
{abre}
{p}
{c}
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

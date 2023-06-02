namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarContracts
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";
        private string _nameUsing;


        public GerarContracts(DirectoryInfo directoryInfo, string nameClass, string nameUsing)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _nameUsing = nameUsing;
        }


        public void Gerar()
        {
            DirectoryInfo directoryDeleteCommand = new(@$"{_directory.FullName}\{_nameClass}\Contracts\Repositories\v1");
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
using {_nameUsing}.Domain.{_nameClass}.Queries.GetAll.v1;

namespace {gerarNamespace(directoryDeleteCommand)};

public interface I{_nameClass}Repository : Core.Domain.Contracts.IBaseRepository<Entities.v1.{_nameClass}>
{abre}
    Task RemoveAsync(Entities.v1.{_nameClass} entity, CancellationToken cancellationToken = default);
    Task<IList<Entities.v1.{_nameClass}>> GetAll(GetAll{_nameClass}Query query, CancellationToken cancellationToken = default);   

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

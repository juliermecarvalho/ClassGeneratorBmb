using System.Collections;

namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarDomino
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        private IDictionary<string, string> _propertys;

        const string abre = "{";
        const string fecha = "}";


        public GerarDomino(DirectoryInfo directoryInfo, string nameClass, IDictionary<string, string> propertys)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _propertys = propertys;
        }

        public void Gerar()
        {
            new GerarCreate(_directory, _nameClass).Gerar();
            new GerarDelete(_directory, _nameClass).Gerar();
            new GerarUpdate(_directory, _nameClass).Gerar();
            new GerarContracts(_directory, _nameClass).Gerar();
            new GerarEntities(_directory, _nameClass, _propertys).Gerar();
            new GerarQueriesAll(_directory, _nameClass).Gerar();
            new GerarQueriesOne(_directory, _nameClass).Gerar();

            DirectoryInfo directoryUpdateCommand = new(@$"{_directory.FullName}\{_nameClass}\Commands\Update{_nameClass}Command\v1");
            if (!directoryUpdateCommand.Exists)
            {
                directoryUpdateCommand.Create();
            }

        }


    }
}

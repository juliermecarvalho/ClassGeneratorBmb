using System.Collections;

namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarDomino
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        private IDictionary<string, string> _propertys;
        private string _nameUsing;
        const string abre = "{";
        const string fecha = "}";
        



        public GerarDomino(DirectoryInfo directoryInfo, string nameClass, IDictionary<string, string> propertys, string nameUsing)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _propertys = propertys;
            _nameUsing = nameUsing;
        }

        public void Gerar()
        {
            new GerarCreate(_directory, _nameClass, _propertys, _nameUsing).Gerar();
            new GerarDelete(_directory, _nameClass, _nameUsing).Gerar();
            new GerarUpdate(_directory, _nameClass, _propertys, _nameUsing).Gerar();
            new GerarContracts(_directory, _nameClass, _nameUsing).Gerar();
            new GerarEntities(_directory, _nameClass, _propertys, _nameUsing).Gerar();
            new GerarQueriesAll(_directory, _nameClass, _propertys, _nameUsing).Gerar();
            new GerarQueriesOne(_directory, _nameClass, _propertys, _nameUsing).Gerar();

            DirectoryInfo directoryUpdateCommand = new(@$"{_directory.FullName}\{_nameClass}\Commands\Update\v1");
            if (!directoryUpdateCommand.Exists)
            {
                directoryUpdateCommand.Create();
            }

        }


    }
}

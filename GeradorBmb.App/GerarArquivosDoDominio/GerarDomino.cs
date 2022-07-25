using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorBmb.App.GerarArquivosDoDominio
{
    public class GerarDomino
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";
        public GerarDomino(DirectoryInfo directoryInfo, string nameClass)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
        }

        public void Gerar()
        {
            new GerarCreate(_directory, _nameClass).Gerar();
            new GerarDelete(_directory, _nameClass).Gerar();
            new GerarUpdate(_directory, _nameClass).Gerar();
            new GerarContracts(_directory, _nameClass).Gerar();
            new GerarEntities(_directory, _nameClass).Gerar();
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

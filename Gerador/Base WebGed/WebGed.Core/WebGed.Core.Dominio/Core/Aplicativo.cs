using WebGed.Core.Dominio.Core.Base;

namespace WebGed.Core.Dominio.Core
{

    public class Aplicativo : Entidade
    {
        public string Nome { get; set; }
        public bool ExibeQuantidadeDeDocumentosSemAceso { get; set; }
        public string EmailRemetentePadrao { get; set; }
        public int PrazoEmDiasParaEnvioDeAlertas { get; set; }
        public bool Publicado { get; set; }
        public bool PermiteAssinaturaDigitalNoUploadDeDocumentos { get; set; }
        public bool PermiteSomenteUploadDeArquivosEmFormatoPdf { get; set; }
        public bool NaoPermiteAlteracaoDaOrdemDosDocumentosNaEstruturaDocumental { get; set; }
        public bool DownloadIntegralAscendente { get; set; }
        public bool UtilizaArvoreDeDocumentos { get; set; }

        public int Cliente { get; set; }

    }
}

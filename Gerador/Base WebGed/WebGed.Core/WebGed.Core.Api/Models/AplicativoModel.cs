using WebGed.Core.Api.Models.Base;

namespace WebGed.Core.Api.Models
{
    public class AplicativoModel: ModelBase
    {
        public int Id { get; set; }
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
    }
}

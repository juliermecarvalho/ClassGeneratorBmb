using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebGed.Core.Dominio.Core;

namespace TiibSigner.Repositorio.Maps
{
    public class MapAplicativo : IEntityTypeConfiguration<Aplicativo>
    {
        public void Configure(EntityTypeBuilder<Aplicativo> builder)
        {
            builder.ToTable("Aplicativos");

           
            builder.HasKey(x => x.Id);
            builder.Property(a => a.Nome).IsRequired();
            builder.Property(a => a.ExibeQuantidadeDeDocumentosSemAceso).IsRequired();
            builder.Property(a => a.EmailRemetentePadrao);
            builder.Property(a => a.PrazoEmDiasParaEnvioDeAlertas).IsRequired();
            builder.Property(a => a.Publicado).IsRequired();
  
            builder.Property(a => a.PermiteAssinaturaDigitalNoUploadDeDocumentos).IsRequired();
            builder.Property(a => a.PermiteSomenteUploadDeArquivosEmFormatoPdf).IsRequired();
            builder.Property(a => a.NaoPermiteAlteracaoDaOrdemDosDocumentosNaEstruturaDocumental).IsRequired();
            builder.Property(a => a.DownloadIntegralAscendente).IsRequired();
            builder.Property(a => a.UtilizaArvoreDeDocumentos).IsRequired();
            builder.Property(a => a.Cliente).HasColumnName("Cliente_Id").HasColumnType("int").IsRequired();

        }
    }
}

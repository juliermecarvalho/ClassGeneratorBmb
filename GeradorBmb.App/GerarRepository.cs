namespace GeradorBmb.App
{
    enum Passos
    {
        passo1, passo2, passo3,
    }
    public class GerarRepository
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";
        const string aspas = "\"";
        private Passos _passos = Passos.passo1;
        public GerarRepository(DirectoryInfo directoryInfo, string nameClass)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
        }

        public void Gerar()
        {
            DirectoryInfo directoryDeleteCommand = new(@$"{_directory.FullName}\Repositories\v1");
            if (!directoryDeleteCommand.Exists)
            {
                directoryDeleteCommand.Create();
            }

            Repository(directoryDeleteCommand);

            DirectoryInfo directoryDeleteMappings = new(@$"{_directory.FullName}\Mappings");
            if (!directoryDeleteMappings.Exists)
            {
                directoryDeleteMappings.Create();
            }

            Mappings(directoryDeleteMappings);
            Contexts(_directory);
            Bootstrapper(_directory);


        }

        private void Bootstrapper(DirectoryInfo directory)
        {
            FileInfo fileInfo = new(@$"{directory.FullName}\Bootstrapper.cs");
            _passos = Passos.passo1;
            if (fileInfo.Exists)
            {

                List<string> linhas = new();
                StreamReader rdr = new(fileInfo.FullName);
                string linha;
                while ((linha = rdr.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
                rdr.Close();

                StreamWriter file = new(fileInfo.FullName);
                file.WriteLine($"using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Contracts.Repositories.v1;");
                foreach (var l in linhas)
                {
                    if (_passos == Passos.passo1)
                    {
                        if (l == "    }")
                        {
                            file.WriteLine($"        serviceCollection.AddScoped<I{_nameClass}Repository, {_nameClass}Repository>();");
                            _passos = Passos.passo3;
                        }
                    }

                    file.WriteLine(l);
                }

                file.Flush();
                file.Close();
            }
            else
            {
                StreamWriter file = new(fileInfo.FullName);
                string linhas = @$"
using Bmb.Corporate.Customer.MasterData.Infra.Data.Repositories.v1;
using Microsoft.Extensions.DependencyInjection;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data;

public static class Bootstrapper
{abre}
    public static void AddInfraData(this IServiceCollection serviceCollection)
    {abre}
    {fecha}
{fecha}
";

                file.WriteLine(linhas.Trim());
                file.Close();
                Bootstrapper(directory);
            }

        }

        private void Contexts(DirectoryInfo directory)
        {
            FileInfo fileInfo = new(@$"{directory.FullName}\Contexts\BmbContext.cs");
            if (fileInfo.Exists)
            {
                _passos = Passos.passo1;

                List<string> linhas = new();

                StreamReader rdr = new(fileInfo.FullName);
                string linha;
                while ((linha = rdr.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }

                rdr.Close();

                StreamWriter file = new(fileInfo.FullName);
                file.WriteLine($"using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Entities.v1;");
                foreach (var l in linhas)
                {

                    if (_passos == Passos.passo1)
                    {
                        if (l.Contains("protected override void OnModelCreating(ModelBuilder modelBuilder)"))
                        {
                            file.WriteLine($"    public DbSet<{_nameClass}> {_nameClass}s => Set<{_nameClass}>();");
                            _passos = Passos.passo2;
                        }
                    }

                    if (_passos == Passos.passo2)
                    {
                        if (string.IsNullOrWhiteSpace(l))
                        {
                            //
                            _passos = Passos.passo3;
                        }
                    }

                    file.WriteLine(l);
                }

                file.Flush();
                file.Close();
            }
            else
            {
                StreamWriter file = new(fileInfo.FullName);
                string linhas = @$"

using Microsoft.EntityFrameworkCore;

namespace Bmb.Corporate.Customer.MasterData.Infra.Data.Contexts;

public class BmbContext : DbContext
{abre}
    public BmbContext(DbContextOptions<BmbContext> options) : base(options) {abre} {fecha}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {abre}
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BmbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    {fecha}
{fecha}

";
                file.WriteLine(linhas.Trim());
                file.Close();
                Contexts(directory);
            }
        }

        private void Repository(DirectoryInfo directoryDeleteCommand)
        {
            StreamWriter file = new(@$"{directoryDeleteCommand.FullName}\{_nameClass}Repository.cs");
            string linhas = @$"
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Contracts.Repositories.v1;
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Entities.v1;
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Queries.ReadAll{_nameClass}Query.v1;
using Bmb.Corporate.Customer.MasterData.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace {gerarNamespace(directoryDeleteCommand)};

public class {_nameClass}Repository : BaseRepository<{_nameClass}>, I{_nameClass}Repository
{abre}
     public {_nameClass}Repository(BmbContext context) : base(context) {abre} {fecha}

     public async Task<IList<{_nameClass}>> ReadAll(ReadAll{_nameClass}Query query,
         CancellationToken cancellationToken = default)
     {abre}
         var queryable = DbSet.AsQueryable();

         if (query.OnlyActive)
             queryable = queryable.Where(x => x.IsActive);

         return await queryable.ToListAsync(cancellationToken);
     {fecha}
{fecha}";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void Mappings(DirectoryInfo directoryDeleteCommand)
        {
            StreamWriter file = new(@$"{directoryDeleteCommand.FullName}\{_nameClass}Mappings.cs");
            string linhas = @$"
using Bmb.Corporate.Customer.MasterData.Domain.{_nameClass}.Entities.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {gerarNamespace(directoryDeleteCommand)};

internal class {_nameClass}Mapping : IEntityTypeConfiguration<{_nameClass}>
{abre}


     public void Configure(EntityTypeBuilder<{_nameClass}> builder)
    {abre}
        builder.ToTable({aspas}tbl_{_nameClass.ToLower()}{aspas});

        builder.HasKey(pk => pk.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.IsActive)
            .HasColumnType({aspas}char(1){aspas})
            .HasColumnName({aspas}is_active{aspas})
            .IsRequired();

    {fecha}
{fecha}";
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

namespace GeradorBmb.App
{
    enum Passos
    {
        passo1, passo2, passo3,
    }
    public class GerarRepository
    {
        private DirectoryInfo _directory;
        private string _nameUsing;
        private string _nameClass;
        const string abre = "{";
        const string fecha = "}";
        const string aspas = "\"";
        private Passos _passos = Passos.passo1;
        private readonly IDictionary<string, string> _propertys;

        public GerarRepository(DirectoryInfo directoryInfo, string nameClass, string nameUsing, IDictionary<string, string> propertys)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _nameUsing = nameUsing;
            _propertys = propertys;
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
            //Contexts(_directory);
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

                //tem que acertar esse using
                //var strUsing = $"using {_nameUsing}.Domain.{_nameClass}.Contracts.Repositories.v1;";
                //if (!linhas.Exists(x => x.Trim().Contains(strUsing.Trim())))
                //{
                //    file.WriteLine(strUsing);
                //}

                var strAddScoped = $"        serviceCollection.AddScoped<I{_nameClass}Repository, {_nameClass}Repository>();";
                if (linhas.Exists(x => x.Trim().Contains(strAddScoped.Trim())))
                {
                    _passos = Passos.passo3;
                }

                foreach (var l in linhas)
                {
                    if (_passos == Passos.passo1)
                    {
                        if (l == "    }")
                        {
                            file.WriteLine(strAddScoped);
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
using {_nameUsing}.Infra.Data.Repositories.v1;
using Microsoft.Extensions.DependencyInjection;

namespace {_nameUsing}.Infra.Data;

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
                var strUsing = $"using {_nameUsing.Replace(".Infra", "")}.Domain.{_nameClass}.Entities.v1;";
                if (!linhas.Exists(x => x.Trim().Contains(strUsing.Trim())))
                {
                    file.WriteLine(strUsing);
                }

                var strDbSet = $"    public DbSet<{_nameClass}> {_nameClass}s => Set<{_nameClass}>();";
                if (linhas.Exists(x => x.Trim().Contains(strUsing.Trim())))
                {
                    _passos = Passos.passo3;
                }

               

                foreach (var l in linhas)
                {

                    if (_passos == Passos.passo1)
                    {
                        if (l.Contains("protected override void OnModelCreating(ModelBuilder modelBuilder)"))
                        {
                            file.WriteLine(strDbSet);
                            _passos = Passos.passo2;
                        }
                    }

                    if (_passos == Passos.passo2)
                    {
                        if (string.IsNullOrWhiteSpace(l))
                        {
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

namespace {_nameUsing}.Infra.Data.Contexts;

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
using {_nameUsing.Replace(".Infra", "")}.Domain.{_nameClass}.Contracts.Repositories.v1;
using {_nameUsing.Replace(".Infra", "")}.Domain.{_nameClass}.Entities.v1;
using {_nameUsing.Replace(".Infra", "")}.Domain.{_nameClass}.Queries.GetAll.v1;
using {_nameUsing.Replace(".Infra", "")}.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace {gerarNamespace(directoryDeleteCommand)};

public class {_nameClass}Repository : BaseRepository<{_nameClass}>, I{_nameClass}Repository
{abre}
     public {_nameClass}Repository(BmbContext context) : base(context) {abre} {fecha}

     public async Task<IList<{_nameClass}>> GetAll(GetAll{_nameClass}Query query,
         CancellationToken cancellationToken = default)
     {abre}
         var queryable = DbSet.AsQueryable();

         if (query.IsActive)
             queryable = queryable.Where(x => x.IsActive);

         return await queryable.ToListAsync(cancellationToken);
     {fecha}
{fecha}";
            file.WriteLine(linhas.Trim());
            file.Close();


        }

        private void Mappings(DirectoryInfo directoryDeleteCommand)
        {
            var builder = getProperty();
            StreamWriter file = new(@$"{directoryDeleteCommand.FullName}\{_nameClass}Mappings.cs");
            string linhas = @$"
using {_nameUsing.Replace(".Infra", "")}.Domain.{_nameClass}.Entities.v1;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {gerarNamespace(directoryDeleteCommand)};

internal class {_nameClass}Mapping : BaseConfiguration<{_nameClass}>
{abre}


    public override void Configure(EntityTypeBuilder<{_nameClass}> builder)
    {abre}

        builder.ToTable({aspas}tbl{_nameClass}{aspas}, {aspas}dbo{aspas});

        builder.HasKey(pk => pk.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
{builder}

        builder.Property(p => p.IsActive)
            .HasColumnType({aspas}char(1){aspas})
            .IsRequired();

    {fecha}
{fecha}";
            file.WriteLine(linhas.Trim());
            file.Close();

        }

        private object getProperty()
        {
            string str = "";
            foreach (var property in _propertys)
            {

                str += $"       builder.Property(p => p.{property.Key}); {Environment.NewLine}";
            }

            return str;
        }

        private string gerarNamespace(DirectoryInfo directoryDeleteCommand)
        {
            var split = directoryDeleteCommand.FullName.Split("src");
            string nameSpace = split[1].Replace('\\', '.').Remove(0, 1);

            return nameSpace;
        }
    }
}

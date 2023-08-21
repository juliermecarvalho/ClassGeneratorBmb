using System.Text;

namespace Gerador.GenerateFiles
{
    public class GenerateClassRepositorioFileCS
    {
   
        public void Generate(Table table, Dictionary<string, DirectoryInfo> directorys)
        {
        

            var directory = directorys.GetValueOrDefault("WebGed.Core.Persistencia");

            GenerateClassMaps(table, directory);
            GenerateIRepositorios(table, directory);
            GenerateWGDbContext(table, directory);
        }

        private string GeradorPropriedades(Table table)
        {
            var toReturn = new StringBuilder();
            var filedes = table.Fildes.Where(f => !f.IsForenKey).ToList();

            //filedes.Add(new Filde { Collum = "DataHoraCriacao", TypeCshap = "DateTime" });
            //filedes.Add(new Filde { Collum = "DataHoraAlteracao", TypeCshap = "DateTime" });
            
            var filedesForenkey = table.Fildes.Where(f => f.IsForenKey).ToList();


            foreach (var campo in filedes)
            {
                if (campo.Collum != "Id")
                {
                    var stringBase =
                        @$"            builder.Property(x => x.{campo.Collum}).HasColumnName(""{(campo.Collum)}"")";

                    if (campo.MaximumCharacters != null && campo.MaximumCharacters > 0)
                    {
                        stringBase += $".HasMaxLength({campo.MaximumCharacters})";
                    }

                    if (!campo.IsNull)
                    {
                        stringBase += ".IsRequired()";
                    }

                    stringBase += @$";";
                    toReturn.Append(stringBase);
                    toReturn.Append(Environment.NewLine);
                }
            }

            toReturn.Append(Environment.NewLine);

            foreach (var campo in filedesForenkey)
            {
                
                var stringBase = @$"            builder.Property(x => x.{campo.CollumForemKey}Id).HasColumnName(""{(campo.Collum)}"").IsRequired();
            builder.HasOne(x => x.{campo.CollumForemKey}).WithMany(x => x.{table.Name}).HasForeignKey(x => x.{campo.CollumForemKey}Id);";
                toReturn.Append(stringBase);
                toReturn.Append(Environment.NewLine);

            }

            return toReturn.ToString();
        }
        private void GenerateClassMaps(Table table, DirectoryInfo? directory)
        {
            var propertys = GeradorPropriedades(table);
            var conteudo = @$"
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebGed.Dominio.Core;

namespace WebGed.Persistencia.Maps
{{
    public class Map{table.NameClass} : IEntityTypeConfiguration<{table.NameClass}>
    {{
        public void Configure(EntityTypeBuilder<{table.NameClass}> builder)
        {{
            builder.ToTable(""{(table.Name)}"");
           
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(""id"");
{propertys}
        }}
    }}
}}
";

            StreamWriter file = new(directory.FullName + "\\Maps\\Map" + table.NameClass +".cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();

        }
        private void GenerateIRepositorios(Table table, DirectoryInfo? directory)
        {

            var conteudo = @$"
using WebGed.Dominio.Core;
using WebGed.Dominio.IRepositorio;
using WebGed.Dominio.IRepositorio.Base;
using WebGed.Dominio.Notificacoes.Interfaces;
using WebGed.Persistencia.Repositorio.Base;

namespace WebGed.Persistencia.Repositorio
{{
    public class Repositorio{table.NameClass} : Repositorio<{table.NameClass}>, IRepositorio{table.NameClass}
    {{
        public Repositorio{table.NameClass}(IUnidadeDeTrabalho unidadeDeTrabalho, INotificador notificador) : 
            base(unidadeDeTrabalho, notificador)
        {{
        }}
    }}
}}
";

            StreamWriter file = new(directory.FullName + "\\Repositorio\\" + "Repositorio" +  table.NameClass + ".cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();
        }
        private void GenerateWGDbContext(Table table, DirectoryInfo? directory)
        {

            FileInfo file = new(directory.FullName + "\\Contexto\\WGDbContext" + ".cs");

            if (file.Exists)
            {
                List<string> linhas = new();
                StreamReader rdr = new (file.FullName);
                string line;
                while ((line = rdr.ReadLine()) != null)
                {
                    linhas.Add(line);
                }
                
                rdr.Close();

                var dbSet = $"DbSet<{table.NameClass}> {table.Name}";

                var exite = linhas.Exists(l => l.Contains(dbSet));

                if (exite)
                {
                    return;
                }
                StreamWriter newRdr = new (file.FullName);


                foreach (var l in linhas)
                {
                    if(l.Contains("public WGDbContext(DbContextOptions<WGDbContext> dbContext)"))
                    {
                        newRdr.WriteLine($"        public DbSet<{table.NameClass}> {table.Name} {{ get; set; }}");
                    }
                    newRdr.WriteLine(l);
                }
                newRdr.Flush();
                newRdr.Close();
            }

        }

        public static string ConvertToSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var result = new StringBuilder();
            result.Append(char.ToLower(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    result.Append("_");
                    result.Append(char.ToLower(input[i]));
                }
                else
                {
                    result.Append(input[i]);
                }
            }

            return result.ToString();
        }

    }
}

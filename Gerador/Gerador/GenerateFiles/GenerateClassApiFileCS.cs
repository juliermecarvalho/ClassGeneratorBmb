using System.Text;

namespace Gerador.GenerateFiles
{
    public class GenerateClassApiFileCS
    {
        public bool Generate(Table table, Dictionary<string, DirectoryInfo> directorys)
        {

            var directory = directorys.GetValueOrDefault("WebGed.Api");




            GenerateModels(table, directory);
            GenerateModelsMapper(table, directory);
            GenerateModelsValidator(table, directory);
            GenerateController(table, directory);
            return true;
        }

        public string PrimeiraLetraMinuscula(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return texto;
            }

            string primeiraLetraMinuscula = texto.Substring(0, 1).ToLower();
            string restoTexto = texto.Substring(1);

            return primeiraLetraMinuscula + restoTexto;
        }

        public string ConverterCamelCaseParaSnakeCase(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return texto;
            }

            StringBuilder result = new StringBuilder();
            result.Append(char.ToLower(texto[0]));

            for (int i = 1; i < texto.Length; i++)
            {
                if (char.IsUpper(texto[i]))
                {
                    result.Append('-');
                    result.Append(char.ToLower(texto[i]));
                }
                else
                {
                    result.Append(texto[i]);
                }
            }

            return result.ToString();
        }


        private void GenerateController(Table table, DirectoryInfo? directory)
        {
            var conteudo = @$"
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebGed.Api.Configuration.Validation.ValidateAttribute;
using WebGed.Api.Models;
using WebGed.Api.Models.Base;
using WebGed.Dominio.Core;
using WebGed.Dominio.IRepositorio;

namespace WebGed.Api.Controllers.V1
{{
    [Authorize]
    [ApiController]
    [ApiVersion(""1.0"")]
    [Route(""api/v{{version:apiVersion}}/{ConverterCamelCaseParaSnakeCase(table.NameClass)}"")]
    public class {table.NameClass}Controller : ControllerBase
    {{
        private readonly IRepositorio{table.NameClass} _repositorio{table.NameClass};
        private readonly IMapper _mapper;

        public {table.NameClass}Controller(
            IRepositorio{table.NameClass} repositorio{table.NameClass},
            IMapper mapper)
        {{
            _repositorio{table.NameClass} = repositorio{table.NameClass};
            _mapper = mapper;
        }}

        [HttpGet(""listar"")]
        public async Task<ActionResult<IList<{table.NameClass}Model>>> List()
        {{
            var entidades = await _repositorio{table.NameClass}.ListarAsync();
            return _mapper.Map<List<{table.NameClass}Model>>(entidades);
        }}

        [HttpGet(""{{pagina:int}}/listar"")]
        [PaginaMaiorQueZero]
        public async Task<ActionResult<PaginacaoModel<{table.NameClass}Model>>> List([FromRoute] int pagina)
        {{
            var entidades = await _repositorio{table.NameClass}.ListarAsync(pagina: pagina);
            return _mapper.Map<PaginacaoModel<{table.NameClass}Model>>(entidades);
        }}

        [HttpGet(""{{id:int}}"")]
        [IdMaiorQueZero]
        public async Task<ActionResult<{table.NameClass}Model>> Get([FromRoute] int id)
        {{
            var entidade = await _repositorio{table.NameClass}.ObterAsync(id);
            return _mapper.Map<{table.NameClass}Model>(entidade);
        }}

        [HttpPost]
        [ValidateModel(typeof({table.NameClass}Model))]
        public async Task<ActionResult<{table.NameClass}Model>> Post([FromBody] {table.NameClass}Model model)
        {{
            var entidade = _mapper.Map<{table.NameClass}>(model);
            await _repositorio{table.NameClass}.SalvarAsync(entidade);
            await _repositorio{table.NameClass}.CommitAsync();
            return _mapper.Map<{table.NameClass}Model>(entidade);
        }}


        [HttpDelete(""{{id:int}}"")]
        [IdMaiorQueZero]
        public async Task<ActionResult> Delete(int id)
        {{
            await _repositorio{table.NameClass}.ExluirAsync(id);
            await _repositorio{table.NameClass}.CommitAsync();
            return Ok();
        }}
    }}
}}
";

            StreamWriter file = new(directory.FullName + "\\Controllers\\V1\\" + table.NameClass + "Controller.cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();
        }

        private void GenerateModelsValidator(Table table, DirectoryInfo? directory)
        {
            var propertys = GeradorPropriedadesValidator(table);
            var conteudo = @$"
using FluentValidation;
using WebGed.Api.Models;

namespace WebGed.Api.Configuration.Validation
{{
    public class {table.NameClass}ModelValidator : AbstractValidator<{table.NameClass}Model>
    {{
        public {table.NameClass}ModelValidator()
        {{
{propertys}
        }}
    }}
}}
";

            StreamWriter file = new(directory.FullName + "\\Configuration\\Validation\\" + table.NameClass + "ModelValidator.cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();
        }

        private object GeradorPropriedadesValidator(Table table)
        {
            var toReturn = new StringBuilder();

            foreach (var campo in table.Fildes)
            {
                if (!campo.IsNull && campo.Collum != "Id")
                {
                    var collum = campo.Collum;
                    if (campo.IsForenKey)
                    {
                        collum = campo.Collum.Replace("_", "");
                        var stringBase = @$"
            RuleFor(x => x.{collum}Id)
                .GreaterThan(0)
                .WithMessage(""{{PropertyName}} deve ser maior que zero"");";

                        toReturn.Append(stringBase);
                        toReturn.Append(Environment.NewLine);
                    }
                    else
                    {
                        var stringBase = @$"
            RuleFor(x => x.{collum})
                .NotNull()
                .WithMessage(""O campo {{PropertyName}} é obrigatório"")";

                        if(campo.TypeCshap == "string")
                        {
                            stringBase += @$"
                .NotEmpty()
                .WithMessage(""O campo {{PropertyName}} é obrigatório"")";
                        }

                        toReturn.Append(stringBase + ";");
                        toReturn.Append(Environment.NewLine);

                    }
                }
            }

            return toReturn.ToString();
        }

        private void GenerateModelsMapper(Table table, DirectoryInfo? directory)
        {
            var propertys = string.Empty;//GeradorPropriedadesForMember(table);
            var conteudo = @$"
using AutoMapper;
using WebGed.Api.Models;
using WebGed.Dominio.Core;

namespace WebGed.Api.Configuration.AutoMapper
{{
    public class {table.NameClass}Mapper : Profile
    {{
        public {table.NameClass}Mapper()
        {{
            CreateMap<{table.NameClass}, {table.NameClass}Model>(){propertys}
                .ReverseMap();
        }}
    }}
}}
";

            StreamWriter file = new(directory.FullName + "\\Configuration\\AutoMapper\\" + table.NameClass + "Mapper.cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();
        }

        private string GeradorPropriedadesForMember(Table table)
        {
            var toReturn = new StringBuilder();

            foreach (var campo in table.Fildes)
            {
                if (campo.IsForenKey && campo.Collum != "Id")
                {
                    toReturn.Append(Environment.NewLine);
                    var stringBase = @$"                .ForMember(d => d.{campo.Collum.Replace("_", "")}, opt => opt.MapFrom(o => o.{campo.CollumForemKey}.Id))";
                    toReturn.Append(stringBase);
                }
            }

            return toReturn.ToString();
        }

        private string GeradorPropriedades(Table table)
        {
            var toReturn = new StringBuilder();
            var filedes = table.Fildes.Where(f => !f.IsForenKey).ToList();
            var filedesForenkey = table.Fildes.Where(f => f.IsForenKey).ToList();



            foreach (var campo in filedes)
            {

                if (campo.Collum != "Id")
                {
                    var stringBase = @$"        public {campo.TypeCshap} {campo.Collum} {{ get; set; }}";

                    toReturn.Append(stringBase);
                    toReturn.Append(Environment.NewLine);
                }
            }

            foreach (var campo in filedesForenkey)
            {
                if (campo.Collum != "Id")
                {

                    var stringBase =
                        @$"        public {campo.TypeCshap} {campo.Collum.Replace("_", "")}Id {{ get; set; }}";

                    toReturn.Append(stringBase);
                    toReturn.Append(Environment.NewLine);
                }
            }

            return toReturn.ToString();
        }
        private void GenerateModels(Table table, DirectoryInfo? directory)
        {
            var propertys = GeradorPropriedades(table);
            var conteudo = @$"
using WebGed.Api.Models.Base;

namespace WebGed.Api.Models
{{
    public class {table.NameClass}Model: Model
    {{
{propertys}
    }}
}}

";

            StreamWriter file = new(directory.FullName + "\\Models\\" + table.NameClass +"Model.cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();

        }

       

    }
}

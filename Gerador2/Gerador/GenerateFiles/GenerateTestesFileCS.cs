using System.Text;

namespace Gerador.GenerateFiles
{
    public class GenerateTestesFileCS
    {
        public bool Generate(Table table, Dictionary<string, DirectoryInfo> directorys)
        {

            var directory = directorys.GetValueOrDefault("WebGed.Core.Test");




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
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebGed.Api.Controllers.V1;
using WebGed.Api.Models;
using WebGed.Api.Models.Base;
using WebGed.Dominio.Core;
using WebGed.Dominio.Core.Base;
using WebGed.Dominio.IRepositorio;

namespace WebGed.Test.ControllerTests
{{
    [TestFixture]
    public class {table.NameClass}ControllerTests
    {{
        private Mock<IRepositorio{table.NameClass}> _mockRepositorio{table.NameClass};
        private Mock<IMapper> _mapper;
        private {table.NameClass}Controller _{PrimeiraLetraMinuscula(table.NameClass)}Controller;

        [SetUp]
        public void SetUp()
        {{
            _mockRepositorio{table.NameClass} = new Mock<IRepositorio{table.NameClass}>();
            _mapper = new Mock<IMapper>();

            _{PrimeiraLetraMinuscula(table.NameClass)}Controller = new {table.NameClass}Controller(
                _mockRepositorio{table.NameClass}.Object,
                _mapper.Object);
        }}

        [Test]
        public async Task List_DeveRetornarListaDe{table.NameClass}s()
        {{
            // Arrange
            var entidades = new List<{table.NameClass}> {{ new {table.NameClass} {{ }} }};
            var models = new List<{table.NameClass}Model> {{ new {table.NameClass}Model {{ }} }};

            _mapper.Setup(m => m.Map<List<{table.NameClass}>>(models)).Returns(entidades);
            _mapper.Setup(m => m.Map<List<{table.NameClass}Model>>(entidades)).Returns(models);
            _mockRepositorio{table.NameClass}.Setup(r => r.ListarAsync(null, null)).ReturnsAsync(entidades);

            // Act
            var resultado = await _{PrimeiraLetraMinuscula(table.NameClass)}Controller.List();

            // Assert
            Assert.IsInstanceOf<ActionResult<IList<{table.NameClass}Model>>>(resultado);
            Assert.IsInstanceOf<List<{table.NameClass}Model>>(resultado.Value);
            Assert.AreEqual(entidades.Count, resultado.Value.Count);
            _mockRepositorio{table.NameClass}.Verify(r => r.ListarAsync(null, null), Times.Once);

        }}

        [Test]
        public async Task List_ComPagina_DeveRetornarListaPaginadaDe{table.NameClass}s()
        {{
            // Arrange
            var pagina = 1;
            var entidades = new Paginacao<{table.NameClass}> {{ Lista = new List<{table.NameClass}> {{ new {table.NameClass} {{ }} }} }};
            var models = new PaginacaoModel<{table.NameClass}Model> {{ Lista = new List<{table.NameClass}Model> {{ new {table.NameClass}Model {{ }} }} }};

            _mapper.Setup(m => m.Map<Paginacao<{table.NameClass}>>(models)).Returns(entidades);
            _mapper.Setup(m => m.Map<PaginacaoModel<{table.NameClass}Model>>(entidades)).Returns(models);
            _mockRepositorio{table.NameClass}.Setup(r => r.ListarAsync(pagina, null, null)).ReturnsAsync(entidades);


            // Act
            var resultado = await _{PrimeiraLetraMinuscula(table.NameClass)}Controller.List(pagina);

            // Assert
            Assert.IsInstanceOf<ActionResult<PaginacaoModel<{table.NameClass}Model>>>(resultado);
            Assert.IsInstanceOf<PaginacaoModel<{table.NameClass}Model>>(resultado.Value);
            Assert.AreEqual(entidades.Lista.Count, resultado.Value.Lista.Count);
            _mockRepositorio{table.NameClass}.Verify(r => r.ListarAsync(pagina, null, null), Times.Once);

        }}

        [Test]
        public async Task Get_DeveRetornar{table.NameClass}PorId()
        {{
            // Arrange
            const int id = 1;
            var entidade = new {table.NameClass} {{ Id = 1 }};
            var model = new {table.NameClass}Model {{ Id = 1, }};
            _mapper.Setup(m => m.Map<{table.NameClass}Model>(entidade)).Returns(model);
            _mockRepositorio{table.NameClass}.Setup(r => r.ObterAsync(id)).ReturnsAsync(entidade);

            // Act
            var resultado = await _{PrimeiraLetraMinuscula(table.NameClass)}Controller.Get(id);

            // Assert
            Assert.IsInstanceOf<ActionResult<{table.NameClass}Model>>(resultado);
            Assert.IsInstanceOf<{table.NameClass}Model>(resultado.Value);
            Assert.AreEqual(entidade.Id, resultado.Value.Id);
            _mockRepositorio{table.NameClass}.Verify(r => r.ObterAsync(id), Times.Once);

        }}

        [Test]
        public async Task Post_ComModelValido_DeveSalvar{table.NameClass}Eretornar{table.NameClass}Model()
        {{
            // Arrange
            var model = new {table.NameClass}Model {{ Id = 1, }};
            var entidade = new {table.NameClass} {{ Id = 1, }};
            _mapper.Setup(m => m.Map<{table.NameClass}>(model)).Returns(entidade);
            _mapper.Setup(m => m.Map<{table.NameClass}Model>(entidade)).Returns(model);
            _mockRepositorio{table.NameClass}.Setup(r => r.SalvarAsync(entidade)).Returns(Task.FromResult(entidade));

            // Act
            var resultado = await _{PrimeiraLetraMinuscula(table.NameClass)}Controller.Post(model);

            // Assert            
            Assert.IsInstanceOf<ActionResult<{table.NameClass}Model>>(resultado);
            Assert.IsInstanceOf<{table.NameClass}Model>(resultado.Value);
            Assert.AreEqual(entidade.Id, resultado.Value.Id);
            _mockRepositorio{table.NameClass}.Verify(r => r.SalvarAsync(entidade), Times.Once);
            _mockRepositorio{table.NameClass}.Verify(r => r.CommitAsync(), Times.Once);

        }}


        [Test]
        public async Task Delete_DeveExcluir{table.NameClass}ECommitarAlteracoes()
        {{
            // Arrange
            var id = 1;

            // Act
            var resultado = await _{PrimeiraLetraMinuscula(table.NameClass)}Controller.Delete(id);

            // Assert
            Assert.IsInstanceOf<OkResult>(resultado);
            _mockRepositorio{table.NameClass}.Verify(r => r.ExluirAsync(id), Times.Once);
            _mockRepositorio{table.NameClass}.Verify(r => r.CommitAsync(), Times.Once);
        }}
    }}
}}
";

            StreamWriter file = new(directory.FullName + "\\ControllerTests\\" + table.NameClass + "ControllerTests.cs");
            file.WriteLine(conteudo.Trim());
            file.Flush();
            file.Close();
        }

       

      

       

    }
}

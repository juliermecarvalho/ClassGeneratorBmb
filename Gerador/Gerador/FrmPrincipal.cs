using Gerador.BD;
using Gerador.GenerateFiles;

namespace Gerador
{
    public partial class FrmPrincipal : Form
    {
        private readonly Repositorio _repositorio;
        private readonly GenerateClassDominioFileCS _generateClassFileCS;
        private readonly GenerateClassRepositorioFileCS _generateClassRepositorioFileCS;
        private readonly GenerateClassApiFileCS _generateClassApiFileCS;
        private readonly GenerateTestesFileCS _generateTestesFileCS;

        public FrmPrincipal(Repositorio repositorio, GenerateClassDominioFileCS generateClassFileCS, GenerateClassRepositorioFileCS generateClassRepositorioFileCS, GenerateClassApiFileCS generateClassApiFileCS, GenerateTestesFileCS generateTestesFileCS)
        {
            InitializeComponent();
            _repositorio = repositorio;
            _generateClassFileCS = generateClassFileCS;
            _generateClassRepositorioFileCS = generateClassRepositorioFileCS;
            _generateClassApiFileCS = generateClassApiFileCS;
            _generateTestesFileCS = generateTestesFileCS;
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fileInfo = new(openFileDialog.FileName);
                DirectoryInfo directoryInfo = fileInfo.Directory;
                var directorys = new Dictionary<string, DirectoryInfo>();

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    if (!directory.Name.EndsWith(".vs"))
                    {
                        directorys.Add(directory.Name, directory);
                    }
                }

                var tabelas = _repositorio.ListTable();

                foreach (var tabela in tabelas)
                {
                    Table table = _repositorio.ListTableAndField(tabela);
                    var gerou = _generateClassFileCS.Generate(table, directorys);
                    if (gerou)
                    {
                        _generateClassRepositorioFileCS.Generate(table, directorys);
                        _generateClassApiFileCS.Generate(table, directorys);
                        

                    }

                }

                //Table table = new Table
                //{
                //    Name = "AlertaClientes",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "DescricaoCliente", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                //table.Fildes.Add(new Filde { Collum = "Aplicativo", TypeCshap = "int", IsForenKey = true, TableForemKey = "Aplicativos" });



                //Table table = new Table
                //{
                //    Name = "Usuario",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "CPF", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                //table.Fildes.Add(new Filde { Collum = "Senha", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                //table.Fildes.Add(new Filde { Collum = "Ativo", TypeCshap = "bool", IsNull = false});
                //table.Fildes.Add(new Filde { Collum = "Email", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                //table.Fildes.Add(new Filde { Collum = "TipoUsuario", TypeCshap = "TipoUsuario", IsNull = false });





                //Table table = new Table
                //{
                //    Name = "GrupoAcesso",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                //table.Fildes.Add(new Filde { Collum = "GrupoAcesso", TypeCshap = "IList<Usuario>", IsNull = false });
                //table.Collection.Add("Usuarios");

                //Table table = new Table
                //{
                //    Name = "Cliente",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "NomeFantasia", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "RazaoSocial", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "Email", TypeCshap = "string", IsNull = false, MaximumCharacters = 100 });
                //table.Fildes.Add(new Filde { Collum = "Telefone", TypeCshap = "string", IsNull = false, MaximumCharacters = 20 });
                //table.Fildes.Add(new Filde { Collum = "Cnpj", TypeCshap = "string", IsNull = false, MaximumCharacters = 20 });
                //table.Fildes.Add(new Filde { Collum = "Ativo", TypeCshap = "bool", IsNull = false });
                //table.Collection.Add("Usuarios");


                //Table table = new Table
                //{
                //    Name = "Permissao",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "Descricao", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "Exibir", TypeCshap = "bool", IsNull = false });
                //table.Collection.Add("Papel");


                //Table table = new Table
                //{
                //    Name = "Papel",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "Administrador", TypeCshap = "bool", IsNull = false });
                //table.Collection.Add("Permissao");

                //Table table = new Table
                //{
                //    Name = "FuncaoTemporalidade",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "OrdemTabelaTemporalidade", TypeCshap = "int", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "CodigoClassificacao", TypeCshap = "string", IsNull = false, MaximumCharacters = 50 });

                //table.Collection.Add("SubFuncaoTemporalidade");


                //Table table = new Table
                //{
                //    Name = "SubFuncaoTemporalidade",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "OrdemTabelaTemporalidade", TypeCshap = "int", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "CodigoClassificacao", TypeCshap = "string", IsNull = false, MaximumCharacters = 50 });
                //table.Fildes.Add(new Filde { Collum = "FuncaoTemporalidade", TypeCshap = "long", IsForenKey = true, TableForemKey = "FuncaoTemporalidade" });

                //table.Collection.Add("AtividadeTemporalidade");


                //Table table = new Table
                //{
                //    Name = "AtividadeTemporalidade",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "OrdemTabelaTemporalidade", TypeCshap = "int", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "CodigoClassificacao", TypeCshap = "string", IsNull = false, MaximumCharacters = 50 });
                //table.Fildes.Add(new Filde { Collum = "SubFuncaoTemporalidade", TypeCshap = "long", IsForenKey = true, TableForemKey = "SubFuncaoTemporalidade" });
                //table.Collection.Add("TipoDocumento");



                //Table table = new Table
                //{
                //    Name = "Aplicativo",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "Publicado", TypeCshap = "bool", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "Cliente", TypeCshap = "long", IsForenKey = true, IsNull = false, TableForemKey = "Cliente" });
                //table.Collection.Add("TipoDocumento");


                //Table table = new Table
                //{
                //    Name = "TipoDocumento",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "TextoTarjaMarcaDagua", TypeCshap = "string?", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "Identificador", TypeCshap = "string", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "PrazoGuardaEmAnosFaseCorrente", TypeCshap = "int?", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "PrazoGuardaEmAnosFaseIntermidiario", TypeCshap = "int?", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "OrdemTabelaTemporalidade", TypeCshap = "int?", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "CodigoClassificacao", TypeCshap = "string?", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "Aplicativo", TypeCshap = "long", IsForenKey = true, TableForemKey = "Aplicativo" });
                //table.Fildes.Add(new Filde { Collum = "AtividadeTemporalidade", TypeCshap = "long", IsForenKey = true, IsNull = true, TableForemKey = "AtividadeTemporalidade" });
                ////table.Collection.Add("TipoDocumento");


                //Table table = new Table
                //{
                //    Name = "Campo",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "Ordem", TypeCshap = "int", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "Exibir", TypeCshap = "bool", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "Identificador", TypeCshap = "string", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "TipoCampo", TypeCshap = "int", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "TipoDocumento", TypeCshap = "long", IsForenKey = true, TableForemKey = "TipoDocumento" });



                //Table table = new Table
                //{
                //    Name = "ValorCampo",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Campo", TypeCshap = "long", IsForenKey = true, TableForemKey = "Campo" });
                //table.Fildes.Add(new Filde { Collum = "Documento", TypeCshap = "long", IsForenKey = true, TableForemKey = "Documento" });

                //Table table = new Table
                //{
                //    Name = "ValorMultiplaEscolha",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Descricao", TypeCshap = "string", IsNull = false, MaximumCharacters = 100 });
                //table.Fildes.Add(new Filde { Collum = "Ordem", TypeCshap = "int", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "ValorObjetiva", TypeCshap = "long", IsForenKey = true, TableForemKey = "ValorObjetiva" });
                //table.Fildes.Add(new Filde { Collum = "Campo", TypeCshap = "long", IsForenKey = true, TableForemKey = "Campo" });
                //table.Collection.Add("OpcaoCaracteristica");



                //Table table = new Table
                //{
                //    Name = "Documento",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "PermiteConsultaPublica", TypeCshap = "bool", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "ChaveConsultaPublica", TypeCshap = "string?", IsNull = true, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "DataReferenciaTemporalidade", TypeCshap = "DateTime?", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "FaseTemporalidade", TypeCshap = "int", IsNull = false });


                //table.Fildes.Add(new Filde { Collum = "TipoDocumento", TypeCshap = "long", IsForenKey = true, TableForemKey = "TipoDocumento" });
                //table.Fildes.Add(new Filde { Collum = "CriadoPor", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });
                //table.Fildes.Add(new Filde { Collum = "UsuarioUltimaAlteracao", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });
                //table.Fildes.Add(new Filde { Collum = "UsuariosDocumento", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });

                //table.Collection.Add("ValorCampo");
                //table.Collection.Add("Versao");


                //Table table = new Table
                //{
                //    Name = "Versao",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "NomeArquivoOriginal", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "Extensao", TypeCshap = "string", IsNull = true, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "NumeroDePaginas", TypeCshap = "int", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "TamanhoEmBytes", TypeCshap = "float", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "ConteudoTexto", TypeCshap = "string?", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "ContentType", TypeCshap = "string", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "VersaoAtual", TypeCshap = "bool", IsNull = false });
                //table.Fildes.Add(new Filde { Collum = "HashArquivo", TypeCshap = "string", IsNull = false });


                //table.Fildes.Add(new Filde { Collum = "Repositorio", TypeCshap = "long", IsForenKey = true, TableForemKey = "Repositorio" });
                //table.Fildes.Add(new Filde { Collum = "CriadoPor", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });
                //table.Fildes.Add(new Filde { Collum = "Documento", TypeCshap = "long", IsForenKey = true, TableForemKey = "Documento" });
                //table.Fildes.Add(new Filde { Collum = "CriadoPor", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });
                //table.Fildes.Add(new Filde { Collum = "UsuarioUltimaAlteracao", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });
                //table.Fildes.Add(new Filde { Collum = "UsuariosDocumento", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });

                //table.Collection.Add("Usuario");
                //table.Collection.Add("Versao");



                //Table table = new Table
                //{
                //    Name = "Repositorio",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "long" });
                //table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "CaminhoDiretorioBaseDisco", TypeCshap = "string?", IsNull = true, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "NomeBucket", TypeCshap = "string?", IsNull = true, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "IdChaveDeAcessoBucket", TypeCshap = "string?", IsNull = true, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "SegredoChaveDeAcessoBucket", TypeCshap = "string?", IsNull = true, MaximumCharacters = 250 });
                //table.Fildes.Add(new Filde { Collum = "Ativo", TypeCshap = "bool", IsNull = true });
                //table.Fildes.Add(new Filde { Collum = "TipoRepositorio", TypeCshap = "int", IsNull = true, MaximumCharacters = 250 });


                //table.Fildes.Add(new Filde { Collum = "Repositorio", TypeCshap = "long", IsForenKey = true, TableForemKey = "Repositorio" });
                //table.Fildes.Add(new Filde { Collum = "CriadoPor", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });
                //table.Fildes.Add(new Filde { Collum = "UsuarioUltimaAlteracao", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });
                //table.Fildes.Add(new Filde { Collum = "UsuariosDocumento", TypeCshap = "long", IsForenKey = true, TableForemKey = "Usuario" });

                //table.Collection.Add("Versao");
                //table.Collection.Add("Versao");




                //_generateClassFileCS.Generate(table, directorys);
                //_generateClassRepositorioFileCS.Generate(table, directorys);
                //_generateClassApiFileCS.Generate(table, directorys);
                //_generateTestesFileCS.Generate(table, directorys);

                MessageBox.Show("Fim!");
            }
        }
    }
}
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

                //var tabelas = _repositorio.ListTable();

                //foreach (var tabela in tabelas)
                //{
                //    Table table = _repositorio.ListTableAndField(tabela);
                //    var gerou =_generateClassFileCS.Generate(table, directorys);
                //    if (gerou)
                //    {
                //        // _generateClassRepositorioFileCS.Generate(table, directorys);
                //        //_generateClassApiFileCS.Generate(table, directorys);
                //        _generateClassApiFileCS.Generate(table, directorys);
                //       // _generateTestesFileCS.Generate(table, directorys);

                //    }

                //}

                //Table table = new Table
                //{
                //    Name = "AlertaClientes",
                //};
                //table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "int" });
                //table.Fildes.Add(new Filde { Collum = "DescricaoCliente", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                //table.Fildes.Add(new Filde { Collum = "Aplicativo", TypeCshap = "int", IsForenKey = true, TableForemKey = "Aplicativos" });



                Table table = new Table
                {
                    Name = "Usuario",
                };
                table.Fildes.Add(new Filde { Collum = "Id", TypeCshap = "int" });
                table.Fildes.Add(new Filde { Collum = "CPF", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                table.Fildes.Add(new Filde { Collum = "Nome", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                table.Fildes.Add(new Filde { Collum = "Senha", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                table.Fildes.Add(new Filde { Collum = "Ativo", TypeCshap = "bool", IsNull = false});
                table.Fildes.Add(new Filde { Collum = "Email", TypeCshap = "string", IsNull = false, MaximumCharacters = 150 });
                table.Fildes.Add(new Filde { Collum = "TipoUsuario", TypeCshap = "TipoUsuario", IsNull = false });


                _generateClassFileCS.Generate(table, directorys);
                _generateClassRepositorioFileCS.Generate(table, directorys);
                _generateClassApiFileCS.Generate(table, directorys);
                _generateTestesFileCS.Generate(table, directorys);

                MessageBox.Show("Fim!");
            }
        }
    }
}
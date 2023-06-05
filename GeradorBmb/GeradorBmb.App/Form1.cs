using GeradorBmb.App.GerarArquivosDoDominio;
using GeradorBmb.App.GerarTestes;
using System.Globalization;

namespace GeradorBmb.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
    
            string nameClass = txtNameClass.Text;

            if (string.IsNullOrWhiteSpace(nameClass))
            {
                MessageBox.Show("Nome da classe não pode fazio");
                return;                
            }
            
            nameClass = ConverterToTitleCase(nameClass);
            IDictionary<string, string> propertys = new Dictionary<string, string>();
            IDictionary<string, string> propertys2 = new Dictionary<string, string>
            {
{ "chave", "string"}




            };


            foreach (var item in propertys2)
            {

                var property = ConverterToTitleCase(item.Key);
                var type = item.Value;

                if (!string.IsNullOrWhiteSpace(property))
                {
                    propertys.Add(property, type);
                }

            }


            //foreach (DataGridViewRow item in dgw.Rows)
            //{
            //    if (item.Cells[0].Value != null)
            //    {
            //        if (item.Cells[1].Value == null)
            //        {
            //            MessageBox.Show("Escolher o tipo da propriedade");
            //            return;
            //        }
            //        var property = ConverterToTitleCase(item.Cells[0].Value.ToString());
            //        var type = item.Cells[1].Value.ToString();

            //        if (!string.IsNullOrWhiteSpace(property))
            //        {
            //            propertys.Add(property, type);
            //        }
            //    }
            //}



            OpenFileDialog openFileDialog = new ();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                FileInfo fileInfo = new(openFileDialog.FileName);
                DirectoryInfo diretorio = new ($"{fileInfo.Directory.FullName}");

                foreach (var directoryInfo in diretorio.EnumerateDirectories())
                {
                    if (directoryInfo.Name.EndsWith("api", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var nameUsing = directoryInfo.Name.Replace(".api", string.Empty, StringComparison.CurrentCultureIgnoreCase).Trim();
                        GerarController gerarController = new(directoryInfo, nameClass, nameUsing);
                        gerarController.Gerar();

                    }

                    if (directoryInfo.Name.EndsWith("domain", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var nameUsing = directoryInfo.Name.Replace(".domain", string.Empty, StringComparison.CurrentCultureIgnoreCase).Trim();

                        GerarDomino gerarDomino = new(directoryInfo, nameClass, propertys, nameUsing);
                        gerarDomino.Gerar();
                    }

                    if (directoryInfo.Name.EndsWith("data", StringComparison.CurrentCultureIgnoreCase))
                    {
                        var nameUsing = directoryInfo.Name.Replace(".data", string.Empty, StringComparison.CurrentCultureIgnoreCase).Trim();

                        GerarRepository gerarDomino = new(directoryInfo, nameClass, nameUsing, propertys);
                        gerarDomino.Gerar();
                    }
                }


                DirectoryInfo diretorioTest = new($"{fileInfo.Directory.FullName}/test");

                foreach (var directoryInfo in diretorioTest.EnumerateDirectories())
                {
                    var nameUsing = directoryInfo.Name.Replace(".data", string.Empty, StringComparison.CurrentCultureIgnoreCase).Trim();

                    CreateExampleCommandHandlerTests gerarDomino = new(directoryInfo, nameClass, nameUsing);
                    gerarDomino.Gerar();
                }


                MessageBox.Show("Fim");
            }
        }


        private string ConverterToTitleCase(string str)
        {
            try
            {
                str = str.Trim().ToLower();
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                str = textInfo.ToTitleCase(str).Replace(" ", "");
                return str;
            }
            catch 
            {
                return string.Empty;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNameClass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
using GeradorBmb.App.GerarArquivosDoDominio;
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
            DirectoryInfo diretornoApi;
            DirectoryInfo diretornoDomain;
            DirectoryInfo diretornoData;
            string nameClass = txtNameClass.Text;

            if (string.IsNullOrWhiteSpace(nameClass))
            {
                MessageBox.Show("Nome da classe não pode fazio");
                return;                
            }

            nameClass = ConverterToTitleCase(nameClass);
            Dictionary<string, string> propertys = new Dictionary<string, string>();
            foreach (DataGridViewRow item in dgw.Rows)
            {
                if (item.Cells[0].Value != null)
                {
                    if (item.Cells[1].Value == null)
                    {
                        MessageBox.Show("Escolher o tipo da propriedade");
                        return;
                    }
                    var property = ConverterToTitleCase(item.Cells[0].Value.ToString());
                    var type = item.Cells[1].Value.ToString();

                    if (!string.IsNullOrWhiteSpace(property))
                    {
                        propertys.Add(property, type);
                    }
                }
            }



            OpenFileDialog openFileDialog = new ();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                FileInfo fileInfo = new(openFileDialog.FileName);
                DirectoryInfo diretorio = new ($"{fileInfo.Directory.FullName}/src");

                foreach (var directoryInfo in diretorio.EnumerateDirectories())
                {
                    if (directoryInfo.Name.EndsWith("api", StringComparison.CurrentCultureIgnoreCase))
                    {
  
                        GerarController gerarController = new(directoryInfo, nameClass);
                        //gerarController.Gerar();

                    }

                    if (directoryInfo.Name.EndsWith("domain", StringComparison.CurrentCultureIgnoreCase))
                    {
                        GerarDomino gerarDomino = new(directoryInfo, nameClass);
                        //gerarDomino.Gerar();
                    }

                    if (directoryInfo.Name.EndsWith("data", StringComparison.CurrentCultureIgnoreCase))
                    {
                        GerarRepository gerarDomino = new(directoryInfo, nameClass);
                        //gerarDomino.Gerar();
                    }
                }

                MessageBox.Show("Fim");
            }
        }


        private string ConverterToTitleCase(string str)
        {
            try
            {
                str = str.ToLower();
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
    }
}
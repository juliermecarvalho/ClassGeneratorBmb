//using System;
//using System.Diagnostics;

//class Program
//{
//    static void Main()
//    {
//        try
//        {
//            Process.Start("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", "https://app2.pontomais.com.br/login");
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("Ocorreu um erro ao abrir o Google Chrome: " + ex.Message);
//        }
//    }
//}

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string url = "https://app2.pontomais.com.br/login"; // Substitua pela URL da página
        string usernameOrEmail = "03907601610";
        string password = "dzuT25%!&oa%";

        var httpClient = new HttpClient();

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("login", usernameOrEmail),
            new KeyValuePair<string, string>("password", password)
        });

        HttpResponseMessage response = await httpClient.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Campos preenchidos com sucesso.");
        }
        else
        {
            Console.WriteLine("Ocorreu um erro ao preencher os campos.");
        }
    }
}


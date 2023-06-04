using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Collections.Generic;

IWebDriver Login()
{

    const string url = "https://app2.pontomais.com.br/login";

    var options = new EdgeOptions();
    options.AddArgument("--headless");//--headless --start-maximized
    IWebDriver driver = new EdgeDriver(options);

    driver.Navigate().GoToUrl(url);

    // Preencher os campos de login e senha
    IWebElement loginInput = driver.FindElement(By.CssSelector("input[placeholder='Nome de usuário / cpf / e-mail']"));
    IWebElement passwordInput = driver.FindElement(By.CssSelector("input[type='password']"));

    loginInput.SendKeys("03907601610");
    passwordInput.SendKeys("dzuT25%!&oa%");

    // Aguardar a visibilidade do botão de envio
    By submitButtonSelector = By.CssSelector("button.pm-primary");

    var submitButtons = driver.FindElements(submitButtonSelector);

    foreach (var button in submitButtons)
    {
        if (button.Text.Contains("Entrar"))
        {
             button.Click();           
        }

    }
    Thread.Sleep(10000);

    return driver;
}


bool RegistarPonto(int hours, int minutes = 0)
{
    int hora = DateTime.Now.Hour;
    int minutos = DateTime.Now.Minute;
    int segundos = DateTime.Now.Second;
    TimeSpan tempo = new TimeSpan(hora, minutos, segundos);
    TimeSpan limiteInferior = new TimeSpan(hours, minutes, 0); 
    TimeSpan limiteSuperior = new TimeSpan(hours, (minutes + 10), 0); 

    if(tempo > limiteSuperior)
    {
        return true;
    }


    if (tempo > limiteInferior && tempo < limiteSuperior)
    {
        var driver = Login();
        const string url = "https://app2.pontomais.com.br/registrar-ponto";
        driver.Navigate().GoToUrl(url);
        Thread.Sleep(10000);

        if (ObterHorasUltimoRegistro(driver, hours))
        {
            driver.Quit();
            return true;
        }

        BaterBonto(driver);

        var ultimoRegistro = ObterHorasUltimoRegistro(driver, hours);
        Thread.Sleep(10000);
        driver.Quit();

        return ultimoRegistro;
    }

    return false;
}


bool ObterHorasUltimoRegistro(IWebDriver driver, int horas)
{
    By horasPSelector = By.CssSelector("p.pm-text-dark-gray");
    var textoHoras = driver.FindElements(horasPSelector);
    var txt = textoHoras.LastOrDefault(b => b.Text.Contains("às", StringComparison.CurrentCultureIgnoreCase));
    if (txt != null)
    {
        var text = txt.Text;
        if (text.Contains("às"))
        {
            var tempo = text.Split("às").LastOrDefault();
            return tempo.Contains(horas.ToString());
        }
    }

    return false;
}
void BaterBonto(IWebDriver driver)
{
    By submitButtonSelector = By.CssSelector("button.pm-primary");
    var submitButtons = driver.FindElements(submitButtonSelector);
    var buttons = driver.FindElements(submitButtonSelector);
    var button = buttons.LastOrDefault(b => b.Text.Contains("Bater ponto", StringComparison.CurrentCultureIgnoreCase));

    if (button != null)
    {
        button.Click();

        var caminho = @"C:\Users\ITFOLIV\Downloads\ponto.txt";
        string[] linhasExistentes = File.ReadAllLines(caminho);
        using (StreamWriter streamWriter = new StreamWriter(caminho))
        {
            foreach (string linhaExistente in linhasExistentes)
            {
                streamWriter.WriteLine(linhaExistente);
            }

            streamWriter.WriteLine(DateTime.Now.ToString("G"));
            streamWriter.Flush();
            streamWriter.Close();
            Console.WriteLine("button.Click(): " + DateTime.Now.ToString("G"));

        }
        Thread.Sleep(60000);
    }
}

Random random = new();
int numeroAleatorio = random.Next(20, 35);
Console.WriteLine("minutos escolhidos: " +  numeroAleatorio);

List<int> horarios = new() { 8, 12, 13, 18 };

foreach (var horario in horarios)
{
    while (!RegistarPonto(horario, numeroAleatorio))
    {
        Thread.Sleep(60000);
    }
    numeroAleatorio = 0;
}

Console.WriteLine("FIM!!!");
Console.ReadLine();





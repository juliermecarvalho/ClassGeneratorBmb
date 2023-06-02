using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


IWebDriver Login()
{

    const string url = "https://app2.pontomais.com.br/login";

    // Configurar o driver do Chrome
    ChromeOptions options = new ChromeOptions();
    options.AddArgument("--start-maximized");
    IWebDriver driver = new ChromeDriver(options);

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


bool RegistarPonto(IWebDriver driver, int hours, int minutes)
{

    const string url = "https://app2.pontomais.com.br/registrar-ponto";


    driver.Navigate().GoToUrl(url);

    Thread.Sleep(10000);

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
        BaterBonto(driver);
        return true;
    }
    return false;
}


bool ObterHorasUltimoRegistro(IWebDriver driver, string horas)
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
            return tempo.Contains(horas);
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
    }
}


var driver = Login();

while (!RegistarPonto(driver, 8, 35))
{
    Thread.Sleep(60000);
}
while (!RegistarPonto(driver, 12, 0))
{
    Thread.Sleep(60000);
}
while (!RegistarPonto(driver, 13, 0))
{
    Thread.Sleep(60000);
}
while (!RegistarPonto(driver, 18, 0))
{
    Thread.Sleep(60000);
}


Thread.Sleep(60000);


driver.Quit();
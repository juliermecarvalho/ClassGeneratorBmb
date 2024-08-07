using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

[DllImport("user32.dll")]
static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
const uint MOUSEEVENTF_MOVE = 0x0001;
int intervalInMinutes = 5;
IWebDriver Login()
{
    const string url = "https://app2.pontomais.com.br/login";

    var options = new FirefoxOptions();
    options.SetPreference("geo.prompt.testing", true);
    options.SetPreference("geo.prompt.testing.allow", true);
    IWebDriver driver = new FirefoxDriver(options);
    //else
    //{
    //    var options = new ChromeOptions();
    //    options.AddArgument("--start-maximized");//--headless --start-maximized
    //    driver = new ChromeDriver();
    //}

    try
    {


        driver.Navigate().GoToUrl(url);

        Thread.Sleep(30000);

        // Preencher os campos de login e senha
        IWebElement loginInput = driver.FindElement(By.CssSelector("input[placeholder='Nome de usuário / cpf / e-mail']"));
        IWebElement passwordInput = driver.FindElement(By.CssSelector("input[type='password']"));

        loginInput.SendKeys("03907601610");
        passwordInput.SendKeys("dzuT25%!&oa%");

        // Aguardar a visibilidade do botão de envio
        By submitButtonSelector = By.CssSelector("button.pm-primary");

        //  var button = buttons.LastOrDefault(b => b.Text.Contains("Bater ponto", StringComparison.CurrentCultureIgnoreCase));
        var submitButtons = driver.FindElements(submitButtonSelector);
        var button = submitButtons.LastOrDefault(b => b.Text.Contains("Entrar", StringComparison.CurrentCultureIgnoreCase));

        if (button != null)
        {
            button.Click();
        }

        Thread.Sleep(60000);
    }
    catch (Exception e)
    {
        if (e.Message.Contains("timed out after"))
        {
            Login();
        }
        Sair(driver);

        //PararPulse();
    }

    return driver;
}

bool RegistarPonto(TimeSpan hora)
{
    IWebDriver? driver = null;
    try
    {
        //driver = Login(contador);
        DateTime hoje = DateTime.Now;
        if (hoje.DayOfWeek == DayOfWeek.Saturday || hoje.DayOfWeek == DayOfWeek.Sunday)
        {
            return true;
        }

         // Obter a hora atual do sistema.
        TimeSpan horaAtual = DateTime.Now.TimeOfDay;
        // Calcular o limite superior do intervalo de 20 minutos.
        TimeSpan vinteMinutosDepois = hora.Add(new TimeSpan(0, 20, 0));


        if (horaAtual > vinteMinutosDepois)
        {
            return true;
        }


        if (horaAtual >= hora && horaAtual <= vinteMinutosDepois)
        {
            
            driver = Login();
            Thread.Sleep(30000);

            const string url = "https://app2.pontomais.com.br/registrar-ponto";
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(30000);

            if (ObterHorasUltimoRegistro(driver, hora.Hours))
            {
                Sair(driver);
                driver.Quit();
                return true;
            }

            BaterPonto(driver);
                       

            if (driver != null)
            {
                driver.Quit();
            }
            return true;
        }

        return false;
    }

    catch (Exception e)
    {
        if (driver != null)
        {
            Sair(driver);

            driver.Quit();
        }
        //PararPulse();

        return false;
    }
    finally
    {
        if (driver != null)
        {
            driver.Quit();
        }
    }
}

void Sair(IWebDriver driver)
{
    try
    {
        IWebElement elementUserToClick = driver.FindElement(By.CssSelector(".user-image"));
        if (elementUserToClick != null)
        {
            elementUserToClick.Click();
            Thread.Sleep(2000);

            IWebElement elementSairToClick = driver.FindElement(By.XPath("//div[contains(text(), 'Sair')]"));
            if (elementSairToClick != null)
            {
                elementSairToClick.Click();
                Thread.Sleep(2000);

                IWebElement elementSimToClick = driver.FindElement(By.XPath("//span[contains(text(), 'Sim')]"));
                if (elementSimToClick != null)
                {
                    elementSimToClick.Click();
                    Thread.Sleep(5000);
                    if (driver != null)
                    {
                        driver.Quit();
                    }
                }
            }
        }
    }
    catch (Exception e)
    {
        Thread.Sleep(5000);
        if (driver != null)
        {
            driver.Quit();
        }
    }
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
            var dataHoje = DateTime.Now.ToString("dd/MM");
            var dataUltimoRegistro = text.Split("às").FirstOrDefault();
            if (dataHoje.Trim().Equals(dataUltimoRegistro.Trim()))
            {
                var tempo = text.Split("às").LastOrDefault();

                EscrveNoConsole($"horas ultimo registro: {text}");

                return tempo.Contains(horas.ToString());
            }
        }
    }

    return false;
}

void EscrveNoConsole(string str)
{
    Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
    Console.WriteLine(str);
    Console.WriteLine("--------------------------------------------------------------------------------------------------------------");

}

void BaterPonto(IWebDriver driver)
{
    IWebElement elementToClick = driver.FindElement(By.XPath("//*[contains(text(), 'Utilizar essa localização')]"));
    elementToClick.Click();
    
    Thread.Sleep(3000);

    By submitButtonSelector = By.CssSelector("button.pm-primary");
    var submitButtons = driver.FindElements(submitButtonSelector);
    var buttons = driver.FindElements(submitButtonSelector);
    var button = buttons.LastOrDefault(b => b.Text.Contains("Bater ponto", StringComparison.CurrentCultureIgnoreCase));

#if !DEBUG
    if (button != null)
    {

        button.Click();
        EscrveNoConsole($"ponto batido as {DateTime.Now.ToString("G")} e vai aguardar 20 mim");
        Thread.Sleep(TimeSpan.FromSeconds(10));//10 segundos
        Sair(driver);
        //StartPulse();
        
        Thread.Sleep(TimeSpan.FromMinutes(20));
    }
#endif
}

static bool CheckInternetConnection()
{
    try
    {
        using var client = new WebClient();
        using var stream = client.OpenRead("https://www.google.com");
        return true;
    }
    catch
    {
        return false;
    }
}

while (true)
{

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    // Define the constant for minimizing the console window
    const int SW_MINIMIZE = 6;
    IntPtr hWnd = GetConsoleWindow();
    // Minimize the console window
   // ShowWindow(hWnd, SW_MINIMIZE);

    [DllImport("kernel32.dll")]
    static extern uint SetThreadExecutionState(uint esFlags);

    // Define the constant for preventing sleep
    const uint ES_CONTINUOUS = 0x80000000;
    const uint ES_SYSTEM_REQUIRED = 0x00000001;
    const uint ES_DISPLAY_REQUIRED = 0x00000002;


    List<TimeSpan> horarios = GenerateTimes();

    foreach (var horario in horarios)
    {
        EscrveNoConsole($"Horario: {horario}");
        KeepTeamsOnline();

    }

    foreach (var horario in horarios)
    {
   
        EscrveNoConsole($"proximo horário a bater o ponto => {horario}");
        SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);

        while (!RegistarPonto(horario))
        {
            KeepTeamsOnline();
            Thread.Sleep(TimeSpan.FromMinutes(1));
        }
        KeepTeamsOnline();
        var index = horarios.IndexOf(horario);
        if (index < 3)
        {
#if !DEBUG            
            Thread.Sleep(TimeSpan.FromMinutes(1));
#endif
        }
      
    }

    DateTime hoje = DateTime.Now;
    if (hoje.DayOfWeek == DayOfWeek.Friday)
    {
        Process.Start("shutdown", "/s /t 0");
    }
    
    var now = DateTime.Now;

    if (now.Hour >= 19)
    {
        Console.WriteLine("São 19h ou mais tarde. Fechando a aplicação.");
        Environment.Exit(0); // Encerra a aplicação e fecha o console
    }
    EscrveNoConsole($"Aguardando 1H");
    TimeSpan umaHora = TimeSpan.FromHours(1);
    Thread.Sleep(umaHora);

}

List<TimeSpan> GenerateTimes()
{
    Random random = new Random();

    int minutes = random.Next(0, 30);
    var entradaAm = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 50, 0).AddMinutes(minutes);
    var quatrosHoras = random.Next(235, 245);
    var saidaAm = entradaAm.AddMinutes(quatrosHoras);



    int almoco = random.Next(60, 70);
    var entradaPm = saidaAm.AddMinutes(almoco);

    int saidaminutes = random.Next(0, 5);
    EscrveNoConsole($"minutos a mais => {saidaminutes}");

    var minutosSaida = (480 - quatrosHoras) + saidaminutes;

    var saidaPm = entradaPm.AddMinutes(minutosSaida);

    return [entradaAm.TimeOfDay, saidaAm.TimeOfDay, entradaPm.TimeOfDay, saidaPm.TimeOfDay];
}

void KeepTeamsOnline()
{
    mouse_event(MOUSEEVENTF_MOVE, 1, 0, 0, 0);
    mouse_event(MOUSEEVENTF_MOVE, -1, 0, 0, 0);

}
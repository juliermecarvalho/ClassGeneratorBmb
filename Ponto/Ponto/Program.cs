using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;

IWebDriver Login(int contador, IWebDriver? d = null)
{
    const string url = "https://app2.pontomais.com.br/login";

    IWebDriver driver;

    if (d != null)
    {
        driver = d;
    }
    else
    {
        if (contador % 2 == 0)
        {
            var options = new FirefoxOptions();
            options.AddArgument("--start-maximized");//--headless --start-maximized
            options.SetPreference("geo.enabled", true);
            options.SetPreference("geo.prompt.testing", true);
            options.SetPreference("geo.prompt.testing.allow", true);
            driver = new FirefoxDriver(options);
        }
        else
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");//--headless --start-maximized
            driver = new ChromeDriver();
        }

    }
    try
    {
        if (d == null)
        {
            driver.Navigate().GoToUrl(url);
        }
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

        Thread.Sleep(10000);
    }
    catch (Exception e)
    {
        if (e.Message.Contains("timed out after"))
        {
            Login(contador, driver);
        }
        Sair(driver);

        PararPulse();
    }

    return driver;
}

bool RegistarPonto(int hours, int minutes = 0, int contador = 0)
{
    IWebDriver? driver = null;
    try
    {
      //  driver = Login(contador);
        DateTime hoje = DateTime.Now;
        if (hoje.DayOfWeek == DayOfWeek.Saturday || hoje.DayOfWeek == DayOfWeek.Sunday)
        {
            return true;
        }

        int hora = DateTime.Now.Hour;
        int minutos = DateTime.Now.Minute;
        int segundos = DateTime.Now.Second;
        TimeSpan tempo = new TimeSpan(hora, minutos, segundos);
        TimeSpan limiteInferior = new TimeSpan(hours, minutes, 0);
        TimeSpan limiteSuperior = new TimeSpan(hours, (minutes + 20), 0);

        if (tempo > limiteSuperior)
        {
            return true;
        }

        if (tempo > limiteInferior && tempo < limiteSuperior)
        {
            
            driver = Login(contador);
            const string url = "https://app2.pontomais.com.br/registrar-ponto";
            driver.Navigate().GoToUrl(url);
            Thread.Sleep(15000);

            if (ObterHorasUltimoRegistro(driver, hours))
            {
                driver.Quit();
                return true;
            }

            BaterPonto(driver);

            var ultimoRegistro = ObterHorasUltimoRegistro(driver, hours);
            //if (ultimoRegistro)
            //{
              
            //}
                     

            driver.Quit();
            return ultimoRegistro;
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
        PararPulse();

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
                }
            }
        }
    }
    catch (Exception e)
    {

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
                Console.WriteLine($"horas ultimo registro: {text}");

                return tempo.Contains(horas.ToString());
            }
        }
    }

    return false;
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

    if (button != null)
    {

        button.Click();


        IWebElement element = driver.FindElement(By.XPath("//*[contains(text(), 'Ponto registrado com sucesso!')]"));

        string texto = element.Text;

        // Verifique se o texto contém a frase desejada
        if (texto.Contains("Ponto registrado com sucesso!"))
        {
            Console.WriteLine("O texto 'Ponto registrado com sucesso!' foi encontrado: " + texto);
        }
        else
        {
            Console.WriteLine("O texto 'Ponto registrado com sucesso!' não foi encontrado.");
        }

        Console.WriteLine("ponto batido vai aguardar 5 mim");
        Thread.Sleep(300000); //5mim
        Sair(driver);
        StartPulse();
        

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

        }
        Thread.Sleep(60000);
    }
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


    Random random = new();
    int init = random.Next(35, 40);
    int fim = random.Next(40, 50);
    int numeroAleatorio =  random.Next(45, 55);
    int horaInicial = 8;

    Console.WriteLine("minuto escolhido: " + numeroAleatorio);
    List<int> horarios = new() { horaInicial, 12, 13, 18 };


    foreach (var horario in horarios)
    {
        if (horario == 18)
        {
            numeroAleatorio = random.Next(5, 15);
        }
        Console.WriteLine($"Horario: {horario}:{numeroAleatorio}");
        StartPulse();
        var contador = 2;
        while (!RegistarPonto(horario, numeroAleatorio, contador))
        {
            //contador++;
            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);
            var hora = DateTime.Now.Hour;
#if !DEBUG
            if (horarios.Contains(hora))
            {
                Thread.Sleep(1000);//1segundos
            }
            else
            {
                Thread.Sleep(180000);//3min
            }
#endif
        }

        contador = 2;
        numeroAleatorio = 0;
    }

    DateTime hoje = DateTime.Now;
    if (hoje.DayOfWeek == DayOfWeek.Friday)
    {
        Process.Start("shutdown", "/s /t 0");
    }
    SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);
    Thread.Sleep(900000);//15mim



}




Console.WriteLine("FIM!!!");
Console.ReadLine();

static void PararPulse()
{
    try
    {
        string serviceName = "PulseSecureService"; // Nome do serviço

        ServiceController sc = new ServiceController(serviceName);


        if (sc.Status == ServiceControllerStatus.Running)
        {
            Console.WriteLine($"Parando o serviço {serviceName}...");
            sc.Stop();
            sc.WaitForStatus(ServiceControllerStatus.Stopped);
            Console.WriteLine($"Status do serviço {serviceName} após parar: {sc.Status}");
        }
    }
    catch (Exception)
    {

    }
}


static void StartPulse()
{
    try
    {
        string serviceName = "PulseSecureService"; // Nome do serviço

        ServiceController sc = new ServiceController(serviceName);


        if (sc.Status == ServiceControllerStatus.Stopped)
        {
            Console.WriteLine($"Iniciando o serviço {serviceName}...");
            sc.Start();
            sc.WaitForStatus(ServiceControllerStatus.Running);
            Console.WriteLine($"Status do serviço {serviceName} após iniciar: {sc.Status}");
        }
    }
    catch (Exception)
    {

    }
}
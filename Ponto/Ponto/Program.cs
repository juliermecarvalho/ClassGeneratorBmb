using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System.Diagnostics;
using System.Runtime.InteropServices;

IWebDriver Login(int contador)
{
    IWebDriver driver;
    const string url = "https://app2.pontomais.com.br/login";

    if (contador % 2 == 0)
    {
        var options = new EdgeOptions();
        options.AddArgument("--start-maximized");//--headless --start-maximized
        driver = new EdgeDriver(options);
    }
    else
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");//--headless --start-maximized
        driver = new ChromeDriver(options);
    }
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


bool RegistarPonto(int hours, int minutes = 0, int contador = 0)
{
    IWebDriver? driver = null;
    try
    {
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
            Thread.Sleep(10000);
            driver.Quit();

            return ultimoRegistro;
        }

        return false;
    }
    catch (Exception e)
    {
        if (driver != null)
        {
            driver.Quit();
        }
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
                return tempo.Contains(horas.ToString());
            }
        }
    }

    return false;
}
void BaterPonto(IWebDriver driver)
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


//while (true)
//{


    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    // Define the constant for minimizing the console window
    const int SW_MINIMIZE = 6;
    IntPtr hWnd = GetConsoleWindow();
    // Minimize the console window
    ShowWindow(hWnd, SW_MINIMIZE);

    [DllImport("kernel32.dll")]
    static extern uint SetThreadExecutionState(uint esFlags);

    // Define the constant for preventing sleep
    const uint ES_CONTINUOUS = 0x80000000;
    const uint ES_SYSTEM_REQUIRED = 0x00000001;
    const uint ES_DISPLAY_REQUIRED = 0x00000002;


    Random random = new();
    int init = random.Next(20, 29);
    int fim = random.Next(30, 35);
    int numeroAleatorio = random.Next(init, fim);
    int horaInicial = 8;

    Console.WriteLine("minuto escolhido: " + numeroAleatorio);
    List<int> horarios = new() { horaInicial, 12, 13, 18 };


    foreach (var horario in horarios)
    {
        //if (horario == 18)
        //{
        //    numeroAleatorio = random.Next(10, 15);
        //}

        var contador = 1;
        while (!RegistarPonto(horario, numeroAleatorio, contador))
        {
            Thread.Sleep(60000);
            contador++;
            SetThreadExecutionState(ES_CONTINUOUS | ES_DISPLAY_REQUIRED | ES_SYSTEM_REQUIRED);
        }
        Console.WriteLine($"Horario: {horario}:{numeroAleatorio}");

        contador = 1;
        numeroAleatorio = 0;
    }

    DateTime hoje = DateTime.Now;
    if (hoje.DayOfWeek == DayOfWeek.Friday)
    {
        Process.Start("shutdown", "/s /t 0");
    }

    Thread.Sleep(900000);



//}




Console.WriteLine("FIM!!!");
Console.ReadLine();





using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SEI.Desktop.Models;
using SEI.Desktop.SeleniumUtils;
using System;
using System.Threading;

namespace SEI.Desktop.PageObjects
{
    public class PaginaSEI
    {
        private readonly AppSettings _appSettings;
        private IWebDriver _driver;

        public PaginaSEI(AppSettings appSettings, bool visualizarNoNavegador)
        {
            _appSettings = appSettings;

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--log-level=3");
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("test-type");
            //chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument("no-sandbox");

            if (!visualizarNoNavegador)
            {
                chromeOptions.AddArgument("--headless");
            }

            var chromeDriverService = ChromeDriverService.CreateDefaultService(System.Reflection.Assembly.GetExecutingAssembly().Location.Split("SEIDesktop.dll")[0]);
            chromeDriverService.HideCommandPromptWindow = true;

            _driver = new ChromeDriver(chromeDriverService, chromeOptions);

            //var firefoxOptions = new FirefoxOptions();
            //firefoxOptions.AddArgument("--log-level=3");
            //firefoxOptions.AddArgument("--disable-extensions");
            //firefoxOptions.AddArgument("test-type");
            ////chromeOptions.AddArgument("--ignore-certificate-errors");
            ////firefoxOptions.AddArgument("no-sandbox");

            //if (!visualizarNoNavegador)
            //{
            //    firefoxOptions.AddArgument("--headless");
            //}

            //var firefoxDriverService = FirefoxDriverService.CreateDefaultService(System.Reflection.Assembly.GetExecutingAssembly().Location.Split("SEIDesktop.dll")[0]);
            //firefoxDriverService.HideCommandPromptWindow = true;

            //_driver = new FirefoxDriver(firefoxDriverService, firefoxOptions);

        }
        public void CarregarPaginaInicial()
        {
            _driver.LoadPage(TimeSpan.FromSeconds(60), _appSettings.UrlPaginaSEI);
        }
        public void EfetuarLogin()
        {
            _driver.SetText(By.Id("txtUsuario"), "gemed");
            _driver.SetText(By.Id("pwdSenha"), "81828384");
            _driver.SetValueSelectElement(By.Id("selOrgao"), "128");
            _driver.Submit(By.Id("sbmLogin"));
        }
        public void DetalharUltimoProcesso()
        {
            var elementosProcessos = _driver.FindElements(By.CssSelector("[id^='P']"));
            var processo = _driver.FindElement(By.CssSelector("#" + elementosProcessos[elementosProcessos.Count - 1].GetAttribute("id") + " > td:nth-child(3) > a"));
            processo.Click();
        }
        public void IrParaUltimaPagina(int quantidadeProcessos)
        {
            if (quantidadeProcessos >= 200 && quantidadeProcessos <= 300)
            {

                if (_driver.IsElementPresent(By.Id("lnkRecebidosProximaPaginaSuperior")))
                {
                    _driver.ClickElement(By.Id("lnkRecebidosProximaPaginaSuperior"));
                }

            }
            else if (quantidadeProcessos > 300)
            {
                if (_driver.IsElementPresent(By.Id("lnkRecebidosUltimaPaginaSuperior")))
                {
                    _driver.ClickElement(By.Id("lnkRecebidosUltimaPaginaSuperior"));
                }
            }
        }
        public void IrParaControleProcessos()
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            _driver.ClickElement(By.Id("lnkControleProcessos"));
        }
        public int DetalharProcessosPorMarcador(string marcador)
        {
            var numeroProcessos = "";

            var elementosMarcadores = _driver.FindElements(By.ClassName("InfraImg"));
            var elementosNumerosProcessos = _driver.FindElements(By.ClassName("ancoraPadraoAzul"));

            for (int i = 0; i < elementosMarcadores.Count; i++)
            {
                if (elementosMarcadores[i].GetAttribute("src").Contains(marcador))
                {
                    numeroProcessos = elementosNumerosProcessos[i].Text;
                    elementosNumerosProcessos[i].Click();
                    break;
                }
            }

            numeroProcessos = numeroProcessos.Replace(".", "");

            return int.Parse(numeroProcessos);
        }
        public void VerPorMarcadores()
        {
            _driver.ClickElement(By.Id("ancVisualizacao2"));
        }
        public void FecharPopUp()
        {
            _driver.ClosePopUp();

        }
        public void Autenticar()
        {

            if (_driver.WindowHandles.Count == 1)
            {
                throw new Exception("Erro: Verifique se o processo mais antigo é sigiloso\\restrito.");
            }

            var popUpAutenticacao = _driver.SwitchTo().Window(_driver.WindowHandles[1]);

            if (popUpAutenticacao != null)
            {
                _driver.SetText(By.Id("pwdSenha"), "81828384");
                _driver.FindElement(By.Id("pwdSenha")).SendKeys(Keys.Enter);
            }

            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            //_driver.SwitchTo().DefaultContent();
        }
        public void Credenciar(string nome)
        {
            var jaFoiCredenciado = false;
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(6));


            //Thread.Sleep(1000);
            //_driver.SwitchTo().Frame("ifrVisualizacao").ClickElement(By.CssSelector("#divArvoreAcoes > a:nth-child(2) > img"));

            _driver.SwitchTo().Frame(1);
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector("#divArvoreAcoes > a:nth-child(2) > img")));
            _driver.ClickElement(By.CssSelector("#divArvoreAcoes > a:nth-child(2) > img"));

            while (!jaFoiCredenciado)
            {
                try
                {
                    _driver.FindElement(By.Id("txtUsuario")).Clear();

                    _driver.SetText(By.Id("txtUsuario"), nome);
                    Thread.Sleep(2000);
                    _driver.FindElement(By.Id("txtUsuario")).SendKeys(Keys.ArrowDown + Keys.Enter);

                    wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("btnConceder")));
                    _driver.ClickElement(By.Id("btnConceder"));
                    Thread.Sleep(500);

                    try
                    {
                        var alert = _driver.SwitchTo().Alert();

                        if (alert != null && alert.Text.Contains($"concedeu acesso ao usuário"))
                        {
                            alert.Accept();
                            jaFoiCredenciado = true;
                            break;
                        }
                    }
                    catch (NoAlertPresentException)
                    {
                        jaFoiCredenciado = true;
                        break;
                    }

                }
                catch (UnhandledAlertException ex)
                {
                    if (ex.AlertText.Contains($"concedeu acesso ao usuário"))
                    {
                        jaFoiCredenciado = true;
                    }
                }
                catch (Exception)
                {
                    jaFoiCredenciado = false;
                    continue;
                }
            }

            //var idProcedimento = _driver.FindElement(By.Id("ifrArvore")).GetProperty("src").Split("&id_procedimento=")[1].Substring(0, 8);

            _driver.SwitchTo().DefaultContent();

        }
        public void EnviarParaMarcador(string marcador)
        {
            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame(0);

            var javascript = (IJavaScriptExecutor)_driver;
            javascript.ExecuteScript("$('[id^=anchorMC]')[0].click();");

            var valorMarcador = "";

            _driver.SwitchTo().DefaultContent();
            _driver.SwitchTo().Frame(1);

            var elementosOptionsImage = _driver.FindElements(By.ClassName("dd-option-image"));
            var elementosOptionsValue = _driver.FindElements(By.ClassName("dd-option-value"));

            for (int i = 0; i < elementosOptionsImage.Count; i++)
            {
                if (elementosOptionsImage[i].GetAttribute("src").Contains(marcador))
                {
                    valorMarcador = elementosOptionsValue[i + 1].GetAttribute("value");
                    break;
                }
            }

            _driver.SetValueInputHidden(By.Id("hdnIdMarcador"), valorMarcador);
            _driver.ClickElement(By.Name("sbmGerenciarMarcador"));

            _driver.SwitchTo().DefaultContent();
        }
        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }
        public void Descredenciar()
        {
            _driver.SwitchTo().DefaultContent();
            _driver.Navigate().Refresh();
            _driver.SwitchTo().Frame(1);
            Thread.Sleep(500);


            //var x = _driver.FindElement(By.CssSelector("#divArvoreAcoes > a:nth-child(3) > img"));

            //x.Click();

            //renunciarCredencial();

            var javascript = (IJavaScriptExecutor)_driver;
            javascript.ExecuteScript("renunciarCredencial();");

            var alert = _driver.SwitchTo().Alert();

            if (alert != null && alert.Text.Contains($"Confirma renúncia"))
            {
                alert.Accept();
            }

            _driver.SwitchTo().DefaultContent();
        }
    }
}
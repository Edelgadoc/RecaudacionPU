using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using RecaudacionPU.PaginaObjeto;
using System;
using System.Configuration;
using System.Threading;

namespace RecaudacionPU
{
    /*
     * Casos de test para verificar una consulta estandar
     */

    [TestFixture]
    public class Test_01_Pagos
    {
        public string PaginaURL = ConfigurationManager.AppSettings["Url"];
        protected IWebDriver Driver;
        protected bool estado;
        private const string CHROME = "Chrome";
        const string FIREFOX = "Firefox";
        const string EDGE = "Edge";

        private IWebDriver getDriver()
        {
            var withBrowser = ConfigurationManager.AppSettings["WithBrowser"];
            var browser = ConfigurationManager.AppSettings["Browser"];

            DriverService driverService;
            switch (browser)
            {
                case CHROME:
                    driverService = ChromeDriverService.CreateDefaultService();

                    break;
                case FIREFOX:
                    driverService = FirefoxDriverService.CreateDefaultService();
                    break;
                case EDGE:
                    driverService = EdgeDriverService.CreateDefaultService();
                    break;
                default:
                    driverService = ChromeDriverService.CreateDefaultService();
                    break;

            }


            if (withBrowser == "false")
            {
                switch (browser)
                {
                    case CHROME:

                        ChromeOptions optionsChrome = new ChromeOptions();
                        optionsChrome.AddArguments("-headless");
                        optionsChrome.AddArguments("window-size=1920,1080");
                        Driver = new ChromeDriver((ChromeDriverService)driverService, optionsChrome);
                        break;
                    case FIREFOX:
                        FirefoxOptions optionsFirefox = new FirefoxOptions();
                        optionsFirefox.AddArguments("-headless");
                        optionsFirefox.AddArguments("window-size=1920,1080");
                        Driver = new FirefoxDriver((FirefoxDriverService)driverService, optionsFirefox);
                        break;
                    case EDGE:
                        EdgeOptions optionEdge = new EdgeOptions();
                        optionEdge.AddArguments("-headless");
                        optionEdge.AddArguments("window-size=1920,1080");
                        Driver = new EdgeDriver((EdgeDriverService)driverService, optionEdge);
                        break;
                    default:
                        throw new Exception("Browser no configurado");


                }


            }
            else
            {
                switch (browser)
                {
                    case CHROME:
                        Driver = new ChromeDriver((ChromeDriverService)driverService);
                        break;
                    case FIREFOX:
                        Driver = new FirefoxDriver((FirefoxDriverService)driverService);
                        break;
                    case EDGE:
                        Driver = new EdgeDriver((EdgeDriverService)driverService);
                        break;
                    default:
                        throw new Exception("Browser no configurado");


                }

            }



            return Driver;
        }

        [SetUp]
        public void InicioTest()
        {


            Driver = getDriver();

            Driver.Navigate().GoToUrl(PaginaURL);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        [Test]
        [Ignore("Egnorar")]
        public void PagoUnSuministro()
        {
            SuministroPagina suministroPagina = new SuministroPagina(Driver);
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro("25152307");

            estado = listarDeudaPagina.DeudaPresente();
            if (estado)
            {
                listarDeudaPagina.MarcarChecks();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                ConfirmaSolicitudPagina confirmaSolicitudPagina = listarDeudaPagina.ClickSiguiente();
                estado = confirmaSolicitudPagina.PedidoPresente();
                if (estado)
                {
                    PagarVisaPagina pagarVisaPagina = confirmaSolicitudPagina.ClickConfirmaPagar();
                    estado = pagarVisaPagina.NumeroPresente();
                    if (estado)
                    {
                        pagarVisaPagina.RealizarPago();
                        estado = pagarVisaPagina.ObtenerResultado(PaginaURL);

                        if (estado)
                            Assert.IsTrue(estado);
                        else
                            Assert.Fail();
                    }
                    else
                        Assert.Fail("Pagina PagarVisa no esta presente");
                }
                else
                    Assert.Fail("Página ConfirmaSolicitud no esta presente");
            }
            else
                Assert.Fail("Página ListarDeuda no esta presente (verifique deuda)");
        }


        [TestCase("25308533")]
        [TestCase("25514587")]
        [Ignore("Este no va")]
        public void PagoSecuencial(string NroServicio)
        {
            SuministroPagina suministroPagina = new SuministroPagina(Driver);
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro(NroServicio);

            estado = listarDeudaPagina.DeudaPresente();
            if (estado)
            {
                listarDeudaPagina.MarcarChecks();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                ConfirmaSolicitudPagina confirmaSolicitudPagina = listarDeudaPagina.ClickSiguiente();
                estado = confirmaSolicitudPagina.PedidoPresente();
                if (estado)
                {
                    PagarVisaPagina pagarVisaPagina = confirmaSolicitudPagina.ClickConfirmaPagar();
                    estado = pagarVisaPagina.NumeroPresente();
                    if (estado)
                    {
                        pagarVisaPagina.RealizarPago();
                        estado = pagarVisaPagina.ObtenerResultado(PaginaURL);

                        if (estado)
                        {
                            Assert.IsTrue(estado);
                            Thread.Sleep(TimeSpan.FromSeconds(1));
                            Driver.Quit();
                        }
                        else
                            Assert.Fail();
                    }
                    else
                        Assert.Fail("Pagina PagarVisa no esta presente");
                }
                else
                    Assert.Fail("Página ConfirmaSolicitud no esta presente");
            }
            else
                Assert.Fail("Página ListarDeuda no esta presente (verifique deuda)");
        }



        [TestCase("25106592")]
        [TestCase("25097902")]
        [TestCase("25539595")]
        [TestCase("25931347")]
        [TestCase("25158294")]
        [TestCase("25155158")]
        [TestCase("26384057")]
        [TestCase("25150723")]
        [TestCase("35679473")]
        [TestCase("25418310")]

        [TestCase("26161777")]


        public void PagoUnMesDeuda(string NroServicio)
        {
            SuministroPagina suministroPagina = new SuministroPagina(Driver);
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro(NroServicio);

            estado = listarDeudaPagina.DeudaPresente();
            if (estado)
            {
                listarDeudaPagina.MarcarCheckMes();
                Thread.Sleep(TimeSpan.FromSeconds(5));
                ConfirmaSolicitudPagina confirmaSolicitudPagina = listarDeudaPagina.ClickSiguiente();
                estado = confirmaSolicitudPagina.PedidoPresente();
                if (estado)
                {
                    PagarVisaPagina pagarVisaPagina = confirmaSolicitudPagina.ClickConfirmaPagar();
                    estado = pagarVisaPagina.NumeroPresente();
                    if (estado)
                    {
                        pagarVisaPagina.RealizarPago();
                        estado = pagarVisaPagina.ObtenerResultado(PaginaURL);

                        if (estado)
                        {
                            Assert.IsTrue(estado);
                            Thread.Sleep(TimeSpan.FromSeconds(1));
                            Driver.Quit();
                        }
                        else
                            Assert.Fail();
                    }
                    else
                        Assert.Fail("Pagina PagarVisa no esta presente");
                }
                else
                    Assert.Fail("Página ConfirmaSolicitud no esta presente");
            }
            else
                Assert.Fail("Página ListarDeuda no esta presente (verifique deuda)");
        }


        [TearDown]
        public void FinalTest()
        {
            //if (Driver != null )
            //    Driver.Quit();
        }

    }
}

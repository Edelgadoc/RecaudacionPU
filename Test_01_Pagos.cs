using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
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

        [SetUp]
        public void InicioTest()
        {
            //ChromeOptions options = new ChromeOptions();
            //options.BinaryLocation = @"C:\Program Files\Google\Chrome Beta\Application\chrome.exe";
            //Driver = new ChromeDriver();
            Driver = new FirefoxDriver();
            Driver.Navigate().GoToUrl(PaginaURL);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        [Test]
        public void PagoUnSuministro()
        {
            SuministroPagina suministroPagina = new SuministroPagina(Driver);
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro("26618423");

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


        [TestCase("38493804")]
        [TestCase("35456057")]
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


        [TestCase("26555360")]
        [TestCase("36908436")]
        [TestCase("26653173")]
        [TestCase("26561788")]
        [TestCase("26562810")]
        [TestCase("26377464")]
        [TestCase("26658410")]
        [TestCase("26657225")]
        [TestCase("25816345")]
        [TestCase("25769942")]
        public void PagoUnMesDeuda(string NroServicio)
        {
            SuministroPagina suministroPagina = new SuministroPagina(Driver);
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro(NroServicio);

            estado = listarDeudaPagina.DeudaPresente();
            if (estado)
            {
                listarDeudaPagina.MarcarCheckMes();
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


        [TearDown]
        public void FinalTest()
        {
            //if (Driver != null )
            //    Driver.Quit();
        }

    }
}

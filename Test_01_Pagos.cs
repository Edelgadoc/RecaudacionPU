using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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
            Driver = new ChromeDriver();
            Driver.Navigate().GoToUrl(PaginaURL);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
        }

        [Test]
        public void PagoUnsuministro()
        {
            SuministroPagina suministroPagina = new SuministroPagina(Driver);
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro("5329723");

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
                    if (!estado)
                        TestContext.Out.WriteLine("Pagina PagarVisa no esta presente");

                    pagarVisaPagina.RealizarPago();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Assert.IsTrue(estado);
                }
                else
                {
                    TestContext.Out.WriteLine("Página ConfirmaSolicitud no esta presente");
                    Assert.Fail();
                }
            }
            else
            {
                TestContext.Out.WriteLine("Página ListarDeuda no esta presente (verifique deuda)");
                Assert.Fail();
            }
        }

        [TestCase("26369220")]
        [TestCase("26369355")]
        [TestCase("26369346")]
        [TestCase("65581468")]
        [TestCase("65581379")]
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
                    if (!estado)
                        TestContext.Out.WriteLine("Pagina PagarVisa no esta presente");

                    pagarVisaPagina.RealizarPago();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Assert.IsTrue(estado);
                }
                else
                {
                    TestContext.Out.WriteLine("Página ConfirmaSolicitud no esta presente");
                    Assert.Fail();
                }
            }
            else
            {
                TestContext.Out.WriteLine("Página ListarDeuda no esta presente (verifique deuda)");
                Assert.Fail();
            }
        }

        [TestCase("27191832")]
        [TestCase("27072330")]
        [TestCase("27182815")]
        [TestCase("27161646")]
        [TestCase("27156388")]
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
                    if (!estado)
                        TestContext.Out.WriteLine("Pagina PagarVisa no esta presente");

                    pagarVisaPagina.RealizarPago();
                    Thread.Sleep(TimeSpan.FromSeconds(1));
                    Assert.IsTrue(estado);
                }
                else
                {
                    TestContext.Out.WriteLine("Página ConfirmaSolicitud no esta presente");
                    Assert.Fail();
                }
            }
            else
            {
                TestContext.Out.WriteLine("Página ListarDeuda no esta presente (verifique deuda)");
                Assert.Fail();
            }
        }


        [TearDown]
        public void FinalTest()
        {
            //if (Driver != null)
            //    Driver.Quit();
        }

    }
}

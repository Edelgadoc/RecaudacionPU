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
        public void PagoUnSuministro()
        {
            SuministroPagina suministroPagina = new SuministroPagina(Driver);
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro("36399269");

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

        [TestCase("27124132")]
        [TestCase("27113916")]
        [TestCase("26555360")]
        [TestCase("26556714")]
        [TestCase("26551594")]
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

        [TestCase("27195887")]
        [TestCase("27055483")]
        [TestCase("27156996")]
        [TestCase("27168861")]
        [TestCase("27071674")]
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

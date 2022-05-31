using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        [Test]
        public void PagoEstandar()
        {
            SuministroPagina suministroPagina = new SuministroPagina(Driver);
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro("26565295");

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
                    Thread.Sleep(TimeSpan.FromSeconds(2));
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

        [TestCase("5148192")]
        [TestCase("5162511")]
        [TestCase("5958989")]
        [TestCase("5938574")]
        [TestCase("5630927")]
        public void PagoParalelo(string NroServicio)
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
                    Thread.Sleep(TimeSpan.FromSeconds(2));
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

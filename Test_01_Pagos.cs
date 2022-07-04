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
            ListarDeudaPagina listarDeudaPagina = suministroPagina.ConsultarSuministro("27064526");

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
        [TestCase("35942353")]
        [TestCase("35454339")]
        [TestCase("26611084")]
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


        [TestCase("37743813")]
        [TestCase("27064553")]
        [TestCase("27074390")]
        [TestCase("27077408")]
        [TestCase("27180777")]
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

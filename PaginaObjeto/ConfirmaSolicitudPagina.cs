using NUnit.Framework;
using OpenQA.Selenium;
using RecaudacionPU.Manejador;
using System;
using System.Threading;

namespace RecaudacionPU.PaginaObjeto
{
    /*
    * Clase para representar la pagina de Confirma Solicitud
    */
    public class ConfirmaSolicitudPagina
    {
        protected IWebDriver Driver;
        protected By lblNroPedido = By.Id("lblNroPedido");
        protected By cmdConfirmaPagar = By.XPath("//*[@id='frmPagoCard']/button");

        public ConfirmaSolicitudPagina(IWebDriver driver)
        {
            Driver = driver;
            Thread.Sleep(TimeSpan.FromSeconds(3));

            if (!Driver.Title.Equals("Suministro"))
                TestContext.Out.WriteLine("Esta no es la pagina de Confirmacion de Solicitud");
        }

        public bool PedidoPresente()
        {
            return Esperar.ElementoPresente(Driver, cmdConfirmaPagar);
        }

        public PagarVisaPagina ClickConfirmaPagar()
        {
            Driver.FindElement(cmdConfirmaPagar).Click();
            return new PagarVisaPagina(Driver);
        }

    }
}

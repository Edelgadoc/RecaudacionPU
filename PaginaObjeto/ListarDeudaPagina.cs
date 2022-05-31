using NUnit.Framework;
using OpenQA.Selenium;
using RecaudacionPU.Manejador;
using RecaudacionPU.PaginaObjeto;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RecaudacionPU
{
    /*
     * Clase para representar la pagina de ListarDeuda
     */
    public class ListarDeudaPagina
    {
        protected IWebDriver Driver;
        protected IWebElement Suministro;
        protected By lblDeudaTotal = By.Id("lblDeudaTotal");
        protected By chkPeriodos = By.XPath("//input[@type='checkbox']");
        protected By btnNext01 = By.Id("btnNext01");
        

        public ListarDeudaPagina(IWebDriver driver)
        {
            Driver = driver;
            Thread.Sleep(TimeSpan.FromSeconds(3));

            if (!Driver.Title.Equals("Suministro"))
                TestContext.Out.WriteLine("Esta no es la pagina de Listar Deuda");
        }

        public bool DeudaPresente()
        {
            return Esperar.ElementoPresente(Driver, lblDeudaTotal);
        }

        public void MarcarChecks()
        {
            IReadOnlyCollection<IWebElement> CheckPeriodo = Driver.FindElements(chkPeriodos);
            foreach (IWebElement chk in CheckPeriodo)
            {
                chk.Click();
            }
        }

        public ConfirmaSolicitudPagina ClickSiguiente()
        {
            Driver.FindElement(btnNext01).Click();
            return new ConfirmaSolicitudPagina(Driver);
        }


    }
}

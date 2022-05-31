using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace RecaudacionPU.PaginaObjeto
{
    /*
     * Clase para representar la pagina del Suministro 
     */

    public class SuministroPagina
    {
        protected IWebDriver Driver;
        protected By txtIdNroServicio = By.Id("txtIdNroServicio");
        protected By btnSearch = By.Id("btnSearch");

        public SuministroPagina(IWebDriver driver)
        {
            Driver = driver;
            Thread.Sleep(TimeSpan.FromSeconds(3));

            if (!Driver.Title.Equals("Suministro"))
                TestContext.Out.WriteLine("Esta no es la pagina de Suministro");
        }

        public void TipeaSuministro(string suministro)
        {
            Driver.FindElement(txtIdNroServicio).SendKeys(suministro);
        }

        public void ClickConsultar()
        {
            Driver.FindElement(btnSearch).Click();
        }

        public ListarDeudaPagina ConsultarSuministro( string suministro)
        {
            TipeaSuministro(suministro);
            ClickConsultar();
            return new ListarDeudaPagina(Driver);
        }
    }
}
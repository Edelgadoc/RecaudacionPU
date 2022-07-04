using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace RecaudacionPU.Manejador
{
    /*
     * Clase para manejar las esperas explicitas
     */
    public class Esperar
    {
        public static bool ElementoPresente(IWebDriver driver, By elemento, int segundos = 7)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(segundos));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(elemento));
                return driver.FindElement(elemento).Displayed;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("Error " + elemento.ToString() + "-> " + ex.Message);
                return false;
            }
        }

        public static bool CambioPagina(IWebDriver driver, string oldPagina, int segundos = 10)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(segundos));
                Func<IWebDriver, bool> cambioPagina = d => { return driver.Url != oldPagina; };
                bool rpta = wait.Until(cambioPagina);
                return rpta;
            }
            catch (Exception ex)
            {
                TestContext.Out.WriteLine("Error Pagina-> " + ex.Message);
                return false;
            }
        }
    }
}

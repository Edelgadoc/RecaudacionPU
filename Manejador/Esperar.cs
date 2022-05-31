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
        public static bool ElementoPresente(IWebDriver driver, By elemento)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(elemento));
                return driver.FindElement(elemento).Displayed;
            }
            catch { }

            return false;
        }

        public static void Tarjeta(IWebDriver driver, By elemento, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));

            Func<IWebDriver, bool> waitForElement = new Func<IWebDriver, bool>((IWebDriver Web) =>
            {
                Web.FindElement(elemento);
                return true;
            });
            wait.Until(waitForElement);
        }
    }
}

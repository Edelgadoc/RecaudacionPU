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
        public static bool ElementoPresente(IWebDriver driver, By elemento, int segundos = 6)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(segundos));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(elemento));
                return driver.FindElement(elemento).Displayed;
            }
            catch 
            {
                return false;
            }
            
        }
    }
}

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RecaudacionPU.Manejador;
using System;
using System.Configuration;
using System.Threading;

namespace RecaudacionPU.PaginaObjeto
{
    /*
    * Clase para representar la pagina de pagar con visa
    */

    public class PagarVisaPagina
    {
        protected IWebDriver Driver;
        protected By cmdPagarVisa = By.XPath("/html/body/div[1]/form/div[1]/div[13]/button[1]");
        protected By txtnumber = By.Id("number");
        protected By txtexpiry = By.Id("expiry");
        protected By txtcvc = By.Id("cvc");
        protected By txtname = By.Id("name");
        protected By txtlastname = By.Id("lastname");
        protected By txtemail = By.Id("email");

        //Datos de la tarjeta
        protected string strnumber = ConfigurationManager.AppSettings["Number"]; 
        protected string strexpity = ConfigurationManager.AppSettings["Expity"]; 
        protected string strcvc = ConfigurationManager.AppSettings["CVC"]; 
        protected string strname = ConfigurationManager.AppSettings["Name"]; 
        protected string strlastname = ConfigurationManager.AppSettings["LastName"]; 
        protected string stremail = ConfigurationManager.AppSettings["Email"];  

        public PagarVisaPagina(IWebDriver driver)
        {
            Driver = driver;
            Thread.Sleep(TimeSpan.FromSeconds(5));

            if (!Driver.Title.Equals("Suministro"))
                TestContext.Out.WriteLine("Esta no es la pagina de Pagar con Visa");
        }

        public bool NumeroPresente()
        {
            Driver.SwitchTo().Frame("visaNetJS");
            return Esperar.ElementoPresente(Driver, txtnumber);
        }

        public void LlenarTarjeta()
        {
            Driver.FindElement(txtnumber).SendKeys(strnumber.Substring(0,4));
            Driver.FindElement(txtnumber).SendKeys(strnumber.Substring(5,4));
            Driver.FindElement(txtnumber).SendKeys(strnumber.Substring(10,4));
            Driver.FindElement(txtnumber).SendKeys(strnumber.Substring(15,4));
            Driver.FindElement(txtexpiry).SendKeys(strexpity.Substring(0,2));
            Driver.FindElement(txtexpiry).SendKeys(strexpity.Substring(2,2));
            Driver.FindElement(txtcvc).SendKeys(strcvc);
            Driver.FindElement(txtname).SendKeys(strname);
            Driver.FindElement(txtlastname).SendKeys(strlastname);
            Driver.FindElement(txtemail).SendKeys(stremail);
        }

        public void ClickPagarVisa()
        {
            if (Driver.FindElement(cmdPagarVisa).Enabled)
                Driver.FindElement(cmdPagarVisa).Click();
        }

        public void RealizarPago()
        {
            Esperar.Tarjeta(Driver, txtnumber, 30);
            TestContext.Out.WriteLine("Empieza llenado de tarjeta");
            LlenarTarjeta();
            TestContext.Out.WriteLine("Termina llenado de tarjeta");
            ClickPagarVisa();
            TestContext.Out.WriteLine("Ejecuta pago Visa (Fin)");
        }

    }
}

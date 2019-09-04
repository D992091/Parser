using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSP
{
     public static class Moves
    {
        public static int posts = 0;
        public static void setTextInField(ChromeDriver chr, String id, String value)
        {
            IWebElement element = chr.FindElementById(id);
            element.SendKeys(value);
        }
        public static void setClickOnField(ChromeDriver chr, String id)
        {
            IWebElement element = chr.FindElementById(id);
            element.Click();
        }
        public static void setClickOnField(ChromeDriver chr, String className, int id)
        {
            IWebElement element = chr.FindElementByClassName(className);
            element.Click();
        }
        public static void MoveDown(ChromeDriver chr, int x, int y)
        {
            ((IJavaScriptExecutor)chr).ExecuteScript("window.scrollBy(" + x + ","
                                                                          + y + ");");
            posts = chr.FindElementsByClassName("feed_row").Count;
        }
    }
}

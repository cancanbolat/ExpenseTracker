using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Expense.UiTests
{
    public class AngularSeleniumTests
    {
        [Fact]
        public void IndexPage_Title()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:4200/");

                Assert.Equal("AngularClient", driver.Title);

                driver.Quit();
            }
        }
        
        [Fact]
        public void Record_Page()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:4200/");

                // Navbar record button
                IWebElement navRecordButton = driver.FindElement(By.Id("navRecordButton"));
                navRecordButton.Click();

                //Add Record Button
                IWebElement addRecordButton = driver.FindElement(By.XPath("//button[@id='addRecordButton']"));

                Assert.Equal("Add Record", addRecordButton.Text);

                driver.Quit();
            }
        }

        [Fact]
        public void AddRecord()
        {

            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:4200");
                
                //Navbar record Button
                IWebElement navRecordButton = driver.FindElement(By.Id("navRecordButton"));
                navRecordButton.Click();

                Thread.Sleep(2000);

                //Add Record Button
                IWebElement addRecordButton = driver.FindElement(By.XPath("//button[@id='addRecordButton']"));
                addRecordButton.Click();

                // Title Input
                IWebElement titleInput = driver.FindElement(By.Id("titleInput"));
                titleInput.SendKeys("Selenium record title");

                // Amount Input
                IWebElement amountInput = driver.FindElement(By.Id("amountInput"));
                amountInput.SendKeys("100");

                // Select Category
                var categoryIdSelect = driver.FindElement(By.Id("categoryIdSelect"));
                var selectElement = new SelectElement(categoryIdSelect);
                selectElement.SelectByIndex(1);

                // Save Button
                IWebElement saveButton = driver.FindElement(By.Id("saveRecordButton"));
                saveButton.Click();
                Thread.Sleep(1000);

                //Page link buttons
                IList<IWebElement> pageLinks = driver.FindElements(By.ClassName("page-item"));
                IWebElement nextPageButton = pageLinks[pageLinks.Count - 2].FindElement(By.ClassName("page-link"));
                for (int i = 1; i < pageLinks.Count; i++)
                {
                    nextPageButton.Click();
                }

                // Last record column
                IList<IWebElement> recordsTr = driver.FindElements(By.Id("recordsTr"));
                IWebElement lastRecordTd = recordsTr[recordsTr.Count - 1].FindElement(By.Id("recordTitleTd"));

                Assert.Equal("Selenium record title", lastRecordTd.Text);

                driver.Quit();
            }
        }

        [Fact]
        public void SelectCategory()
        {
            //delete all Records and add one record (run AddRecord test)

            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:4200");

                //Navbar record Button
                IWebElement navRecordButton = driver.FindElement(By.Id("navRecordButton"));
                navRecordButton.Click();

                Thread.Sleep(2000);

                //categoryButton
                IWebElement categoryButton = driver.FindElement(By.Id("category-home"));
                categoryButton.Click();
                
                Thread.Sleep(1000);

                //Record List
                IList<IWebElement> recordsTr = driver.FindElements(By.Id("recordsTr"));

                Assert.Equal(1, recordsTr.Count);
            }
        }
    }
}

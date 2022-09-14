using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webdriver.SpecFlow.StepDefinitions
{

    [Binding]
    public class MoreSteps
    {
        string title;
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public MoreSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._driver = (IWebDriver?)_scenarioContext["mydriver"];
        }

        [When(@"I search for '(.*)'")]
        public void WhenISearchForEdgewords(string searchTerm)
        {
            Console.WriteLine((string)_scenarioContext["pagetitle"]);
            _driver.FindElement(By.CssSelector("input[name=q]")).SendKeys(searchTerm + Keys.Enter);
        }
    }
   
}

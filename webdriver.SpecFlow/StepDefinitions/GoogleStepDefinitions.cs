using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using static webdriver.SpecFlow.StepDefinitions.Hooks;
using System.Reflection;

namespace webdriver.SpecFlow.StepDefinitions
{

    [Binding]
    public class GoogleStepDefinitions
    {
        string title;
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        public GoogleStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext; 
            this._driver = (IWebDriver?)_scenarioContext["mydriver"];
        }

        [Given(@"Google is open")] //Alternative phrasing
        [Given(@"(?:I|i) am on the (?i)Google(?-i) Homepage")] //Google is case insesitive
        public void GivenIAmOnTheGoogleHomepage()
        {
            _driver.Url = "https://www.google.co.uk/";
            IWebElement acceptButton = _driver.FindElement(By.Id("L2AGLb"));
            
            acceptButton.Click();
            _scenarioContext["pagetitle"] = _driver.Title;
        }


        [Then(@"'(.*)' is the top result")]
        public void ThenEdgewordsIsTheTopResult(string searchResult)
        {
            Thread.Sleep(1000);
            string topsearch = _driver.FindElement(By.CssSelector("div#rso > div:first-of-type")).Text;
            Assert.That(topsearch, Does.Contain(searchResult), "Not in the top result");
            //Fluent assertion
            //topsearch.Should().Contain("Edgewords");



        }

        [Then(@"I see in the results")]
        public void ThenISeeInTheResults(Table table)
        {
            string searchResults = _driver.FindElement(By.Id("rso")).Text;
            //Need to unpack the data table
            foreach (var row in table.Rows)
            {
                Assert.That(searchResults, Does.Contain(row["url"]), "Didn't find url");
                Assert.That(searchResults, Does.Contain(row["title"]), "Title is missing");
            }
        }

    }
}

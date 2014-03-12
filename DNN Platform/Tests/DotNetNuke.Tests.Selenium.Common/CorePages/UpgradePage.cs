using System;
using System.Diagnostics;
using DNNSelenium.Common.BaseClasses;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace DNNSelenium.Common.CorePages
{
	public class UpgradePage : BasePage
	{
		public UpgradePage(IWebDriver driver) : base(driver) {}

		public override string PageTitleLabel
		{
			get { return ""; }
		}

		public override string PageHeaderLabel
		{
			get { return ""; }
		}

		public override string PreLoadedModule
		{
			get { return ""; }
		}

		public static string HostUsernameTextBox = "//input[@id = 'txtUsername']";
		public static string HostPasswordTexBox = "//input[@id = 'txtPassword']";
		public static string UpgradeButton = "//a[@id = 'continueLink']";

		public static string ProgressBar = "//div[@id='progressbar' and @aria-valuenow ='100']";
		public static string SeeLogsButton = "//a[@id = 'seeLogs']";
		public static string UpgraderLogContainer = "//div[@id = 'installation-log']";
		public static string VisitSiteButton = "//a[@id = 'visitSite']";

		public void OpenUsingUrl(string baseUrl)
		{
			Trace.WriteLine(BasePage.TraceLevelPage + "Open 'Upgrader' page:");
			GoToUrl(baseUrl);
		}

		public void FillAccountInfo(string userName, string password)
		{
			WaitAndType(By.XPath(HostUsernameTextBox), userName);
			Type(By.XPath(HostPasswordTexBox), password);
		}

		public void WaitForUpgradingProcessToFinish()
		{
			Trace.WriteLine(BasePage.TraceLevelPage + "Wait for Upgrade Process to finish:");
			WaitForElement(By.XPath(ProgressBar), 100);
		}

		public void ClickOnSeeLogsButton()
		{
			Trace.WriteLine(BasePage.TraceLevelPage + "Click on 'View Logs' button: ");
			WaitForElement(By.XPath(SeeLogsButton)).WaitTillEnabled(60).Click();
		}

		public void WaitForLogContainer()
		{
			Trace.WriteLine(BasePage.TraceLevelPage + "Wait for 'Log Container' is visible:");
			WaitForElement(By.XPath(UpgraderLogContainer)).WaitTillVisible(20);

			var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
			wait.Until(driver => driver.FindElement(By.XPath(UpgraderLogContainer)).Text.Length > 0);

			Trace.WriteLine(BasePage.TraceLevelElement + "Logs :" + FindElement(By.XPath(UpgraderLogContainer)).Text);
		}

		public void ClickOnUpgradeButton()
		{
			Trace.WriteLine(BasePage.TraceLevelPage + "Click on 'Upgrade now' button:");
			Click(By.XPath(UpgradeButton));
		}

		public void ClickOnVisitWebsiteButton()
		{
			Trace.WriteLine(BasePage.TraceLevelElement + "Click on 'Visit Site' button:");
			FindElement(By.XPath(VisitSiteButton)).WaitTillEnabled().Click();
		}

		public void WelcomeScreen()
		{
			Trace.WriteLine(BasePage.TraceLevelComposite + "Login to the site first time, using 'Visit Website' button:");

			WaitForElement(By.XPath("//div[contains(@class, 'dnnFormPopup') and contains(@style,'display: block;')]"), 30);

			WaitForElement(By.XPath("//div/iframe[contains(@src, 'GettingStarted')]"), 60);
			_driver.SwitchTo().Frame(0);

			WaitForElement(By.XPath("//div/a[last()]"), 60).WaitTillVisible(60);

			_driver.SwitchTo().DefaultContent();

			WaitForElement(By.XPath("//div[contains(@class, 'footer-left-side')]/label")).WaitTillEnabled(30);

			Actions action = new Actions(_driver);
			action.MoveToElement(FindElement(By.XPath("//div[contains(@class, 'footer-left-side')]/label"))).Click().Build().Perform();

			WaitAndClick(By.XPath("//div[contains(@style,'display: block;')]//button[@role = 'button' and @title = 'close']"));

			WaitForElementNotPresent(By.XPath("//div[contains(@class, 'dnnFormPopup') and contains(@style,'display: block;')]"), 30);
		}
	}
}
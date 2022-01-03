using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;

namespace WinAppTest.Tests
{
    public abstract class BaseTest
    {
        private AppiumOptions options;
        private WindowsDriver<WindowsElement> driver;
        private AppiumLocalService appiumService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            options = new AppiumOptions();
            appiumService = new AppiumServiceBuilder().UsingPort(4723).WithLogFile(new FileInfo(GetLogFile())).Build();
            appiumService.Start();

            options.AddAdditionalCapability("app", @"Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

            driver = new WindowsDriver<WindowsElement>(appiumService, options);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (appiumService.IsRunning)
            {
                appiumService.Dispose();
            }
        }

        /// <summary>
        /// Returns full path to log file
        /// </summary>
        private string GetLogFile()
        {
            var directory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;
            var folder = Path.Combine(directory.FullName, "Logs");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return $"{folder}/TestLog.txt";
        }
    }
}

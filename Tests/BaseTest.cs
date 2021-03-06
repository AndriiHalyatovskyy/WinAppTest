using CsvHelper;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Globalization;
using System.IO;
using WinAppTest.Pages;

namespace WinAppTest.Tests
{
    public abstract class BaseTest
    {
        private AppiumOptions options, options2;
        private WindowsDriver<WindowsElement> driver;
        private AppiumLocalService appiumService;
        private Page page;

        protected Page Page => page ?? (page = new Page(
               driver: driver));

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            options = new AppiumOptions();
            options2 = new AppiumOptions();
            appiumService = new AppiumServiceBuilder().UsingPort(4723).WithLogFile(new FileInfo(GetLogFile())).Build();
            appiumService.Start();
            var mainFolder = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;

            // options.AddAdditionalCapability("app", @"Microsoft.WindowsCalculator_8wekyb3d8bbwe!App"); //calc
            //options.AddAdditionalCapability("app", @"Microsoft.WindowsAlarms_8wekyb3d8bbwe!App"); // clock
            options.AddAdditionalCapability("app", Path.Combine(mainFolder, "APPs", "DoNotDistrurbMortgageCalculatorFrom1999.exe")); // Custom app


            options2.AddAdditionalCapability("app", @"Root");

            driver = new WindowsDriver<WindowsElement>(appiumService, options);
            //driver = new WindowsDriver<WindowsElement>(appiumService, options2);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
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

        private protected void ReadTestData<T>()
        {
            using (var streamReader = new StreamReader(@"C:\WinAppTest\WinAppTest\TestData\Cities.xlsx"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Read();
                    var qq = csvReader.ReadHeader();
                    var records = csvReader.GetRecords<T>();
                }
            }
        }
    }
}

using CsvHelper;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using SharpAvi;
using System;
using System.Globalization;
using System.IO;
using WinAppTest.Pages;
using WinAppTest.VideoRecorder;

namespace WinAppTest.Tests
{
    public abstract class BaseTest
    {
        private AppiumOptions options, options2;
        private WindowsDriver<WindowsElement> driver;
        private AppiumLocalService appiumService;
        private Page page;
        private Recorder recorder;

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
            SetUpRecorder();

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

        [SetUp]
        public void SetUpTestBase()
        {
            StartVideoRecording();
        }

        [TearDown]
        public void TearDownTestBase()
        {
            EndTest();
        }

        /// <summary>
        /// Returns full path to log file
        /// </summary>
        private string GetLogFile()
        {
            var folder = CheckIfFolderExist("Logs");
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

        private void EndTest()
        {
            StopVideoRecording();
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                var folder = CheckIfFolderExist("ScreenShots");
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(Path.Combine(folder, $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now.ToString("dd.MM.yyyy")}.png"), ScreenshotImageFormat.Png);
            }
        }

        private void StartVideoRecording()
        {
            recorder.StartRecording();
        }

        private void StopVideoRecording()
        {
            //var rawData = driver.StopRecordingScreen();
            //byte[] decoded = Convert.FromBase64String(rawData);
            //var folderPath = CheckIfFolderExist("Videos");
            //var videoName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now.ToString("dd.MM.yyyy")}_{DateTime.Now.Ticks}.mp4";
            //File.WriteAllBytes(Path.Combine(folderPath, videoName), decoded);
            recorder.Dispose();
        }

        private string CheckIfFolderExist(string folderName)
        {
            var directory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;
            var folder = Path.Combine(directory.FullName, folderName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }

        private void SetUpRecorder()
        {
            var folderPath = CheckIfFolderExist("Videos");
            var videoName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now.ToString("dd.MM.yyyy")}_{DateTime.Now.Ticks}.avi";
            recorder = new Recorder(new RecorderParams(Path.Combine(folderPath, videoName), 30, CodecIds.MotionJpeg, 70));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Device
{
    internal class NetworkTests
    {
        private AppiumDriver<IWebElement> _driver;
        private AppiumOptions _androidOptions;
        private const string ApiDemosPackageName = "io.appium.android.apis";

        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos));
            _driver = new AndroidDriver<IWebElement>(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _androidOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test]
        public void CanToggleData()
        {
            _driver.ToggleData();
            _driver.ToggleData();
        }

        [Test]
        public void CanToggleAirplaneModeTest()
        {
            var androidDriver = (AndroidDriver<IWebElement>)_driver;

            androidDriver.ToggleAirplaneMode();
            
            var currentConnectionType = androidDriver.ConnectionType;
            Assert.That(currentConnectionType, Is.EqualTo(ConnectionType.AirplaneMode));

            androidDriver.ToggleAirplaneMode();
        }

        [Test]
        public void CanGetSystemBarInfoTest()
        {
            var androidDriver = (AndroidDriver<IWebElement>)_driver;
            Assert.That(androidDriver.GetSystemBars().Count, Is.EqualTo(2));
        }

        [Test]
        public void CanGetDisplayDensityTest()
        {
            var androidDriver = (AndroidDriver<IWebElement>)_driver;
            Assert.That(androidDriver.GetDisplayDensity, Does.Not.EqualTo(0));
        }

        [Test]
        public void CanSetGsmSignalStrengthTest()
        {
            var androidDriver = (AndroidDriver<IWebElement>)_driver;

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.NoneOrUnknown));
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.Poor));
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.Good));
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.Moderate));
                Assert.DoesNotThrow(() => androidDriver.SetGsmSignalStrength(GsmSignalStrength.Great));
            });
        }
    }
}

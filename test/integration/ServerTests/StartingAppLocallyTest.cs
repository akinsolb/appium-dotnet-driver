using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;

namespace Appium.Net.Integration.Tests.ServerTests
{
    class StartingAppLocallyTest
    {
        [Test]
        public void StartingAndroidAppWithCapabilitiesOnlyTest()
        {
            var app = Apps.Get("androidApiDemos");
            var capabilities =
                Caps.GetAndroidUIAutomatorCaps(app);

            AndroidDriver<AppiumElement<AndroidElement>> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumElement<AndroidElement>>(capabilities);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void StartingAndroidAppWithCapabilitiesAndServiceTest()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));


            var argCollector = new OptionCollector()
                .AddArguments(GeneralOptionList.OverrideSession()).AddArguments(GeneralOptionList.StrictCaps());
            var builder = new AppiumServiceBuilder().WithArguments(argCollector);

            AndroidDriver<AppiumElement<AndroidElement>> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumElement<AndroidElement>>(builder, capabilities);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }


        [Test]
        public void StartingAndroidAppWithCapabilitiesOnTheServerSideTest()
        {
            var app = Apps.Get("androidApiDemos");

            var serverCapabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));

            var clientCapabilities = new AppiumOptions();
            clientCapabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            clientCapabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".view.WebView1");

            var argCollector = new OptionCollector().AddCapabilities(serverCapabilities);
            var builder = new AppiumServiceBuilder().WithArguments(argCollector);

            AndroidDriver<AppiumElement<AndroidElement>> driver = null;
            try
            {
                driver = new AndroidDriver<AppiumElement<AndroidElement>>(builder, clientCapabilities);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void StartingIosAppWithCapabilitiesOnlyTest()
        {
            var app = Apps.Get("iosTestApp");
            var capabilities =
                Caps.GetIosCaps(app);

            IOSDriver<AppiumElement<IOSElement>> driver = null;
            try
            {
                driver = new IOSDriver<AppiumElement<IOSElement>>(capabilities, Env.InitTimeoutSec);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void StartingIosAppWithCapabilitiesAndServiseTest()
        {
            var app = Apps.Get("iosTestApp");
            var capabilities =
                Caps.GetIosCaps(app);

            var argCollector = new OptionCollector()
                .AddArguments(GeneralOptionList.OverrideSession()).AddArguments(GeneralOptionList.StrictCaps());

            var builder = new AppiumServiceBuilder().WithArguments(argCollector);
            IOSDriver<AppiumElement<IOSElement>> driver = null;
            try
            {
                driver = new IOSDriver<AppiumElement<IOSElement>>(builder, capabilities, Env.InitTimeoutSec);
                driver.CloseApp();
            }
            finally
            {
                driver?.Quit();
            }
        }

        [Test]
        public void CheckThatServiseIsNotRunWhenTheCreatingOfANewSessionIsFailed()
        {
            var capabilities = Env.ServerIsRemote()
                ? //it will be a cause of error
                Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone Simulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, MobilePlatform.IOS);

            var builder = new AppiumServiceBuilder();
            var service = builder.Build();
            service.Start();

            IOSDriver<AppiumElement<IOSElement>> driver = null;
            try
            {
                try
                {
                    driver = new IOSDriver<AppiumElement<IOSElement>>(service, capabilities);
                }
                catch (Exception e)
                {
                    Assert.IsTrue(!service.IsRunning);
                    return;
                }
                throw new Exception("Any exception was expected");
            }
            finally
            {
                driver?.Quit();
            }
        }
    }
}
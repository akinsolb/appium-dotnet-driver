//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Enums;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WindowsElement : AppiumElement<WindowsElement>, IFindByWindowsUIAutomation<WindowsElement>
    {
        public WindowsElement(RemoteWebDriver parent, string id)
            : base(parent, id)
        {
        }

        #region IFindByWindowsUIAutomation Members

        public WindowsElement FindElementByWindowsUIAutomation(string selector) =>
            FindElement(MobileSelector.WindowsUIAutomation, selector) as WindowsElement;

        public IReadOnlyCollection<WindowsElement> FindElementsByWindowsUIAutomation(string selector) =>
            FindElements(MobileSelector.WindowsUIAutomation, selector);

        #endregion IFindByWindowsUIAutomation Members
    }
}
using Cora.Entities;
using Cora.UI;
using Cora.UI.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;

namespace Cora
{
    public static class InstanceManager
    {
        #region UI Controllers
        public static MainMenu Menu { get; set; }

        #endregion

        public static Enterprise CurrentEnterprise { get; set; }
        public static User ConnectedUser { get; set; }
        public static string CurrentVersion 
        { 
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            } 
        }
    }
}

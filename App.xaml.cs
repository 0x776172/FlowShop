using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace FlowShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var myCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = myCulture;
            Thread.CurrentThread.CurrentUICulture = myCulture;
            CultureInfo.CurrentUICulture = myCulture;
            CultureInfo.CurrentCulture = myCulture;
            base.OnStartup(e);
        }
    }
}

using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRentals.Web
{
    public class WebRouteStrategy : DefaultRouteStrategy
    {
        public WebRouteStrategy(DotvvmConfiguration configuration, string viewsFolder = "Books", string pattern = "*.dothtml") : base(configuration, viewsFolder, pattern)
        {
        }

        protected override string GetRouteUrl(RouteStrategyMarkupFileInfo file)
        {
            var url = file.AppRelativePath;
            if (url.Contains("Books/"))
            {
                // instead of /home, we need the route to be directly in the website root /
                url = url.Replace("Books/", "");
            }

            if (url.Contains("Default.dothtml"))
            {
                url = url.Replace("Default.dothtml", "");
            }

            return url;
        }
    }
}

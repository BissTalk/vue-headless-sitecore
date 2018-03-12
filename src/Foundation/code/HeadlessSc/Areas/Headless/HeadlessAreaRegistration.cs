using System.Web.Mvc;
using Sitecore;

namespace HeadlessSc.Areas.Headless
{
    /// <summary>
    ///     Registers 
    /// </summary>
    /// <seealso cref="AreaRegistration" />
    public class HeadlessAreaRegistration : AreaRegistration
    {
        public override string AreaName => ModuleInfo.DomainName;

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Headless_default",
                ModuleInfo.RoutePrefix.TrimStart('/'),
                new {controller = "HeadlessContent", action = "Index"},
                new [] { "HeadlessSc.Areas.Headless.Controllers" }
            );
        }
    }
}
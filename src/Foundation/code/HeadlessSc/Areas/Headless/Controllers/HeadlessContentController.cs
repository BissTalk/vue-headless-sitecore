using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using HeadlessSc.Pipelines.GetJsonSerializerSettings;
using HeadlessSc.Pipelines.GetModel;
using HeadlessSc.Pipelines.GetRenderings;
using Newtonsoft.Json;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines;

namespace HeadlessSc.Areas.Headless.Controllers
{
    public class HeadlessContentController : Controller
    {
        // GET: Headless/Content
        public ActionResult Index()
        {
            var renderings = GetRenderings();
            var result = CreateResult(renderings);
            if (result == null)
                return HttpNotFound();
            return Content(SerializeObject(result), "application/json", Encoding.UTF8);
        }

        private object CreateResult(IEnumerable<Rendering> renderings)
        {
            var args = new GetModelArgs(renderings);
            CorePipeline.Run("headless.getModel", args, false);
            return args.Result;
        }

        private static string SerializeObject(object model)
        {
            return JsonConvert.SerializeObject(
                model,
                Formatting.None,
                GetJsonSerializerSettings());
        }

        private IEnumerable<Rendering> GetRenderings()
        {
            var args = new GetRenderingsArgs(ControllerContext.RequestContext);
            CorePipeline.Run("headless.getRenderings", args, false);
            return args.Result ?? Enumerable.Empty<Rendering>();
        }

        private static JsonSerializerSettings GetJsonSerializerSettings()
        {
            var args = new JsonSerializerSettingsArgs();
            CorePipeline.Run("headless.getJsonSerializerSettings", args, false);
            return args.Result ?? new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }
    }
}
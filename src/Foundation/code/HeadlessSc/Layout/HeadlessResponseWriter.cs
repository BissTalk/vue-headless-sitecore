using System.IO;
using HeadlessSc.Areas.Headless.Models;

namespace HeadlessSc.Layout
{
    /// <summary>
    ///     Used to collect data about rendered components.
    /// </summary>
    /// <seealso cref="System.IO.StringWriter" />
    public class HeadlessResponseWriter : StringWriter
    {
        public ComponentItem Result { get; set; }
    }
}
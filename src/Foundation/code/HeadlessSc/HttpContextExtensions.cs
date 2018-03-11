using System.Web;

namespace HeadlessSc
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// The  context item key used to flag the context as headless.
        /// </summary>
        private const string HeadlessContextKey = "Sitecore-Headless-Content-Request";

        /// <summary>
        /// Flags as headless.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void FlagAsHeadless(this HttpContextBase context)
        {
            context.Items.Add(HeadlessContextKey, true);
        }

        /// <summary>
        /// Gets the query string value given its name.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetQueryStringValue(this HttpContextBase context, string name)
        {
            return context.Request.QueryString[name];
        }

        /// <summary>
        /// Determines whether this instance is headless.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <c>true</c> if the specified context is headless; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHeadless(this HttpContextBase context)
        {
            return context.Items.Contains(HeadlessContextKey)
                   && (bool) context.Items[HeadlessContextKey];
        }

        /// <summary>
        /// Determines whether this instance is headless.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <c>true</c> if the specified context is headless; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHeadless(this HttpContext context)
        {
            return context.Items.Contains(HeadlessContextKey)
                   && (bool) context.Items[HeadlessContextKey];
        }
    }
}
using Sitecore;

namespace HeadlessSc.Abstractions
{
    /// <summary>
    ///     Interface to create an abstraction over the static/untestable <see cref="Context" />
    /// </summary>
    public interface ISitecoreContext
    {
        /// <summary>
        ///     Resolves the context item by path.
        /// </summary>
        /// <param name="itemPath">The item path.</param>
        bool TryResolveContextItemByPath(string itemPath);
    }
}
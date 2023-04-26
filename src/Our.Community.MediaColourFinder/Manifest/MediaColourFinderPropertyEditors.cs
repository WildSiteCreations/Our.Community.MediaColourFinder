using Umbraco.Cms.Core.PropertyEditors;

namespace Umbraco.Community.MediaColourFinder.Manifest
{

    [DataEditor(EditorAlias, EditorName, EditorView, ValueType = ValueTypes.Json, Group = "common", Icon = EditorIcon)]

    public class MediaColourFinderEditor : DataEditor
    {

        #region Constants

        /// <summary>
        /// Gets the alias of the <see cref="MediaColourFinderEditor"/> property editor.
        /// </summary>
        public const string EditorAlias = "wsc.mediaColourFinder";

        /// <summary>
        /// Gets the name of the <see cref="MediaColourFinderEditor"/> property editor.
        /// </summary>
        public const string EditorName = "Media Colour Finder";

        /// <summary>
        /// Gets the URL of the view of the <see cref="MediaColourFinderEditor"/> property editor.
        /// </summary>
        public const string EditorView = "/App_Plugins/Our.Community.MediaColourFinder/mediaColourFinder.html";

        /// <summary>
        /// Gets the icon of the <see cref="MediaColourFinderEditor"/> property editor.
        /// </summary>
        public const string EditorIcon = "color-circle";

        #endregion

        #region Constructors

        public MediaColourFinderEditor(IDataValueEditorFactory dataValueEditorFactory) : base(dataValueEditorFactory) { }

        #endregion

    }
   
}

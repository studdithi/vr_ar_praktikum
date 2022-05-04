using System.Collections.Generic;
using Innoactive.Creator.Core.Configuration;
using Innoactive.Creator.Core.Configuration.Modes;

namespace Innoactive.Hub.Training.Examples.Configuration
{
    /// <summary>
    /// Extension of <see cref="DefaultRuntimeConfiguration"/> implementing three modes:
    /// 1. Always show highlights for snapzones;
    /// 2. Only show the highlight when the snappable object is inside the snapzone trigger;
    /// 3. Never show highlights, the user needs to know where to place the object.
    /// </summary>
    public class ModesExampleRuntimeConfiguration //: DefaultRuntimeConfiguration
    {
        protected ModesExampleRuntimeConfiguration()
        {
            //Dictionary<string, object> alwaysHighlightParameters = new Dictionary<string, object>();
            //alwaysHighlightParameters.Add("ShowSnapzoneHoverMeshes", true);
            //alwaysHighlightParameters.Add("ShowSnapzoneHighlightObject", true);
            //IMode alwaysHighlight = new Mode("Always", new WhitelistTypeRule<IOptional>(), alwaysHighlightParameters);

            //Dictionary<string, object> partialHighlightParameters = new Dictionary<string, object>();
            //partialHighlightParameters.Add("ShowSnapzoneHoverMeshes", true);
            //partialHighlightParameters.Add("ShowSnapzoneHighlightObject", false);
            //IMode partialHighlight = new Mode("When Near", new WhitelistTypeRule<IOptional>(),
            //    partialHighlightParameters);

            //Dictionary<string, object> neverHighlightParameters = new Dictionary<string, object>();
            //neverHighlightParameters.Add("ShowSnapzoneHoverMeshes", false);
            //neverHighlightParameters.Add("ShowSnapzoneHighlightObject", false);
            //IMode neverHighlight = new Mode("Never", new WhitelistTypeRule<IOptional>(), neverHighlightParameters);

            //Modes = new BaseModeHandler(new List<IMode> { alwaysHighlight, partialHighlight, neverHighlight });
        }
    }
}

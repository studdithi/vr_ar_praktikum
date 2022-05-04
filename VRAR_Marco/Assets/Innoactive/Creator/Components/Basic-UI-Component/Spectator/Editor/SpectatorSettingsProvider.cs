﻿using UnityEditor;

namespace Innoactive.CreatorEditor.UI
{
    internal class SpectatorSettingsProvider : BaseSettingsProvider
    {
        const string Path = "Project/Creator/Spectator";

        public SpectatorSettingsProvider() : base(Path, SettingsScope.Project)
        {
        }

        protected override void InternalDraw(string searchContext)
        {
        }

        [SettingsProvider]
        public static SettingsProvider Provider()
        {
            SettingsProvider provider = new SpectatorSettingsProvider();
            return provider;
        }
    }
}

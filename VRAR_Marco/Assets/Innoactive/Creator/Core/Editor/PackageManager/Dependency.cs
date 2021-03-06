using System;

namespace Innoactive.CreatorEditor.PackageManager
{
    /// <summary>
    /// Base class for dependencies used by the <see cref="DependencyManager"/>.
    /// </summary>
    public abstract class Dependency
    {
        /// <summary>
        /// A string representing the package to be added.
        /// </summary>
        public virtual string Package { get; } = "";

        /// <summary>
        /// Priority lets you tweak in which order each <see cref="Dependency"/> will be performed.
        /// The priority is considered from lowest to highest.
        /// </summary>
        public virtual int Priority { get; } = 0;

        /// <summary>
        /// A list of layers to be added.
        /// </summary>
        protected virtual string[] Layers { get; } = null;

        /// <summary>
        /// Emitted when this <see cref="Dependency"/> is set as enabled.
        /// </summary>
        public event EventHandler<EventArgs> OnPackageEnabled;

        /// <summary>
        /// Emitted when this <see cref="Dependency"/> is set as disabled.
        /// </summary>
        public event EventHandler<EventArgs> OnPackageDisabled;

        /// <summary>
        /// Represents the current status of this <see cref="Dependency"/>.
        /// </summary>
        internal bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled != value)
                {
                    if (value)
                    {
                        EmitOnEnabled();
                        AddMissingLayers();
                    }
                    else
                    {
                        EmitOnDisabled();
                    }
                }

                isEnabled = value;
            }
        }

        private bool isEnabled;

        protected virtual void EmitOnEnabled()
        {
            OnPackageEnabled?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void EmitOnDisabled()
        {
            OnPackageDisabled?.Invoke(this, EventArgs.Empty);
        }

        private void AddMissingLayers()
        {
            if (Layers != null)
            {
                LayerUtils.AddLayers(Layers);
            }
        }
    }
}

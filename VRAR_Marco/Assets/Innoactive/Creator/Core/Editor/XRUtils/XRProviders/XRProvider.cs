#if UNITY_XR_MANAGEMENT && (OCULUS_XR || WINDOWS_XR)
using System;
using UnityEditor;
using Innoactive.CreatorEditor.PackageManager;

namespace Innoactive.CreatorEditor.XRUtils
{
    internal abstract class XRProvider : Dependency, IDisposable
    {
        protected virtual string XRLoaderName { get; } = "";

        protected XRProvider()
        {
            if (EditorPrefs.GetBool(XRLoaderHelper.IsXRLoaderInitialized) == false)
            {
                OnPackageEnabled += InitializeXRLoader;
            }
        }

        public void Dispose()
        {
            OnPackageEnabled -= InitializeXRLoader;
        }

        protected virtual void InitializeXRLoader(object sender, EventArgs e)
        {
            OnPackageEnabled -= InitializeXRLoader;
            XRLoaderHelper.EnableLoader(Package, XRLoaderName);
        }
    }
}
#endif

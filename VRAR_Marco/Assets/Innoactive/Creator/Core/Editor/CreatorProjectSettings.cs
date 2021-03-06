using System.IO;
using Innoactive.CreatorEditor;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Settings for an Innoactive Creator Unity project.
/// </summary>
public partial class CreatorProjectSettings : ScriptableObject
{
    /// <summary>
    /// Was the Creator imported and therefore started for the first time.
    /// </summary>
    [HideInInspector]
    public bool IsFirstTimeStarted = true;

    /// <summary>
    /// Creator version used last time this was checked.
    /// </summary>
    [HideInInspector]
    public string ProjectCreatorVersion = null;

    /// <summary>
    /// Loads the Creator settings for this Unity project from Resources.
    /// </summary>
    /// <returns>Creator Settings</returns>
    public static CreatorProjectSettings Load()
    {
        CreatorProjectSettings settings = Resources.Load<CreatorProjectSettings>("CreatorProjectSettings");
        if (settings == null)
        {
            if (!Directory.Exists("Assets/Resources"))
            {
                Directory.CreateDirectory("Assets/Resources");
            }
            // Create an instance
            settings = CreateInstance<CreatorProjectSettings>();
            AssetDatabase.CreateAsset(settings, "Assets/Resources/CreatorProjectSettings.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return settings;
        }
        return settings;
    }

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(ProjectCreatorVersion))
        {
            ProjectCreatorVersion = EditorUtils.GetCoreVersion();
        }
    }

    /// <summary>
    /// Saves the Creator settings.
    /// </summary>
    public void Save()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}

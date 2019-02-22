using UnityEngine;
using UnityEditor;

namespace com.ich.Editor.Utils.ProjectConfigs
{
    /// <summary>
    /// Editor script that deletes all player prefs
    /// </summary>
    class ClearEditorPrefsMenu : ScriptableObject
    {
        [MenuItem("Utils/Clear all Editor Preferences")]
        static void DoClearAllEditorPreferences()
        {
            if (EditorUtility.DisplayDialog("Delete all editor preferences?",
                "Delete all editor preferences?",
                "Yes",
                "No"))
                PlayerPrefs.DeleteAll();
        }
    }
}
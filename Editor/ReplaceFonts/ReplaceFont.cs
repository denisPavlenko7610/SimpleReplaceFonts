using UnityEngine;
using UnityEditor;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

namespace Plugins
{
    public static class ReplaceFont
    {
        private static bool IsAssetInPackagesFolder(string assetPath)
        {
            return assetPath.StartsWith("Packages/", System.StringComparison.OrdinalIgnoreCase);
        }

        public static void ReplaceFontInScene(Font targetFont, Font newFont)
        {
            if (targetFont == null || newFont == null)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or both fonts are null", "Ok");
                return;
            }

            var currentScene = EditorSceneManager.GetActiveScene();
            var textComponents = Resources.FindObjectsOfTypeAll<Text>();

            Undo.SetCurrentGroupName("Replace specific legacy text fonts");

            int replacedCount = 0;  // Counter for replaced fonts

            foreach (var component in textComponents)
            {
                if (component.gameObject.scene != currentScene || component.font != targetFont)
                    continue;

                Undo.RecordObject(component, "Replace Font");
                component.font = newFont;
                Debug.Log($"Replaced font in: {component.name}", component);
                replacedCount++;  // Increment counter for each replacement
            }

            Undo.IncrementCurrentGroup();

            // Display a message with the count of replaced fonts
            if (replacedCount > 0)
                EditorUtility.DisplayDialog("Replace Font Result", $"{replacedCount} fonts replaced in the scene.", "Ok");
        }

        public static void ReplaceFontInScene(TMP_FontAsset targetFont, TMP_FontAsset newFont)
        {
            if (targetFont == null || newFont == null)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or both fonts are null", "Ok");
                return;
            }

            var currentScene = EditorSceneManager.GetActiveScene();
            var textComponents = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();

            Undo.SetCurrentGroupName("Replace specific TMP fonts");

            int replacedCount = 0;  // Counter for replaced TMP fonts

            foreach (var component in textComponents)
            {
                if (component.gameObject.scene != currentScene || component.font != targetFont)
                    continue;

                Undo.RecordObject(component, "Replace Font");
                component.font = newFont;
                Debug.Log($"Replaced font in: {component.name}", component);
                replacedCount++;  // Increment counter for each replacement
            }

            Undo.IncrementCurrentGroup();

            // Display a message with the count of replaced TMP fonts
            if (replacedCount > 0)
                EditorUtility.DisplayDialog("Replace Font Result", $"{replacedCount} TMP fonts replaced in the scene.", "Ok");
        }

        public static void ReplaceFontInPrefabs(Font targetFont, Font newFont)
        {
            if (targetFont == null || newFont == null)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or both fonts are null", "Ok");
                return;
            }

            string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase) && !IsAssetInPackagesFolder(path))
                .ToArray();

            Undo.SetCurrentGroupName("Replace specific legacy text fonts in prefabs");

            int replacedCount = 0;  // Counter for replaced fonts in prefabs

            foreach (var path in prefabPaths)
            {
                using (var prefabScope = new PrefabUtility.EditPrefabContentsScope(path))
                {
                    var prefabTexts = prefabScope.prefabContentsRoot.GetComponentsInChildren<Text>(true);

                    foreach (var text in prefabTexts)
                    {
                        if (text.font != targetFont)
                            continue;

                        Undo.RecordObject(text, "Replace Font");
                        text.font = newFont;
                        replacedCount++;  // Increment counter for each replacement
                    }
                }
            }

            Undo.IncrementCurrentGroup();

            // Display a message with the count of replaced fonts in prefabs
            if (replacedCount > 0)
                EditorUtility.DisplayDialog("Replace Font Result", $"{replacedCount} fonts replaced in prefabs.", "Ok");
        }

        public static void ReplaceFontInPrefabs(TMP_FontAsset targetFont, TMP_FontAsset newFont)
        {
            if (targetFont == null || newFont == null)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or both fonts are null", "Ok");
                return;
            }

            string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase) && !IsAssetInPackagesFolder(path))
                .ToArray();

            Undo.SetCurrentGroupName("Replace specific TMP fonts in prefabs");

            int replacedCount = 0;  // Counter for replaced TMP fonts in prefabs

            foreach (var path in prefabPaths)
            {
                using (var prefabScope = new PrefabUtility.EditPrefabContentsScope(path))
                {
                    var prefabTexts = prefabScope.prefabContentsRoot.GetComponentsInChildren<TextMeshProUGUI>(true);

                    foreach (var text in prefabTexts)
                    {
                        if (text.font != targetFont)
                            continue;

                        Undo.RecordObject(text, "Replace Font");
                        text.font = newFont;
                        replacedCount++;  // Increment counter for each replacement
                    }
                }
            }

            Undo.IncrementCurrentGroup();

            // Display a message with the count of replaced TMP fonts in prefabs
            if (replacedCount > 0)
                EditorUtility.DisplayDialog("Replace Font Result", $"{replacedCount} TMP fonts replaced in prefabs.", "Ok");
        }
    }
}

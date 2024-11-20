using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Plugins
{
    public static class ReplaceFont
    {
        public static void ReplaceFontInScene(List<Font> targetFonts, Font newFont)
        {
            if (targetFonts == null || newFont == null || targetFonts.Count == 0)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or more inputs are null or empty", "Ok");
                return;
            }

            var currentScene = EditorSceneManager.GetActiveScene();
            var textComponents = Resources.FindObjectsOfTypeAll<Text>();

            Undo.SetCurrentGroupName("Replace multiple legacy text fonts");

            int replacedCount = 0;

            foreach (var component in textComponents)
            {
                if (component.gameObject.scene != currentScene || !targetFonts.Contains(component.font))
                    continue;

                Undo.RecordObject(component, "Replace Font");
                component.font = newFont;
                replacedCount++;
            }

            Undo.IncrementCurrentGroup();

            if (replacedCount > 0)
                EditorUtility.DisplayDialog("Replace Font Result", $"{replacedCount} fonts replaced in the scene.", "Ok");
        }

        public static void ReplaceFontInScene(List<TMP_FontAsset> targetFonts, TMP_FontAsset newFont)
        {
            if (targetFonts == null || newFont == null || targetFonts.Count == 0)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or more inputs are null or empty", "Ok");
                return;
            }

            var currentScene = EditorSceneManager.GetActiveScene();
            var textComponents = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();

            Undo.SetCurrentGroupName("Replace multiple TMP fonts");

            int replacedCount = 0;

            foreach (var component in textComponents)
            {
                if (component.gameObject.scene != currentScene || !targetFonts.Contains(component.font))
                    continue;

                Undo.RecordObject(component, "Replace Font");
                component.font = newFont;
                replacedCount++;
            }

            Undo.IncrementCurrentGroup();
            if (replacedCount > 0)
                EditorUtility.DisplayDialog("Replace Font Result", $"{replacedCount} TMP fonts replaced in the scene.", "Ok");
        }

        public static void ReplaceFontInPrefabs(List<Font> targetFonts, Font newFont)
        {
            if (targetFonts == null || newFont == null || targetFonts.Count == 0)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or more inputs are null or empty", "Ok");
                return;
            }

            string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();

            Undo.SetCurrentGroupName("Replace multiple legacy text fonts in prefabs");

            int replacedCount = 0;

            foreach (var path in prefabPaths)
            {
                using (var prefabScope = new PrefabUtility.EditPrefabContentsScope(path))
                {
                    var prefabTexts = prefabScope.prefabContentsRoot.GetComponentsInChildren<Text>(true);

                    foreach (var text in prefabTexts)
                    {
                        if (!targetFonts.Contains(text.font))
                            continue;

                        Undo.RecordObject(text, "Replace Font");
                        text.font = newFont;
                        replacedCount++;
                    }
                }
            }

            Undo.IncrementCurrentGroup();
            if (replacedCount > 0)
                EditorUtility.DisplayDialog("Replace Font Result", $"{replacedCount} fonts replaced in prefabs.", "Ok");
        }

        public static void ReplaceFontInPrefabs(List<TMP_FontAsset> targetFonts, TMP_FontAsset newFont)
        {
            if (targetFonts == null || newFont == null || targetFonts.Count == 0)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or more inputs are null or empty", "Ok");
                return;
            }

            string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();

            Undo.SetCurrentGroupName("Replace multiple TMP fonts in prefabs");

            int replacedCount = 0;

            foreach (var path in prefabPaths)
            {
                using (var prefabScope = new PrefabUtility.EditPrefabContentsScope(path))
                {
                    var prefabTexts = prefabScope.prefabContentsRoot.GetComponentsInChildren<TextMeshProUGUI>(true);

                    foreach (var text in prefabTexts)
                    {
                        if (!targetFonts.Contains(text.font))
                            continue;

                        Undo.RecordObject(text, "Replace Font");
                        text.font = newFont;
                        replacedCount++;
                    }
                }
            }

            Undo.IncrementCurrentGroup();
            if (replacedCount > 0)
                EditorUtility.DisplayDialog("Replace Font Result", $"{replacedCount} TMP fonts replaced in prefabs.", "Ok");
        }
    }
}

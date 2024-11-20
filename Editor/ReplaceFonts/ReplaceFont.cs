using UnityEngine;
using UnityEditor;
using TMPro;
using System.Linq;
using Editors;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

namespace Plugins
{
    public static class ReplaceFont
    {
        [MenuItem("Tools/Project/Replace Font")]
        private static void ReplaceFontMenuItem()
        {
            ReplaceFontEditorWindow.ShowWindow();
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

            foreach (var component in textComponents)
            {
                if (component.gameObject.scene != currentScene || component.font != targetFont)
                    continue;

                Undo.RecordObject(component, "Replace Font");
                component.font = newFont;
                Debug.Log($"Replaced font in: {component.name}", component);
            }

            Undo.IncrementCurrentGroup();
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

            foreach (var component in textComponents)
            {
                if (component.gameObject.scene != currentScene || component.font != targetFont)
                    continue;

                Undo.RecordObject(component, "Replace Font");
                component.font = newFont;
                Debug.Log($"Replaced font in: {component.name}", component);
            }

            Undo.IncrementCurrentGroup();
        }

        public static void ReplaceFontInPrefabs(Font targetFont, Font newFont)
        {
            if (targetFont == null || newFont == null)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or both fonts are null", "Ok");
                return;
            }

            string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();

            Undo.SetCurrentGroupName("Replace specific legacy text fonts in prefabs");

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
                    }
                }
            }

            Undo.IncrementCurrentGroup();
        }

        public static void ReplaceFontInPrefabs(TMP_FontAsset targetFont, TMP_FontAsset newFont)
        {
            if (targetFont == null || newFont == null)
            {
                EditorUtility.DisplayDialog("Replace Font Result", "One or both fonts are null", "Ok");
                return;
            }

            string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase))
                .ToArray();

            Undo.SetCurrentGroupName("Replace specific TMP fonts in prefabs");

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
                    }
                }
            }

            Undo.IncrementCurrentGroup();
        }
    }
}
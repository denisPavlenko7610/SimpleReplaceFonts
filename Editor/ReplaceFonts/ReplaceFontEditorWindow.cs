using Plugins;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Editors
{
    public class ReplaceFontEditorWindow : EditorWindow
    {
        private Font targetLegacyFont;
        private Font newLegacyFont;
        private TMP_FontAsset targetTMPFont;
        private TMP_FontAsset newTMPFont;

        private FontType fontType;

        [MenuItem("Tools/Replace Font Tool")]
        public static void ShowWindow()
        {
            var window = GetWindow<ReplaceFontEditorWindow>("Replace Font Tool");
            window.minSize = new Vector2(400, 200);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Replace Fonts", EditorStyles.boldLabel);

            fontType = (FontType)EditorGUILayout.EnumPopup("Font Type", fontType);

            if (fontType == FontType.LegacyText)
            {
                targetLegacyFont = (Font)EditorGUILayout.ObjectField("Target Font", targetLegacyFont, typeof(Font), false);
                newLegacyFont = (Font)EditorGUILayout.ObjectField("New Font", newLegacyFont, typeof(Font), false);
            }
            else
            {
                targetTMPFont = (TMP_FontAsset)EditorGUILayout.ObjectField("Target Font", targetTMPFont, typeof(TMP_FontAsset), false);
                newTMPFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New Font", newTMPFont, typeof(TMP_FontAsset), false);
            }

            if (GUILayout.Button("Replace for current scene/prefabs"))
            {
                if (fontType == FontType.LegacyText)
                {
                    ReplaceFont.ReplaceFontInScene(targetLegacyFont, newLegacyFont);
                    ReplaceFont.ReplaceFontInPrefabs(targetLegacyFont, newLegacyFont);
                }
                else
                {
                    ReplaceFont.ReplaceFontInScene(targetTMPFont, newTMPFont);
                    ReplaceFont.ReplaceFontInPrefabs(targetTMPFont, newTMPFont);
                }
            }
        }
    }

    public enum FontType
    {
        LegacyText,
        TextMeshPro
    }
}

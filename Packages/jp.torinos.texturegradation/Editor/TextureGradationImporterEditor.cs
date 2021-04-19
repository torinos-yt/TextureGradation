using UnityEngine;
using UnityEditor;
#if UNITY_2020_2_OR_NEWER
using UnityEditor.AssetImporters;
#else
using UnityEditor.Experimental.AssetImporters;
#endif

namespace TextureGradation
{
    [CustomEditor(typeof(TextureGradationImporter))]
    sealed class TextureGradationImporterEditor : ScriptedImporterEditor
    {
        SerializedProperty _shape;
        SerializedProperty _gradient;
        SerializedProperty _resolution;
        SerializedProperty _line;
        SerializedProperty _circular;
        SerializedProperty _radial;
        SerializedProperty _diagonal;
        SerializedProperty _box;

        public override void OnEnable()
        {
            base.OnEnable();
            _shape = serializedObject.FindProperty("_shape");
            _gradient = serializedObject.FindProperty("_gradient");
            _resolution = serializedObject.FindProperty("_resolution");
            _line = serializedObject.FindProperty("_line");
            _circular = serializedObject.FindProperty("_circular");
            _radial = serializedObject.FindProperty("_radial");
            _diagonal = serializedObject.FindProperty("_diagonal");
            _box = serializedObject.FindProperty("_box");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_shape);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_gradient);
            EditorGUILayout.PropertyField(_resolution);
            EditorGUILayout.Space();

            switch ((Shape)_shape.enumValueIndex)
            {
                case Shape.Line     : EditorGUILayout.PropertyField(_line); break;
                case Shape.Circular : EditorGUILayout.PropertyField(_circular); break;
                case Shape.Radial   : EditorGUILayout.PropertyField(_radial); break;
                case Shape.Diagonal : EditorGUILayout.PropertyField(_diagonal); break;
                case Shape.Box      : EditorGUILayout.PropertyField(_box); break;
            }

            serializedObject.ApplyModifiedProperties();
            ApplyRevertGUI();
        }

        [MenuItem("Assets/Create/GradationTexture")]
        public static void CreateNewAsset()
          => ProjectWindowUtil.CreateAssetWithContent
               ("New Gradation Texture.gradation", "");
    }
}

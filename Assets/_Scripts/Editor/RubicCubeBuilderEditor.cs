using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RubicCubeBuilder))]
public class RubicCubeBuilderEditor : Editor
{
    SerializedProperty rubicPiece;
    SerializedProperty rubicSize;
    SerializedProperty pieceSpace;

    RubicCubeBuilder rubicCubeBuilder;

    private void OnEnable()
    {
        rubicCubeBuilder = (RubicCubeBuilder)target;

        rubicPiece = serializedObject.FindProperty("rubicPiece");
        rubicSize = serializedObject.FindProperty("rubicSize");
        pieceSpace = serializedObject.FindProperty("pieceSpace");
    }

    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {

        GUILayout.BeginVertical("box");
        {
            EditorGUILayout.PropertyField(rubicPiece, new GUIContent("Piece Prefab", "Add Rubic Piece Prefab Want to Build"));
            GUILayout.Space(10);

            EditorGUILayout.PropertyField(rubicSize, new GUIContent("Rubic Size", "Set Rubic Cube grid size"));
            GUILayout.Space(10);

           EditorGUILayout.PropertyField(pieceSpace, new GUIContent("Piece Space", "Space between each cube piece in unit"));
           GUILayout.Space(10);          
        }
        GUILayout.EndVertical();
        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");
        {
            EditorGUILayout.BeginHorizontal();

            GUI.contentColor = Color.cyan;
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Space(80);
                if (GUILayout.Button("Build Rubic", GUILayout.MaxWidth(500f), GUILayout.MaxHeight(30f)))
                    rubicCubeBuilder.Build();
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}

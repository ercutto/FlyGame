using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RageRunGames.EasyFlyingSystem
{
    [CustomEditor(typeof(DroneController))]
    public class FlyControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            DroneController droneController = (DroneController)target;
            
            GUI.color = Color.white;

            EditorGUILayout.LabelField("Input Settings:", EditorStyles.boldLabel, GUILayout.Width(140));

            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Keyboard"))
            {
                droneController.AddKeyboardInputs();

                if (!Application.isPlaying)
                {
                    EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
                }
            }
            
            if (GUILayout.Button("Mobile"))
            {
                droneController.AddMobileInputs();

                if (!Application.isPlaying)
                {
                    EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
                }
            }
            
            if (GUILayout.Button("Mouse"))
            {
                droneController.AddMouseInputs();

                if (!Application.isPlaying)
                {
                    EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
                }
            }
            
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Current Input Selected:", EditorStyles.boldLabel, GUILayout.Width(140));
            GUI.color = Color.green;
            EditorGUILayout.LabelField( $"{droneController.GetInputType()}", EditorStyles.boldLabel, GUILayout.Width(70));
            EditorGUILayout.EndHorizontal();
        }
    }
}
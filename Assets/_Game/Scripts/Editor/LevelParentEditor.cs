using _Game.Scripts.Level;
using UnityEngine;
using UnityEditor;

namespace _Game.Scripts.Editor
{
    [CustomEditor(typeof(LevelParent))]
    public class LevelParentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var targetParent = (LevelParent)target;

            if (GUILayout.Button("Find all obstacle references"))
            {
                targetParent.GetAllObstacles();
            }
            
            if (GUILayout.Button("Find door references"))
            {
                targetParent.GetAllDoors();
            }

            if (GUILayout.Button("Distribute doors evenly"))
            {
                targetParent.DistributeDoors();
            }

            if (GUILayout.Button("Set Length of Level"))
            {
                targetParent.SetLengthOfLevel();
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "LevelData", menuName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
    public string levelName;
    public GameObject levelPrefab;

    public HiddenObject hiddenObject1;
    public HiddenObject hiddenObject2;
    
    public void SaveHiddenObject(HiddenObject hiddenObject, HiddenObjectTypes hiddenObjectType)
    {
        if (hiddenObjectType == HiddenObjectTypes.hiddenObject1)
        {
            hiddenObject1.Save(hiddenObject);
        }
        
        else if (hiddenObjectType == HiddenObjectTypes.hiddenObject2)
        {
            hiddenObject2.Save(hiddenObject);
        }
    }
}


#if UNITY_EDITOR

[CustomEditor(typeof(LevelData))]
public class HiddenObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (LevelData)target;

        GUILayoutOption layoutOption;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Select Object 1", GUILayout.Height(40)))
        {
            LevelCreator.instance.ChangeCurrentHiddenObject(script.hiddenObject1, script, HiddenObjectTypes.hiddenObject1);
        }
        
        if (GUILayout.Button("Select Object 2", GUILayout.Height(40)))
        {
            LevelCreator.instance.ChangeCurrentHiddenObject(script.hiddenObject2, script, HiddenObjectTypes.hiddenObject2);
        }
        GUILayout.EndHorizontal();
        
        if (GUILayout.Button("Reset", GUILayout.Height(40)))
        {
            LevelCreator.instance.ResetGrid();
        }
    }
}
#endif

public enum HiddenObjectTypes
{
    hiddenObject1,
    hiddenObject2
}
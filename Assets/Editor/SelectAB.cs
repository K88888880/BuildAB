using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelectAB :EditorWindow
{
    [MenuItem("Tools/AB")]
     public static void Init()
    {
        GetWindow<SelectAB>().Show();
        
    }




    private void OnGUI()
    {
        if (GUILayout.Button("≤‚ ‘"))
        {
            ABBuild.Build();
        }

        if (GUILayout.Button("’˝ Ω"))
        {
            ABBuild.Build();
        }
    }
}

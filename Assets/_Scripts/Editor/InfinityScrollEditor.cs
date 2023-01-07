using Assets._Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(InfinityScroll), true)]
public class InfinityScrollEditor : ScrollRectEditor
{
    //private SerializedProperty m_ItemPrefabsObject;
    private SerializedProperty m_Pool;

    protected override void OnEnable()
    {
        //m_ItemPrefabsObject = serializedObject.FindProperty(nameof(InfinityScroll.ItemPrefabs));
        m_Pool = serializedObject.FindProperty(nameof(InfinityScroll.Pool));

        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //EditorGUILayout.PropertyField(m_ItemPrefabsObject);
        EditorGUILayout.PropertyField(m_Pool);

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }
}

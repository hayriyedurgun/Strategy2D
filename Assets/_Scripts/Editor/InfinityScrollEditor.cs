using Assets._Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(InfinityScroll), true)]
public class InfinityScrollEditor : ScrollRectEditor
{
    SerializedProperty m_ItemPrefabObject;

    protected override void OnEnable()
    {
        m_ItemPrefabObject = serializedObject.FindProperty(nameof(InfinityScroll.ItemPrefab));

        base.OnEnable();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(m_ItemPrefabObject);

        base.OnInspectorGUI();
    }
}

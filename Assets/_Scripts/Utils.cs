using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public class Utils
    {
        public static TextMesh CreateWorldText(string text, Transform parent, Vector3 localPos, Color color, TextAnchor anchor)
        {
            var go = new GameObject("WordText", typeof(TextMesh));
            var transform = go.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPos;

            var textMesh = go.GetComponent<TextMesh>();
            textMesh.anchor = anchor;
            textMesh.alignment = TextAlignment.Center;
            textMesh.text = text;
            textMesh.color = color;
            textMesh.fontSize = 20;
            var renderer = textMesh.GetComponent<MeshRenderer>();
            renderer.sortingOrder = 5000;
            return textMesh;
        }
    }
}

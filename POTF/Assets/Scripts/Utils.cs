using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class Utils
{
    public const int sortingOrderDefault = 5000;
    public static TextMeshPro CreateWorldText(string text, Transform parent, Vector3 localPosition, int fontSize, Color color, TextAlignmentOptions textAlignment, int sortingOrder = sortingOrderDefault)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMeshPro));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public static T Pop<T>(this List<T> list)
    {
        if (list.Count > 0)
        {
            var element = list[0];
            list.RemoveAt(0);

            return element;
        }
        else
            throw new IndexOutOfRangeException("List is empty!");
    }
}

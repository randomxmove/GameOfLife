using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPLGridView : MonoBehaviour
{
    public void ClearCells()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
    }
}

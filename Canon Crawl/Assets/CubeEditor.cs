using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    [SerializeField] [Range(1f, 20f)] float gridSize = 10f;

    TextMesh textMesh;

    void Update()
    {
        Vector3 snapPos;

        snapPos.x = Mathf.Round(transform.position.x /gridSize) * gridSize;
        snapPos.z = Mathf.Round(transform.position.z / gridSize) * gridSize;

        textMesh = GetComponentInChildren<TextMesh>();
        textMesh.text = snapPos.x / gridSize + "," + snapPos.z / gridSize;

        transform.position = new Vector3(snapPos.x, 0, snapPos.z);

    }
}

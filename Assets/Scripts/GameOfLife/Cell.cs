using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cell : MonoBehaviour
{
    public bool On { get; private set; }
    private MeshRenderer _meshRenderer;
    public void SetCellActive(bool on)
    {
        On = on;
        if (On)
        {
            _meshRenderer.material.color = Color.black;
        }
        else
        {
            _meshRenderer.material.color = Color.white;
        }
    }

    private void Awake()
    {
        On = false;
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetCellActive(!On);
        }
    }

    
}

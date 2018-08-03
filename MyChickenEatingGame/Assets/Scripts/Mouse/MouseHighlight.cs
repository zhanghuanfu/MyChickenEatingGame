using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlight : MonoBehaviour {

    Color _oldColor;
    MeshRenderer _renderer;
	// Use this for initialization
	void Start () {
        _renderer = GetComponent<MeshRenderer>();
        _oldColor = _renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseEnter()
    {
        _renderer.material.color = Color.yellow;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _oldColor;
    }
}

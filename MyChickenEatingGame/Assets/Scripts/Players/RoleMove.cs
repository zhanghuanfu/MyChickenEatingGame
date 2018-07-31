using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMove : MonoBehaviour {

    Animator _animator;
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        _animator.SetFloat("SpeedX", hor);
        _animator.SetFloat("SpeedZ", ver);
 	}
}

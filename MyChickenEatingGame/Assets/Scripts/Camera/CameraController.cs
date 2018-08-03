using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform Neck;
    public Transform Role;

    public float XSensitivity = 180;
    public float YSensitivity = 180;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        transform.RotateAround(Neck.position, Role.right, -y * YSensitivity * Time.deltaTime);

        Role.Rotate(0, x * XSensitivity * Time.deltaTime, 0);
	}
}

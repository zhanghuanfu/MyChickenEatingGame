using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float ForceSize = 10;
    Rigidbody2D rigidBall;
    public Transform EmitPosition;

	// Use this for initialization
	void Start () {
        rigidBall = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        EmitBall();

	}

    void EmitBall()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - transform.position;
            direction.Normalize();

            rigidBall.bodyType = RigidbodyType2D.Dynamic;
            rigidBall.AddForce(direction * ForceSize, ForceMode2D.Impulse);
        }
    }

    public void ResetBall()
    {
        transform.position = EmitPosition.position;

        //rigidBall.velocity = Vector2.zero;
        //rigidBall.angularVelocity = 0;
        //rigidBall.mass = 0;
        rigidBall.bodyType = RigidbodyType2D.Static;
    }
}

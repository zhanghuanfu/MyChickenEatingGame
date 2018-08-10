using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float ForceSize = 10;
    Rigidbody2D rigidBall;
    public Transform EmitPosition;
    public Transform ResetPosition;

    public PhysicsMaterial2D Bounce;
    public PhysicsMaterial2D NoBounce;

    // Use this for initialization
    void Start () {
        rigidBall = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //EmitBall();

	}

    public void EmitBall()
    {
        BeforeEmit();

        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - EmitPosition.position;
        direction.Normalize();

        rigidBall.AddForce(direction * ForceSize, ForceMode2D.Impulse);
    }

    void BeforeEmit()
    {        
        transform.position = EmitPosition.position;
        rigidBall.velocity = Vector2.zero;
        rigidBall.angularVelocity = 0;

        GetComponent<CircleCollider2D>().sharedMaterial = Bounce;
    }

    public void ResetBall()
    {
        transform.position = ResetPosition.position;

        rigidBall.velocity = Vector2.zero;
        rigidBall.angularVelocity = 0;

        GetComponent<CircleCollider2D>().sharedMaterial = NoBounce;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public float ForceSize = 10;
    public float EmitInterval = 0.5f;
    public Transform EmitPosition;
    public float Bounciness = 1;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //start a coroutine
        StartCoroutine(EmitBalls());
            
	}

    //coroutine
    IEnumerator EmitBalls()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - EmitPosition.position;
            direction.Normalize();

            for (int i = 0; i < transform.childCount; i ++)
            {
                var ball = transform.GetChild(i);
                var ballRigidBody = ball.GetComponent<Rigidbody2D>();

                ball.position = EmitPosition.position;
                ballRigidBody.velocity = Vector2.zero;
                ballRigidBody.angularVelocity = 0;
                ball.GetComponent<CircleCollider2D>().sharedMaterial.bounciness = Bounciness;

                ballRigidBody.AddForce(direction * ForceSize, ForceMode2D.Impulse);

                yield return new WaitForSeconds(EmitInterval);
            }            
        }
    }
}

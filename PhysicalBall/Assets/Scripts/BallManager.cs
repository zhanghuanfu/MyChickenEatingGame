using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public float EmitInterval = 0.5f;

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
            //var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 direction = mousePos - EmitPosition.position;
            //direction.Normalize();

            for (int i = 0; i < transform.childCount; i ++)
            {
                transform.GetChild(i).GetComponent<Ball>().EmitBall();

                yield return new WaitForSeconds(EmitInterval);
            }            
        }
    }
}

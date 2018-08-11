using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    public float EmitInterval = 0.5f;
    public static BallManager GetInstance;

    // Use this for initialization
    void Start () {
        GetInstance = this;
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
            for (int i = 0; i < transform.childCount; i ++)
            {
                transform.GetChild(i).GetComponent<Ball>().EmitBall();

                yield return new WaitForSeconds(EmitInterval);
            }            
        }
    }

    public bool CheckAllBallBack()
    {
        foreach (Transform child in transform)
        {
            var ball = child.GetComponent<Ball>();
            if (ball.IsRunning) return false;
        }
        return true;
    }

    public void OneBallBack()
    {
        if(CheckAllBallBack())
        {
            ObstacleManager.GetInstance.ObstacleLineUp();
        }
    }
}

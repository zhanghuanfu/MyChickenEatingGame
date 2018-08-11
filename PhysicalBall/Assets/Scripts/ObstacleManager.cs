using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

    public float LowestLevel = -4f;
    public GameObject Text;
    public Transform Canvas;
    public GameObject[] Obstacles;
    public float[] HorizontalPositions = { 0.4f, -0.4f, 1.2f, -1.2f, 2f, -2f };

    public static ObstacleManager GetInstance;

	// Use this for initialization
	void Start () {
        buildObstacleLine();
        GetInstance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void buildObstacleLine()
    {
        foreach (float PosIndex in HorizontalPositions)
        {
            var random = UnityEngine.Random.Range(0, 2);
            if (random == 0) continue;

            var obstacleIndex = UnityEngine.Random.Range(0, Obstacles.Length);
            var obstacle = Obstacles[obstacleIndex];

            var realObstacle = Instantiate(obstacle, transform);
            realObstacle.transform.position = new Vector3(PosIndex, LowestLevel, -1);

            if (realObstacle.name == "Splite(Clone)" || realObstacle.name == "Epanse(Clone)")
            {
                return;
            }

            var ui = BuildText();
            realObstacle.GetComponent<Obstacle>().Init(ui);
        }        
    }

    public void ObstacleLineUp()
    {
        foreach (Transform item in transform)
        {            
            item.position += new Vector3(0, 1f, 0);
            var obstacle = item.GetComponent<Obstacle>();
            if (obstacle != null) obstacle.UpdateUI();
        }
            buildObstacleLine();
    }

    GameObject BuildText()
    {
        var ui = Instantiate(Text, Canvas);
        return ui;
    }


}

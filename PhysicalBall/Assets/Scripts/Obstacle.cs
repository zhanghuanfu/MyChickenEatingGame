using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour {

    public int Hp = 10;
    public Text HpText;
	// Use this for initialization
	void Start () {
        HpText.text = Hp.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hp -= 1;
        HpText.text = Hp.ToString();

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacle : MonoBehaviour {

    public int Hp = 10;
    public Text HpText;
	// Use this for initialization
	void Start () {
        
        //Init(HpText.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init(GameObject ui)
    {       

        HpText = ui.GetComponent<Text>();
        HpText.text = Hp.ToString();

        UpdateUI();
    }

    public void UpdateUI()
    {
        var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        HpText.transform.position = screenPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Hp -= collision.transform.GetComponent<Ball>().Damage;
        HpText.text = Hp.ToString();

        if (Hp <= 0)
        {
            Destroy(gameObject);
            Destroy(HpText);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (transform.name == "Splite(Clone)")
        {
            Destroy(gameObject);

            var oldBall = collision.gameObject;
            Instantiate(oldBall, oldBall.transform.parent);

            return;
        }

        if (transform.name == "Epanse(Clone)")
        {
            Destroy(gameObject);

            collision.transform.localScale *= 1.3f ;
            collision.GetComponent<Ball>().Damage = 2;

            return;
        }
    }
}

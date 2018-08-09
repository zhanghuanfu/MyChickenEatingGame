using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUI : MonoBehaviour {
    Text Hp;
    Text Kills;
    Text Death;
    Text BurdenDamage;
    Text CauseDamage;

    float kills;
    float death;
    float burdenDamage;
    float causeDamage;

    public static CountUI getInstance;

	// Use this for initialization
	void Start () {
        getInstance = this;
        Hp = transform.Find("Hp").GetComponent<Text>();
        Kills = transform.Find("Kills").GetComponent<Text>();
        Death = transform.Find("Death").GetComponent<Text>();
        BurdenDamage = transform.Find("BurdenDamage").GetComponent<Text>();
        CauseDamage = transform.Find("CauseDamage").GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SetHp(float hp)
    {
        if (hp < 0) hp = 0;
        Hp.text = "Hp:" + hp;
    }

    public void SetKills(float kill)
    {
        kills += kill;
        Kills.text = "Kills:" + kills;
    }

    public void SetDeath(float death)
    {
        this.death += death;
        Death.text = "Death:" + this.death;
    }

    public void SetBurdenDamage(float damage)
    {
        burdenDamage += damage;
        BurdenDamage.text = "BurdenDamage:" + burdenDamage;
    }

    public void SetCauseDamage(float damage)
    {
        causeDamage += damage;
        CauseDamage.text = "CauseDamage:" + causeDamage;
    }
}

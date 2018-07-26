using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTreat : MonoBehaviour {
    public int Hp = 100;
    public int Energy = 0;

    float _energyDecreaseTimer;
    float _energyToHpTimer;

	// Use this for initialization
	void Start () {
        HpFullCheck();
        HpLowestCheck();
        EnergyFullCheck();
        EnergyLowestCheck();
    }
	
	// Update is called once per frame
	void Update () {
        EnergyDecreaseByTime();
        HpRecoverByEnergy();

        KeyboardEventListener();
    }

    void HpRecoverByEnergy()
    {
        if (HpFullCheck()) return;
        if (EnergyLowestCheck()) return;

        _energyToHpTimer += Time.deltaTime;
        if (_energyToHpTimer < 8) return;

        //Recover Hp every 8 seconds and set Timer to zero
        if (Energy <= 20)
        {
            Hp += 1;
        }
        else if (Energy <= 60) 
        {
            Hp += 2;
        }
        else if (Energy <= 90)
        {
            Hp += 3;
        }
        else
        {
            Hp += 4;
        }

        HpFullCheck();
        Debug.Log("Hp Recover to : " + Hp);
        _energyToHpTimer = 0;
    }

    void EnergyDecreaseByTime()
    {
        if (EnergyLowestCheck()) return;

        _energyDecreaseTimer += Time.deltaTime;

        if (_energyDecreaseTimer < 3) return;

        //one Energy has been minus every 3 seconds and set Timer to zero
        Energy -= 1;
        Debug.Log("Energy decrease to : " + Energy);
        _energyDecreaseTimer = 0;
    }

    void KeyboardEventListener()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            BeenShot();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseBandage();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseEmergencyPackage();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UserMedicalBox();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UseRedBullDrinks();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UsePainMedicine();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            UseAdrenaline();
        }
    }

    void BeenShot()
    {
        int damage = Random.Range(0, Hp);
        Hp -= damage;
        Debug.Log("You have been shot, your Hp is : " + Hp);
    }

    void UseBandage()
    {
        if (HpOver75Check("Bandage")) return;

        Hp += 10;
        if (Hp > 75)
        {
            Hp = 75;
        }
        Debug.Log("Use Bandage recover Hp to : " + Hp);
    }

    void UseEmergencyPackage()
    {
        if (HpOver75Check("EmergencyPackage")) return;

        Hp = 75;
        Debug.Log("Use Emergency Package recover Hp to 75");
    }

    void UserMedicalBox()
    {
        if (HpFullCheck())
        {
            Debug.Log("Your Hp is Full ,can not use MedicalBox");
            return;
        }

        Hp = 100;
        Debug.Log("Use Medical Box to make the Hp Full !");
    }

    void UseRedBullDrinks()
    {
        if (EnergyFullCheck())
        {
            Debug.Log("Your Energy is Full , can not use RedBull Drinks");
            return;
        }

        Energy += 40;
        EnergyFullCheck();
        Debug.Log("Use RedBull Drinks to add Energy to : " + Energy);
    }

    void UsePainMedicine()
    {
        if (EnergyFullCheck())
        {
            Debug.Log("Your Energy is Full , can not use Pain Medicine");
            return;
        }

        Energy += 60;
        EnergyFullCheck();
        Debug.Log("Use Pain Medicine to add Energy to : " + Energy);
    }

    void UseAdrenaline()
    {
        if (EnergyFullCheck())
        {
            Debug.Log("Your Energy is Full , can not use Adrenaline");
            return;
        }

        Energy = 100;
        Debug.Log("Use Adrenaline to make the Energy Full !");
    }

    bool HpOver75Check(string prop)
    {
        if (Hp >= 75)
        {
            Debug.Log("Your Hp is over 75 , can not use the " + prop);
            return true;
        }
        return false;
    }

    bool HpFullCheck()
    {
        if (Hp >= 100)
        {
            if(Hp > 100) Hp = 100;
            return true;
        }
        return false;
    }

    bool HpLowestCheck()
    {
        if (Hp <= 0) 
        {
            //Hp = 100;
            Debug.Log("Your Hp is collapse ! Game Over !");
            GameObject.Find("Player").GetComponent<PlayerTreat>().enabled = false;
            return true;
        }
        return false;
    }

    bool EnergyFullCheck()
    {
        if (Energy >= 100)
        {
            if (Energy > 100) Energy = 100;
            return true;
        }
        return false;
    }

    bool EnergyLowestCheck()
    {
        if (Energy <= 0)
        {
            if (Energy < 0) Energy = 0;
            return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleGunPick : MonoBehaviour {

    Animator _animator;

    public GameObject[] Weapons;
    private GameObject _currentGun;

    // if collided with gun
    private bool isTrigger;

    // record the collider
    private Collider other;
    
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        GunStateEventListener();

    }

    void GunStateEventListener()
    {
        if (Input.GetKeyDown(KeyCode.F) && isTrigger)
        {
            //change role state and play the pick gun clip
            _animator.SetBool("WithGun", true);
            _animator.SetTrigger("PickGun");
            PickGun();
        }

        if (Input.GetKeyDown(KeyCode.G) && _currentGun != null)
        {
            DropGun();
            _animator.SetBool("WithGun", false);

            for (int i = 0; i < Weapons.Length; i++)
            {
                if (_currentGun.name == Weapons[i].name)
                {
                    Weapons[i].SetActive(false);
                }
            }
        }
    }

    void PickGun()
    {
        if (_currentGun != null)
        {
            // drop the gun in the hand
            DropGun();
        }

        for (int i = 0; i < Weapons.Length; i++)
        {
            if (other.name == Weapons[i].name)
            {
                _currentGun = Weapons[i];
                Weapons[i].SetActive(true);
            }
            else
            {
                Weapons[i].SetActive(false);
            }
        }

        Destroy(other.gameObject);
        isTrigger = false;

    }

    /*
    int FindTheWeaponIndex(GameObject gun)
    {
        for (int i = 0; i < Weapons.Length; i++)
        {
            if (gun.name == Weapons[i].name)
            {
                return i;
            }
        }
        return 0;
    }
    */

    void DropGun()
    {
        var dropedGun = Instantiate(_currentGun);
        dropedGun.transform.position = transform.position + transform.forward * 2 + new Vector3(0, 0.1f, 0);
        dropedGun.transform.rotation = Quaternion.identity;

        dropedGun.name = _currentGun.name;

        _currentGun.SetActive(false);
        _currentGun = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        isTrigger = true;
        this.other = other;
    }

    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
        this.other = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }
}

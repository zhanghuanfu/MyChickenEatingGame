﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleGunPick : MonoBehaviour {

    Animator _animator;

    public GameObject[] Weapons;
    private GameObject _currentGun;
    //the shoot time accumulate
    private float shotTimeSum;

    //shoot hole
    public GameObject Hole;

    // if collided with gun
    private bool isTrigger;
    // record the collider
    private Collider other;
    
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update () {

        GunStateEventListener();

        Shoot();       
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

    void Shoot()
    {
        if (_currentGun == null) return;

        if (!Input.GetButton("Fire1")) return;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction);

        RaycastHit hit;
        bool detected = Physics.Raycast(ray, out hit);

        if (detected)
        {
            Debug.Log(hit.collider.name);
        }

        var weaponController = _currentGun.GetComponent<Weapon>();
        if (weaponController == null) return;
        var shotInterval = weaponController.shotInterval;
        shotTimeSum += Time.deltaTime;
        if (shotTimeSum < shotInterval) return;

        var source = _currentGun.GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
            shotTimeSum = 0;
        }
        //shoot flash
        var muzzleFlash = _currentGun.transform.Find("MuzzleFlash");
        if (muzzleFlash != null)
        {
            muzzleFlash.gameObject.SetActive(true);
            Invoke("HideFlash", 0.05f);
        }

        //shoot bullethole
        if (detected)
        {
            //TODO more
            if (hit.collider.tag != "enemy") ShootBulletHole(hit);

        //send Message hit someone
        hit.collider.SendMessage("DoDamageToEnemy", weaponController.Damage, SendMessageOptions.DontRequireReceiver);
        }
        
    }

    void ShootBulletHole(RaycastHit hit)
    {
        var hole = Instantiate(Hole);

        Debug.Log(hit.transform.position);
        hole.transform.position = hit.point + hit.normal / 1000;
        //set hole image the bule axis to hit`s vertical face direction
        hole.transform.forward = hit.normal;

        //Debug.DrawRay(hit.point, hit.normal, Color.red, 5);

        //hole.transform.rotation = hit.collider.gameObject.transform.rotation;
        //hole.transform.Rotate(new Vector3(90, 0, 0));
        //hole.transform.rotation = Quaternion.Euler(90f, 0.0f, 0.0f);

        hole.SetActive(true);

        Destroy(hole, 6);
    }

    void HideFlash()
    {
        var muzzleFlash = _currentGun.transform.Find("MuzzleFlash");
        if (muzzleFlash != null)
        {
            muzzleFlash.gameObject.SetActive(false);
        }
    }
}

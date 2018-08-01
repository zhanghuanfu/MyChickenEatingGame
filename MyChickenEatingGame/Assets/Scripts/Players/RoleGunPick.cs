using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleGunPick : MonoBehaviour {

    Animator _animator;

    public GameObject[] Weapons;
    private GameObject _currentGun;
    private bool isTrigger;
    private Collider other;
    
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F) && isTrigger)
        {
            //change role state and play the pick gun clip
            _animator.SetBool("WithGun", true);
            PickGun();
        }
    }

    void PickGun()
    {
        if (_currentGun != null)
        {
            var dropedGun = Instantiate(_currentGun);
            dropedGun.transform.position = transform.position + transform.forward * 5 + new Vector3(0, 0.1f, 0);
            dropedGun.transform.rotation = Quaternion.identity;

            dropedGun.name = _currentGun.name;

            dropedGun.SetActive(false);
            _currentGun = null;
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

    private void OnTriggerEnter(Collider other)
    {
        isTrigger = true;
        this.other = other;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMove : MonoBehaviour {

    Animator _animator;
    // Use this for initialization
    void Start() {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        RoleMoveByAxis();

        RoleStateChangeListener();

    }

    void RoleMoveByAxis()
    {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        _animator.SetFloat("SpeedX", hor * 2.16f);
        _animator.SetFloat("SpeedZ", ver * 4.18f);
    }

    void RoleStateChangeListener()
    {
        JumpEventNoGunListener();

        CrouchEventNoGunListener();

        ProneEventNoGunListener();

        PicGunEventListener();
    }

    void JumpEventNoGunListener()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
        }
    }

    void CrouchEventNoGunListener()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //加Prone为true但Crouch为false的时候情况
            if (_animator.GetBool("Prone"))
            {
                _animator.SetBool("Prone", false);
            }

            if (!_animator.GetBool("Crouch"))
            {
                _animator.SetBool("Crouch", true);
                return;
            }
            _animator.SetBool("Crouch", false);
        }
    }

    void ProneEventNoGunListener()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_animator.GetBool("Crouch"))
            {
                _animator.SetBool("Crouch", false);
            }

            if (!_animator.GetBool("Prone"))
            {
                _animator.SetBool("Prone", true);
                return;
            }
            _animator.SetBool("Prone", false);
        }
    }

    void PicGunEventListener()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetBool("WithGun", true);
        }
    }
}

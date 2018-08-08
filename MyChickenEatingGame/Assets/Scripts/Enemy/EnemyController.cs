using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float Hp = 100;
    public float ShootDistance = 4;
    private float shotDeltaTimeSum;

    public GameObject EnemyGun;
    private Vector3 enemyStopDistance;
    public Transform EnemyRelifePoint;
    public Transform Target;

    NavMeshAgent _agent;
    Animator _animator;

	// Use this for initialization
	void Start () {
        enemyStopDistance = new Vector3(0, 0, -3f);
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Hp > 0)
        {
            _animator.SetBool("WithGun", true);

            _agent.SetDestination(Target.position + enemyStopDistance);

            //set the world axis velocity to this model`s axis velocity 
            var velocity = transform.InverseTransformDirection(_agent.desiredVelocity);

            _animator.SetFloat("SpeedX", velocity.x);
            _animator.SetFloat("SpeedZ", velocity.z);

            if (Vector3.Distance(transform.position, Target.position) <= ShootDistance)
            {
                Shoot();
            }
        }
	}

    void DoDamageToEnemy(float damage)
    {
        Hp -= damage;
        if (Hp <= 0) EnemyOnDead();
    }

    void EnemyOnDead()
    {
        _animator.SetTrigger("EnemyDying");
        _agent.isStopped = true;
    }

    void EnemyFinishedDying()
    {
        _animator.SetTrigger("EnemyReLife");
        transform.position = EnemyRelifePoint.position;
        Hp = 100;
        _agent.isStopped = false;
    }

    void Shoot()
    {
        var weaponController = EnemyGun.GetComponent<Weapon>();
        if (weaponController == null) return;
        var shotInterval = 2;
        //var shotInterval = weaponController.shotInterval;
        shotDeltaTimeSum += Time.deltaTime;
        if (shotDeltaTimeSum < shotInterval) return;

        var source = EnemyGun.GetComponent<AudioSource>();
        if (source != null)
        {
            source.Play();
            shotDeltaTimeSum = 0;
        }
        //shoot flash
        var muzzleFlash = EnemyGun.transform.Find("MuzzleFlash");
        if (muzzleFlash != null)
        {
            muzzleFlash.gameObject.SetActive(true);
            Invoke("HideFlash", 0.05f);
        }

        //shoot bullethole

        //send Message hit someone
        Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
        //GameObject.Find("Role").SendMessage("DoDamageToPlayer", weaponController.Damage, SendMessageOptions.DontRequireReceiver);
        GameObject.Find("Role").GetComponent<PlayerTreat>().DoDamageToPlayer(weaponController.Damage);
        //SendMessage("DoDamageToPlayer", weaponController.Damage, SendMessageOptions.DontRequireReceiver);
    }

    void HideFlash()
    {
        var muzzleFlash = EnemyGun.transform.Find("MuzzleFlash");
        if (muzzleFlash != null)
        {
            muzzleFlash.gameObject.SetActive(false);
        }
    }
}

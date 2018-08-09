using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour {

    public float Hp = 100;
    public float ShootDistance = 4;
    public float discoverDistance = 500;
    private float distanse;
    private float shotDeltaTimeSum;

    public GameObject EnemyGun;
    private Vector3 playerBodyCenter;
    private Vector3 muzzlePointToBody;
    private Vector3 enemyStopDistance;
    public Transform EnemyRelifePoint;
    public Transform Target;

    NavMeshAgent _agent;
    Animator _animator;

	// Use this for initialization
	void Start () {
        playerBodyCenter = new Vector3(0, 1.2f, 0);
        muzzlePointToBody = new Vector3(1.6f, 2f, 0);
        enemyStopDistance = new Vector3(0, 0, -3f);

        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Hp <= 0) return;

        WalkAround();

        distanse = Vector3.Distance(transform.position, Target.position);
        if (distanse <= discoverDistance)
        {
            MoveAndShootPlayer();
        }
        
	}

    void WalkAround()
    {

    }

    void MoveAndShootPlayer()
    {                
        _animator.SetBool("WithGun", true);

        _agent.SetDestination(Target.position + enemyStopDistance);

        //set the world axis velocity to this model`s axis velocity 
        var velocity = transform.InverseTransformDirection(_agent.desiredVelocity);

        _animator.SetFloat("SpeedX", velocity.x);
        _animator.SetFloat("SpeedZ", velocity.z);

        if (distanse <= ShootDistance)
        {
            Shoot();
        }
        
    }

    void DoDamageToEnemy(float damage)
    {
        //can`t shoot the dead body
        if (Hp <= 0) return;

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
        
        var shotInterval = weaponController.shotInterval;
        shotDeltaTimeSum += Time.deltaTime;
        if (shotDeltaTimeSum < shotInterval) return;

        //make the forward direction to the target
        transform.LookAt(Target.position + new Vector3(2.5f, 0, 0));
        //Random the fire target in an area
        var fireTarget = Target.position + Random.insideUnitSphere * 1 + playerBodyCenter;

        //muzzle position
        var muzzlePoint = transform.position + muzzlePointToBody;

        //fire direction
        var fireDir = fireTarget - muzzlePoint;

        //ray detection
        RaycastHit hit;
        bool detected = Physics.Raycast(muzzlePoint, fireDir, out hit);

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

        //send Message hit someone
        if (detected)
        {            
            hit.collider.SendMessage("DoDamageToPlayer", weaponController.Damage, SendMessageOptions.DontRequireReceiver);
            Debug.Log(hit.collider.name);
        }
        //GameObject.Find("Role").SendMessage("DoDamageToPlayer", weaponController.Damage, SendMessageOptions.DontRequireReceiver);
        //GameObject.Find("Role").GetComponent<PlayerTreat>().DoDamageToPlayer(weaponController.Damage);
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

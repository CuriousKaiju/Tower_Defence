using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Pool))]
public class Gun : MonoBehaviour
{
    [Header("ATTACK")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float delayBetweenAttacks;
    private bool gunIsAttacking;
    private bool gunCanAttack = true;

    [Header("ROTATION TO TARGET")]
    [SerializeField] private GameObject target;
    [SerializeField] private Transform towerHeadTransform;
    [SerializeField] private LookingForTarget lookingForTargetScript;

    [Header("ROTATION TO BASE")]
    [SerializeField] private float desiredTimeToRotation;
    private float elapsedTime;
    private bool gunOnTheStartPos = true;

    [Header("POOL")]
    [SerializeField] private Transform bulletStartPos;
    private Pool poolObjects;

    [Header("ANIMATIONS")]
    [SerializeField] private GameObject blowParticles;
    [SerializeField] private GameObject gunVisual;
    private Animator animator;

    [Header("UPGRADE")]
    [SerializeField] private int gunLevel;
    [SerializeField] private GameObject nextEvolutionForm;

    public void UpgradeGun(Platform plat)
    {
        plat.DestoryCurrentGun();
        plat.SpawnNewGun(nextEvolutionForm);      
    }
    public int GetGunLevel()
    {
        return gunLevel;
    }
    private void Awake()
    {
        GameEvents.GameStop += ChangeGunStatus;
    }
    private void OnDestroy()
    {
        GameEvents.GameStop -= ChangeGunStatus;
    }
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        poolObjects = gameObject.GetComponent<Pool>();
    }

    void Update()
    {
        if(gunCanAttack)
        {
            StateManager();
        }
    }
    private void FollowTheTarget()
    {
        Vector3 difference = target.transform.position - towerHeadTransform.position;
        float rotationY = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        towerHeadTransform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
    }
    private void RotationToBaseState()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredTimeToRotation;
        towerHeadTransform.rotation = Quaternion.Lerp(towerHeadTransform.rotation, Quaternion.Euler(0, 0, 0), percentageComplete);
        if (percentageComplete >= 1)
        {
            elapsedTime = 0;
        }
    }
    private void StateManager()
    {
        if (target)
        {
            if (!gunIsAttacking)
            {
                StartCoroutine("FireCoroutine");
                animator.SetBool("Fire", true);
                gunIsAttacking = true;
            }
            FollowTheTarget();
        }
        else if (!target)
        {

            target = lookingForTargetScript.FindNearestEnemy();

            if (gunIsAttacking)
            {
                StopCoroutine("FireCoroutine");
                animator.SetBool("Fire", false);
                gunIsAttacking = false;
            }
            RotationToBaseState();
        }
    }
    private void Shoot()
    {
        poolObjects.GetFreeElement(bulletStartPos.position, bulletStartPos.rotation);
    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }
    private void ChangeGunStatus()
    {
        gunCanAttack = false;
        StopCoroutine("FireCoroutine");
        animator.SetBool("Fire", false);
        TowerDestroy();
    }
    private void TowerDestroy()
    {
        gunVisual.SetActive(false);
        blowParticles.SetActive(true);
    }
    
}

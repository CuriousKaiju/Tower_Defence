using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    [Header("MAIN FUNCTIONS")]
    [SerializeField] private GameObject followObject;
    [SerializeField] private GameObject trashBox;
    [SerializeField] private float enemySpeed;
    private bool isAction;

    [Header("HEALTH BAR")]
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Image healthBarRedFill;
    [SerializeField] private int enemyHealth;
    private int currentHealth;

    [Header("ANIMATION")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject booldParticles;
    [SerializeField] private Transform particlePoint;

    [Header("AWARDS VALUE")]
    [SerializeField] private int[] coinsFromEnemy;

    private Collider enemyCollider;
    private Rigidbody enemyRigidbody;

    private void Awake()
    {
        GameEvents.GameStatusAction += InitiateEnemyMove;
        GameEvents.GameStop += StopMoveAndFinish;
    }
    private void OnDestroy()
    {
        GameEvents.GameStatusAction -= InitiateEnemyMove;
        GameEvents.GameStop -= StopMoveAndFinish;
    }
    void Start()
    {
        enemyRigidbody = gameObject.GetComponent<Rigidbody>();
        enemyCollider = gameObject.GetComponent<Collider>();
        currentHealth = enemyHealth;
    }
    private void Update()
    {
        if(isAction)
        {
            Move();
        }
    }
    private void ChangeHealthBarStatus()
    {
        float healthPercentage = (float)currentHealth / (float)enemyHealth;
        healthBarRedFill.fillAmount = healthPercentage;
    }
    public void GetDamage(int damage)
    {
        healthBar.SetActive(true);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DestroyEnemyPhase_1();
        }
        ChangeHealthBarStatus();
    }
    private void DestroyEnemyPhase_1()
    {
        GameEvents.OnEnemyDieAction(coinsFromEnemy[Random.Range(0, coinsFromEnemy.Length)]); //ACTION
        GameEvents.OnClearFollowObjectFromTargetList(followObject);

        enemySpeed = 0;
        Destroy(followObject);
        Instantiate(booldParticles, particlePoint.position, Quaternion.Euler(0, 0, 0)).transform.SetParent(trashBox.transform);
        animator.SetTrigger("Die");
        healthBar.SetActive(false);
        enemyCollider.enabled = false;
    }
    public void DestroyEnemyPhase_2()
    {
        enemyRigidbody.useGravity = true;
    }
    public void DestroyEnemyPhase_3()
    {
        Destroy(gameObject);
    }
    public void Move()
    {
        transform.Translate(-Vector3.forward * enemySpeed * Time.deltaTime);
    }
    public GameObject GetFollowObject()
    {
        return followObject;
    }
    private void InitiateEnemyMove(bool flag)
    {
        isAction = flag;
        animator.SetTrigger("Start");
    }
    private void StopMoveAndFinish()
    {
        isAction = false;
        animator.SetTrigger("Win");
    }
}
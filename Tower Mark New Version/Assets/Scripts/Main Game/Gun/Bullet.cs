using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolObject))]
public class Bullet : MonoBehaviour
{
    [Header("BULLET LIFE PARAMS")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float desiredTimeToDie;
    [SerializeField] private GameObject destroyPaticles;
    private float elapsedTime;
    private PoolObject poolObject;

    [Header("BULLET ATTACK PARAMS")]
    [SerializeField] private int bulletdamage;

    [Header("BULLET VISUAL")]
    [SerializeField] private TrailRenderer trail;

    private Transform container;

    private void OnEnable()
    {

    }
    private void Start()
    {
        //container = FindObjectOfType<ThrashBox>().transform;
        poolObject = gameObject.GetComponent<PoolObject>();
    }
    private void Update()
    {
        BulletMove();
    }
    private void DetroyObject()
    {
        elapsedTime = 0;
        trail.Clear();
        poolObject.ReturnToPool();
        Instantiate(destroyPaticles, transform.position, Quaternion.Euler(0, 0, 0)).transform.SetParent(transform.parent);
    }
    private void BulletMove()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredTimeToDie;
        if(percentageComplete >= 1)
        {
            DetroyObject();
        }
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetDamage(bulletdamage);
            DetroyObject();
        }
    }
    

}

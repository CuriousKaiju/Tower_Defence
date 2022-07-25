using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingForTarget : MonoBehaviour
{
    [SerializeField] private List<GameObject> listOfEnemies = new List<GameObject>();
    private GameObject nearesEnemy;

    private void Awake()
    {
        GameEvents.ClearFollowObjectFromTargetList += DeleteObjectFormList;
    }
    private void OnDestroy()
    {
        GameEvents.ClearFollowObjectFromTargetList -= DeleteObjectFormList;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
           listOfEnemies.Add(other.gameObject.GetComponent<Enemy>().GetFollowObject());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            listOfEnemies.Remove(other.gameObject.GetComponent<Enemy>().GetFollowObject());
        }
    }
    private void DeleteObjectFormList(GameObject followTarget)
    {
        listOfEnemies.Remove(followTarget);
    }
    public GameObject FindNearestEnemy()
    {
        if(listOfEnemies.Count > 0)
        {
            foreach (GameObject enemy in listOfEnemies)
            {
                if (nearesEnemy)
                {
                    if (enemy.transform.position.z < nearesEnemy.transform.position.z)
                    {
                        nearesEnemy = enemy;
                    }
                }
                else
                {
                    nearesEnemy = enemy;
                }
            }
            return nearesEnemy;
        }
        return null;
    }
        

}

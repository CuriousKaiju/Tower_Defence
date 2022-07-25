using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWall : MonoBehaviour
{
    [SerializeField] private float delayBeforeLosePopUpOpen;
    [SerializeField] private WindowsManager windowsManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameEvents.OnGameStop();
            StartCoroutine("DelayBeforLosePopUpWilBeOpen");
        }
    }
    private IEnumerator DelayBeforLosePopUpWilBeOpen()
    {
        yield return new WaitForSeconds(delayBeforeLosePopUpOpen);
        windowsManager.OpenLoseMenu();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private bool platformConsistGun;
    private GameObject currentGunOnThePlatfrom;

    [SerializeField] private Transform spawnGunPoint;
    [SerializeField] private GameObject trashBox;
    [SerializeField] private GameObject currentGun;
    [SerializeField] private GameObject whiteBodyOfPlatform;


    public void DestoryCurrentGun()
    {
        Destroy(currentGun);
    }
    public int GetCurrentGunID()
    {
        if(currentGun)
        {
            return currentGun.GetComponent<Gun>().GetGunLevel();
        }
        else
        {
            return 0;
        }
    }
    public void SetColorOfWhiteBodyOFPlatform(Color newColor)
    {
        whiteBodyOfPlatform.GetComponent<MeshRenderer>().material.color = newColor;
    }
    public void ChangePlatformStatus(bool status)
    {
        platformConsistGun = status;
    }
    public bool IsGunOnThePlatform()
    {
        return platformConsistGun;
    }

    public void SpawnNewGun(GameObject gun)
    {
        platformConsistGun = true;
        Instantiate(gun, spawnGunPoint.position, Quaternion.identity).transform.SetParent(transform);
        SetCurrentGun(gun);
    }

    public GameObject GetTrashBox()
    {
        return trashBox;
    }
    public bool isPlatformFree()
    {
        if(platformConsistGun)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public Transform GetSpawnGunPoint()
    {
        return spawnGunPoint;
    }
    public bool GetPlatformStatus()
    {
        return platformConsistGun;
    }
    public void SetCurrentGun(GameObject gun)
    {
        currentGun = gun;
    }
    public GameObject GetWhiteBodyOfPlatform()
    {
        return whiteBodyOfPlatform;
    }
}

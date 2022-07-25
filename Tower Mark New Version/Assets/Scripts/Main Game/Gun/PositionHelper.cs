using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionHelper : MonoBehaviour
{
    [SerializeField] private Platform startPlatfrom;
    [SerializeField] private LayerMask pltformLayer;

    [SerializeField] private float rayOfset;
    [SerializeField] private GameObject Plat;

    [SerializeField] private Gun gun;
    void Start()
    {
        startPlatfrom = gameObject.transform.parent.gameObject.GetComponent<Platform>();
        startPlatfrom.SetCurrentGun(gameObject);
    }
    private void Update()
    {
        
    }
    public Platform GetStartPlatform()
    {
        return startPlatfrom;
    }
    public GameObject GetDownPlatform()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitt;
        if (Physics.Raycast(ray, out hitt, 100, pltformLayer))
        {
            var nextPlat = hitt.collider.gameObject;
            if (nextPlat.CompareTag("Platform") && !nextPlat.GetComponent<Platform>().GetPlatformStatus()) //cюда проверку на тип пушки
            {
                startPlatfrom.ChangePlatformStatus(false);
                startPlatfrom.SetCurrentGun(null);
                Plat = hitt.collider.gameObject;
                Plat.GetComponent<Platform>().ChangePlatformStatus(true);
                Plat.GetComponent<Platform>().SetCurrentGun(gameObject);
                startPlatfrom = Plat.GetComponent<Platform>();
            }
            else if (nextPlat.CompareTag("Platform") && nextPlat.GetComponent<Platform>().GetPlatformStatus())
            {
                if (nextPlat == startPlatfrom.gameObject)
                {
                    Debug.Log("StarPlat");
                    Plat = startPlatfrom.gameObject;
                }
                else if(nextPlat.GetComponent<Platform>().GetCurrentGunID() == 5)
                {
                    Plat = startPlatfrom.gameObject; //new
                }     
                else if (gun.GetGunLevel() == nextPlat.GetComponent<Platform>().GetCurrentGunID())
                {
                    startPlatfrom.ChangePlatformStatus(false);
                    startPlatfrom.SetCurrentGun(null);
                    Plat = hitt.collider.gameObject;
                    //апгрейд
                    gun.UpgradeGun(Plat.GetComponent<Platform>());

                    Debug.Log("Upgrade");
                    Plat.GetComponent<Platform>().ChangePlatformStatus(true);
                    startPlatfrom = Plat.GetComponent<Platform>();
                    Plat = null;
                }
                else
                {
                    Plat = startPlatfrom.gameObject;
                }
            }
            else
            {
                Plat = startPlatfrom.gameObject;
            }    
        }
        else
        {
           Plat = startPlatfrom.gameObject;
        }

        return Plat;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GunManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gunTypes;
    [SerializeField] private int currentGunID;

    [SerializeField] private Platform[] platformsForGuns;
    [SerializeField] private List<Platform> currentFreePlatforms;

    [SerializeField] private GameObject trashBox;

    [SerializeField] private PlatformsForSaveList saveMass = new PlatformsForSaveList();

    [SerializeField] private int platformID;
    [SerializeField] private int gunLevel;

    private string path;

    private void Awake()
    {
        GameEvents.SaveAction += DoSave;
    }
    private void OnDestroy()
    {
        GameEvents.SaveAction -= DoSave;
    }
    private void Start()
    {
        path = Application.persistentDataPath;
        FromJson();
        CreateNewGunsAfterSaves();
    }
    public void CheckAllPlatformsWithIdebtityLevel(int gunLevel)
    {
        foreach (Platform plat in platformsForGuns)
        {
            if (plat.GetPlatformStatus())
            {
                if (plat.GetCurrentGunID() == gunLevel)
                {
                    plat.SetColorOfWhiteBodyOFPlatform(Color.green);
                }
            }
        }
    }
    public void SetWhiteStatusForPlatforms()
    {
        foreach (Platform plat in platformsForGuns)
        {
            plat.SetColorOfWhiteBodyOFPlatform(Color.white);
        }
    }
    private Platform GetFreePlatform()
    {
        currentFreePlatforms.Clear();

        foreach (Platform platform in platformsForGuns)
        {
            if (!platform.IsGunOnThePlatform())
            {
                currentFreePlatforms.Add(platform);
            }
            else
            {
                currentFreePlatforms.Remove(platform);
            }
        }

        if (currentFreePlatforms.Count > 0)
        {
            var freePlatform = currentFreePlatforms[Random.Range(0, currentFreePlatforms.Count - 1)];
            currentFreePlatforms.Remove(freePlatform);
            return freePlatform;
        }
        else
        {
            return null;
        }
    }
    public void TrySpawnNewGun()
    {
        GetFreePlatform()?.SpawnNewGun(gunTypes[currentGunID]);
    }
    public void TrySpawnNewGun(int gunID)
    {
        GetFreePlatform()?.SpawnNewGun(gunTypes[gunID]);
    }



    public void DoSave()
    {
        Debug.Log("save");
        for (int i = 0; i < platformsForGuns.Length; i++)
        {
            saveMass.platformsForSaveList[i].platformID = i;
            saveMass.platformsForSaveList[i].isGunExist = platformsForGuns[i].GetPlatformStatus();
            saveMass.platformsForSaveList[i].levelOftheGun = platformsForGuns[i].GetCurrentGunID();
        }
        ToJson();
    }
    public void ToJson()
    {
        string strToJson = JsonUtility.ToJson(saveMass);
        Debug.Log(strToJson);
        File.WriteAllText(path + "/saves.txt", strToJson);
    }
    public void FromJson()
    {
        string masString = File.ReadAllText(path + "/saves.txt");
        Debug.Log(masString);
        saveMass = JsonUtility.FromJson<PlatformsForSaveList>(masString);
    }
    public void CreateNewGunsAfterSaves()
    {
        for (int i = 0; i < platformsForGuns.Length; i++)
        {
            if (saveMass.platformsForSaveList[i].isGunExist)
            {
                int lvlOfGun = saveMass.platformsForSaveList[i].levelOftheGun;
                platformsForGuns[i].SpawnNewGun(gunTypes[lvlOfGun - 1]);
            }
        }
    }

    public void ClearJson()
    {
        for (int i = 0; i < platformsForGuns.Length; i++)
        {
            saveMass.platformsForSaveList[i].platformID = i;
            saveMass.platformsForSaveList[i].isGunExist = false;
            saveMass.platformsForSaveList[i].levelOftheGun = 0;
        }
        ToJson();
    }

}

[System.Serializable]
public class PlatformForSave
{
    public int platformID;
    public bool isGunExist;
    public int levelOftheGun;
}

[System.Serializable]
public class PlatformsForSaveList
{
    public PlatformForSave[] platformsForSaveList;
}

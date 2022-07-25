using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("BLACK SCREEN")]
    [SerializeField] private Animator blackScreenAnimator;

    [SerializeField] private WindowsManager windowsManager;

    [SerializeField] private GameObject NextLevelButton;

    private int totalEnemyValue; //10 health = 1 point
    private int enemiesWasDie;

    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Slider levelProgress;
    [SerializeField] private TMP_Text coinsCountText;
    [SerializeField] private TMP_Text currentGunSostText;

    [SerializeField] private GunManager gunManager;

    private int coinsCount = 1;
    [SerializeField] private int currentGunCost;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private float delayBetweenCompleteLevel;

    [SerializeField] private string currentlevelName;

    private void SaveCoinsAndLevel()
    {
        PlayerPrefs.SetInt("CoinsValue", coinsCount);
        PlayerPrefs.SetInt("CurrentGunCost", currentGunCost);
        PlayerPrefs.SetString("Level", currentlevelName);
    }
    private void LoadCoinsAndLevel()
    {
        coinsCount = PlayerPrefs.GetInt("CoinsValue", coinsCount);
        currentGunCost = PlayerPrefs.GetInt("CurrentGunCost", currentGunCost);
        currentlevelName = PlayerPrefs.GetString("Level", currentlevelName);
    }
    public void ClearCoinsStatus()
    {
        PlayerPrefs.SetInt("CoinsValue", 1);
        PlayerPrefs.SetInt("CurrentGunCost", 1);
        PlayerPrefs.SetString("Level", "Level 1");
    }
    private void Awake()
    {
        LoadCoinsAndLevel();
        GameEvents.EnemyDieAction += ChangeCoinValue;
    }
    private void OnDestroy()
    {
        GameEvents.EnemyDieAction -= ChangeCoinValue;
    }
    void Start()
    {
        CheackButtonFunctions();
        coinsCountText.text = coinsCount.ToString();
        currentGunSostText.text = currentGunCost.ToString();
        totalEnemyValue = enemies.Count;
    }
    private void EnemyWasDie()
    {
        enemiesWasDie += 1;
        levelProgress.value = (float)enemiesWasDie / (float)totalEnemyValue;
        if(levelProgress.value >= 1)
        {
            StartCoroutine("LevelComplete");
        }
    }
    private void ChangeCoinValue(int adCoins)
    {
        EnemyWasDie();
        coinsCount += adCoins;
        coinsCountText.text = coinsCount.ToString();
        CheackButtonFunctions();
        SaveCoinsAndLevel();
    }

    public void CreateNewGun()
    {
        coinsCount -= currentGunCost;
        coinsCountText.text = coinsCount.ToString();
        gunManager.TrySpawnNewGun();
        currentGunCost += 1;
        currentGunSostText.text = currentGunCost.ToString();
        CheackButtonFunctions();
        SaveCoinsAndLevel();
        GameEvents.OnSaveAction();
    }
    private void CheackButtonFunctions()
    {
        if(coinsCount < currentGunCost)
        {
            SetButtonStatus(false);
        }
        else
        {
            SetButtonStatus(true);
        }
    }
    private void SetButtonStatus(bool status)
    {
        buyButton.interactable = status;
    }
    private IEnumerator LevelComplete()
    {
        yield return new WaitForSeconds(delayBetweenCompleteLevel);
        winPopUp.SetActive(true);
        blackScreenAnimator.SetTrigger("Open");
        windowsManager.CloseGameMenu();

        yield return new WaitForSeconds(1);
        NextLevelButton.SetActive(true);
    }
    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}

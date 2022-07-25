using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    [Header("BLACK SCREEN")]
    [SerializeField] private Animator blackScreenAnimator;

    [Header("MAIN MENU")]
    [SerializeField] private Animator mainMenuAnimator;

    [Header("GAME MENU")]
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private Animator gameMenuAnimator;

    [Header("LOSE MENU")]
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private Animator loseMenuAnimator;

    [Header("BLACK FADE")]
    [SerializeField] private GameObject blackFade;
    [SerializeField] private Animator blackFadeAnimator;
    [SerializeField] private BlackFade blackFadeScript;

    private bool initiateGame = true;
    public void CloseMainMenu()
    {
        gameMenu.SetActive(true);
        blackScreenAnimator.SetTrigger("Close");
        mainMenuAnimator.SetTrigger("Close");
    }
    public void StartAction()
    {
        if(initiateGame)
        {
            GameEvents.OnGameStatusAction(initiateGame);
            initiateGame = false;
        }
    }
    public void OpenLoseMenu()
    {
        blackScreenAnimator.SetTrigger("Open");
        loseMenu.SetActive(true);
        gameMenuAnimator.SetTrigger("Close");
    }
    public void RetryLevel(string sceneName)
    {
        loseMenuAnimator.SetTrigger("Close");
        blackFade.SetActive(true);
        blackFadeAnimator.SetTrigger("Open");
        blackFadeScript.SetNextSceneName(sceneName);
    }
    public void CloseGameMenu()
    {
        gameMenuAnimator.SetTrigger("Close");
    }

}

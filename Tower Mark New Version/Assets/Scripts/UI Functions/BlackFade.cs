using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackFade : MonoBehaviour
{
    private string levelAfterLoadName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelAfterLoadName);
    }
    public void SetNextSceneName(string levelName)
    {
        levelAfterLoadName = levelName;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    [SerializeField] private Enemy enemeyMainScript;

    public void DieAction()
    {
        enemeyMainScript.DestroyEnemyPhase_2();
    }
    public void TotalDieAction()
    {
        enemeyMainScript.DestroyEnemyPhase_3();
    }
}

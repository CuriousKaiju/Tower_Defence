using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class GameEvents
{
    public static event Action<int> EnemyDieAction;

    public static event Action<GameObject> ClearFollowObjectFromTargetList;

    public static event Action<bool> GameStatusAction;

    public static event Action GameStop;

    public static event Action SaveAction;


    public static void OnEnemyDieAction(int valueFromEnemy)
    {
        EnemyDieAction?.Invoke(valueFromEnemy);
    }
    public static void OnClearFollowObjectFromTargetList(GameObject followTarget)
    {
        ClearFollowObjectFromTargetList?.Invoke(followTarget);
    }
    public static void OnGameStatusAction(bool flag)
    {
        GameStatusAction?.Invoke(flag);
    }

    public static void OnGameStop()
    {
        GameStop?.Invoke();
    }
    public static void OnSaveAction()
    {
        SaveAction?.Invoke();
    }



}

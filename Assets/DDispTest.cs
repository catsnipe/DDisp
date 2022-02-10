using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDispTest : MonoBehaviour
{
    const string GROUP_PLAYER = "Player";
    const string GROUP_ENEMY  = "Enemy";

    void Start()
    {
        Debug.Log("[Console]Debug.Log");
        Debug.LogWarning("[Console]Debug.LogWarning");
        
        DDisp.AddGroup(GROUP_PLAYER);
        DDisp.AddGroup(GROUP_ENEMY);
    }

    int cnt = 0;

    // Update is called once per frame
    void Update()
    {
        DDisp.Log($"count: {cnt++}");

        DDisp.Log($"�@�@�g��: 100", GROUP_PLAYER);
        DDisp.Log($"�@�U����:  80", GROUP_PLAYER);
        DDisp.Log($"�@�h���:  90", GROUP_PLAYER);
        DDisp.Log($"�@�@���x: 120", GROUP_PLAYER);
        DDisp.Log($"�J�E���g: {cnt}", GROUP_PLAYER);

        DDisp.DisplayGroup(GROUP_ENEMY);
        for (int i = 0; i < 100; i++)
        {
            DDisp.Log($"Enemy{i+1, 3} Hp: {100 * i, 5}");
        }
        DDisp.ResetDisplayGroup();
    }
}

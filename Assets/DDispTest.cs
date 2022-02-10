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
        DDisp.Log($"display {cnt++}");

        DDisp.Log($"player Hp: 100", GROUP_PLAYER);

        DDisp.DisplayGroup(GROUP_ENEMY);
        for (int i = 0; i < 100; i++)
        {
            DDisp.Log($"Enemy{i+1} Hp: {100 * i}");
        }
        DDisp.ResetDisplayGroup();
    }
}

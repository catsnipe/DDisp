using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    const string GROUP_PLAYER = "Player";
    const string GROUP_ENEMY  = "Enemy";

    void Start()
    {
        Debug.Log("[Console]Debug.Log");
        Debug.LogWarning("[Console]Debug.LogWarning");
        
        // add user group
        DDisp.AddGroup(GROUP_PLAYER);
        DDisp.AddGroup(GROUP_ENEMY);
    }

    int cnt = 0;

    // Update is called once per frame
    void Update()
    {
        // Group: Display
        DDisp.Log($"count: {cnt++}");

        // Group: Player
        DDisp.Log($"　　Ｈｐ: 100", GROUP_PLAYER);
        DDisp.Log($"　攻撃力:  80", GROUP_PLAYER);
        DDisp.Log($"　防御力:  90", GROUP_PLAYER);
        DDisp.Log($"　　速度: 120", GROUP_PLAYER);
        DDisp.Log($"カウント: {cnt}", GROUP_PLAYER);

        // Group: Enemy
        DDisp.DisplayGroup(GROUP_ENEMY);
        for (int i = 0; i < 100; i++)
        {
            DDisp.Log($"Enemy{i+1, 3} Hp: {100 * i, 5}");
        }
        DDisp.ResetDisplayGroup();
    }
}

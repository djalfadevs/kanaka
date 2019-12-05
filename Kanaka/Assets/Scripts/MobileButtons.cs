using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileButtons : MonoBehaviour
{

    private PlayerController pc;
    private Player pl;

    void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
        pl = pc.getPlayer();
    }

    public void Attack()
    {
        pl.attack();
    }
    public void Ability()
    {
        pl.ability();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobileButtons : MonoBehaviour
{
    [SerializeField]GameObject j;
    private PlayerController pc;
    private Player pl;

    void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        pl = pc.getPlayer();
    }

    public void Attack()
    {
        if (j.activeSelf)pl.attack();
        
    }
    public void Ability()
    {
        if (j.activeSelf)pl.ability();
    }
}

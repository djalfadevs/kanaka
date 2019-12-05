using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowncheck : MonoBehaviour
{
    public Button button;
    public bool isAbility;
    private PlayerController pc;
    private Player pl;

    // Start is called before the first frame update
    void Awake()
    {
        pc = FindObjectOfType<PlayerController>();
        pl = pc.getPlayer();
    }
    private void Update()
    {
        if (isAbility)
        {
            if (pl.getAbilityCD() <= 0.0f)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
        else
        {
            if (pl.getAtackCD() <= 0.0f)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }
}

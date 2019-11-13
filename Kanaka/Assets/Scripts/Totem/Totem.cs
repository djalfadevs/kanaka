﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float aliveTime = 3;
    private float currentAliveTime;
    // Start is called before the first frame update
    void Start()
    {
        currentAliveTime = aliveTime;
    }

    // Update is called once per frame
    void Update()
    {
        LifeCycle();
    }

    void LifeCycle()
    {
        currentAliveTime -= Time.deltaTime;
        if (currentAliveTime <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
 
        //De momento solo podemos concretar esto hasta aqui, Hero no se sabe si se llamara asi
        /*
        if (collision.gameObject.GetComponent<Hero>() != null)
        {
            Dead();
        }
        */
    }
}

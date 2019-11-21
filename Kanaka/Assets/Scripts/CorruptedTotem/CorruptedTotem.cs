using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedTotem : MonoBehaviour
{
    [SerializeField] private float hp = 1;
    [SerializeField] private float aliveTime = 30;
    private float currentAliveTime;


    //Animator
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentAliveTime = aliveTime;
        animator = GetComponent<Animator>();
        animator.SetInteger("STATE",0);
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
        //animator.SetInteger("STATE",2);
        //Destroy();
        //
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
 
        //De momento solo podemos concretar esto hasta aqui, Hero no se sabe si se llamara asi
        
        if (collision.gameObject.GetComponent<Hero>() != null)
        {
            Dead();
        }
        
    }
    */

    public void Hit(Collider collider)
    {
        animator.SetInteger("STATE",1);
        Dead();
    }
}

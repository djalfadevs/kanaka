using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedTotem : MonoBehaviour
{
    [SerializeField] private float hp = 1;
    [SerializeField] private float aliveTime = 3;
    private float currentAliveTime;

    //Animator
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentAliveTime = aliveTime;
        animator = GetComponent<Animator>();
        animator.SetFloat("HP", hp);
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
        hp = 0;
        animator.SetFloat("HP", hp);
        //Destroy(this.gameObject);
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
        Dead();
    }
}

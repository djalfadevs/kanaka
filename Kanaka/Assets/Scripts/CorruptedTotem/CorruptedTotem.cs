using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedTotem : MonoBehaviour
{
    [SerializeField] private float hp = 1;
    [SerializeField] private float aliveTime = 10;
    private CorruptedTotemSpawner spawner;
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
    public void setSpawner(CorruptedTotemSpawner cts)
    {
        this.spawner = cts;
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
        animator.SetInteger("STATE",2);
    }
    void DestroyThis()
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
        hp = 0;
        offlinegm.destroyTotem();
        offlinegmlife.destroyTotem();
        animator.SetInteger("STATE",1);
        this.spawner.DespawnTotems();
        Dead();
    }
}

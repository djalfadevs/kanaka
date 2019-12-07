using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedTotem : MonoBehaviour
{
    [SerializeField] private float hp = 1;
    [SerializeField] private float aliveTime = 10;
    public GameObject HitEffect;
    public GameObject KillEffect;
    public GameObject DissapearEffect;
    private CorruptedTotemSpawner spawner;
    private float currentAliveTime;

    Vector3 aux;
    //Animator
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentAliveTime = aliveTime;
        animator = GetComponent<Animator>();
        animator.SetInteger("STATE",0);
        aux = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
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
        if (hp>0)
        {
            Instantiate(DissapearEffect,aux, Quaternion.Euler(-90, 0, 0));
        }
        else
        {
            Instantiate(KillEffect, aux, Quaternion.Euler(-90, 0, 0));
        }
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
        Instantiate(HitEffect, aux, Quaternion.Euler(-90, 0, 0));
        Dead();
    }
}

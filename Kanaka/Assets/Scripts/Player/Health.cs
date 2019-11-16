using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public RectTransform hb;
    public Player info;
    private float barDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hb.sizeDelta = new Vector2(info.HP*1.5f, hb.sizeDelta.y);
    }
}

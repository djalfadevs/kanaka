using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private enum ataques
    {
        MareasAgresivas,
        TajoArdiente,
        RupturaSismica,
        FlechaAudaz
    };

    [SerializeField] private ataques ataque;
    [SerializeField] private MonoBehaviour MareasAgresivas;
    [SerializeField] private MonoBehaviour TajoArdiente;
    [SerializeField] private MonoBehaviour RupturaSismica;
    [SerializeField] private MonoBehaviour FlechaAudaz;

    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        
    }
    public void use(GameObject player)
    {
        Debug.Log("Se esta llamando los putos ataques");
        switch (ataque)
        {
            case ataques.FlechaAudaz:
                FlechaAudaz.SendMessage("ataque",player);
                break;
            case ataques.MareasAgresivas:
                MareasAgresivas.SendMessage("ataque", player);
                break;
            case ataques.RupturaSismica:
                break;
            case ataques.TajoArdiente:
                break;
            default:
                break;
        }
    }
}

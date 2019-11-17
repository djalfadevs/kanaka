using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User 
{
    private string name { get; set; }
    private int level { get; set; }
    private float gameMoney { get; set; }
    private float realMoney { get; set; }
    private List<CharacterInfo> buyCharactersAndSkinsInfo { get; set; }


    public User(string name, int level, float gameMoney, float realMoney)
    {
        this.name = name;
        this.level = level;
        this.gameMoney = gameMoney;
        this.realMoney = realMoney;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

}

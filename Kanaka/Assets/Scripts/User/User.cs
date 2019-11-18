using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase es mejorable , haciendo que la contraseña y el username esten en otra  y se relacionen con la info usando un hashmap
[System.Serializable]
public class User 
{
    public string name;
    public string userName;
    public string password;
    public int level;
    public float gameMoney;
    public float realMoney;
    public List<int> charactersID;//Sirve para identificar que personaje nos referimos y tenemos desbloqueado
    public List<int> skinsIDList; //Sirve para identificar que skins del personaje desbloqueado tenemos desbloqueadas

    public User(string username,string password,string nameT, int level, float gameMoney, float realMoney)
    {
        this.name = nameT;
        this.password = password;
        this.userName = username;
        this.level = level;
        this.gameMoney = gameMoney;
        this.realMoney = realMoney;
        this.charactersID = new List<int>();
        this.skinsIDList = new List<int>();
    }

}

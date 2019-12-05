using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineUser
{
    public string userName;
    public int selchar;
    public bool ismobile;
    public int team;

    public OnlineUser(string username, int selchar,bool ismobile,int team)
    {
        this.userName = username;
        this.selchar = selchar;
        this.ismobile = ismobile;
        this.team = team;
    }
}

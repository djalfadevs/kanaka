using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineUser
{
    public string userName;
    public int selchar;

    public OnlineUser(string username, int selchar)
    {
        this.userName = username;
        this.selchar = selchar;
    }
}

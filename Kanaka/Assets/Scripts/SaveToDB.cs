﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveToDB : MonoBehaviour
{

    string path = Application.dataPath + "/UsersData/User.json";
    string path2 = Application.dataPath + "/UsersData/Users.json";

    public void savetoDB()
    {
        string text = File.ReadAllText(path);
        if (text != null)
        {
            User auxU = JsonUtility.FromJson<User>(text);

            File.WriteAllText(path, JsonUtility.ToJson(auxU));

            string text2 = File.ReadAllText(path2);
            List<User> auxlistUsers = new List<User>();

            if (System.IO.File.Exists(path2))
            {
                FileInfo fileinfo = new FileInfo(path2);
                StreamReader reader = fileinfo.OpenText();
                string aux = "";

                while (aux != null)
                {
                    aux = reader.ReadLine();
                    User userAux = JsonUtility.FromJson<User>(aux);
                    if (userAux != null)
                        auxlistUsers.Add(userAux);
                }
                reader.Close();
                Debug.Log(auxlistUsers);

                int auxN = 0;
                bool isfound = false;

                while (auxN < auxlistUsers.Count && !isfound)
                {
                    if (auxlistUsers[auxN].userName == auxU.userName)
                    {
                        isfound = true;
                        auxlistUsers[auxN].level = auxU.level;
                        auxlistUsers[auxN].gameMoney = auxU.gameMoney;
                        auxlistUsers[auxN].realMoney = auxU.realMoney;
                        auxlistUsers[auxN].skinsIDList = auxU.skinsIDList;
                        auxlistUsers[auxN].charactersID = auxU.charactersID;
                    }
                    auxN++;
                }
                string auxS = "";
                foreach (User u in auxlistUsers)
                {
                    auxS += JsonUtility.ToJson(u) + Environment.NewLine;
                }
                File.WriteAllText(path2, auxS);

            }
        }
    }
}
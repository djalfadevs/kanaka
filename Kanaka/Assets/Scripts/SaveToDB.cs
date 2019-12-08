using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class SaveToDB : MonoBehaviour
{
    static string path = Application.streamingAssetsPath + "/UsersData/User.json";
    static string path2 = Application.streamingAssetsPath + "/UsersData/Users.json";



    static IEnumerator getRequest1()
    {

        UnityWebRequest request = UnityWebRequest.Get("https://api.myjson.com/bins/asgog");
        yield return request.SendWebRequest();
        User auxU = JsonConvert.DeserializeObject<User>(request.downloadHandler.text);

        UnityWebRequest request2 = UnityWebRequest.Get("https://api.myjson.com/bins/ejhsg");
        yield return request2.SendWebRequest();
        List<User> auxlistUsers = JsonConvert.DeserializeObject<List<User>>(request.downloadHandler.text);

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

        var uwr = UnityWebRequest.Put("https://api.myjson.com/bins/ejhsg", JsonConvert.SerializeObject(auxlistUsers));
        uwr.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
            Debug.LogError(uwr.error);
        else
        {
            // file data successfully sent
        }
    }


    public static void savetoDB()
    {
        getRequest1();
        /*string text = File.ReadAllText(path);
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
        }*/
    }
}

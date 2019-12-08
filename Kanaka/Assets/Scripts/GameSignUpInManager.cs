﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class GameSignUpInManager : MonoBehaviour
{
    [SerializeField] private int baseLevel = 1;
    [SerializeField] private float baseGameMoney = 100;
    [SerializeField] private float baseRealGameMoney = 100;
    private string username;
    private string password;
    private string gameUserName;
    private User user;
    private string path;
    private string path2;
    public bool correctlog;
    // Start is called before the first frame update


    IEnumerator getRequest(string uri, List<User> auxlistUsers)
    {
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();
        string text2 = request.downloadHandler.text;
        string [] data =  text2.Split(new[] { Environment.NewLine },StringSplitOptions.None);
        int k = 0;
        while (data.Length > k)
        {
            //Console.WriteLine(text);
            User userAux = JsonUtility.FromJson<User>(data[k]);
            if (userAux != null)
                auxlistUsers.Add(userAux);//Se cargan los anteriores usuarios si existe tal archivo
            k++;
        }
    }

    IEnumerator UploadFile(string formData)
    {
        var request = new UnityWebRequest(path, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(formData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    IEnumerator UploadFile2(string formData)
    {
        var request = new UnityWebRequest(path2, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(formData);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    void Awake()
    {
       path = Application.streamingAssetsPath + "/UsersData/Users.json";
       path2 = Application.streamingAssetsPath + "/UsersData/User.json";
       correctlog = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignUp()
    {
        List<User> auxlistUsers = new List<User>();

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine(getRequest(path,auxlistUsers));
        }
        else
        {
            //Si existe el archivo y guardamos los datos en la lista de usuarios
            if (System.IO.File.Exists(path))
            {
                FileInfo fileinfo = new FileInfo(path);
                StreamReader reader = fileinfo.OpenText();
                auxlistUsers = JsonUtility.FromJson < List<User>>(reader.ReadToEnd());

            }

        }

        //Por si falla la lectura
        if (auxlistUsers == null)
        {
            auxlistUsers = new List<User>();
        }

        //Comprobamos que ese nombre de usuario no exista ya
        bool exist = false;
        foreach (User u in auxlistUsers)
        {
            if (u.userName == username)
            {
                exist = true;
            }
        }

        //Si no existe ese usuario se registra
        if (!exist)
            auxlistUsers.Add(new User(username, password, gameUserName, baseLevel, baseGameMoney, baseRealGameMoney));//Se añade el nuevo usuario

        
        String auxS = JsonUtility.ToJson(auxlistUsers);
        Debug.Log("Se han guardado " + auxlistUsers.Count);
        Debug.Log("El nuevo usuario es " + username);
        Debug.Log("El nuevo pass es " + password);
        //Guardamos los datos de nuevo

        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine(UploadFile(auxS));
        }
        else
        {
            File.WriteAllText(path, auxS);
        }

    }

    public void savePassword(string password)
    {
        this.password = password;
    }

    public void saveName(string name)
    {
        this.username = name;
    }

    public void saveGameUserName(string name)
    {
        this.gameUserName = name;
    }

    public void SignIn()
    {
        List<User> auxlistUsers = new List<User>();

        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            StartCoroutine(getRequest(path, auxlistUsers));
        }
        else
        {
            //Si existe el archivo lo leemos y guardamos los datos en la lista de usuarios
            if (System.IO.File.Exists(path))
            {
                FileInfo fileinfo = new FileInfo(path);
                StreamReader reader = fileinfo.OpenText();
                auxlistUsers = JsonUtility.FromJson<List<User>>(reader.ReadToEnd());
            }
        }
      

        //Por si falla la lectura
        if (auxlistUsers == null)
        {
            auxlistUsers = new List<User>();
        }

        //Recorremos la lista de usuarios y comprobamos si alguno coincide su nombre y password
        int auxN = 0;
        bool isfound = false;
        while(auxN<auxlistUsers.Count && !isfound)
        {
            if(auxlistUsers[auxN].userName==username && auxlistUsers[auxN].password == password)
            {
                //Debug.LogError("Exito al loguear");
                isfound = true;
                //Lo que sea que hace la escena si acierta  va AQUI
                //Empezamos guardando un archivo con la informacion del usuario logueado
                string a = JsonUtility.ToJson((auxlistUsers[auxN]));

                if(Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    StartCoroutine(UploadFile2(a));
                }
                else
                {
                    File.WriteAllText(path2, a);
                }
                
                //////////////
                correctlog = true;

            }
            else
            {
                //Debug.LogError("Fallo en usuario o contraseña");
                //Lo que sea que hace la escena si falla  va AQUI
                //////////////
            }
            auxN++;
        }
    }
}

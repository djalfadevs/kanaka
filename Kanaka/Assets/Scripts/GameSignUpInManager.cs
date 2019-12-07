using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

        //Si existe el archivo y guardamos los datos en la lista de usuarios
        if (System.IO.File.Exists(path))
        {
            FileInfo fileinfo = new FileInfo(path);
            StreamReader reader = fileinfo.OpenText();
            string text ="";
            while (text != null)
            {
                text = reader.ReadLine();
                //Console.WriteLine(text);
                User userAux = JsonUtility.FromJson<User>(text);
                if (userAux != null)
                    auxlistUsers.Add(userAux);//Se cargan los anteriores usuarios si existe tal archivo
            }
            reader.Close();
            
        }

        //Por si falla la lectura
        if (auxlistUsers == null)
        {
            auxlistUsers = new List<User>();
        }

        //Comprobamos que ese nombre de usuario no exista ya
        bool exist = false;
        foreach(User u in auxlistUsers)
        {
            if(u.userName == username)
            {
                exist = true;
            }
        }

        //Si no existe ese usuario se registra
        if(!exist)
        auxlistUsers.Add(new User(username, password, gameUserName, baseLevel, baseGameMoney, baseRealGameMoney));//Se añade el nuevo usuario

        String auxS ="";
        int j = 0;
        foreach(User auxU in auxlistUsers)
        {
            Debug.Log(JsonUtility.ToJson(auxU) + j);
            auxS += JsonUtility.ToJson(auxU) + Environment.NewLine;
            j++;
        }
        Debug.Log("Se han guardado " + auxlistUsers.Count);
        Debug.Log("El nuevo usuario es "+ username);
        Debug.Log("El nuevo pass es " + password);
        //Guardamos los datos de nuevo

        File.WriteAllText(path,auxS);

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

        //Si existe el archivo lo leemos y guardamos los datos en la lista de usuarios
        if (System.IO.File.Exists(path))
        {
            FileInfo fileinfo = new FileInfo(path);
            StreamReader reader = fileinfo.OpenText();
            string text = "";
            while (text != null)
            {
                text = reader.ReadLine();
                //Console.WriteLine(text);
                User userAux = JsonUtility.FromJson<User>(text);
                if (userAux != null)
                    auxlistUsers.Add(userAux);//Se cargan los anteriores usuarios si existe tal archivo
            }
            reader.Close();

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
                File.WriteAllText(path2,a );
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

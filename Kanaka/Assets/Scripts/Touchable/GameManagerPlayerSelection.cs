using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerPlayerSelection : MonoBehaviour
{
    string path;
    int idCharacterSelect;
    [SerializeField] private GameObject LockButton;

    void Awake()
    {
        path = Application.dataPath + "/UsersData/User.json";
    }

    //Has pulsado un personaje y te sale su informacion;
    public void CharacterSelection(int idCharacter)
    {
        Debug.Log("Has pulsado el boton y no eres imbecil" + idCharacter);
        this.idCharacterSelect = idCharacter;
        LockButton.SetActive(true);

    }

    //Fijas el personaje seleccionado y añades al usuario el personaje elegido
    public void LockCharacter()
    {
        string text = File.ReadAllText(path);
        if (text != null)
        {
            User auxU = JsonUtility.FromJson<User>(text);
            auxU.charactersID.Add(this.idCharacterSelect);//Se añade el nuevo personaje al archivo;

            File.WriteAllText(path, JsonUtility.ToJson(auxU));//Guardamos la nueva info del usuario;

            //SceneManager.LoadScene("");
        }
       
    }

}

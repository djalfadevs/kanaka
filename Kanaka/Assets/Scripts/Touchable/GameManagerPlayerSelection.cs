using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManagerPlayerSelection : MonoBehaviour
{
    string path;
    int idCharacterSelect;
    [SerializeField] private GameObject LockButton;

    void Awake()
    {
        path = Application.streamingAssetsPath + "/UsersData/User.json";
    }

    //Has pulsado un personaje y te sale su informacion;
    public void CharacterSelection(int idCharacter)
    {
        Debug.Log("Has pulsado el boton y no eres imbecil" + idCharacter);
        this.idCharacterSelect = idCharacter;
        LockButton.SetActive(true);

    }

    IEnumerator UploadFile(string payload)
    {
        {
            Debug.Log(payload);
            var uwr = UnityWebRequest.Put("https://api.myjson.com/bins/asgog", payload);
            uwr.SetRequestHeader("Content-Type", "application/json; charset=utf-8");
            yield return uwr.SendWebRequest();
            SceneManager.LoadScene("MainMenu");
            if (uwr.isNetworkError || uwr.isHttpError)
                Debug.LogError(uwr.error);
            else
            {
                // file data successfully sent
            }
        }
    }

    IEnumerator getRequest(string uri)
    {

        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();
        string text2 = request.downloadHandler.text;
        if (text2 != null)
        {
            Debug.Log(text2);
            User auxU = JsonUtility.FromJson<User>(text2);
            auxU.charactersID.Add(this.idCharacterSelect);//Se añade el nuevo personaje al archivo;
            StartCoroutine(UploadFile(JsonUtility.ToJson(auxU)));
            

            //SceneManager.LoadScene("");
        }
    }

    //Fijas el personaje seleccionado y añades al usuario el personaje elegido
    public void LockCharacter()
    {
        if (true)
        {
            StartCoroutine(getRequest("https://api.myjson.com/bins/asgog"));
        }
        else
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

}

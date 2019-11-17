using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * La funcion de esta clase es la de proporcionar un almacenamiento de datos 
 * para cualquier informacion de los personajes, con esto me refiero a que leera ciertos json o informacion que se le mande desde el 
 * servidor, una vez recibida esa informacion podra ser contenida en el juego por esta clase
 * para posteriormente poder ser leida por ciertos gameobject que se generaran en la escena
 * Lo mas seguro esq entre escenas esta informacion sea guardada en jsons que se podran leer por estos gameobjects
 * Aunque en una primera instancia cuando esta informacion sea
 * 
 * Esquema de datos:
 * 
 * Servidor -> CharacterInfo -> Gameobjects y demas elementos de la escena
 *          ↑           ↑                           ↑
 *        Vendra en     Tendra la               Cuando lo requieran podran
 *        formato       capacidad de                pedir dicha informacion almacenada
 *        JSON          Convertir y desconvertir 
 *        u otro        Dicha informacion
 *        formato
 *        texto
 *        
 * En realidad la informacion sera suministrada desde la clase User , en sustitucion de CharacterInfo
 */
[System.Serializable]
public class CharacterInfo
{
    private int characterID { get; set; } //Sirve para identificar que personaje nos referimos y tenemos desbloqueado
    private List<int> skinsIDList {get; set;} //Sirve para identificar que skins del personaje desbloqueado tenemos desbloqueadas
}

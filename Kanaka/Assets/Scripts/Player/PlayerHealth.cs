using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GUIStyle progress_empty;
    public GUIStyle progress_full;

    //current progress
    public float barDisplay;

    Vector2 pos = new Vector2(10, 50);
    Vector2 size = new Vector2(250, 50);

    public Texture2D emptyTex;
    public Texture2D fullTex;

    public Player info;
    public Transform target;
    public Camera cam;

    void OnGUI()
    {
        //draw the background:
        GUI.BeginGroup(new Rect(cam.WorldToScreenPoint(target.position).x, cam.WorldToScreenPoint(target.position).y, size.x, size.y), emptyTex, progress_empty);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullTex, progress_full);

        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {
        //the player's health
        barDisplay = info.HP / info.MaxHP;
    }
}
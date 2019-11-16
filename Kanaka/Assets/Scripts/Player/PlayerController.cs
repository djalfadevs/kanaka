﻿ ﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float MoveSpeed;
    CharacterController cc;
    Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;
    private Camera cam;
    public float deadzone;
    private Vector3 rotvec;

    private RuntimePlatform platform
    {
        get
        {
            #if UNITY_ANDROID
                return RuntimePlatform.Android;
            #elif UNITY_IOS
                return RuntimePlatform.IPhonePlayer;
            #elif UNITY_STANDALONE_OSX
                return RuntimePlatform.OSXPlayer;
            #elif UNITY_STANDALONE_WIN
                return RuntimePlatform.WindowsPlayer;
            #else
            return RuntimePlatform.WebGLPlayer;

            #endif
        }
    }


    void Start()
    {
        cam = FindObjectOfType<Camera>();
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (platform == RuntimePlatform.Android)
        {
            Vector3 move = new Vector3 (CrossPlatformInputManager.GetAxis("Horizontal") * MoveSpeed,0f, CrossPlatformInputManager.GetAxis("Vertical") * MoveSpeed);

            Vector3 v3 = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0.0f, CrossPlatformInputManager.GetAxis("Vertical"));
            Quaternion qTo = Quaternion.LookRotation(v3);
            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, MoveSpeed * Time.deltaTime);

            cc.Move(move * Time.deltaTime);
            cc.SimpleMove(Physics.gravity);
        }

        else
        {
            mouse_pos = Input.mousePosition;
            mouse_pos.z = 5; //The distance between the camera and object
            object_pos = cam.WorldToScreenPoint(target.position);
            rotvec.x = mouse_pos.x - object_pos.x;
            rotvec.y = mouse_pos.y - object_pos.y;

            //if distancia entre mouse y object
            if (Vector3.Distance(mouse_pos,object_pos) > deadzone)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(rotvec.x, 0, rotvec.y));

                Debug.Log(Vector3.Distance(mouse_pos, object_pos));
            }

            Vector3 move = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * MoveSpeed;
            cc.Move(move * Time.deltaTime);
            cc.SimpleMove(Physics.gravity);
        }
    }
}
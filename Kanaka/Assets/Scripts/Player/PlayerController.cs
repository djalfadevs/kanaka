using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    public float MoveSpeed;
    CharacterController cc;
    Vector3 mouse_pos;
    public Transform target; //Assign to the object you want to rotate
    Vector3 object_pos;
    float angle;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.LookRotation( new Vector3(mouse_pos.x, 0, mouse_pos.y));

        Vector3 forward = Input.GetAxis("Vertical") * transform.TransformDirection(Vector3.forward) * MoveSpeed;
        cc.Move(forward * Time.deltaTime);
        cc.SimpleMove(Physics.gravity);
    }
}
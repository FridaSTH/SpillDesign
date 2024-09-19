using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{

    Vector3 mousePos;
    float smooth = 100f;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        Quaternion target;

        // If mouse is on either side of the player
        if (mousePos.x > screenPos.x) {
            // Find the angle between mouse and character
            float degree = Mathf.Atan((mousePos.y - screenPos.y)/(mousePos.x - screenPos.x))*180/Mathf.PI;

            // transform.Rotate(0f, 0f, mousePos.x*0.001f, Space.Self);
            target = Quaternion.Euler(0, 0, degree);
        } else {
            float degree = Mathf.Atan((screenPos.y - mousePos.y)/(mousePos.x - screenPos.x))*180/Mathf.PI;

            // transform.Rotate(0f, 0f, mousePos.x*0.001f, Space.Self);
            target = Quaternion.Euler(0, 180, degree);
        }

        

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }
}

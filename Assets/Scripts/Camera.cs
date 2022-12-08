using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    float horizontal;
    float vertical;

    public float mouseSensitivity;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        vertical = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity * -1;
        
        Mathf.Clamp(vertical, -45, 45);

        transform.parent.Rotate(new Vector3(0, horizontal, 0));
        transform.Rotate(new Vector3(vertical, 0, 0));
    }
}

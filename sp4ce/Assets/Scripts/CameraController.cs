using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseX = mouseY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X");
        mouseY += -Input.GetAxis("Mouse Y");
        mouseY = Mathf.Clamp(mouseY, -89f, 89f);
        Camera.main.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
    }
}

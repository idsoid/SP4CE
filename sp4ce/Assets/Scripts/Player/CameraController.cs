using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float mouseX, mouseY;

    [SerializeField]
    private float mouseSensitivity = 1f;

    [SerializeField] private float timeBetweenShakes;
    [SerializeField] private float shakeMagnitude;
    
    Vector3 originalPos;
    
    bool blyat = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseX = mouseY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isInUI) return;
        if(GameManager.instance.bGameOver) return;
        mouseX += Input.GetAxis("Mouse X")* mouseSensitivity;
        mouseY += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseY = Mathf.Clamp(mouseY, -89f, 89f);
        Camera.main.transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);

        if (Input.GetButtonDown("Jump"))
        {
            blyat = !blyat;
            originalPos = Camera.main.transform.localPosition;
            if (blyat)
                StartCoroutine(CameraShake(shakeMagnitude));
            else
            {
                StopAllCoroutines();
                Camera.main.transform.localPosition = originalPos;
            }
        }
    }

    private IEnumerator CameraShake(float magnitude)
    {
        float x = originalPos.x + Random.Range(-1f, 1f) * magnitude;
        float y = originalPos.y + Random.Range(-1f, 1f) * magnitude;
        float z = originalPos.z + Random.Range(-1f, 1f) * magnitude;

        Camera.main.transform.localPosition = new Vector3(x, y, z);

        yield return new WaitForSeconds(timeBetweenShakes);

        StartCoroutine(CameraShake(magnitude));
    }
}

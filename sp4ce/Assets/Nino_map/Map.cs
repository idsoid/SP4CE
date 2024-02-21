using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private GameObject map;
    private Camera mapCamera;
    private float mapMaxSize;
    
    // Start is called before the first frame update
    void Start()
    {
        mapCamera = GetComponent<Camera>();
        mapMaxSize = 110.0f;
        map.SetActive(false);

        Vector3 newPos = Camera.main.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }

    // Update is called once per frame
    void Update()
    {
        MapEnable();
    }

    private void MapEnable()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            map.SetActive(!map.activeSelf);
            Cursor.lockState = map.activeSelf?CursorLockMode.None:CursorLockMode.Locked;
            GameManager.instance.isInUI = map.activeSelf;
        }

        if (map.activeSelf)
        {
            MapDisplay();
            Zoom();
        }
    }
    private void MapDisplay()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            float mouseX, mouseY;
            mouseX = -Input.GetAxis("Mouse X");
            mouseY = -Input.GetAxis("Mouse Y");
            Debug.Log("mouseX: " + mouseX + ", mouseY: " + mouseY);
            Vector3 moveDir = mapCamera.transform.up * mouseY + mapCamera.transform.right * mouseX;
            mapCamera.transform.position += new Vector3(moveDir.x, 0, moveDir.z) * Time.deltaTime * 15.0f;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Cursor.lockState = CursorLockMode.None;
            Vector3 newPos = Camera.main.transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;
        }
    }
    private void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (mapCamera.orthographicSize + 0.5f < mapMaxSize)
            {
                mapCamera.orthographicSize += 0.5f;
            }
            else
            {
                mapCamera.orthographicSize = mapMaxSize;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (mapCamera.orthographicSize - 0.5f > 15.0f)
            {
                mapCamera.orthographicSize -= 0.5f;
            }
            else
            {
                mapCamera.orthographicSize = 15.0f;
            }
        }
    }
}

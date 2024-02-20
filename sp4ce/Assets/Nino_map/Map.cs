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
        mapMaxSize = 90.0f;
        map.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            map.SetActive(!map.activeSelf);
            GameManager.instance.isInUI= map.activeSelf;
        }

        //Zoom
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

        //Camera update
        Vector3 newPos = Camera.main.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
    }
}

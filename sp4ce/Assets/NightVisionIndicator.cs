using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NightVisionIndicator : MonoBehaviour
{
    public static NightVisionIndicator instance;

    [SerializeField]
    private GameObject indicator;

    [SerializeField]
    private SightController sightController;

    [SerializeField]
    private TMP_Text scanDetails;

    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(targetObject!=null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(targetObject.transform.position);
            indicator.transform.position = Vector3.Lerp(indicator.transform.position,pos,0.2f);
        }

    }

    Coroutine co;
    void OnEnable()
    {
        co = StartCoroutine(ScanObjects());
    }
    
    void OnDisable()
    {
        StopCoroutine(co);
        co = null;
    }

    private IEnumerator ScanObjects()
    {
        List<GameObject> targetsList = sightController.GetObjectsInRange(10f);
        if(targetsList.Count > 0)
        {
            targetObject = targetsList[0];
            if(!indicator.activeInHierarchy)
            {
                indicator.SetActive(true);
                indicator.transform.position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
                
                scroll = StartCoroutine(ScrollDetails(targetObject.GetComponent<ISightObserver>()?.GetDetails()));
            }
        }
        else
            indicator.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        co = StartCoroutine(ScanObjects());
    }

    Coroutine scroll;
    private IEnumerator ScrollDetails(string details)
    {
        if(scroll!=null) yield break;

        scanDetails.text = "";
        foreach(char character in details)
        {
            scanDetails.text = scanDetails.text + character;
            PlayerAudioController.instance.PlayAudio(AUDIOSOUND.SCROLL);
            yield return new WaitForSeconds(0.03f);
        }

        scroll = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTip : MonoBehaviour
{
    public float fTime_elapsed;

    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDesc;
    [SerializeField] private Image durationBar;

    // Start is called before the first frame update
    void Start()
    {
        fTime_elapsed = 6f;
    }

    void OnEnable()
    {
        fTime_elapsed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        if(fTime_elapsed > 0f)
        {
            fTime_elapsed-=Time.deltaTime;
            durationBar.fillAmount = Mathf.SmoothStep(0f,1f,fTime_elapsed/6f);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetDetails(string name, string desc)
    {
        fTime_elapsed = 6f;
        itemName.text = name;
        itemDesc.text = desc;
    }
}

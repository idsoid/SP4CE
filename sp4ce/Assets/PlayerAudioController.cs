using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAudioController : MonoBehaviour
{
    //singleton for easier use, we only have one player audio ctrl anyway 
    public static PlayerAudioController instance;

    [SerializeField]
    private List<AudioClip> audioList;

    [SerializeField]
    private AudioSource src;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(AUDIOSOUND sound)
    {
        src.PlayOneShot(audioList[(int)sound]);
    }
}

public enum AUDIOSOUND
{
    KEYCARD = 0,
    ITEMTIP = 1,
    FOOTSTEP_LEFT = 2,
    FOOTSTEP_RIGHT = 3,

    DISCOVINDEX = 4

    //more to add
}

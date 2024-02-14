using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AndrewTestEntity : MonoBehaviour, ISightObserver
{
    [SerializeField]
    private GameObject playerTarget;

    [SerializeField]
    private Volume globalVolume;

    bool canMove;

    void Start()
    {
        canMove = false;
    }

    public void OnLookAway()
    {
        canMove = true;
    }

    public void OnSighted()
    {
        StartCoroutine(StopMoving());
    }

    void Update()
    {
        if(!canMove) {

            //get post processing effect
            if(globalVolume.profile.TryGet<NoisePostProcess>(out NoisePostProcess tmp))
            {
                tmp.blend.value -= Time.deltaTime;
            }
            return;
        }

        //get post processing effect
        if(globalVolume.profile.TryGet<NoisePostProcess>(out NoisePostProcess e))
        {
            e.blend.value += Time.deltaTime;
        }
        //move
        transform.Translate((playerTarget.transform.position - transform.position).normalized * Time.deltaTime * 15f);
    }

    private IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(0.2f);
        canMove = false;
    }
}

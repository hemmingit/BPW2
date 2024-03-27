using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.VFX;

public class Phasing : MonoBehaviour
{
    public VisualEffect phaseEffect;
    public VisualEffect phaseOutEffect;
    public VisualEffect impact;

    public RectTransform mask;
    public RectTransform image;

    public AudioMixerSnapshot phaseSnap;
    public AudioMixerSnapshot unphaseSnap;

    public float phaseSpeed = 8f;

    Vector3 output;

    public AudioSource phaseAudioSource;
    public AudioClip phaseIn;
    public AudioClip phaseOut;
    public bool phased = false;

    Vector3 maskSize;
    Vector3 maximumMaskSize;


    public KeyCode phaseKey = KeyCode.Mouse0;



    public float duration = 0f;

    public bool lerpingUp = false;
    public bool lerpingDown = false;

    float outputVal;



    public CinemachineImpulseSource impulse;

    public float force;

    public Animator animator;
    public Animator animator2;

    [SerializeField] private Interaction interaction;

    // Start is called before the first frame update
    void Start()
    {
        phased = false;


        //image.transform.SetParent(mask, true);



        maximumMaskSize = new Vector3(40, 40, 40);
        maskSize = new Vector3(0.01f, 0.01f, 0.01f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(phaseKey))
        {
            PhaseIn();

        }

        if (Input.GetKeyUp(phaseKey))
        {
            PhaseOut();
        }

        /*if (phased)
        {
            GrowMask();
        }

        if (!phased)
        {
            ShrinkMask();
        }*/

        var scaleX = mask.transform.localScale.x;
        var scaleY = mask.transform.localScale.y;
        var scaleZ = mask.transform.localScale.z;

        var invertedScale = new Vector3(1 / scaleX, 1 / scaleY, 1 / scaleZ);

        //mask.localScale = output;

        image.localScale = invertedScale;


    }

    void PhaseIn()
    {
        phased = true;
        

        phaseAudioSource.Stop();
        phaseAudioSource.PlayOneShot(phaseIn);
        phaseEffect.SendEvent("OnPlay");
        impact.SendEvent("OnPlay");
        force = 1;
        Invoke(nameof(PlayImpulse), 0.2f);
        phaseSnap.TransitionTo(0.4f);
        animator.SetTrigger("PhaseIn");
        animator.SetBool("Phased", true);
        animator2.SetTrigger("Phase In Trigger");

    }

    void PhaseOut()
    {
        phased = false;

        phaseAudioSource.Stop();
        phaseAudioSource.PlayOneShot(phaseOut);
        phaseOutEffect.SendEvent("OnPlay");
        impact.SendEvent("OnPlay");
        force = 0.4f;
        Invoke(nameof(PlayImpulse), 0.2f);
        unphaseSnap.TransitionTo(1f);
        animator.SetTrigger("PhaseOut");
        animator.SetBool("Phased", false);
        animator2.SetTrigger("Phase Out Trigger");
    }
/*
    void GrowMask()
    {
        


    }

    void ShrinkMask()
    {
        

    }*/


    void PlayImpulse()
    {
        impulse.GenerateImpulseWithForce(force);
    }


}

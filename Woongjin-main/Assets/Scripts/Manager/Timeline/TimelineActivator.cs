using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class TimelineActivator : MonoBehaviour
{
    [SerializeField] PlayableDirector myTimeline;
    [SerializeField] GameObject character;

    public void ActiveGo()
    {    
        Invoke("ActivateTimeline", 3f);
        Invoke("CharacterPosition", 6f);

    }
    public void ActivateTimeline()
    {
        myTimeline.Play();
    }

    public void CharacterPosition()
    {
        character.transform.position = new Vector3(-1.5f, -1.24f, 0);
    }
}

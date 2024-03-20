using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyPlayed = false;
        public void OnTriggerEnter(Collider other)
       {


            if(!alreadyPlayed && other.CompareTag("Player"))
            {
            GetComponent<PlayableDirector>().Play();
            alreadyPlayed = true;
            }
       } 
    }
}

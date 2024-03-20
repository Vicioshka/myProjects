using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CombatTarget))]



    public class DummyAIStript : MonoBehaviour

    {
        
        Health health;
        float dummyHealth;
        float newDummyHealth;
        float takenDamage;
        // void Start()
        // {
        //     health = GetComponent<Health>();
        //     dummyHealth = health.healthPoints;
        // }
        // void Update()
        // {
        //     Debug.Log(dummyHealth);
        //     Debug.Log(newDummyHealth + "new");
        //     newDummyHealth = health.healthPoints;
        //     IsHealthLower();
        //     newDummyHealth = dummyHealth;
        // }
        // void IsHealthLower()
        // {
        //     if(dummyHealth == newDummyHealth) return;
        //     if(dummyHealth > newDummyHealth)
        //     {
        //         GetComponent<Animator>().SetTrigger("GetHit");
        //         newDummyHealth = dummyHealth;
        //     }
            
        // }

    }
}

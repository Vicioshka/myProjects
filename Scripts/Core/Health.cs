using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Core
{ public class Health : MonoBehaviour
    {
        bool isDead = false;
        [SerializeField] public float healthPoints = 15000f;
        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            Debug.Log(healthPoints);
            if(healthPoints == 0)
            {
                Die();                
            }
        }
        void Die()
        {
            if(isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionScheduler>().CancelCurrentAction(); //aby wróg nie atakował, gdy jesteśmy martwi
        }
        public bool IsDead()
        {
            return isDead;
        }
    }
}

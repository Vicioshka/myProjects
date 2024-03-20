using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using Unity.VisualScripting;
using UnityEngine;
namespace RPG.Combat
{ public class Fighter : MonoBehaviour, IAction
    {
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        [SerializeField] float weaponRange = 4f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 10f;
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }
        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) {return false;}
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>(); 
        }
        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        } // zaprzestanie atakowania i wyczyszczenie celu, anulowanie akcji ataku
        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("Attack");
            GetComponent<Animator>().SetTrigger("StopAttack");
        } //kończenie animacji ataku
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        } //wywołanie animacji ataku

        //Animations events potrzebne do wyznaczenia momentu, kiedy ma zostać zadany cios
        void MainCharHit()
        {
            Debug.Log("MainCharhit");
            if(target == null) {return;}
            target.TakeDamage(weaponDamage);
        }
        void Hit()
        {
            if(target == null) {return;}
            target.TakeDamage(weaponDamage);
        }
    }
}
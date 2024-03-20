using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Movement
{  public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 13;
        NavMeshAgent navMeshAgent;
        Health health;
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            navMeshAgent.enabled = !health.IsDead(); //gdy postać jest martwa, wyłączamy jej kolizje z graczem
            UpdateAnimator();
        }
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        } //ActionScheduler odpowiada za zatrzymanie aktualnej akcji, aby móc włączyć następną - tu:
        //przeniesienie do nowej lokalizacji
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        } // metoda odpowiedzialna za przeniesienie postaci do miejsca docelowego, a następne zatrzymanie animacji
        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        } //zależność między prędkością poruszania się a szybkością animacji
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        } //zatrzymuje poruszanie się
    }
}
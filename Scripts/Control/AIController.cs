using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using Unity.VisualScripting;
using UnityEngine;
namespace RPG.Control
{    
    public class AIController : MonoBehaviour
    {
        Vector3 guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspisionTime = 0f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        private void Start()
            {
                fighter = GetComponent<Fighter>();
                player = GameObject.FindWithTag("Player");
                health = GetComponent<Health>();
                mover = GetComponent<Mover>();
                guardLocation = transform.position; //zapisanie miejsca startowego strażnika
            }
        private void Update()
        {
            if (health.IsDead()) return; //gdy postać jest dead, nie może nic zrobić (return)

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspisionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0f;
            fighter.Attack(player); // jeśli odległość jest mniejsza niż 5, zaczynają gonić
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardLocation;
            if(patrolPath != null)
            {
                if(AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if(timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
            mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private bool InAttackRangeOfPlayer()
            {
                float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
                return distanceToPlayer < chaseDistance;
            }
        //sprawdzenie dystansu pomiędzy wrogiem (na którym jest skrypt, i graczem). 
        //Funckja oddaje tę odległość, aby użyć jej w funkcji if.

        private void OnDrawGizmosSelected()
        {
            Gizmos.color=Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        } // wizualizacja odległości, od jakiej goni nas wróg/rysowanie sfery o promieniu chaseDistance
    }
}
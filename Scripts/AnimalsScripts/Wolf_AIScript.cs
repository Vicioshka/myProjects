using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using JetBrains.Annotations;
using UnityEngine;
using RPG.Core;
public class Wolf_AIScript : MonoBehaviour
{
    public float movSpeed = 2f;
    public float runsSpeed = 10f;
    public float rotationSpeed = 100f;
    private bool isWandering = false;
    private bool isWalking = false;
    private bool isRunning = false;
    private bool isRotLeft = false;
    private bool isRotRight = false;
    private bool isSitting = false;
     Rigidbody rb;
     Animator animator;
     Health deathStatemant;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        deathStatemant = GetComponent<Health>();
    }
    void Update()
    {
        CheckIfDead();
        
        if(isWandering == false)
        {
            StartCoroutine(Wander());
        }
            if(isWalking == true)
            {animator.SetBool("isWalkBool", true);
            rb.transform.position += transform.forward * movSpeed *Time.deltaTime;}
                if(isWalking == false)
                {animator.SetBool("isWalkBool", false); }
            if(isRunning == true)
            {animator.SetBool("isRunningBool", true);
            rb.transform.position += transform.forward * runsSpeed * Time.deltaTime; }
                if(isRunning == false)
                {animator.SetBool("isRunningBool", false); }
            if(isRotLeft == true)
            {animator.SetBool("isTurnL", true);
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed); }
                if(isRotLeft == false)
                { animator.SetBool("isTurnL", false); }
            if (isRotRight == true)
            {animator.SetBool("isTurnR", true);
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed); }
                if (isRotRight == false)
                { animator.SetBool("isTurnR", false); }
            if (isSitting == true)
            {animator.SetBool("isSitting", true); }
                if (isSitting == false)
                { animator.SetBool("isSitting", false); }
    }
    void CheckIfDead()
    {
        if(deathStatemant.IsDead())
        {
            StopCoroutine(Wander());
            isWandering = true;
        }  
    }
    IEnumerator Wander()
    {
        int whatToDo = Random.Range(1,5);
        isWandering = true;
           if(whatToDo == 1) //Walk
            {
                int walkWait = Random.Range(1,6);
                int walkTime = Random.Range(1,6);
                yield return new WaitForSeconds(walkWait);
                isWalking = true;
                yield return new WaitForSeconds(walkTime);
                isWalking = false;
                isWandering = false;
            }
            if(whatToDo == 2) //Run
            {
                int runWait = Random.Range(1,6);
                int runTime = Random.Range(1,6);
                yield return new WaitForSeconds(runWait);
                isRunning = true;
                yield return new WaitForSeconds(runTime);
                isRunning = false;
                isWandering = false;
            } if(whatToDo == 3)
            {
                int rotWait = Random.Range(1,6);
                int rotTime = Random.Range(1,4);
                int rotDirection = Random.Range(1,3);
                if(rotDirection == 1) // Right Rotation
                {
                    yield return new WaitForSeconds(rotWait);
                    isRotRight = true;
                    yield return new WaitForSeconds(rotTime);
                    isRotRight = false;
                } if (rotDirection == 2) // Right Left
                {
                    yield return new WaitForSeconds(rotWait);
                    isRotLeft = true;
                    yield return new WaitForSeconds(rotTime);
                    isRotLeft = false;
                }
                isWandering = false;
            }
            if(whatToDo == 4)
            {
                int sitWait = Random.Range(1,6); 
                yield return new WaitForSeconds(sitWait);
                isSitting = true;
                yield return new WaitForSeconds(5);
                isSitting= false;
                isWandering = false;
        }
             isWandering = false;}}
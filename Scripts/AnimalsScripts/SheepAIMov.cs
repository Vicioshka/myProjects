using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class SheepAIMov : MonoBehaviour
{
    public float movSpeed =0.01f;
        public float bouceSpeed = 0.06f;
    public float rotSpeed = 100f;


    private bool isWandering = false;
    private bool isBouncing = false;
    private bool isEating = false;
    private bool isWalking = false;
    private bool isRotRight = false;
    private bool isRotLeft = false;

    Rigidbody rb;
Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        //---------------------
        if (isBouncing == true)
        {
            animator.SetBool("isBounce", true);
            rb.transform.position += transform.forward * bouceSpeed * Time.deltaTime;
        }
        if (isBouncing == false)
        {
            animator.SetBool("isBounce", false);
        }
        //-------------------
        if (isEating == true)
        {
            animator.SetBool("isEating", true);
        }
        if (isEating == false)
        {
            animator.SetBool("isEating", false);
        }
                //-------------------
        if (isWalking == true)
        {
            animator.SetBool("isWalking", true);
            rb.transform.position += transform.forward * movSpeed * Time.deltaTime;
        }
        if (isWalking == false)
        {
            animator.SetBool("isWalking", false);
        }
        //-----------

        if(isRotRight == true)
        {            
            animator.SetBool("isTurningRight", true);
            transform.Rotate(transform.up *Time.deltaTime * rotSpeed);
        }
        if(isRotRight == false)
        {
            animator.SetBool("isTurningRight", false);
        }
        if(isRotLeft == true)
        {            
            animator.SetBool("isTurningLeft", true);
            transform.Rotate(transform.up *Time.deltaTime * -rotSpeed);
        }
        if(isRotLeft == false)
        {
            animator.SetBool("isTurningLeft", false);
        }
    }

    IEnumerator Wander()
    {
    
        int whatToDO = Random.Range(1,5);

        isWandering = true;

    
    if(whatToDO == 1)
    {
        int bouncewait = Random.Range(1, 5);
        int bouncetime = Random.Range(1, 5);

        yield return new WaitForSeconds(bouncewait);
        isBouncing = true;
        yield return new WaitForSeconds(bouncetime);
        isBouncing = false;  
        isWandering = false;
    }    
    if(whatToDO == 2)
    {
        int eatewait = Random.Range(1, 6);
        int eattime = Random.Range(1, 6);

        yield return new WaitForSeconds(eatewait);
        isEating = true;
        yield return new WaitForSeconds(eattime);
        isEating = false;  
        isWandering = false;
    } 
    if(whatToDO == 3)
    {
        int walkwait = Random.Range(1, 6);
        int walktime = Random.Range(1, 6);

        yield return new WaitForSeconds(walkwait);
        isWalking = true;
        yield return new WaitForSeconds(walktime);
        isWalking = false;  
        isWandering = false;
    } 

        if(whatToDO == 4)
    {
        int rotwait = Random.Range(1, 6);
        int rottime = Random.Range(1, 6);
        int rotDiretion = Random.Range(1,3);

        yield return new WaitForSeconds(rotwait);
        if(rotDiretion == 1)
        {
            isRotRight = true;
            yield return new WaitForSeconds(rottime);
            isRotRight = false;
        }
        if(rotDiretion == 2)
        {
            isRotLeft = true;
            yield return new WaitForSeconds(rottime);
            isRotLeft = false;
        }
        isWandering = false;
    } 

    }
}
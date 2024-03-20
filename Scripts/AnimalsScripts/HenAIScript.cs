using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HenAIScript : MonoBehaviour
{
    public Transform other;


    public float movSpeed =0.01f;
    public float runSpeed = 0.06f;
    public float rotSpeed = 100f;
    public float rangeToRun = 20f;

    private bool isWandering = false;
    private bool isRotL = false;
    private bool isRotR = false;
    private bool isWalking = false;
    private bool isEating = false;
    private bool isRunning = false;


    Rigidbody rb;
Animator animator;

// deklaracja potrzebnych zmiennych: bool'e aby sprawdzić, w jakim stanie znajduje się kurczak,
// początkowo wszystkie na false, kurczak po prostu się buja. MovSpeed i rotSpeed do definiowania prędkości.

     void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    // dostanie się do komponentów: Animacja oraz Rb.
     void Update()
    {

        EnemyInRange();

        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }

        if (isRotR == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            animator.SetBool("isTurningRight", true);
        }
        if (isRotR == false)
        {
            animator.SetBool("isTurningRight", false);
        }
        if (isRotL == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            animator.SetBool("isTurningLeft", true);
        }
        if (isRotL == false)
        {
            animator.SetBool("isTurningLeft", false);
        }

        if(isEating == true)
        {
            animator.SetBool("isEating", true);
        }
        if(isEating == false)
        {
            animator.SetBool("isEating", false);
        }

        if (isWalking == true)
        {
            rb.transform.position += transform.forward * Time.deltaTime * movSpeed;
            animator.SetBool("isWalking", true);
        }
        if (isWalking == false)
        {
            animator.SetBool("isWalking", false);
        }

        if (isRunning == true)
        {
            rb.transform.position += transform.forward * Time.deltaTime * runSpeed;
            animator.SetBool("isRunning", true);
        }
        if (isRunning == false)
        {
            animator.SetBool("isRunning", false);
        }
    }

    // tu troche nie wiem co się dzieje XD ale utworzenie CORO do zmiany bool'i, aby kurczak robił rzeczy. isWondering = false, więc uruchamia
    // się CORO "Wander". Następnie przy każdej zmianie bool w CORO wykonują się animacje, rotacje i ruchy kurczaka. 
    // SetBool odnosi się do zaimplementowanych parametrów i warunków do wykonania się animacji -> AnimatorController.


    IEnumerator Wander()
    {

        int whatToDO = Random.Range(1, 5);

        isWandering = true;

        if (whatToDO == 1)
        {
            int eattime = Random.Range(1, 5);
            int eatwait = Random.Range(1, 7);

            yield return new WaitForSeconds(eatwait);
            isEating = true;
            yield return new WaitForSeconds(eattime);
            isEating = false;
            isWandering = false;
        }

        if (whatToDO == 2)
        {
            int walkwait = Random.Range(1, 7);
            int walktime = Random.Range(1, 4);

            yield return new WaitForSeconds(walkwait);
            isWalking = true;
            yield return new WaitForSeconds(walktime);
            isWalking = false;
            isWandering = false;
        }

        if (whatToDO == 3)
        {
            int rottime = Random.Range(1, 3);
            int rotwait = Random.Range(1, 7);
            int rotatelorR = Random.Range(1, 3);

            yield return new WaitForSeconds(rotwait);
            if (rotatelorR == 1)
            {
                isRotR = true;
                yield return new WaitForSeconds(rottime);
                isRotR = false;
            }
            if (rotatelorR == 2)
            {
                isRotL = true;
                yield return new WaitForSeconds(rottime);
                isRotL = false;
            }
            isWandering = false;
        }

        if (whatToDO == 4)
        {
            int runwait = Random.Range(1, 7);
            int runtime = Random.Range(1, 4);

            yield return new WaitForSeconds(runwait);
            isRunning = true;
            yield return new WaitForSeconds(runtime);
            isRunning = false;
            isWandering = false;
        }
    }

    void EnemyInRange()
    {
        GameObject[] animalEnemies = GameObject.FindGameObjectsWithTag("EnemyBoar");

        foreach(GameObject enemy in animalEnemies) 
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
                if(enemyDistance <= rangeToRun)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

        }

    }

   





    // Iterator Wander do wykonywania czynności. Deklaracja randomowych czasów pomiędzy czynnościami i ich czasów trwania (Random.Range()), 
    // Zmiana isWandering = true, aby tylko nie uruchamiać nowego CORO. Następnie wykonanie deklarowanych czynności i animacji.
    // Random whatToDO, aby zrandomizować kolejność czynności.
}

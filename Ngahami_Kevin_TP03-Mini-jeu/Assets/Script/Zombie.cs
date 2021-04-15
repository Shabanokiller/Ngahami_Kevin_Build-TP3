using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public Transform target;
    private float distance;
    private float chaseRange = 10;
    private float attackRange = 2.2f;
    private float attackRepeaTime = 1;
    private float attackTime;
    

    private NavMeshAgent zombie;
    private bool isDead = false;
    private bool isZombieBusy = false;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = Time.time;
        zombie = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si le zombie n'est pas occupé
        if (!isZombieBusy)
        {

            Vector3 newDestination = new Vector3(Random.Range(12f, 8f), 1f, Random.Range(-12f, 12));

            StartCoroutine(GoToDestination(newDestination));
        }
        // Le zombie me poursuit en permanence
        zombie.destination = target.position;
    }


    // Coroutine de déplacements
    IEnumerator GoToDestination(Vector3 newDestination)
    {
        //Je suis maintenant occupé
        isZombieBusy = true;

        //Se deplacer vers la destination
        zombie.SetDestination(newDestination);

        //Tant que je ne suis pas rendu a destination, la coroutine attend
        while (zombie.pathPending || zombie.remainingDistance > zombie.stoppingDistance)
        {
            // Attendre
            yield return null;
        }

        // le zombie est rendu a destination, il prend une pause
        yield return new WaitForSeconds(Random.Range(4f, 6f));

        //Je suis rendu a la destination et j'ai pris ma pause
        isZombieBusy = false;
    }
    // fonction pour tuer les zombies 
    public void Dead()
    {
        isDead = true;
        zombie.isStopped = true;
        Destroy(transform.gameObject, 5);
    }
    
}

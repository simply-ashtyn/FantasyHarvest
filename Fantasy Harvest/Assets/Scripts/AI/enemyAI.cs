using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamageable
{
    [Header("----Enemy Stats----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int enemyHP;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = gameManager.instance.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(gameManager.instance.player.transform.position);
    }

    public void TakeDamage(int dmg)
    {
        enemyHP -= dmg;
        if (enemyHP <= 0)
        {
            Destroy(gameObject); 
        }
    }
}

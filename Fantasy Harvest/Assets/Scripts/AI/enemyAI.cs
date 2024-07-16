using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamageable
{
    [Header("----Enemy Stats----")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer rend;
    [SerializeField] int enemyHP;
    [SerializeField] float playerFaceSpeed;

    [Header("----Enemy Weapon Stats----")]
    [SerializeField] float shootRate;
    [SerializeField] GameObject enemySpell;
    [SerializeField] Transform enemyShootPosition;

    Vector3 playerDirection;
    bool isShooting;


    // Start is called before the first frame update
    void Start()
    {
        //transform.position = gameManager.instance.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = gameManager.instance.player.transform.position - transform.position;
        agent.SetDestination(gameManager.instance.player.transform.position);
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            FacePlayer();
            if (!isShooting)
            {
                StartCoroutine(EnemyShoot());
            }
        }
    }

    void FacePlayer()
    {
        playerDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * playerFaceSpeed);
    }

    public void TakeDamage(int dmg)
    {
        enemyHP -= dmg;
        StartCoroutine(FlashColor());
        if (enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator FlashColor()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = Color.white;
    }

    IEnumerator EnemyShoot()
    {
        isShooting = true;
        Instantiate(enemySpell, enemyShootPosition.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);
        isShooting = false;
    }
}
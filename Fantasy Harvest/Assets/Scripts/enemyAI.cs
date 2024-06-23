using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAI : MonoBehaviour, IDamageable
{
    [SerializeField] int enemyHP;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = gameManager.instance.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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

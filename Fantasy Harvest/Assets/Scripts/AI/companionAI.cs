using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class companionAI : MonoBehaviour
{
    [SerializeField] NavMeshAgent companionAgent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        companionAgent.SetDestination(gameManager.instance.player.transform.position);
    }
}

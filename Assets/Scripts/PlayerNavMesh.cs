using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;
using System;

public class PlayerNavMesh : MonoBehaviour
{
    public Transform movePosition;
    private Animator ani;
    private NavMeshAgent navMeshAgent;
    public bool linking;
    public float linkSpeed = 1;
    public float origSpeed = 4;

    public bool can_move = true;

    private void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        linking = false;
    }

    private void Update()
    {
        ani.SetBool("Run", true);
        // Vector2 randomDirection = UnityEngine.Random.insideUnitSphere * 2;
        // float x = movePosition.position.x+randomDirection.x;
        // float y = movePosition.position.y;
        // float z = movePosition.position.z + randomDirection.y;
        navMeshAgent.destination = movePosition.position;
        if (!navMeshAgent.pathPending)
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    ani.SetBool("Run", false);
                }
            }
        }



        if (navMeshAgent.isOnOffMeshLink && linking == false)
        {
            linking = true;
            navMeshAgent.speed = navMeshAgent.speed * linkSpeed;
            
            //ani.Play("JumpOneTake 0");
            ani.SetTrigger("Jump");
        }
        else if (navMeshAgent.isOnNavMesh && linking == true)
        {
            linking = false;
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.speed = origSpeed;
            ani.ResetTrigger("Jump");
        }
        
    }
}

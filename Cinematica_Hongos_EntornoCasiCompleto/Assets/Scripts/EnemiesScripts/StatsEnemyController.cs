using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatsEnemyController : MonoBehaviour
{
    [SerializeField] public EnemyStats enemyStats;

    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;
    private NavMeshAgent navMeshAgent;
    private HealthManager healthManagerEnemy;

    private void Start()
    {
        if (gameObject.GetComponent<BossCorceps>() == null)
        {
            enemyMovement = GetComponent<EnemyMovement>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            healthManagerEnemy = GetComponent<HealthManager>();
        }

           


        if(GameObject.FindAnyObjectByType<EnemyAttack>() != null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
        }
        AñadirStatsEnemigos();
       
    }
    public void  AñadirStatsEnemigos()
    {
        if(gameObject.GetComponent<BossCorceps>()==null)
        {
            navMeshAgent.speed = enemyStats.VelocidadDeMovimiento;
            healthManagerEnemy.VidaMaxima = enemyStats.VidaMaxima;
            healthManagerEnemy.VidaActual = enemyStats.VidaMaxima;

        }
        
    }


}

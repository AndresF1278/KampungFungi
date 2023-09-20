using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
 


    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
         //Gizmos.DrawCube(transform.position, new Vector3(1,1,1));
         Gizmos.DrawWireCube(transform.position, new Vector3(1, 0.5f, 1)); 

    }






}

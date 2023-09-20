using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueAcido : EnemyAttack
{
    [SerializeField] private GameObject acidoPrefab;
    [SerializeField] private float tiempoEntreAcido;
    [SerializeField] float tiempoCadencia;
    [SerializeField] private List<GameObject> acidosPool;
    private void Update()
    {
        tiempoCadencia =  Time.deltaTime + tiempoCadencia;
        if(tiempoCadencia >= tiempoEntreAcido)
        {
            AtaqueEnemigo();
            Debug.Log("CreoAcido");
            tiempoCadencia = 0;
        }
    }

    public override void AtaqueEnemigo()
    {
        Vector3 posAcido = new Vector3(transform.position.x, 2.33f, transform.position.z);
        if (acidosPool.Find(objeto => !objeto.activeSelf) == false)
        {
            
            GameObject acidoCreado = Instantiate(acidoPrefab, posAcido, Quaternion.Euler(90, 0, 0));
            acidosPool.Add(acidoCreado);
        }
        else
        {
            var objeto1 = acidosPool.Find(objeto => !objeto.activeSelf);
            objeto1.transform.position = posAcido;
            objeto1.SetActive(true);
        }

          

    }

    private void OnDisable()
    {
        tiempoCadencia = 0;
    }
}

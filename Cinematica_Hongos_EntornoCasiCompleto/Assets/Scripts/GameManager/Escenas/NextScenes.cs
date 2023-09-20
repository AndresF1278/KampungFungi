using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScenes : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            SceneController.instance.IrScenaSiquiente();
        }
    }
}

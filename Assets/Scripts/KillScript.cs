using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillScript : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player reset");

            SceneManager.LoadScene("SampleScene");

        }
    }
}

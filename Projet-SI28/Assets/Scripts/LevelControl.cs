using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public string levelName;
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("okok");
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("coucou");
            SceneManager.LoadScene(levelName);
        }        
    }
}

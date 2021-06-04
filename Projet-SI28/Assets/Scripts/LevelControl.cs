using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    //public string levelName;
    int index;
    int newindex;

        private void Start()
    {
        index = SceneManager.GetActiveScene().buildIndex;
        newindex = index + 1;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("okok");
        //Debug.Log(index);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("coucou");
            SceneManager.LoadScene(newindex);
            //SceneManager.LoadScene(levelName);
        }        
    }
}

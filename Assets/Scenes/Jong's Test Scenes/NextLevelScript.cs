using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelScript : MonoBehaviour
{
    public int currentLevel;
    public GameObject radio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(currentLevel == 2)
            {
                AkSoundEngine.PostEvent("StopTeenageMusic", radio);
            } else if(currentLevel == 3)
            {
                AkSoundEngine.PostEvent("StopAdultMusic", radio);
            }
            
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}

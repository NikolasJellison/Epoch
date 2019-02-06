using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJournalOpen : MonoBehaviour
{
    public GameObject journalPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)){
            journalPanel.SetActive(!journalPanel.activeSelf);
        }
    }
}

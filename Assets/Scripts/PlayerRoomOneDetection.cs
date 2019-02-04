using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoomOneDetection : MonoBehaviour
{
    public int blocksFound;
    public Text notifcationText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Block"))
        {
            Destroy(other.gameObject);
            blocksFound++;
        }
    }
}

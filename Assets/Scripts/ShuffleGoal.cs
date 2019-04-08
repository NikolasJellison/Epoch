using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShuffleGoal : MonoBehaviour, IPointerDownHandler
{
    public ShuffleMiniGame shuffleMiniGame;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("Trying to end game you know");
        shuffleMiniGame.EndMiniGame();
    }
}

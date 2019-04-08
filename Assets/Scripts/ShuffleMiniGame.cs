using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShuffleMiniGame : MonoBehaviour
{
    public List<Sprite> memories = new List<Sprite>();
    [Header("In percentage. EX: min is 20% away so put .2. (0-1)")]
    public Vector2 hLimits = new Vector2(.2f, .8f);
    public Vector2 vLimits = new Vector2(.2f, .8f);

    // Start is called before the first frame update
    void Start()
    {
        GenerateMemories();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMemories()
    {
        foreach (Sprite s in memories)
        {
            //Make new object
            GameObject memory = new GameObject();
            memory.name = "memory";
            //Set it as a child of the Canvas(which this script is attached to)
            memory.transform.parent = transform;
            //Make image component
            Image memoryImage = memory.AddComponent<Image>();
            //Put sprite into image
            memoryImage.sprite = s;
            //Change size of image
            memoryImage.SetNativeSize();

            //Give the stuff the stuff needed to move
            memory.AddComponent<BoxCollider2D>();
            Rigidbody2D rb = memory.AddComponent<Rigidbody2D>();

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            memory.AddComponent<ShuffleMovement>();

            //Random Location
            float x = Random.Range(Screen.width * hLimits[0], Screen.width * hLimits[1]);
            float y = Random.Range(Screen.height * vLimits[0], Screen.height * vLimits[1]);

            memory.GetComponent<RectTransform>().position = new Vector2(x, y);
            //For some reason my test images were coming in at a different scale
            memory.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }

    public void EndMiniGame()
    {
        //Stuff to end game
        Destroy(gameObject);
    }
}

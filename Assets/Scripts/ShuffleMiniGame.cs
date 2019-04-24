using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class ShuffleMiniGame : MonoBehaviour
{
    public List<Sprite> memories = new List<Sprite>();
    [Header("In percentage. EX: min is 20% away so put .2. (0-1)")]
    public Vector2 hLimits = new Vector2(.2f, .8f);
    public Vector2 vLimits = new Vector2(.2f, .8f);
    private GameObject fadeWhite;
    public Color fadeColor = Color.white;
    private Image fadeWhiteImage;
    private List<GameObject> memoriesGO = new List<GameObject>();
    private bool fadeOut;
    private TextMeshProUGUI text;
    public GameObject empty;
    [Header("Outline Stuff")]
    public Color outlineColor;
    // Start is called before the first frame update
    void Start()
    {
        GenerateMemories();
        fadeColor.a = 0f;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOut)
        {
            var temp = fadeWhiteImage.color;
            temp.a += Time.deltaTime / 4;
            if (temp.a >= 1)
            {
                temp.a = 1;
                fadeOut = false;
                EndScene();
            }
            fadeWhiteImage.color= temp;
        }
    }

    private void GenerateMemories()
    {
        foreach (Sprite s in memories)
        {
            //Make new object
            GameObject memory = new GameObject();
            //Add to list so we can delete later?
            memoriesGO.Add(memory);
            memory.name = "memory";
            //Set it as a child of the Canvas(which this script is attached to)
            memory.transform.parent = empty.transform;
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
            memory.GetComponent<RectTransform>().localScale = 2.0f*Vector3.one;

            //Add outlines
            memory.AddComponent<Outline>().effectColor = outlineColor;
            memory.GetComponent<Outline>().effectDistance = new Vector2(-3, 3);
        }
        //Make the White Screen here
        fadeWhite = new GameObject();
        fadeWhite.name = "White-Fade";
        fadeWhite.transform.parent = transform;

        fadeWhiteImage = fadeWhite.AddComponent<Image>();
        fadeWhiteImage.color = fadeColor;

        //Full screen the white Image
        fadeWhite.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        fadeWhite.GetComponent<RectTransform>().anchorMax = Vector2.one;
        fadeWhite.GetComponent<RectTransform>().localScale = Vector3.one;
        fadeWhite.GetComponent<RectTransform>().offsetMin = Vector3.zero;
        fadeWhite.GetComponent<RectTransform>().offsetMax = Vector3.zero;

        //Deactivate for now
        fadeWhite.SetActive(false);
    }

    public void EndMiniGame()
    {
        //Stuff to end game
        //Get rid of all memories once the correct memory is selected
        foreach(GameObject obj in memoriesGO)
        {
            obj.SetActive(false);
        }

        GetComponent<AudioSource>().Play();
        fadeWhite.SetActive(true);
        text.text = "Return to the real world";
        //Let the player see the last memory
        StartCoroutine(WaitABit());
    }

    private IEnumerator WaitABit()
    {
        yield return new WaitForSeconds(5);
        fadeOut = true;
    }

    private void EndScene()
    {
        SceneManager.LoadScene("Cutscene Last");
    }
}

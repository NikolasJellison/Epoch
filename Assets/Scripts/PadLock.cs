using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class PadLock : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    #region Public
    [Header("Objects")]
    [Tooltip("Image that will rotate for the combination")]
    public Image dial;
    [Header("Combination")]
    [Range(0, 39)]
    public int firstNumber = 15;
    [Range(0, 39)]
    public int secondNumber = 10;
    [Range(0, 39)]
    public int thirdNumber = 33;
    //This is "too much" work for a setting we're never going to use, just going to round and it'll be the same as max
    //[Header("Settings")]
    //[Range(1, 4)]
    //[Tooltip("Degrees away from the actual number that will still count for hitting the number. Max of 4 since that is just before halfway to the next number")]
    //public int leeway = 4;

    [TextArea]
    public string notes = "Make sure this script is located on the rotation image itself";

    [Header("Debugs")]
    public Text debugNum;
    public Text debugOrder;

    #endregion

    #region Private
    private float firstNumberRotation;
    private float secondNumberRotation;
    private float thirdNumberRotation;

    private float baseAngle;

    private RectTransform rectTransform;
    private int trackerCounter = 0;
    //This isn't a good thin to do but eh i guess?
    //Start at 40 because we need to go down from 40 to first num and we can't start  at 0 because then we'll be 1 off
    private int previousNum = 40;
    private int trackerGoal;
    private bool firstNumReached;
    private bool secondNumReached;
    private bool thirdNumReached;
    private bool failed;
    private float ang;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        firstNumberRotation = Remap(firstNumber);
        secondNumberRotation = Remap(secondNumber);
        thirdNumberRotation = Remap(thirdNumber);
        Debug.Log("first remap: "+firstNumberRotation);
        Debug.Log("Second remap: "+secondNumberRotation);
        Debug.Log("Third Remap: " + thirdNumberRotation);

        rectTransform = GetComponent<RectTransform>();
        Debug.Log("Screen Width : " + Screen.width);
        Debug.Log("Screen Height : " + Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private int Remap(int value)
    {
        /*PieceWise
         * x/9          if 0<x<180
         * 40 + (x/9)   if -180<x<0
         * 
         * so we input Y to get the needed X(Inverse)
         * 
         * 9Y
         * 9(Y-40)
         */
        if(value <= 20)
        {
            return value * 9;
        }
        else
        {
            return 9 * (value - 40);
        }
    }
    private int CurrentNumber(float value)
    {
        //Send in the angle value (-180,180)
        /*PieceWise
         * x/9          if 0<x<180
         * 40 + (x/9)   if -180<x<0
         * 
         * so we input Y to get the needed X(Inverse)
         * 
         * 9Y
         * 9(Y-40)
         */
        if (value >= 0)
        {
            return Mathf.RoundToInt(value / 9);
        }
        else
        {
            return Mathf.RoundToInt(40 + (value / 9));
        }

    }


    public void OnPointerDown(PointerEventData pointerEventData)
    {
      //  Debug.Log("ONMouseDown");
      //  Debug.Log("Transform:" + transform.position);
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        //Debug.Log("pos initial:" + pos);
       // Debug.Log("MousePOS" + Input.mousePosition);
        //pos = Input.mousePosition - pos;
        pos = Input.mousePosition - transform.position;
        //Debug.Log("pos after subtraction:" + pos);
        baseAngle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        //Debug.Log(baseAngle);
        //Debug.Log(transform.right.y + " " + transform.right.x);
        baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        //Debug.Log(baseAngle);
    }


    public void OnDrag(PointerEventData data)
    {
        //Debug.Log("OnMouseDrag");
         Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        // Debug.Log(pos);
        //pos = Input.mousePosition - pos;
        pos = Input.mousePosition - transform.position;
        //Debug.Log(pos);
        ang = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg - baseAngle;
        //ang -= (ang % (360.0f / snapdegrees));
         rectTransform.rotation =Quaternion.Euler(0.0f,0.0f,ang);
        //Debug.Log("Ang: " + ang);
        //Debug.Log("Percent" + (ang % 9));
        debugNum.text = "Num: " + CurrentNumber(ang);
        Tracker(CurrentNumber(ang));
        
    }

    public void Tracker(int currentNum)
    {
        if (!firstNumReached)
        {
            trackerGoal = 40 - firstNumber;
            if(previousNum > currentNum)
            {
                trackerCounter++;
            }
            else if(previousNum == currentNum)
            {
                return;
            }
            else
            {
                if(trackerCounter == trackerGoal)
                {
                    firstNumReached = true;
                    trackerCounter = 0;
                    Debug.Log("YAY");
                }
                else
                {
                    failed = true;
                    Debug.Log("Failed");
                }
            }
            previousNum = currentNum;
        }
        else if (!secondNumReached)
        {
            //Since you have to do a full rotation before going to second number
            if(firstNumber > secondNumber)
            {
                //New "formula" doesn't need Abs anymore, but it stays because I don't want to break things (they won't break if removed though) (i think)
                trackerGoal = Mathf.Abs(40+(40-firstNumber) + secondNumber - 1);
            }
            else
            {
                //Minus 1 because counting reasons
                trackerGoal = Mathf.Abs(40 + (secondNumber - firstNumber) -1);
            }
            Debug.Log("TreackerGoal: " + trackerGoal);
            Debug.Log("Threshold for first pass: " +  (40 - firstNumber));
            Debug.Log("Counter: " + trackerCounter);
            //We need to rotate fully around so check this first
            if(trackerCounter == 0 && previousNum == currentNum)
            {
                return;
            }
            else if(previousNum < currentNum)
            {
                Debug.Log("UP1");
                trackerCounter++;
            }
            else if(previousNum == currentNum)
            {
                return;
            }
            else if(trackerCounter >= (40 - firstNumber) && previousNum < currentNum)
            {
                Debug.Log("UP2");
                trackerCounter++;
            }
            else
            {
                if (trackerCounter == trackerGoal)
                {
                    secondNumReached = true;
                    trackerCounter = 0;
                    Debug.Log("YAYx2");
                }
                else
                {
                    failed = true;
                    Debug.Log("Failedx2");
                }
            }
            previousNum = currentNum;
        }
        else if (!thirdNumReached)
        {
            if (secondNumber > thirdNumber)
            {
                trackerGoal = Mathf.Abs(secondNumber - thirdNumber) -1 ;
            }
            else
            {
                //Minus 1 because counting reasons
                trackerGoal = Mathf.Abs(40 - (thirdNumber - secondNumber) - 1);
            }
            Debug.Log("TreackerGoal3: " + trackerGoal);
            Debug.Log("Threshold for first pass3: " + secondNumber);
            Debug.Log("Counter3: " + trackerCounter);

            if (trackerCounter == 0 && previousNum == currentNum)
            {
                return;
            }
            else if (previousNum > currentNum)
            {
                Debug.Log("UP1");
                trackerCounter++;
            }
            else if (previousNum == currentNum)
            {
                return;
            }
            else if (trackerCounter >= (secondNumber) && previousNum > currentNum)
            {
                Debug.Log("UP2");
                trackerCounter++;
            }
            else
            {
                if (trackerCounter == trackerGoal)
                {
                    thirdNumReached = true;
                    trackerCounter = 0;
                    Debug.Log("YAYx3");
                }
                else
                {
                    failed = true;
                    Debug.Log("Failedx3");
                }
            }
            previousNum = currentNum;
        }
        else
        {
            Debug.Log("SUCCESS :)");
        }
    }

    //This doesn't work
    public void ResetLock()
    {
        rectTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        previousNum = 40;
        trackerGoal = 0;
        firstNumReached = false;
        secondNumReached = false;
        thirdNumReached = false;
        ang = 0;
        Tracker(0);
        debugNum.text = "Num: " + CurrentNumber(ang);
    }

}

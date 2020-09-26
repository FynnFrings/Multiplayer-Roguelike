using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using NaughtyAttributes;
using UnityEditor;

public class FlyInAnimation : MonoBehaviour
{
    #region VARIABLES

    //Animated Object
    #region ANIMATED OBJECT: VARIABLES

    [Tooltip("Choose the Type you want the animation to be on")]
    [BoxGroup("Animated Object")]
    [Dropdown("animatedObjects")]
    [OnValueChanged("ChangeObjectType")]
    public string objectType;                   //animation is on that object type

    private string[] animatedObjects = new string[] { "GameObject", "Text", "Image", "Sprite" };

    private bool isText;                        //Is true if objectType = "Text"
    private bool isImage;                       //Is true if objectType = "Image"
    private bool isSprite;                      //Is true if objectType = "Sprite"

    #endregion

    //Text
    #region TEXT ANIMATION: VARIABLES

    [Tooltip("Tick if you want the text to be animated")]
    [ShowIf("isText")]
    [Foldout("Text Animation")]
    public bool textAnimation;          //If is true: the variables from beneath are visible in the Inspector!

    [ShowIf(EConditionOperator.And, "textAnimation", "isText")]
    [Tooltip("The speed each character takes to instantiate")]
    [Foldout("Text Animation")]
    public float typingSpeed = 0.05f;   //The speed each character takes to instantiate

    [ShowIf(EConditionOperator.And, "textAnimation", "isText")]
    [Tooltip("The last letter instantiated is bold")]
    [Foldout("Text Animation")]
    public bool startsBold;             //The last letter instantiated is bold

    [ShowIf(EConditionOperator.And, "textAnimation", "isText")]
    [Tooltip("The last letter instantiated is italic")]
    [Foldout("Text Animation")]
    public bool startsItalic;           //The last letter instantiated is italic

    [ShowIf(EConditionOperator.And, "textAnimation", "isText")]
    [Tooltip("The last letter instantiated is underlined")]
    [Foldout("Text Animation")]
    public bool startsUnderlined;       //The last letter instantiated is underlined

    [ShowIf(EConditionOperator.And, "textAnimation", "isText", "showDebugVariables")]
    [Tooltip("Is true when the text animation is done")]
    [Foldout("Text Animation")]
    [ReadOnly]
    public bool textAnimationDone;      //Is true when the text animation is done

    [ShowIf(EConditionOperator.And, "textAnimation", "isText", "showDebugVariables")]
    [Tooltip("stores original color")]
    [Foldout("Text Animation")]
    [ReadOnly]
    [SerializeField]
    private Color startColor_text;      //Color at start

    [ShowIf(EConditionOperator.And, "textAnimation", "isText", "showDebugVariables")]
    [Tooltip("current color on animation")]
    [Foldout("Text Animation")]
    [ReadOnly]
    [SerializeField]
    private Color currentColor_text;    //current Color (alpha)

    [ShowIf(EConditionOperator.And, "textAnimation", "isText", "showDebugVariables")]
    [Tooltip("stores orginal text")]
    [Foldout("Text Animation")]
    [ReadOnly]
    [SerializeField]
    private string startText;           //text at start

    #endregion

    //From Side
    #region Animation: VARIABLES

    [Tooltip("Tick if you want the GameObject to Fly in from anywhere outside the screen")]
    [BoxGroup("Fly in from outside of Screen")]
    public bool fromSide;               //If is true: the booleans from beneath are visible in the Inspector!

    [ShowIf("fromSide")] 
    [Tooltip("Fly in from TOP of Screen")]
    [BoxGroup("Fly in from outside of Screen")]
    [Space(5)]
    public bool fromTop;                //Fly in from top of Screen

    [ShowIf("fromSide")] 
    [Tooltip("Fly in from BOTTOM of Screen")]
    [BoxGroup("Fly in from outside of Screen")]
    [Space(-2)]
    public bool fromBottom;             //Fly in from bottom of Screen

    [ShowIf("fromSide")] 
    [Tooltip("Fly in from RIGHT of Screen")]
    [BoxGroup("Fly in from outside of Screen")]
    [Space(-2)]
    public bool fromRight;              //Fly in from right of Screen

    [ShowIf("fromSide")] 
    [Tooltip("Fly in from LEFT of Screen")]
    [BoxGroup("Fly in from outside of Screen")]
    [Space(-2)]
    public bool fromLeft;               //Fly in from left of Screen

    #endregion

    //From current Position
    #region FLY IN FROM CURRENT POSITION: VARIABLES 

    [Tooltip("Tick if you want the GameObject to Fly in from the current position")]
    [BoxGroup("Fly in from current position")]
    public bool fromCurrentPosition;    //If is true: the booleans from beneath are visible in the Inspector!

    [ShowIf("fromCurrentPosition")] 
    [Tooltip("Opacity of TEXT or IMAGE increases from Zero to Current")]
    [BoxGroup("Fly in from current position")]
    [Space(5)]
    public bool fadeIn;                 //Opacity of text or image increases from Zero to Current

    [ShowIf("fromCurrentPosition")] 
    [Tooltip("Scale increases from Zero to Current")]
    [BoxGroup("Fly in from current position")]
    public bool zoomIn;                 //Scale increases from Zero to Current

    #endregion

    //Animation Time
    #region ANIMATION TIME: VARIABLES

    [Header("Animation Time")]

    [Tooltip("Time in seconds the animation takes to finish")]
    [Foldout("Animation Settings")]
    public float animationTime = 0.75f; //How long the Animation lasts in Seconds

    [Tooltip("Time in seconds the animation takes to start")]
    [Foldout("Animation Settings")]
    public float waitTime = 0.05f;      //How long to wait till Animation starts in Seconds (AFTER first frame of Animation)

    [Tooltip("Shows the current Progress of the animation in Percentage")]
    [Foldout("Animation Settings")]
    [ProgressBar("Progress", 1, EColor.Blue)]
    [ReadOnly]
    public float currentTime = 0f;      //Shows the current Time of the animation

    [Tooltip("Is true when the animation is done")]
    [ShowIf("showDebugVariables")]
    [Foldout("Animation Settings")]
    [ReadOnly]
    public bool animationDone;          //Is true when the animation is done

    #endregion

    //Debug
    #region DEBUG: VARIABLES

    [Tooltip("If is true: private variables are visible in the Inspector!")]
    [Foldout("Debug")]
    public bool showDebugVariables;         //If is true: the variables from beneath are visible in the Inspector!

    [ShowIf(EConditionOperator.And, "showDebugVariables", "isImage")]
    [Foldout("Debug")]
    [ReadOnly]
    [SerializeField]
    private Color startColor_image;       //original Color of image

    [ShowIf(EConditionOperator.And, "showDebugVariables", "isImage")]
    [Foldout("Debug")]
    [ReadOnly]
    [SerializeField]
    private Color currentColor_image;     //current Color of image while animation is running

    [ShowIf(EConditionOperator.And, "showDebugVariables", "isSprite")]
    [Foldout("Debug")]
    [ReadOnly]
    [SerializeField]
    private Color startColor_sprite;       //original Color of sprite

    [ShowIf(EConditionOperator.And, "showDebugVariables", "isSprite")]
    [Foldout("Debug")]
    [ReadOnly]
    [SerializeField]
    private Color currentColor_sprite;     //current Color of sprite while animation is running

    [ShowIf("showDebugVariables")]
    [Foldout("Debug")]
    [ReadOnly]
    [SerializeField]
    private Vector3 startPos;               //Position at start

    [ShowIf("showDebugVariables")]
    [Foldout("Debug")]
    [ReadOnly]
    [SerializeField]
    private Vector3 startPosScale;          //Scale at start

    [ShowIf("showDebugVariables")]
    [Foldout("Debug")]
    [ShowAssetPreview]
    [ReadOnly]
    public GameObject animatedObject;       //Object that is being Animated

    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region START

        //InitializeData();

        PlayAnimation();

        #endregion
    }

    //Initializes Data
    [Button("Initialize", enabledMode: EButtonEnableMode.Editor)]
    private void InitializeData()
    {
        #region INITIALIZE

        animatedObject = gameObject;

        ChangeObjectType();

        startPos = animatedObject.transform.localPosition;
        startPosScale = animatedObject.transform.localScale;

        currentTime = 0f;
        animationDone = false;
        textAnimationDone = false;

        #region CHECK BOOLEANS

        if (!fromSide)
        {
            fromTop = false;
            fromBottom = false;
            fromRight = false;
            fromLeft = false;
        }
        if (!fromCurrentPosition)
        {
            fadeIn = false;
            zoomIn = false;
        }
        if (!isText)
        {
            textAnimation = false;
        }

        #endregion

        #region IF SCRIPT IS ON TEXT OR IMAGE

        if (isText)
        {
            startText = animatedObject.GetComponent<TextMeshProUGUI>().text;

            currentColor_text = animatedObject.GetComponent<TextMeshProUGUI>().color;
            startColor_text = currentColor_text;
            currentColor_text.a = 0f;
        }

        if (isImage)
        {
            currentColor_image = animatedObject.GetComponent<Image>().color;
            startColor_image = currentColor_image;
            currentColor_image.a = 0f;
        }

        if (isSprite)
        {
            currentColor_sprite = animatedObject.GetComponent<SpriteRenderer>().color;
            startColor_sprite = currentColor_sprite;
            currentColor_sprite.a = 0f;
        }

        #endregion

        #endregion
    }

    private void ChangeObjectType()
    {
        #region CHANGE OBJECT TYPE

        if (objectType.Equals("Text")) { isText = true; } else { isText = false; }
        if (objectType.Equals("Image")) { isImage = true; } else { isImage = false; }
        if (objectType.Equals("Sprite")) { isSprite = true; } else { isSprite = false; }

        #endregion
    }

    //Plays Animation
    [Button("Play Again", enabledMode: EButtonEnableMode.Playmode)]
    [EnableIf("animationDone")]
    private void PlayAnimation()
    {
        #region PLAY ANIMATION

        InitializeData();

        //Start Animation
        StartCoroutine(StartAnimation());

        if (textAnimation)
        {
            //Text specific animations
            StartCoroutine(TextAnimations());
        }

        #endregion
    }

    //Start Animation
    IEnumerator StartAnimation()
    {
        #region START ANIMATION

        while (currentTime <= 1.0)
        {
            currentTime += Time.deltaTime / animationTime;

            //Animation
            FlyInFromOutsideOfScreen();

            //Fly in from current Position
            FlyInFromCurrentPosition();

            #region WAIT FOR ANIMATION TO START

            //Wait until Start of Animation (AFTER first Frame)
            if (waitTime > 0)
                    {
                        yield return new WaitForSeconds(waitTime);
                        waitTime = 0f;
                    }

            #endregion

        yield return null;
        }

        #region NORMALIZE EVERYTHING

        if (isText)
        {
            animatedObject.GetComponent<TextMeshProUGUI>().color = startColor_text;
        }
        if (isImage)
        {
            animatedObject.GetComponent<Image>().color = startColor_image;
        }
        if(isSprite)
        {
            animatedObject.GetComponent<SpriteRenderer>().color = startColor_sprite;
        }

        animatedObject.transform.localPosition = startPos;

        #endregion

        animationDone = true;
        currentTime = 1f;

        #endregion
    }

    private void FlyInFromOutsideOfScreen()
    {
        #region FLY IN FROM DIFFERENT POSITION

        //Fly in from Top
        if (fromTop)
        {
            animatedObject.transform.localPosition = Vector3.Lerp(new Vector3(0, Screen.height, 0), startPos, Mathf.SmoothStep(0f, 1f, currentTime));
        }

        //Fly in from Bottom
        else if (fromBottom)
        {
            animatedObject.transform.localPosition = Vector3.Lerp(new Vector3(0, -Screen.height, 0), startPos, Mathf.SmoothStep(0f, 1f, currentTime));
        }

        //Fly in from Right
        else if (fromRight)
        {
            animatedObject.transform.localPosition = Vector3.Lerp(new Vector3(Screen.width, startPos.y, 0), startPos, Mathf.SmoothStep(0f, 1f, currentTime));
        }

        //Fly in from Left
        else if (fromLeft)
        {
            animatedObject.transform.localPosition = Vector3.Lerp(new Vector3(-Screen.width, startPos.y, 0), startPos, Mathf.SmoothStep(0f, 1f, currentTime));
        }

        #endregion
    }

    private void FlyInFromCurrentPosition()
    {
        #region FLY IN FROM CURRENT POSITION

        //Fade In (color opacity)
        if (fadeIn)
        {
            if (isText)
            {
                animatedObject.GetComponent<TextMeshProUGUI>().color = currentColor_text;
                currentColor_text.a = currentTime * startColor_text.a;
            }
            if (isImage)
            {
                animatedObject.GetComponent<Image>().color = currentColor_image;
                currentColor_image.a = currentTime * startColor_image.a;
            }
            if (isSprite)
            {
                animatedObject.GetComponent<SpriteRenderer>().color = currentColor_sprite;
                currentColor_sprite.a = currentTime * startColor_sprite.a;
            }
        }

        //Zoom In (Scale)
        if (zoomIn)
        {
            animatedObject.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(startPosScale.x, startPosScale.y, startPosScale.z), Mathf.SmoothStep(0f, 1f, currentTime));
        }

        #endregion
    }

    IEnumerator TextAnimations()
    {
        #region TEXT ANIMATION

        animatedObject.gameObject.GetComponent<TextMeshProUGUI>().text = "";

        foreach (char letter in startText.ToCharArray())
        {
            //stores original text
            string text = animatedObject.gameObject.GetComponent<TextMeshProUGUI>().text;

            #region MODIFY TEXT ON LAST CHARACTER

            string modifystart = "";
            string modifyend = "";

            if (startsBold)
            {
                modifystart += "<b>";
                modifyend += "</b>";
            }
            if(startsItalic)
            {
                modifystart += "<i>";
                modifyend += "</i>";
            }
            if(startsUnderlined)
            {
                modifystart += "<u>";
                modifyend += "</u>";
            }

            #endregion

            animatedObject.gameObject.GetComponent<TextMeshProUGUI>().text += modifystart + letter + modifyend;
            yield return new WaitForSeconds(typingSpeed);

            animatedObject.gameObject.GetComponent<TextMeshProUGUI>().text = text;
            animatedObject.gameObject.GetComponent<TextMeshProUGUI>().text += letter;
        }

        textAnimationDone = true;

        #endregion
    }
}


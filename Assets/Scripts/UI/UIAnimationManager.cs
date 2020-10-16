using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class UIAnimationManager : MonoBehaviour
{
    [ReorderableList]
    public List<FlyInAnimation> animationList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Plays All Animations
    [Button("Play All Animations", enabledMode: EButtonEnableMode.Playmode)]
    public void PlayAllAnimations(bool flyIn)
    {
        for(int i = 0; i < animationList.Count; i++)
        {
            if(animationList[i].animationDone)
            {
                animationList[i].flyIn = flyIn;

                animationList[i].PlayAnimation();
            }
        }
    }

    //Sets Active State of all animations
    public void SetActiveAllAnimation(bool setActive)
    {
        if (setActive)
        {
            EnableAllAnimation();
        }
        else
        {
            DisableAllAnimation();
        }
    }

    //Enables All Animations
    [Button("Enable All Animations", enabledMode: EButtonEnableMode.Always)]
    private void EnableAllAnimation()
    {
        for (int i = 0; i < animationList.Count; i++)
        {
            if (animationList[i] != null)
            {
                animationList[i].gameObject.SetActive(true);
            }
        }
    }

    //Disables All Animations
    [Button("Disable All Animations", enabledMode: EButtonEnableMode.Always)]
    private void DisableAllAnimation()
    {
        for (int i = 0; i < animationList.Count; i++)
        {
            if (animationList[i] != null)
            {
                animationList[i].gameObject.SetActive(false);
            }
        }
    }
}

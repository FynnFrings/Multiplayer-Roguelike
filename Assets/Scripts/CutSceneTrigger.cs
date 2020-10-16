using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutSceneTrigger : MonoBehaviour
{
    //General
    private WalkTowards walktowards;
    public CinemachineVirtualCamera cmCamera;
    public UIAnimationManager uianimationmanager;

    // Start is called before the first frame update
    void Start()
    {
        walktowards = GetComponent<WalkTowards>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(WalkTowardsCutScene());
    }

    IEnumerator WalkTowardsCutScene()
    {
        DisablePlayerMovement();

        //Cutscene Animation
        uianimationmanager.SetActiveAllAnimation(true);
        uianimationmanager.PlayAllAnimations(true);

        yield return new WaitForSeconds(1.5f);

        CameraLookAt(walktowards.targetPosition);

        yield return new WaitForSeconds(2f);

        CameraLookAt(walktowards.targetGameobject.transform);

        yield return new WaitForSeconds(0.3f);

        uianimationmanager.PlayAllAnimations(false);

        yield return new WaitForSeconds(1f);

        walktowards.enabled = true;
    }

    private void DisablePlayerMovement()
    {
        walktowards.targetGameobject.GetComponent<PlayerMovement>().rigidbody2d.velocity = new Vector2(0, 0);
        walktowards.targetGameobject.GetComponent<PlayerMovement>().enabled = false;
        walktowards.targetGameobject.GetComponentInChildren<PlayerAnimationManager>().ChangeAnimationState("Player_Idle");
    }

    private void CameraLookAt(Transform targetTransform)
    {
        cmCamera.m_Follow = targetTransform;
    }

    public void CutSceneFinished()
    {
        uianimationmanager.SetActiveAllAnimation(false);
    }
}

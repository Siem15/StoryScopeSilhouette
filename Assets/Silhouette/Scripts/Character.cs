using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float WalkSpeed, RunSpeed;
    [Min(0.1f)]
    public float stopRunningUntilThisDist;
    [Tooltip("Y axis from bottom 0 to top 1 - 0 = can fly")]
    [Range(0f,1f)]
    public float groundHeight;
    public float groundOffset;
    public bool controlRotation, controlPosition, facingRight;
    public GameObject endMarkerPrefab;
    string check;
    float speed, distance;
    Vector3 alteredHeight;
    GameObject endMarker;
   public Animator animator;
    FiducialController endMarkerFC;
    SpriteRenderer spriteRenderer;
    public bool singleSprite;
    Vector3 lastPos;
    FiducialController fidu;

    void Start()
    {
       if(singleSprite) spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        endMarker = SetEndMarker();
        fidu = GetComponent<FiducialController>();
    }

    void Update()
    {
        if (fidu.m_IsVisible && animator == null) GetAnimator();
        CheckGrounded();
        Flip();
    }

    private void CheckGrounded()
    {
        transform.position = Vector3.MoveTowards(transform.position, alteredHeight, speed * Time.deltaTime); //move to target

        alteredHeight = endMarker.transform.position; //flying height

        if (1 - endMarkerFC.m_ScreenPosition.y < groundHeight)
        {
            alteredHeight.y = 0 + groundOffset; //grounded
            SetAnimation(true);
        }
        else
        {
            SetAnimation(false);
        }
    }

    private GameObject SetEndMarker()
    {
        GameObject tempEndMarker = Instantiate(endMarkerPrefab);
        tempEndMarker.name = gameObject.name + "_MARKER";
        endMarkerFC = tempEndMarker.GetComponent<FiducialController>();
        endMarkerFC.MarkerID = GetComponentInParent<FiducialController>().MarkerID;
        endMarkerFC.IsRotationMapped = controlRotation;
        endMarkerFC.IsPositionMapped = controlPosition;
        return tempEndMarker;
    }

    private void GetAnimator()
    {
        if (animator == null)
        {
            if (GetComponentInChildren<Animator>()) animator = GetComponentInChildren<Animator>();
            else animator = GetComponent<Animator>();
        }
    }
    private void SetAnimation(bool x)
    {
        var temppos = endMarker.transform.position;
        if (x)temppos.y = transform.position.y;

        distance = Vector3.Distance(transform.position,temppos);
        if (distance > 0.1f && distance < stopRunningUntilThisDist) SetAnimationAndSpeed("walk", WalkSpeed);
        else if (distance > stopRunningUntilThisDist) SetAnimationAndSpeed("run", RunSpeed);
        else SetAnimationAndSpeed("idle", 0f);
    }

    public void SetAnimationAndSpeed(string animation, float setSpeed)
    {
        if (animation != check)
        {
            speed = setSpeed;
            if (!animator) return;
            if (fidu.m_IsVisible) animator.Play(animation);
            check = animation;
        }
    }

    private void Flip()
    {
        if (endMarker.transform.position.x - 0.05f >= transform.position.x && facingRight) Rotate();
        else if (endMarker.transform.position.x + 0.05f <= transform.position.x && !facingRight) Rotate();
    }
    void Rotate()
    {
        if (singleSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            return;
        }
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0, +180, 0));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchManager : MonoBehaviour
{
    public static TouchManager Instance;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float minSwipeDistance = 50f;

    private bool isSwiping = false;

    public event Action<Player, Vector2> OnSwipe;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isSwiping = true;
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)  touchEndPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        touchEndPos = touch.position;
                        ProcessSwipe();
                        isSwiping = false;
                    }
                    break;
            }
        }
    }

    void ProcessSwipe()
    {
        Vector2 swipeVector = touchEndPos - touchStartPos;
        if (swipeVector.magnitude < minSwipeDistance) return;

        Vector2 swipeDirection = swipeVector.normalized;

        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touchStartPos);

        Collider2D hitCollider = Physics2D.OverlapPoint(worldPoint);
        if (hitCollider != null)
        {
            Debug.Log("touch");
            Debug.Log(hitCollider.name);
            Player character = hitCollider.GetComponent<Player>();
            if (character != null)
            {
                Debug.Log("character");
                OnSwipe?.Invoke(character, swipeDirection);
            }
        }
    }
}

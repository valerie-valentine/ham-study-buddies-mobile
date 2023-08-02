using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishAndReappearOnTouch : MonoBehaviour
{
    private bool isVanished = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch phase is began
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // Check if the touch position is on the game object's collider
                if (GetComponent<Collider2D>().OverlapPoint(touchPosition))
                {
                    ToggleVisibility();
                }
            }
        }
    }

    void ToggleVisibility()
    {
        // Toggle the visibility of the game object
        isVanished = !isVanished;
        spriteRenderer.enabled = !isVanished;
    }
}
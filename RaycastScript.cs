using UnityEngine;
using System;

public class RaycastScript2D : MonoBehaviour
{
    Camera cam;
    public LayerMask mask;

    public static event Action OnHeadHit;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // Draw Ray
        Vector3 mousePos = Input.mousePosition;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);

        if (Input.GetMouseButtonDown(0))
        {
            // Create a 2D Ray
            Vector2 rayOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, mask);

            if (hit.collider != null)
            {
                OnHeadHit?.Invoke();
                Debug.Log("Head Hit");
            }
        }
    }
}

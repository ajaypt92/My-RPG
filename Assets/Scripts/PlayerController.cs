using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public LayerMask movementMask;

    Camera cam;
    PlayerMotor motor;

    public Interactable focus;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {

        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // If Left mouse button clicked - move to a point
		if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask))
            {
                // Move our player to where we hit
                motor.MoveToPoint(hit.point);

                // Stop focusing on the current obect
                RemoveFocus();
            }
        }

        // If Right mouse button clicked - focus on an interactable object
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {                
                // Check if we clicked on an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // If we did, set it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    // Set our focus to a new focus

    void SetFocus(Interactable newFocus)
    {
        // If our focus has changed
        if (newFocus != focus)
        {
            // Defocus the old one
            if (focus != null)
                focus.OnDefocused();

            focus = newFocus;   // Set our new focus
            motor.FollowTarget(newFocus);   // Follow the new focus
        }
        newFocus.OnFocused(transform);
    }



    // Remove our current focus
    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();

        focus = null;
        motor.StopFollowingTarget();
    }
}

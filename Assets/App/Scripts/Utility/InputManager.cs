using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode forwards, backwards, left, right;
    ObjectMover toMove;

    private void Awake()
    {
        toMove = this.GetComponent<ObjectMover>();
        CameraController.Me().SetPlayerToFollow(this.transform);
    }

    private void Update()
    {
        MouseInput();
    }

    private void FixedUpdate()
    {
        toMove.MoveObject(GetInputForMovement());
    }

    Vector2 GetInputForMovement()
    {
        Vector2 retVal = Vector2.zero;

        if (Input.GetKey(forwards))
        {
            retVal += new Vector2(0, 1);
        }

        if (Input.GetKey(backwards))
        {
            retVal += new Vector2(0, -1);
        }

        if (Input.GetKey(left))
        {
            retVal += new Vector2(-1, 0);
        }

        if (Input.GetKey(right))
        { 
            retVal += new Vector2(1, 0);
        }

        return retVal.normalized;
    }

    void MouseInput()
    {
        Vector2 MouseInWorld = CameraController.Me().GetMainCamera().ScreenToWorldPoint(Input.mousePosition);
        toMove.FacePoint(MouseInWorld);
    }
}

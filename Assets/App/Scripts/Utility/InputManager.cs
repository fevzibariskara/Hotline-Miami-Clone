using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] KeyCode forwards, backwards, left, right;

    ObjectMover toMove;
    PersonInventory playerInv;
    PersonWeaponController weaponController;

    private void Awake()
    {
        toMove = this.GetComponent<ObjectMover>();
        CameraController.Me().SetPlayerToFollow(this.transform);
        playerInv = this.GetComponent<PersonInventory>();
        weaponController = this.GetComponent<PersonWeaponController>();
    }

    private void Update()
    {
        MouseInput();
        PickupItem();
        DropItem();
        Attack();
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

    void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            weaponController.FireRangedWeapon();
        }
    }

    void PickupItem()
    {
        for (int x = 0; x < ItemManager.Me().GetItemsInWorld().Count; x++)
        {
            if (Vector2.Distance(this.transform.position, ItemManager.Me().GetItemsInWorld()[x].transform.position) < 2f)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ItemManager.Me().GetItemsInWorld()[x].EquipItem(this.gameObject);
                }
            }
        }
    }

    void DropItem()
    {
        if (playerInv.GetAllItems() == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int x = 0; x < playerInv.GetAllItems().Count; x++)
            {
                playerInv.GetAllItems()[x].UnequipItem();
            }
        }
    }

    void MouseInput()
    {
        Vector2 MouseInWorld = CameraController.Me().GetMainCamera().ScreenToWorldPoint(Input.mousePosition);
        toMove.FacePoint(MouseInWorld);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;	// Item to put in the inventory on pickup

    public override void Interact()
    {
        base.Interact();

        // Function to pickup the item and add to inventory
        PickUp();
    }

    public void PickUp()
    {
        Debug.Log("Picking up " + item.name);
        // Code to add the item to the inventory

        bool wasPickedUp = Inventory.instance.Add(item);    // Add to inventory

        // If successfully picked up
        if (wasPickedUp)
            Destroy(gameObject);	// Destroy item from scene
    }
}

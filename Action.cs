using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    // ActionType
    public enum ActionType { InventoryItem, OpenScene };
    public ActionType actionType;
    public bool selectBlockType = false;

    // Inventory Item
    public String itemName = "";
    public bool selectInventoryItem = false;
    public enum ItemStatus { Get, Set };
    public ItemStatus itemStatus;
    public bool selectItemStatus = false;

    //Scene
    public String sceneName = "";
    public bool selectScene = false;

    //(Wait Until Finish?)
}

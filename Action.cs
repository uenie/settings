using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public enum ActionType { InventoryItem, OpenScene };

    ActionType actionType;

    public Action(ActionType _actionType)
    {
        actionType = _actionType;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

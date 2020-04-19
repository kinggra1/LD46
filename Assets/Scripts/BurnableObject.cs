using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableObject : MonoBehaviour {
    [SerializeField]
    private ConsumeAction action;

    private bool burned = false;

    public void TryBeBurnedBy(PlayerController player)
    {
        if (!burned && action.CanBeAppliedTo(player))
        {
            burned = true;
            action.ApplyTo(player);
        }
    }
}

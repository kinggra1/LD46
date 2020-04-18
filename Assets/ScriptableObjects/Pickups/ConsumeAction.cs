using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumeAction : ScriptableObject {
    
    // If negative, it takes away size from the player. If positive, it adds.
    [SerializeField]
    protected int fuel;

    // Required player fuel level to consume this.
    [SerializeField]
    protected int minPlayerSize;

    public abstract void ApplyTo(PlayerController player);

    // Override in subtypes for any special behavior to determine if the player can consume.
    public virtual bool CanBeAppliedTo(PlayerController player) {
        return player.GetFuel() >= minPlayerSize;
    }
}

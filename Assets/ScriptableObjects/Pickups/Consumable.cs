using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumeAction : ScriptableObject {
    
    // If negative, it takes away size from the player. If positive, it adds.
    [SerializeField]
    public int fuel;

    public abstract void Apply(PlayerController player);
}

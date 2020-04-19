using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeSmallCreature.asset", menuName = "Consumables/SmallCreature")]
public class ConsumeSmallCreature : ConsumeAction{

    public override void ApplyTo(PlayerController player)
    {
        player.AddFuel(fuel);
        // TODO: Small animal *BIP* sound
    }
}

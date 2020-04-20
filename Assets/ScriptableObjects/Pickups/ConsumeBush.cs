using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeBush.asset", menuName = "Consumables/Bush")]
public class ConsumeBush : ConsumeAction{
    public override void ApplyTo(PlayerController player)
    {
        player.AddFuel(fuel);
    }
}
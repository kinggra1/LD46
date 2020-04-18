using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeIceCube.asset", menuName = "Consumables/IceCube")]
public class ConsumeIceCube : ConsumeAction {

    public override void ApplyTo(PlayerController player) {
        player.AddFuel(fuel);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeSmallRock.asset", menuName = "Consumables/SmallRock")]
public class ConsumeSmallRock : ConsumeAction {

    public override void ApplyTo(PlayerController player) {
        player.AddFuel(fuel);
    }
}

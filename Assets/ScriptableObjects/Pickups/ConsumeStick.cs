using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeStick.asset", menuName = "Consumables/Stick")]
public class ConsumeStick : ConsumeAction {

    public override void ApplyTo(PlayerController player) {
        player.AddFuel(fuel);
    }
}

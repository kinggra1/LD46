using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeStick.asset", menuName = "Consumables/Stick")]
public class ConsumeStick : ConsumeAction {

    public override void Apply(PlayerController player) {
        player.AddFuel(fuel);
    }
}

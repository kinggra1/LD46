using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConsumeLog.asset", menuName = "Consumables/Log")]
public class ConsumeLog : ConsumeAction{

    public override void Apply(PlayerController player)
    {
        player.AddFuel(fuel);
    }
}

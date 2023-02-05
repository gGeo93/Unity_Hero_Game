using UnityEngine;

public interface IPlayerParams
{
    PlayerStats PlayerStats { get; }
    Transform DekusRealPosition { get; set; }
    SweepFall SweepFall { get; }
    PlayerAnimatingConditions PlayerAnimatingConditions { get; }
    DangerSenseQuirk dangerSenseQuirk { get; }
}

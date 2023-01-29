using UnityEngine;

public interface ISettingUpEnemyParams : IPlayerParams, ISoundsEffect
{      
    DronesMainControllerCenter DronesMainController { get; }
}

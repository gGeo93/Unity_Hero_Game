using UnityEngine;
using System;
public static class Events
{
    public static Func<bool> onSunsetArrival;
    public static Action<bool> onToggleRewinding;
    public static Action<float> onSkyboxHeightChanged;
}

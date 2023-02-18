using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance 
    {
        get
        {
            return instance;
        } 
        private set
        {
            instance = value;
        }
    }
    public QuirksSlidersFunctionality quirksSlidersFunctionality;
    public QuirksSliders quirksSliders;
    public QuirksRateChange quirksRateChange;

    void Awake() => instance = this;
}

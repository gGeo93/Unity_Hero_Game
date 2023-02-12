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

    void Awake() => instance = this;
}

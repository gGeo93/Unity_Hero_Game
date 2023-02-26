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
    [SerializeField] Transform uiTransform;
    [SerializeField] Transform audioManipulator;
    [SerializeField] Transform player;
    public Transform UITransform{get; private set;}
    public Transform AudioManipulator{get; private set;}
    public Transform Player{get; private set;}

    void Awake()
    {
        UITransform = uiTransform;
        AudioManipulator = audioManipulator;
        Player = player;
        instance = this;
    }    
}

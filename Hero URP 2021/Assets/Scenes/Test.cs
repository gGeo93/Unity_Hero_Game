using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour
{
    public Transform objectToAnimate;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
          // objectToAnimate.DOMoveX(2f, 1f).OnComplete(() => objectToAnimate.DOScale(Vector3.one * 2f, 1f));
          objectToAnimate.DOShakePosition(1f);
        }
    }
}

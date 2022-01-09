using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    public static UILevel instance { get; private set; }

    public Image maskLevel;
    float originalLevelSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalLevelSize = maskLevel.rectTransform.rect.height;
    }

    public void SetValueLevel(float value)
    {
        maskLevel.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originalLevelSize * value);
    }

}
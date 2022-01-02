using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance { get; private set; }

    public Image maskHealth;
    public Image maskMana;
    float originalHealthSize;
    float originalManaSize;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalHealthSize = maskHealth.rectTransform.rect.width;
        originalManaSize = maskMana.rectTransform.rect.width;
    }

    public void SetValueHealth(float value)
    {
        maskHealth.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalHealthSize * value);
    }
    public void SetValueMana(float value)
    {
        maskMana.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalManaSize * value);
    }
}
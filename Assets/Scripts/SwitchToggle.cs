using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class SwitchToggle : MonoBehaviour
{
    public Image fillImage;
    public Image handleImage;
    public TextMeshProUGUI stateText;
    public DayNightToggle dayNightToggle; // DayNightToggle referansý EKLENDÝ!

    private RectTransform Handle;
    private bool isOn = false;
    private float targetFillAmount;

    public Color onColor = new Color(37f / 255f, 37f / 255f, 37f / 255f);
    public Color offColor = Color.white;


    private void Start()
    {
        Handle = handleImage.GetComponent<RectTransform>();

        Handle.pivot = new Vector2(0f, 0.5f);

        targetFillAmount = isOn ? 1f : 0f;


        UpdateHandlePivot();
        UpdateFillAmount();
        UpdateStateText();

    }

    public void Toggle()
    {
        isOn = !isOn; //durumu deðiþtir

        targetFillAmount = isOn ? 1f : 0f;

        UpdateHandlePivot();
        UpdateStateText();

        if(dayNightToggle != null)
        {
            dayNightToggle.ToggleDayNight();
        }

    }

    private void Update()
    {
        float fillSpeed = 5f;

        fillImage.fillAmount = Mathf.Lerp(fillImage.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);

        float PosX = isOn ? fillImage.rectTransform.rect.width : 0f;
        Handle.anchoredPosition = new Vector2(Mathf.Lerp(Handle.anchoredPosition.x, PosX, Time.deltaTime * 8f), Handle.anchoredPosition.y);

    }


    private void UpdateStateText()
    {
        stateText.text = isOn ? "on" : "off";
        stateText.color = isOn ? onColor : offColor;
    }

    // update fill amount instantly
    private void UpdateFillAmount()
    {
        //update fill amount 
        fillImage.fillAmount = targetFillAmount;

        //Update handle position instantly
        Handle.anchoredPosition = new Vector2(targetFillAmount * fillImage.rectTransform.rect.width, Handle.anchoredPosition.y);

    }

    private void UpdateHandlePivot()
    {
        if (isOn)
        {
            Handle.pivot = new Vector2(1f, 0.5f); //pivotu saða çeker
        }
        else
        {
            Handle.pivot = new Vector2 (0f, 0.5f); //pivotu sola çeker 
        }
    }
}

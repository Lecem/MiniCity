using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Switch_Toggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform; //Sürüklwmw butonu
    [SerializeField] DayNightToggle dayNightToggle;

    [SerializeField] Color DayBackgroundColor;
    [SerializeField] Color DayHandleColor;
    [SerializeField] Color NightBackgroundColor;
    [SerializeField] Color NightHandleColor;

    Image BackgroundImage;
    Image HandleImage;


    public TextMeshProUGUI buttonText;


    Toggle toggle;
    Vector2 handlePosition;//
    public Image Handle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();

        handlePosition = uiHandleRectTransform.anchoredPosition;

        BackgroundImage =uiHandleRectTransform.parent.GetComponent<Image>();
        HandleImage = uiHandleRectTransform.GetComponent<Image>();

        DayBackgroundColor = BackgroundImage.color;
        DayHandleColor = HandleImage.color; 

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(true);
        }
    }
    void OnSwitch(bool on)
    {
        //if (on)
        //{
        //    uiHandleRectTransform.anchoredPosition = handlePosition * -1;
        //    buttonText.text = "Night Mode";
            
        //}
        //else
        //{
        //    uiHandleRectTransform.anchoredPosition = handlePosition;
        //}

        uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition;

        BackgroundImage.color = on ? NightBackgroundColor : DayBackgroundColor  ;

        HandleImage.color = on? NightHandleColor : DayHandleColor ;

        buttonText.text = on? "Day Mode": "Night Mode"  ;


        if (dayNightToggle != null)
        {
            dayNightToggle.ToggleDayNight();
        }

    }
    private void Start()
    {
        
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }

}

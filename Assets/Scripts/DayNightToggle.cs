using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayNightToggle : MonoBehaviour
{
    [SerializeField] Color dayButtonColor;
    [SerializeField] Color nightButtonColor; 
    
    public Light directionalLight;
    public Color dayColor = new Color(1f, 0.95f, 0.8f); 
    public Color nightColor = new Color(0.1f, 0.1f, 0.3f);
    public Color dayBackground;
    public Color nightBackground;

    public Camera mainCamera;
    public TextMeshProUGUI buttonText; // Butonun �zerindeki yaz�
    public Button DayNightButton;

    public Light[] houseLights;

    private bool isDay = true;// ba�lang��ta g�nd�z

    private void Start()
    {
        houseLights = GameObject.FindGameObjectsWithTag("SpotLight")
            .Select(obje => obje.GetComponent<Light>()) 
            .Where(light => light != null)
            .ToArray();

        DayNightButton.onClick.AddListener(ToggleDayNight);
        isDay = true;
        UpdateUI(); // �lk UI g�ncellemesini yap
    }

    public void ToggleDayNight()
    {
        isDay = !isDay;

        if (isDay)
        {
            //G�nd�z ayarlar�
            directionalLight.color = dayColor;
            directionalLight.intensity = 2.2f;
            buttonText.text = "Night Mode";
            DayNightButton.image.color = nightButtonColor;
            mainCamera.backgroundColor = dayBackground;

            //I��klar
            SetHouseLights(false);

        }

        else
        {
            //Gece ayarlar�
            directionalLight.color = nightColor;
            directionalLight.intensity = 0f;
            buttonText.text = "Day Mode";
            DayNightButton.image.color = dayButtonColor;
            mainCamera.backgroundColor = nightBackground;

            //I��klar
            SetHouseLights(true);
        }
    }

    private void UpdateUI()
    {
        // Ba�lang��ta butonun rengini ve metnini g�ncelle
        buttonText.text = isDay ? "Night Mode" : "Day Mode";
        DayNightButton.image.color = isDay ? nightButtonColor : dayButtonColor ;
        SetHouseLights(!isDay); //i�iklar� kapal� ba�lat
    }

    private void SetHouseLights(bool state)
    {
        foreach (Light light in houseLights)
        {
            light.enabled = state; //true ise false, false ise true
        }
    }
}

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
    public TextMeshProUGUI buttonText; // Butonun üzerindeki yazý
    public Button DayNightButton;

    public Light[] houseLights;

    private bool isDay = true;// baþlangýçta gündüz

    private void Start()
    {
        houseLights = GameObject.FindGameObjectsWithTag("SpotLight")
            .Select(obje => obje.GetComponent<Light>()) 
            .Where(light => light != null)
            .ToArray();

        DayNightButton.onClick.AddListener(ToggleDayNight);
        isDay = true;
        UpdateUI(); // Ýlk UI güncellemesini yap
    }

    public void ToggleDayNight()
    {
        isDay = !isDay;

        if (isDay)
        {
            //Gündüz ayarlarý
            directionalLight.color = dayColor;
            directionalLight.intensity = 2.2f;
            buttonText.text = "Night Mode";
            DayNightButton.image.color = nightButtonColor;
            mainCamera.backgroundColor = dayBackground;

            //Iþýklar
            SetHouseLights(false);

        }

        else
        {
            //Gece ayarlarý
            directionalLight.color = nightColor;
            directionalLight.intensity = 0f;
            buttonText.text = "Day Mode";
            DayNightButton.image.color = dayButtonColor;
            mainCamera.backgroundColor = nightBackground;

            //Iþýklar
            SetHouseLights(true);
        }
    }

    private void UpdateUI()
    {
        // Baþlangýçta butonun rengini ve metnini güncelle
        buttonText.text = isDay ? "Night Mode" : "Day Mode";
        DayNightButton.image.color = isDay ? nightButtonColor : dayButtonColor ;
        SetHouseLights(!isDay); //iþiklarý kapalý baþlat
    }

    private void SetHouseLights(bool state)
    {
        foreach (Light light in houseLights)
        {
            light.enabled = state; //true ise false, false ise true
        }
    }
}

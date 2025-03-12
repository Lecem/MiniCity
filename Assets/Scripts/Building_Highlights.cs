using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    public string selectableTag = "Selectable"; // Hangi tag'e sahip objeler seçilebilir olacak
    private GameObject lastObject; // En son etkileþime girilen obje

    void Update()
    {
        // Mouse'un ekran üzerindeki noktasýndan bir ýþýn gönderiyoruz
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Eðer ýþýn bir objeye çarparsa
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Eðer çarpýlan obje "Selectable" tag'ine sahipse
            if (hitObject.CompareTag(selectableTag))
            {
                // Yeni objeye çarptýysak, görünür yap
                if (hitObject != lastObject)
                {
                    if (lastObject != null)
                    {
                        lastObject.SetActive(false); // Önceki objeyi gizle
                    }

                    hitObject.SetActive(true); // Yeni objeyi göster
                    lastObject = hitObject; // Yeni objeyi kaydet
                }
            }
        }
        else
        {
            // Eðer ýþýn hiçbir objeye çarpmýyorsa, son görünen objeyi gizle
            if (lastObject != null)
            {
                lastObject.SetActive(false);
                lastObject = null;
            }
        }
    }
}

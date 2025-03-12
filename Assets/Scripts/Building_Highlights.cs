using UnityEngine;

public class ObjectHighlighter : MonoBehaviour
{
    public string selectableTag = "Selectable"; // Hangi tag'e sahip objeler se�ilebilir olacak
    private GameObject lastObject; // En son etkile�ime girilen obje

    void Update()
    {
        // Mouse'un ekran �zerindeki noktas�ndan bir ���n g�nderiyoruz
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // E�er ���n bir objeye �arparsa
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

            // E�er �arp�lan obje "Selectable" tag'ine sahipse
            if (hitObject.CompareTag(selectableTag))
            {
                // Yeni objeye �arpt�ysak, g�r�n�r yap
                if (hitObject != lastObject)
                {
                    if (lastObject != null)
                    {
                        lastObject.SetActive(false); // �nceki objeyi gizle
                    }

                    hitObject.SetActive(true); // Yeni objeyi g�ster
                    lastObject = hitObject; // Yeni objeyi kaydet
                }
            }
        }
        else
        {
            // E�er ���n hi�bir objeye �arpm�yorsa, son g�r�nen objeyi gizle
            if (lastObject != null)
            {
                lastObject.SetActive(false);
                lastObject = null;
            }
        }
    }
}

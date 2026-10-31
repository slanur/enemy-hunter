using UnityEngine;

public class HavanMermisi : MonoBehaviour
{
    [SerializeField] private float hiz = 6f;
    [SerializeField] private GameObject patlamaEfektiPrefab;

    private Vector3 hedefDusmanPozisyonu;
    private float hasar;
    private float yaricap;

    public void Ayarla(Vector3 hedefPozisyon, float hasar, float yaricap)
    {
        this.hedefDusmanPozisyonu = hedefPozisyon;
        this.hasar = hasar;
        this.yaricap = yaricap;
    }

    private void Update()
    {
        // Hedef pozisyona doğru ilerle
        Vector3 yon = (hedefDusmanPozisyonu - transform.position).normalized;
        transform.position += yon * hiz * Time.deltaTime;

        // Yeterince yaklaştıysak patlat
        if (Vector3.Distance(transform.position, hedefDusmanPozisyonu) < 0.1f)
        {
            Patlat();
        }
    }

    private void Patlat()
    {
        // Görsel efekt
        if (patlamaEfektiPrefab != null)
        {
            GameObject patlamaEfekti = Instantiate(patlamaEfektiPrefab, hedefDusmanPozisyonu, Quaternion.identity);
            Destroy(patlamaEfekti, 0.5f);
        }

        // Çevredeki düşmanlara alan hasarı
        Dusman[] tumDusmanlar = FindObjectsOfType<Dusman>();
        foreach (var geciciDusman in tumDusmanlar)
        {
            // Uçan düşmanlar etkilenmesin
            if (geciciDusman.UcanMi)
            {
                continue;
            }

            float mesafe = Vector3.Distance(hedefDusmanPozisyonu, geciciDusman.transform.position);
            if (mesafe <= yaricap)
            {
                geciciDusman.HasarAl(hasar);
            }
        }

        Destroy(gameObject);
    }
}
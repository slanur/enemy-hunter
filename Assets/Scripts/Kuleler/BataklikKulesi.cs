using UnityEngine;

public class BataklikKulesi : Kule
{
    [Header("Bataklık Ayarları")]
    [SerializeField] private GameObject camurMermisiPrefab;   // GameObject
    [SerializeField] private float yavaslatmaOrani = 0.5f;
    [SerializeField] private float yavaslatmaSuresi = 3f;

    private void Reset()
    {
        menzil = 3.5f;
        temelHasar = 15f;
        atisSuresi = 2f;
    }

    protected override void AtesEt()
    {
        if (DusmanHedef == null)
        {
            return;
        }

        if (camurMermisiPrefab == null)
        {
            Debug.LogError("BataklikKulesi:Camur mermisi Prefabi atanmadı.");
            return;
        }

        GameObject MermiyiFirlat = Instantiate(camurMermisiPrefab, atisNoktasi.position, Quaternion.identity);

        Mermi camurMermisi = MermiyiFirlat.GetComponent<Mermi>();
        if (camurMermisi == null)
        {
            Debug.LogError("BataklikKulesi:Camur mermisinin prefabi üzerinde Mermi script'i yok!");
            return;
        }

        camurMermisi.Ayarla(DusmanHedef, temelHasar, true, yavaslatmaOrani, yavaslatmaSuresi);
    }
}
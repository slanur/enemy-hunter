using UnityEngine;

public class ZehirliOkKulesi : Kule
{
    [Header("Özel Ayarlar")]
    [SerializeField] private GameObject okMermisiPrefab;

    private void Reset()
    {
        menzil = 4f;
        temelHasar = 25f;
        atisSuresi = 1f;
    }

    protected override void AtesEt()
    {
        if (DusmanHedef == null)
        {
            return;
        }

        if (okMermisiPrefab == null)
        {
            Debug.LogError("ZehirliOkKulesi: ok mermisi prefabi atanamadi. ");
            return;
        }

        // Prefabdan yeni mermi üret
        GameObject firlatilanOk = Instantiate(okMermisiPrefab, atisNoktasi.position, Quaternion.identity);


        // Üzerindeki Mermi script'ini al
        Mermi mermiScripti = firlatilanOk.GetComponent<Mermi>();
        if (mermiScripti == null)
        {
            Debug.LogError("ZehirliOkKulesi: ok mermisi prefabi üzerinde Mermi script'i yok!");
            return;
        }

        mermiScripti.Ayarla(DusmanHedef, temelHasar, false, 0f, 0f);
    }
}
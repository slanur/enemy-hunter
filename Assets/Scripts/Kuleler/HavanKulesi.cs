using UnityEngine;

public class HavanKulesi : Kule
{
    [Header("Havan Ayarları")]
    [SerializeField] private HavanMermisi havanMermisiPrefab;
    [SerializeField] private float patlamaYaricapi = 2f;

    private void Reset()
    {
        menzil = 5f;
        temelHasar = 20f;
        atisSuresi = 3f; // her 3 sn
    }

    protected override bool UygunHedefMi(Dusman d)
    {
        // uçan düşmanları hedeflememeli
        return !d.UcanMi;
    }

    protected override void AtesEt()
    {
        if (DusmanHedef == null)
        {
            return;
        }

        HavanMermisi havanMermisiFirlat = Instantiate(havanMermisiPrefab, atisNoktasi.position, Quaternion.identity);
        havanMermisiFirlat.Ayarla(DusmanHedef.transform.position, temelHasar, patlamaYaricapi);
    }
}
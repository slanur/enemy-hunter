using UnityEngine;

public class Sahin : Dusman
{
    protected override void Start()
    {
        dusmanAdi = "Uçan Düşman (Şahin)";
        MaxCan = 50f;
        hiz = 50f * 1.5f; // %50 hızlı
        zirh = 0f;
        paraOdulu = 15;
        usHasari = 5;
        UcuyoMu = true;

        base.Start(); //Üst sınıftaki Start() fonksiyonunu çalıştırır
    }
}
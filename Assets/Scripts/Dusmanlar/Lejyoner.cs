using UnityEngine;

public class Lejyoner : Dusman
{
    protected override void Start()
    {
        dusmanAdi = "Zırhlı Düşman (Lejyoner)";
        MaxCan = 50f * 1.5f;  // %50 daha fazla can
        hiz = 50f * 0.5f;          // %50 daha yavaş
        zirh = Random.Range(50, 101); // 50 100 arası
        paraOdulu = 20;
        usHasari = 10;
        UcuyoMu = false;

        base.Start();
    }
}
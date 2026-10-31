using UnityEngine;

public class Eskiya : Dusman
{
    protected override void Start()
    {
        dusmanAdi = "Standart Düşman (Eşkıya)";
        MaxCan = 50f;
        hiz = 50f;
        zirh = 0f;
        paraOdulu = 10;
        usHasari = 5;
        UcuyoMu = false;

        base.Start();
    }
}
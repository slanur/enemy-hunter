using UnityEngine;
using System.Linq;

public abstract class Kule : MonoBehaviour
{
    [Header("Genel Kule Ayarları")]
    [SerializeField] protected float menzil = 3f;
    [SerializeField] protected float temelHasar = 10f;
    [SerializeField] protected float atisSuresi = 1f;
    [SerializeField] protected Transform atisNoktasi;

    protected float atisSayaci = 0f;
    protected Dusman DusmanHedef;

    protected virtual void Update()
    {
        atisSayaci -= Time.deltaTime;
        if (atisSayaci <= 0f)
        {
            HedefSec();
            if (DusmanHedef != null)
            {
                atisSayaci = atisSuresi;
                AtesEt();
            }
        }
    }

    protected virtual void HedefSec()
    {
        Dusman[] TumDusmanlar = FindObjectsOfType<Dusman>();

        // Menzilde ve şartlara uyanlar
        var UygunDusmanlar = TumDusmanlar
            .Where(d => UygunHedefMi(d) &&
                        Vector3.Distance(transform.position, d.transform.position) <= menzil);

        // Üsse en yakın olanı seç (Ilerleme değeri en büyük)
        DusmanHedef = UygunDusmanlar
            .OrderByDescending(d => d.KimOnde)
            .FirstOrDefault();
    }

    protected virtual bool UygunHedefMi(Dusman d) => true;

    protected abstract void AtesEt();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, menzil);
    }
}
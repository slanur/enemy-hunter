using UnityEngine;

public class ProjectileArrow : MonoBehaviour
{
    public float okHizi = 12f;
    public float okHasari = 10f;

    private Transform okunDusmanHedefi;

    public void HedefBelirle(Transform h)
    {
        okunDusmanHedefi = h;
    }

    private void Update()
    {
        if (okunDusmanHedefi == null)
        {
            Destroy(gameObject);
            return;
        }

        // hedefe doğru hareket
        Vector3 direction = (okunDusmanHedefi.position - transform.position).normalized;
        transform.position += direction * okHizi * Time.deltaTime;

        // ok yönünü hedefe döndür,ileriye baksın
        float okuHedefeDondur = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, okuHedefeDondur);

        // mesafe çok yakınsa çarp
        if (Vector3.Distance(transform.position, okunDusmanHedefi.position) < 0.2f)
        {
            Dusman okunVurduguDusman = okunDusmanHedefi.GetComponent<Dusman>();
            if (okunVurduguDusman != null)
            {
                okunVurduguDusman.HasarAl(okHasari);
            }


            Destroy(gameObject);
        }
    }
}
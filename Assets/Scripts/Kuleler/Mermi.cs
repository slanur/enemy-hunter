using UnityEngine;

public class Mermi : MonoBehaviour
{
    [SerializeField] private float hiz = 10f;

    private Dusman mermininHedefi;
    private float hasar;
    private bool yavaslatmaUygula;
    private float yavaslatmaOrani;
    private float yavaslatmaSuresi;


    public void Ayarla(Dusman mermininHedefi, float hasar, bool yavaslatma, float oran, float sure)
    {
        this.mermininHedefi = mermininHedefi;
        this.hasar = hasar;
        this.yavaslatmaUygula = yavaslatma;
        this.yavaslatmaOrani = oran;
        this.yavaslatmaSuresi = sure;
    }

    private void Update()
    {
        if (mermininHedefi == null)
        {
            Destroy(gameObject);
            return;
        }

        // Hedefe doğru ilerle
        Vector3 yon = (mermininHedefi.transform.position - transform.position).normalized;
        transform.position += yon * hiz * Time.deltaTime;

        // Çarpışma kontrolü,yaklaştıysak vur
        if (Vector3.Distance(transform.position, mermininHedefi.transform.position) < 0.1f)
        {
            mermininHedefi.HasarAl(hasar);

            if (yavaslatmaUygula)
            {
                mermininHedefi.Yavaslat(yavaslatmaOrani, yavaslatmaSuresi);
            }

            Destroy(gameObject);
        }
    }
}
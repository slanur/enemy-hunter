using UnityEngine;

//mono behaviour: unity sahnesine companent olarak eklenebilsin diye
//abstractan doğrudan nesne üretmiyoruz
public abstract class Dusman : MonoBehaviour
{
    //düşman sınıfının temel özellikleri, inspectorde görmek için
    [Header("Temel Özellikler")]
    [SerializeField] protected string dusmanAdi;
    [SerializeField] protected float MaxCan = 50f;
    [SerializeField] protected float hiz = 50f;
    [SerializeField] protected float zirh = 0f;
    [SerializeField] protected int paraOdulu = 10;
    [SerializeField] protected int usHasari = 5;
    [SerializeField] protected bool UcuyoMu = false;

    [Header("Görsel")]
    [SerializeField] private HealthBarUI saglikBari;

    protected float mevcutCan;
    protected int MevcutIndex = 0;
    protected WaypointPath yol;


    // bunlar property, dışarıdan bu değerlere erişilebilsin ama direkt değiştirilmesin diye böyle yazılmışlar
    public int ParaOdulu => paraOdulu;
    public int UsHasari => usHasari;
    public string DusmanAdi => dusmanAdi;
    public bool UcanMi => UcuyoMu;
    public float Zirh => zirh;

    // Kulelerin en öndekini seçmesi için
    public float KimOnde
    {
        get;
        private set; // private set, sadece bu sinif içinde değer atanabilir
    }


    // yavaşlatma 
    private float yavaslamaCarpani = 1f;
    private float yavaslamaSuresi = 0f;

    // öldü ya da üsse ulaştı, aynı karede ikinci kez sayılmasın diye
    private bool oyundanCikti = false;

    protected virtual void Start() // alt sınıflar isterseler bunu override edebilirler
    {
        mevcutCan = MaxCan;
        if (saglikBari) saglikBari.AyarlaMaksimumCan(MaxCan);
    }

    // düşman spawn olduğunda wavemanager bunu çağırır
    public void Baslat(WaypointPath path)
    {
        yol = path;
        transform.position = yol.GetNokta(0);
        MevcutIndex = 0;
    }

    protected virtual void Update()
    {
        Hareket();
        YavaslamaKontrol();
    }

    private void Hareket()
    {
        if (yol == null || oyundanCikti) //yol atanmadıysa null dön
        {
            return;
        }


        Vector3 HedefWaypoint = yol.GetNokta(MevcutIndex);
        float adim = hiz * yavaslamaCarpani * Time.deltaTime / 50f; // hız 50 baz alındı
        transform.position = Vector3.MoveTowards(transform.position, HedefWaypoint, adim);

        if (Vector3.Distance(transform.position, HedefWaypoint) < 0.01f)
        {
            MevcutIndex++;
            if (MevcutIndex >= yol.WaypointSayisi)
            {
                // Üsse ulaştı
                oyundanCikti = true;
                GameManager.Instance.DusmanUsseUlasti(this);
                Destroy(gameObject);
                return;
            }
        }

        // ilerleme kaba hesap
        KimOnde = MevcutIndex + Vector3.Distance(transform.position, HedefWaypoint);
    }

    private void YavaslamaKontrol()
    {
        if (yavaslamaSuresi <= 0f)
        {
            return;
        }
        yavaslamaSuresi -= Time.deltaTime;
        if (yavaslamaSuresi <= 0f)
        {
            yavaslamaCarpani = 1f;
        }
    }

    //hasar fonksiyonu 
    public void HasarAl(float kuleHasari)
    {
        if (oyundanCikti)
        {
            return;
        }

        //hasar formülü
        float netHasar = kuleHasari * (1f - (Zirh / (Zirh + 100f)));
        mevcutCan -= netHasar;

        if (saglikBari)
        {
            saglikBari.Guncelle(mevcutCan);
        }


        if (mevcutCan <= 0)
        {
            oyundanCikti = true;
            GameManager.Instance.DusmanOldu(this);
            Destroy(gameObject);
        }
    }

    public void Yavaslat(float YavaslamaOrani, float sure)
    {
        // %50 yavaşlatma/ oran = 0.5f
        yavaslamaCarpani = 1f - YavaslamaOrani;
        yavaslamaSuresi = sure;
    }
}
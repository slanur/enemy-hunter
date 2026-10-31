using UnityEngine;

public class KuleInsaaYonetici : MonoBehaviour
{
    public static KuleInsaaYonetici Instance
    {
        get;
        private set;
    }

    [Header("Kule Prefabları")]
    [SerializeField] private GameObject zehirliOkKulesiPrefab;
    [SerializeField] private int zehirliOkKulesiMaliyeti = 50;

    [SerializeField] private GameObject havanKulesiPrefab;
    [SerializeField] private int havanKulesiMaliyeti = 75;

    [SerializeField] private GameObject bataklikKulesiPrefab;
    [SerializeField] private int bataklikKulesiMaliyeti = 70;

    private GameObject seciliKulePrefab;
    private int seciliKuleMaliyeti;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    #region Kule Seçim Fonksiyonları (UI butonlarına bağla)
    public void ZehirliOkKulesiSec()
    {
        seciliKulePrefab = zehirliOkKulesiPrefab;
        seciliKuleMaliyeti = zehirliOkKulesiMaliyeti;
    }

    public void HavanKulesiSec()
    {
        seciliKulePrefab = havanKulesiPrefab;
        seciliKuleMaliyeti = havanKulesiMaliyeti;
    }

    public void BataklikKulesiSec()
    {
        seciliKulePrefab = bataklikKulesiPrefab;
        seciliKuleMaliyeti = bataklikKulesiMaliyeti;
    }
    #endregion

    public void BuildSpotTiklandi(BuildSpot insaaNoktasi)
    {
        if (insaaNoktasi == null)
        {
            return;
        }
        if (insaaNoktasi.DoluMu)
        {
            return;
        }
        if (seciliKulePrefab == null)
        {
            Debug.Log("Önce bir kule seçmelisin.");
            return;
        }

        // Parayı GameManager'dan eksiltiyoruz
        if (!GameManager.Instance.ParaHarca(seciliKuleMaliyeti))
        {
            Debug.Log("Yetersiz para. Kule kurulamadı.");
            return;
        }

        // Kuleyi kur
        insaaNoktasi.KuleKur(seciliKulePrefab);
        SavunmaGunlugu.Kaydet(
    $"Kule Kuruldu-Kule: {seciliKulePrefab.name},Maliyet:{seciliKuleMaliyeti},Konum: {insaaNoktasi.transform.position}");
    }
}
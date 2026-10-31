using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get;
        private set;
    }

    [Header("Oyuncu Değerleri")]
    [SerializeField] private int baslangicCani = 100;
    [SerializeField] private int baslangicParasi = 200;

    public int OyuncununCani
    {
        get;
        private set;
    }
    public int OyuncununParasi
    {
        get;
        private set;
    }

    [Header("UI Referansları")]
    [SerializeField] private TMP_Text canText;
    [SerializeField] private TMP_Text paraText;
    [SerializeField] private TMP_Text dalgaText;
    [SerializeField] private GameObject kazandinPanel;
    [SerializeField] private GameObject kaybettinPanel;

    [Header("Dalga Sistemi")]
    public int toplamDalgaSayisi = 2;
    public int hangiDalga = 0;

    private int sahnedekiDusmanSayisi = 0;
    private bool oyunBitti = false;
    private bool dalgalarTamamlandi = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        OyuncununCani = baslangicCani;
        OyuncununParasi = baslangicParasi;
        GuncelleUI();
    }

    public void DalgaBasladi(int dalgaIndex)
    {
        hangiDalga = dalgaIndex;
        GuncelleUI();
        Debug.Log($"Dalga {hangiDalga + 1} başladı.");

        SavunmaGunlugu.Kaydet($"Dalga Başladı- Dalga:{hangiDalga + 1}/{toplamDalgaSayisi}");
    }

    public void DusmanSpawnlandi()
    {
        sahnedekiDusmanSayisi++;
    }

    // WaveManager tüm dalgaları doğurduğunda çağırır
    public void DalgalarTamamlandi()
    {
        dalgalarTamamlandi = true;
        KontrolKazandinMi();
    }

    public void DusmanOldu(Dusman dusman)
    {
        sahnedekiDusmanSayisi--;
        OyuncununParasi += dusman.ParaOdulu;
        Debug.Log($"{dusman.DusmanAdi} öldü.Ödül +{dusman.ParaOdulu}. Toplam Para: {OyuncununParasi}");
        GuncelleUI();
        KontrolKazandinMi();

        SavunmaGunlugu.Kaydet(
            $"Dusman Öldu - Ad:{dusman.DusmanAdi},Ödül: {dusman.ParaOdulu},Toplam Para: {OyuncununParasi}");
    }

    public void DusmanUsseUlasti(Dusman dusman)
    {
        sahnedekiDusmanSayisi--;
        OyuncununCani -= dusman.UsHasari;
        Debug.Log($"{dusman.DusmanAdi} üsse ulaştı.Oyuncu canı-{dusman.UsHasari}.Kalan Can:{OyuncununCani}");
        GuncelleUI();
        KontrolKaybettinMi();
        KontrolKazandinMi();

        SavunmaGunlugu.Kaydet(
        $"Üsse ulaşıldı: {dusman.DusmanAdi}, Verilen Hasar: {dusman.UsHasari}, Kalan Can: {OyuncununCani}");
    }

    public bool ParaHarca(int kuleParasi)
    {
        if (OyuncununParasi < kuleParasi)
        {
            SavunmaGunlugu.Kaydet(
                $"Para Yetersiz - Harcanmak istenen:{kuleParasi},Mevcut:{OyuncununParasi}");
            return false;
        }

        OyuncununParasi -= kuleParasi;
        GuncelleUI();

        SavunmaGunlugu.Kaydet(
            $"Para Harcandı-Miktar:{kuleParasi},Kalan Para: {OyuncununParasi}");

        return true;
    }


    private void GuncelleUI()
    {
        if (canText)
        {
            canText.text = $"Can: {OyuncununCani}";
        }
        if (paraText)
        {
            paraText.text = $"Para: {OyuncununParasi}";
        }
        if (dalgaText)
        {
            dalgaText.text = $"Dalga: {hangiDalga + 1}/{toplamDalgaSayisi}";
        }
    }

    private void KontrolKaybettinMi()
    {
        if (oyunBitti)
        {
            return;
        }

        if (OyuncununCani <= 0)
        {
            oyunBitti = true;
            Debug.Log("Oyun Bitti! Kaybettiniz.");
            if (kaybettinPanel) kaybettinPanel.SetActive(true);
            Time.timeScale = 0f;

            SavunmaGunlugu.Kaydet("OYUN SONUCU-KAYBETTİNİZ. Oyuncu canı 0'a düştü.");
        }
    }

    public void KontrolKazandinMi()
    {
        if (oyunBitti) return;

        if (dalgalarTamamlandi && sahnedekiDusmanSayisi <= 0)
        {
            oyunBitti = true;
            Debug.Log($"Tüm dalgalar temizlendi.Oyun Kazandın! (Kalan Can: {OyuncununCani},Toplam Para:{OyuncununParasi})");
            if (kazandinPanel) kazandinPanel.SetActive(true);
            Time.timeScale = 0f;

            SavunmaGunlugu.Kaydet(
            $"OYUN SONUCU - KAZANDINIZ. Kalan Can: {OyuncununCani}, Toplam Para: {OyuncununParasi}");
        }
    }

    #region UI Buton Fonksiyonları
    public void YenidenBaslat()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AnaMenuyeDon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    #endregion
}
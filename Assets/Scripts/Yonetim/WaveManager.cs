using System.Collections;
using UnityEngine;

[System.Serializable] // inspectorde görünmesi için serializable 
public class Dalga
{
    public string ad;

    [Header("Standart Düşman (Eşkiya)")]
    public Dusman standartDusmanPrefab;
    public int standartAdet;

    [Header("Zırhlı Düşman (Lejyoner)")]
    public Dusman zirhliDusmanPrefab;
    public int zirhliAdet;

    [Header("Uçan Düşman (Şahin)")]
    public Dusman ucanDusmanPrefab;
    public int ucanAdet;

    [Header("Genel Ayarlar")]
    public float dogmaAraligi = 0.7f;
}

public class WaveManager : MonoBehaviour
{
    [Header("Dalga Ayarları")]
    [SerializeField] private Dalga[] dalgalar;

    [Header("Sahne Referansları")]
    [SerializeField] private Transform spawnPoint;   // SpawnPoint objesi
    [SerializeField] private WaypointPath yol;       // Yol objesi

    private void Start()
    {
        if (dalgalar == null || dalgalar.Length == 0)
        {
            Debug.LogError("[WaveManager] dalgalar boş.Inspector’da en az 1 dalga ekle.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("[WaveManager] 'spawnPoint' atanmadı.");
            return;
        }

        if (yol == null)
        {
            Debug.LogError("[WaveManager] 'yol' (WaypointPath) atanmadı.");
            return;
        }

        StartCoroutine(DalgaRutini());
    }

    private IEnumerator DalgaRutini()
    {
        for (int dalgaIndex = 0; dalgaIndex < dalgalar.Length; dalgaIndex++)
        {
            Dalga dalga = dalgalar[dalgaIndex];

            // GameManager'a bu dalganın başladığını bildirir
            GameManager.Instance.DalgaBasladi(dalgaIndex);

            Debug.Log($"[WaveManager] '{dalga.ad}' dalgası başlıyor.");

            SavunmaGunlugu.Kaydet($"Dalga başladı: - Dalga: {dalgaIndex + 1}, Ad: {dalga.ad}");

            // 1) Standart
            for (int i = 0; i < dalga.standartAdet; i++)
            {
                SpawnDusman(dalga.standartDusmanPrefab);
                yield return new WaitForSeconds(dalga.dogmaAraligi);
            }

            // 2) Zırhlı
            for (int i = 0; i < dalga.zirhliAdet; i++)
            {
                SpawnDusman(dalga.zirhliDusmanPrefab);
                yield return new WaitForSeconds(dalga.dogmaAraligi);
            }

            // 3) Uçan
            for (int i = 0; i < dalga.ucanAdet; i++)
            {
                SpawnDusman(dalga.ucanDusmanPrefab);
                yield return new WaitForSeconds(dalga.dogmaAraligi);
            }

            // Dalga bitti, az mola
            yield return new WaitForSeconds(2f);
        }

        Debug.Log("[WaveManager] Tüm dalgalar tamamlandı.");
    }

    private void SpawnDusman(Dusman prefab)
    {
        if (prefab == null) return; // O tip bu dalgada kullanılmıyorsa

        Dusman yeni = Instantiate(prefab, spawnPoint.position, Quaternion.identity);

        if (yeni == null)
        {
            Debug.LogError("[WaveManager] Instantiate sonrası dusman null döndü.");
            return;
        }

        yeni.Baslat(yol);

        GameManager.Instance.DusmanSpawnlandi();

        // LOG: düşman sahneye geldi
        SavunmaGunlugu.Kaydet($"DÜŞMAN DOĞDU - Prefab: {prefab.name}, Konum: {spawnPoint.position}");
    }
}
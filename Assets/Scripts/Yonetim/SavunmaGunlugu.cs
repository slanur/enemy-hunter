using System.IO;
using UnityEngine;

public static class SavunmaGunlugu
{
    private static string dosyaYolu;

    //sinif ilk kez kullanıldığında çalışır
    static SavunmaGunlugu()
    {
        //dosya konumu
        dosyaYolu = Path.Combine(Application.persistentDataPath, "SavunmaGunlugu.txt");

        //eni oyun başladığında dosyayı baştan yazar
        try
        {
            File.WriteAllText(dosyaYolu,
                "=== SAVUNMA GÜNLÜĞÜ BAŞLADI ===\n" +
                $"Oyun Açılış Zamanı: {System.DateTime.Now:dd.MM.yyyy HH:mm:ss}\n\n");
        }
        catch (System.Exception e)
        {
            Debug.LogError("SavunmaGunlugu: Dosya oluşturulamadı: " + e.Message);
        }

        Debug.Log("SavunmaGunlugu dosya yolu: " + dosyaYolu);
    }

    public static void Kaydet(string mesaj)
    {
        string satir = $"[{System.DateTime.Now:HH:mm:ss}] {mesaj}";

        try
        {
            File.AppendAllText(dosyaYolu, satir + "\n");
        }
        catch (System.Exception e)
        {
            Debug.LogError("SavunmaGunlugu: Log yazılamadı: " + e.Message);
        }
    }
}
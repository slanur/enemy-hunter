using UnityEngine;
using TMPro;   // Para yazısını TextMeshPro ile göstereceksen lazım

public class ParaYoneticisi : MonoBehaviour
{
    // Diğer scriptlerden kolay erişmek için singleton
    public static ParaYoneticisi Instance { get; private set; }

    [Header("Başlangıç Ayarları")]
    [SerializeField] private int baslangicParasi = 100;

    [Header("UI Referansı")]
    [SerializeField] private TextMeshProUGUI paraMetni; // Canvas içindeki para yazısı

    public int MevcutPara
    {
        get;
        private set;
    }

    private void Awake()
    {
        // Singleton kurulumu
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // İstersek sahneler arası taşımak için:
    }

    private void Start()
    {
        MevcutPara = baslangicParasi;
        ParaMetniniGuncelle();
    }

    private void ParaMetniniGuncelle()
    {
        if (paraMetni != null)
        {
            paraMetni.text = MevcutPara.ToString();
        }

    }

    public bool YeterliParaVar(int miktar)
    {
        return MevcutPara >= miktar;
    }

    public bool ParaHarca(int miktar)
    {
        if (YeterliParaVar(miktar) == null)
        {
            return false;
        }

        MevcutPara -= miktar;
        ParaMetniniGuncelle();
        return true;
    }

    public void ParaEkle(int miktar)
    {
        if (miktar <= 0)
        {
            return;
        }

        MevcutPara += miktar;
        ParaMetniniGuncelle();
    }
}
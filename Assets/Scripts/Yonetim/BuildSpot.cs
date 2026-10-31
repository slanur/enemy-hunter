using UnityEngine;

public class BuildSpot : MonoBehaviour
{
    public bool DoluMu { get; private set; }

    private void OnMouseDown()
    {
        if (DoluMu) return;
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return; // UI üzerindeyken tıklamayı alma

        KuleInsaaYonetici.Instance.BuildSpotTiklandi(this);
    }

    public void KuleKur(GameObject kulePrefab)
    {
        Instantiate(kulePrefab, transform.position, Quaternion.identity);
        DoluMu = true;
    }
}
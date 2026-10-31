using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void AyarlaMaksimumCan(float max)
    {
        if (slider == null)
        {
            return;
        }
        slider.maxValue = max;
        slider.value = max;
    }

    public void Guncelle(float mevcut)
    {
        if (slider == null)
        {
            return;
        }
        slider.value = mevcut;
    }

    private void LateUpdate()
    {

        if (Camera.main)
            transform.forward = Camera.main.transform.forward;
    }
}
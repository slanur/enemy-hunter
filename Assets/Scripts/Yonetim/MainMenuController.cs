using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OyunuBaslat()
    {
        SceneManager.LoadScene("SampleScene"); // senin oyun sahnenin adı
    }

    public void OyundanCik()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
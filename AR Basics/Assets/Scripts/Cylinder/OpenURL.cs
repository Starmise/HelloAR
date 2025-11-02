using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public string url;

    public void AbrirEnlace(string url)
    {
        Application.OpenURL(url);
    }
}

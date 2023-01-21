using UnityEngine;

public class ScreenColor : MonoBehaviour
{
    public Color color;
    private Material material;
    public Color initialColor;

    void Start()
    {
        material = new Material(Shader.Find("Unlit/Color"));
        initialColor = color;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetColor("_Color", color);
        Graphics.Blit(source, destination, material);
    }

    public void ChangeColor(Color newColor)
    {
        color = newColor;
    }
}
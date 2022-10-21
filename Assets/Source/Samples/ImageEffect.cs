using UnityEngine;

public abstract class ImageEffect : MonoBehaviour
{
    [SerializeField] Shader shader;
    Material material;

    private void Start()
    {
        if (shader != null) material = new Material(shader);
    }

    protected abstract void ProcessMaterial(Material material);

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material != null)
        {
            ProcessMaterial(material);
            Graphics.Blit(source, destination, material);
        }
    }
}

using UnityEngine;

public class CloneBehaviour : MonoBehaviour
{
    void Start()
    {
        // Get all renderers in this object and its children
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer r in renderers)
        {
            // Create a unique material instance for this renderer
            Material mat = new Material(r.material);
            r.material = mat;

            // Set the color to green
            mat.SetColor("_BaseColor", Color.green);

            // Optional: remove any texture that could override color
            mat.SetTexture("_BaseMap", Texture2D.whiteTexture);
        }
    }


}

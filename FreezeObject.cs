using UnityEngine;
using System.Collections;

public class FreezeObject : MonoBehaviour
{
    readonly float timeToFreeze = 1.0f;
    readonly float freezeAmount = 1.0f;

    private Renderer objectRenderer;

    private void Awake()
    {
        if (GetComponent<Renderer>())
            objectRenderer = GetComponent<Renderer>();
        else
            objectRenderer = GetComponentInChildren<Renderer>();

        if (objectRenderer == null) return;

        AssignShader();
        Freeze();
    }

    public void AssignShader()
    {
        // Load the shader
        Shader iceShader = Shader.Find("Custom/IceShader");

        // Assign the shader to the material
        if (iceShader != null)
        {
            objectRenderer.material.shader = iceShader;
        }
        else
        {
            Debug.LogError("Ice shader not found. Please ensure it's included in the build.");
        }
    }

    public void Freeze()
    {
        StartCoroutine(FreezeOverTime(timeToFreeze));
    }

    private IEnumerator FreezeOverTime(float time)
    {
        float elapsed = 0;
        float currentFreezeAmount = objectRenderer.material.GetFloat("_FrozenAmount");

        // Get the initial values of the properties to be lerped
        float initialRefraction = objectRenderer.material.GetFloat("_Refraction");
        float initialMetallic = objectRenderer.material.GetFloat("_Metallic");
        float initialTranslucency = objectRenderer.material.GetFloat("_Translucency");
        Color initialSubsurfaceColor = objectRenderer.material.GetColor("_SubsurfaceColor");

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;

            // Perform lerping for each property
            float newRefraction = Mathf.Lerp(initialRefraction, 0.155f, t);
            float newMetallic = Mathf.Lerp(initialMetallic, 0.395f, t);
            float newTranslucency = Mathf.Lerp(initialTranslucency, 0.648f, t);
            Color newSubsurfaceColor = Color.Lerp(initialSubsurfaceColor, new Color(136 / 255f, 219 / 255f, 1f), t);

            // Update the material properties
            objectRenderer.material.SetFloat("_FrozenAmount", Mathf.Lerp(currentFreezeAmount, freezeAmount, t));
            objectRenderer.material.SetFloat("_Refraction", newRefraction);
            objectRenderer.material.SetFloat("_Metallic", newMetallic);
            objectRenderer.material.SetFloat("_Translucency", newTranslucency);
            objectRenderer.material.SetColor("_SubsurfaceColor", newSubsurfaceColor);

            yield return null;
        }

        // Set the final values of the properties
        objectRenderer.material.SetFloat("_FrozenAmount", freezeAmount);
        objectRenderer.material.SetFloat("_Refraction", 0.155f);
        objectRenderer.material.SetFloat("_Metallic", 0.395f);
        objectRenderer.material.SetFloat("_Translucency", 0.648f);
        objectRenderer.material.SetColor("_SubsurfaceColor", new Color(136 / 255f, 219 / 255f, 1f));
    }
}

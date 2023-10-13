using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        if (objectRenderer.materials.Length > 1)
        {
            // The objectRenderer has multiple materials
            Material[] materials = objectRenderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                if (i == 0)
                    materials[i].shader = iceShader;
                else
                    materials[i].shader = materials[0].shader;
            }
        }
        else
        {
            // The objectRenderer has a single material
            objectRenderer.material.shader = iceShader;
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

        // Create a list to store the initial values of the properties to be lerped for each material
        List<float> initialRefraction = new List<float>();
        List<float> initialMetallic = new List<float>();
        List<float> initialTranslucency = new List<float>();
        List<Color> initialSubsurfaceColor = new List<Color>();

        // Store the initial values of the properties for each material
        foreach (Material material in objectRenderer.materials)
        {
            initialRefraction.Add(material.GetFloat("_Refraction"));
            initialMetallic.Add(material.GetFloat("_Metallic"));
            initialTranslucency.Add(material.GetFloat("_Translucency"));
            initialSubsurfaceColor.Add(material.GetColor("_SubsurfaceColor"));
        }

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;

            // Perform lerping for each property and material
            for (int i = 0; i < objectRenderer.materials.Length; i++)
            {
                Material material = objectRenderer.materials[i];

                float newRefraction = Mathf.Lerp(initialRefraction[i], 0.155f, t);
                float newMetallic = Mathf.Lerp(initialMetallic[i], 0.395f, t);
                float newTranslucency = Mathf.Lerp(initialTranslucency[i], 0.648f, t);
                Color newSubsurfaceColor = Color.Lerp(initialSubsurfaceColor[i], new Color(136 / 255f, 219 / 255f, 1f), t);

                // Update the material properties for each material
                material.SetFloat("_FrozenAmount", Mathf.Lerp(currentFreezeAmount, freezeAmount, t));
                material.SetFloat("_Refraction", newRefraction);
                material.SetFloat("_Metallic", newMetallic);
                material.SetFloat("_Translucency", newTranslucency);
                material.SetColor("_SubsurfaceColor", newSubsurfaceColor);
            }

            yield return null;
        }

        // Set the final values of the properties for each material
        foreach (Material material in objectRenderer.materials)
        {
            material.SetFloat("_FrozenAmount", freezeAmount);
            material.SetFloat("_Refraction", 0.155f);
            material.SetFloat("_Metallic", 0.395f);
            material.SetFloat("_Translucency", 0.648f);
            material.SetColor("_SubsurfaceColor", new Color(136 / 255f, 219 / 255f, 1f));
        }
    }
}

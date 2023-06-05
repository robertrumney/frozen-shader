using UnityEngine;
using System.Collections;

public class FreezeObject : MonoBehaviour
{
    public float timeToFreeze = 2.0f;
    public float freezeAmount = 0.7f;
    private Renderer objectRenderer;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
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

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float newValue = Mathf.Lerp(currentFreezeAmount, freezeAmount, elapsed / time);
            objectRenderer.material.SetFloat("_FrozenAmount", newValue);
            yield return null;
        }

        objectRenderer.material.SetFloat("_FrozenAmount", freezeAmount);
    }
}

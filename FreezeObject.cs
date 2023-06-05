using UnityEngine;
using System.Collections;

public class FreezeObject : MonoBehaviour 
{
    public float timeToFreeze = 2.0f;
    public float freezeAmount = 0.5f;
    private Renderer objectRenderer;

    private void Awake() 
    {
        objectRenderer = GetComponent<Renderer>();
        Freeze();
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

using UnityEngine;

public class IceShatter : MonoBehaviour
{
    public int numberOfChunks = 10;
    public GameObject chunkPrefab;

    public void Shatter()
    {
        for (int i = 0; i < numberOfChunks; i++)
        {
            GameObject chunk = Instantiate(chunkPrefab, transform.position, Quaternion.identity);
            chunk.transform.localScale = transform.localScale / numberOfChunks;
            Rigidbody rb = chunk.AddComponent<Rigidbody>();
            rb.AddExplosionForce(1000, transform.position, 1);
        }

        Destroy(gameObject);
    }
}

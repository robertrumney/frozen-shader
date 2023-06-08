using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class ShatterSkinnedMesh : MonoBehaviour
{
    public AudioClip shatterSound;
    public int numberOfPieces = 10;
    public float destroyDelay = 30f;

    void Start()
    {
        // Invoke shatter method for demonstration purposes.
        Shatter();
    }

    public void Shatter()
    {
        SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();

        // Bake mesh
        Mesh staticMesh = new Mesh();
        smr.BakeMesh(staticMesh);

        // Fracture mesh into pieces
        Vector3 size = staticMesh.bounds.size / Mathf.Sqrt(numberOfPieces);
        for (int i = 0; i < numberOfPieces; i++)
        {
            GameObject piece = new GameObject("Piece_" + i);
            Mesh pieceMesh = new Mesh();

            piece.transform.position = transform.position;
            piece.transform.rotation = transform.rotation;

            // This is a simplified method; it will create chunks rather than realistic fractured pieces
            pieceMesh.vertices = staticMesh.vertices;
            pieceMesh.triangles = staticMesh.triangles;
            pieceMesh.bounds = new Bounds(staticMesh.bounds.center, size);

            MeshFilter meshFilter = piece.AddComponent<MeshFilter>();
            meshFilter.mesh = pieceMesh;

            MeshRenderer meshRenderer = piece.AddComponent<MeshRenderer>();
            meshRenderer.materials = smr.materials;

            // Add Rigidbody
            piece.AddComponent<Rigidbody>();

            // Destroy after delay
            Destroy(piece, destroyDelay);
        }

        // Play shattering sound
        AudioSource.PlayClipAtPoint(shatterSound, transform.position);

        // Destroy the original object
        Destroy(gameObject);
    }
}

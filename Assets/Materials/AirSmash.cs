using UnityEngine;
using UnityEngine.Rendering.Universal;
public class AirSmash : MonoBehaviour
{
    [SerializeField]private Shader shader;
    private Material mat;
    private ParticleSystemRenderer particleSystemRenderer;
    private Mesh mesh;
    private ParticleSystem airSmashEffect;
    private Vector3[] vertices;
    private void Awake() 
    {
        airSmashEffect = GetComponent<ParticleSystem>();
    }
    private void Start()
    {
        particleSystemRenderer = airSmashEffect.GetComponent<ParticleSystemRenderer>();
        // Create a new mesh
        mesh = new Mesh();

        // Create an array of vertices for the mesh
        vertices = new Vector3[90000];
        
        float z = 0f;
        int counter = 0;
        for (float x = 300; x > 0f; x-= 1f)
        {
            for (float y = 300; y > 0f; y -= 1f)
            {
                float referenceX = transform.localPosition.x;
                float referenceY = transform.localPosition.y;
                z = Mathf.Pow((x-300),2) + Mathf.Pow((y-300),2) + 1;
                vertices[counter] = new Vector3(x,y,z);
                counter++;
            }
        }
       Debug.Log(counter);
        // Assign the vertices to the mesh
        mesh.vertices = vertices;

        // Create an array of triangles for the mesh
        int[] triangles = new int[90000];
        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] = i;
        }

        // Assign the triangles to the mesh
        mesh.triangles = triangles;

        mesh.name = "hemisphere";
        
        // Create a new mesh filter and set it to use the created mesh
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        
        particleSystemRenderer.mesh = mesh;
        // Create a new mesh renderer and set it to use the Universal Render Pipeline material
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        mat = new Material(shader);
        meshRenderer.material = mat;
        
        particleSystemRenderer.mesh = mesh;
        particleSystemRenderer.material = mat;

        var colorOverLifetime = airSmashEffect.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 0.5f), new GradientColorKey(new Color(1,1,1,0), 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        colorOverLifetime.color = new ParticleSystem.MinMaxGradient(gradient);
    }
}

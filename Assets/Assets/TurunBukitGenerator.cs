using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TurunBukitGenerator : MonoBehaviour
{
    public Vector2[] awalBukit;
    float maju = 0;
    float timer = 0;

    //Variable buat mesh
    int vert = 0;
    int sizex = 0;
    Mesh mesh;
    List<Vector3> verticeslist = new List<Vector3>();
    List<int> triangles = new List<int>();

    //collider
    EdgeCollider2D edge;
    List<Vector2> colliderLanjutan = new List<Vector2>();

    //fungsi
    public int simpangan = 10;
    public float kerapatan = 0.3f;
    float trigonometricTimer;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        edge = GetComponent<EdgeCollider2D>();
        awalBukit = edge.points;
        sizex = awalBukit.Length;
        for (int i = 0; i < awalBukit.Length; i++)
        {
            colliderLanjutan.Add(awalBukit[i]);
        }
        BuatMeshAwal();
        
    }
    

    private void Update() {
       

        //generator mesh dan vertext
        timer += Time.deltaTime;
            trigonometricTimer += Time.deltaTime;
            float y = simpangan * Mathf.Cos(kerapatan * trigonometricTimer) - simpangan;
            maju += Time.deltaTime;

            if(y < 2 * -simpangan + .11111f){
                trigonometricTimer = 0;
                awalBukit[sizex - 1] = new Vector2(awalBukit[sizex - 1].x, awalBukit[sizex - 1].y + y);
            }
            //buat vertex
            verticeslist.Add(new Vector3(awalBukit[sizex - 1].x + maju,awalBukit[sizex - 1].y + y));
            verticeslist.Add(new Vector3(awalBukit[sizex - 1].x + maju,awalBukit[sizex - 1].y + y - 10));

            //buat trianle
            triangles.Add(vert + 0);
            triangles.Add(vert + 1);
            triangles.Add(vert + 3);
            triangles.Add(vert + 0);
            triangles.Add(vert + 3);
            triangles.Add(vert + 2);

            //buat collider
            colliderLanjutan.Add(new Vector2(awalBukit[sizex - 1].x + maju,awalBukit[sizex - 1].y + y));
            edge.points = colliderLanjutan.ToArray();

            vert += 2;
            timer = 0;
        mesh.vertices = verticeslist.ToArray();
        mesh.triangles = triangles.ToArray();
    }

    private void OnDrawGizmos() {
        Vector3[] vertices = verticeslist.ToArray();
        for (int i = 0; i < vertices.Length;i++)
        {
            Gizmos.DrawSphere(vertices[i],.1f);
        }
    }

    void BuatMeshAwal()
    {
        //buat vertex atau titik titik
        for(int i = 0; i < sizex; i++)
        {
            verticeslist.Add(awalBukit[i]);
            verticeslist.Add(new Vector3(awalBukit[i].x, awalBukit[i].y - 10));
        }

        //buat mesh
        for(int i = 0; i < sizex - 1; i++)
        {
            triangles.Add(vert + 0);
            triangles.Add(vert + 1);
            triangles.Add(vert + 3);
            triangles.Add(vert + 0);
            triangles.Add(vert + 3);
            triangles.Add(vert + 2);

            vert += 2;
        }
    }
    
}

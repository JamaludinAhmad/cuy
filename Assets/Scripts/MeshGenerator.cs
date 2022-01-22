using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    [SerializeField]
    List<Vector3> verticeslist = new List<Vector3>();
    [SerializeField]
    Vector3[] vertices;
    int tris = 0;
    int vert = 0;
    [SerializeField]
    int[] triangle;
    float timer = 0;
            int perkalian = 2;
    int x = 0;
    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        triangle = new int[12 * 3];
    }
    private void Update() {
        
        timer += Time.deltaTime;
        

        if(timer >= 1f)
        {
            Debug.Log("buat vertex");
            //buat kotak
            x += 1;
            verticeslist.Add(new Vector3(x,0));
            verticeslist.Add(new Vector3(x,1));
            vertices = verticeslist.ToArray();
            //buat segitiga
            if(vertices.Length == 2 * perkalian){
                triangle[tris + 0] = vert + 0;
                triangle[tris + 1] = vert + 1;
                triangle[tris + 2] = vert + 2;
                triangle[tris + 3] = vert + 1;
                triangle[tris + 4] = vert + 3;
                triangle[tris + 5] = vert + 2;

                perkalian ++;
                vert += 2;
                tris += 6;
            }

            mesh.vertices = vertices;
            mesh.triangles = triangle;
            timer = 0;
            

        }
    }

    private void OnDrawGizmos() {
        if (vertices == null)
            return;

        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }

        
}

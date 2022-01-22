using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TanahGenerator : MonoBehaviour
{   float timerSpawn;
    float timerTrigonometric;
    float maju;
    public float simpangan = 1;
    public float kerapatan = 1;
    List<Vector2> titik_titik = new List<Vector2>();
    EdgeCollider2D edge;
    Vector2[] startHill;
    Mesh mesh;

    private void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        edge = GetComponent<EdgeCollider2D>();
        startHill = edge.points;
        for(int i = startHill.Length - 1; i >= 0; i--)
        {
            titik_titik.Add(startHill[i]);
            edge.points = titik_titik.ToArray();
        }

        CreateShape();
        
    }

    void CreateShape()
    {
        
    }
    
    private void FixedUpdate() {
        timerTrigonometric += Time.deltaTime;
        float y = simpangan * Mathf.Cos(kerapatan * timerTrigonometric) - simpangan;
        Debug.Log(y);
 
        if(y < 2 * -simpangan + .11111)
        {
            timerTrigonometric = 0;
            startHill[0] = new Vector2(startHill[0].x, startHill[0].y + y);
            
        }
        timerSpawn += Time.deltaTime;
        maju += Time.deltaTime;
        if(timerSpawn >= 0.2f){
            titik_titik.Add(new Vector2(startHill[0].x + maju, y + startHill[0].y));
            timerSpawn = 0;
        }
        
        edge.points = titik_titik.ToArray();
    }
}

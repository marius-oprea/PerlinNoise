using UnityEngine;
using System.Collections;

public class VoxelPerlinNoise : MonoBehaviour 
{
    public int size;
    public GameObject cube;
    public float scale;
    public float height;

    public bool is3D;
    public bool isAnimation;
    public bool isRounded;

    Orbit orbit;

    private float originalScale;
    private Vector3 position;
    private Transform[] t;
    private Transform child;
    private float h;
    private Vector3 childPosition;
    private int i;
    private int len;
    private float childHeight;

    public Color color1;
    public Color color2;

	private void Start () 
    {
        CreateEmptyGrid();
        AddPerlinNoise();
    }

    private void CreateEmptyGrid()
    {
        GameObject c;
        Vector3 pos;
        int x;
        int z;

        for (x = 0; x < size; x++)
        {
            for (z = 0; z < size; z++)
            {
                pos = new Vector3(x + transform.position.x, transform.position.y,  z + transform.position.z);
                c = Instantiate(cube, pos, Quaternion.identity) as GameObject;
                c.transform.parent = transform;

                if (x == 24 && z == 24)
                    c.name = "Pivot";
            }
        }

        t = transform.GetComponentsInChildren<Transform>();
        len = t.Length;
    }

    private void AddPerlinNoise()
    {        
        for (i = 0; i < len; i++ )
        {
            child = t[i];
            childPosition = child.transform.position;
            h = Mathf.PerlinNoise(childPosition.x / scale, childPosition.z / scale);

            child.GetComponent<MeshRenderer>().material.color = new Color(color1.r * h + color2.r * (1 - h), color1.g * h + color2.g * (1 - h), color1.b * h + color2.b * (1 - h), color1.a * h + color2.a * ( 1 - h));

            position = childPosition;

            if (is3D)
            {
                // set height = 2 and scale = 0.2 for a nice wave effect
                if (isAnimation)
                {
                    if (isRounded)
                        childHeight = Mathf.RoundToInt(height * Mathf.PerlinNoise(Time.time + (childPosition.x * scale), Time.time + (childPosition.z * scale)));
                    else
                        childHeight = height * Mathf.PerlinNoise(Time.time + (childPosition.x * scale), Time.time + (childPosition.z * scale));
                }
                //  set height = 7 and scale = 10 for a nice hill effect
                else
                {
                    if (isRounded)
                        childHeight = Mathf.RoundToInt(h * height);
                    else
                        childHeight = h * height;
                }
            }
            else
            {
                childHeight = 0.0f;
            }
            
            position.y = childHeight;
            child.transform.position = position;            
        }
	}
	
	private void Update () 
    {
        if (isAnimation || is3D || isRounded)
            AddPerlinNoise();
	}
}
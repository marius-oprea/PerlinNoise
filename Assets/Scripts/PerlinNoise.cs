using UnityEngine;
using System.Collections;

public class PerlinNoise : MonoBehaviour
{
    public int pixWidth;
    public int pixHeight;
    public float xOrg;
    public float yOrg;
    public float scale = 1.0F;
    private Texture2D noiseTex;
    private Color[] pix;
    private Renderer rend;

    public float heightScale = 1.0F;
    public float xScale = 1.0F;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        noiseTex = new Texture2D(pixWidth, pixHeight);
        pix = new Color[noiseTex.width * noiseTex.height];
        rend.material.mainTexture = noiseTex;
    }

    private void CalcNoise()
    {
        float y = 0.0F;

        while (y < noiseTex.height)
        {
            float x = 0.0F;

            while (x < noiseTex.width)
            {
                float xCoord = xOrg + x / noiseTex.width * scale;
                float yCoord = yOrg + y / noiseTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                float ntw = noiseTex.width;
                int index = Mathf.FloorToInt(y * ntw + x);
                pix[index] = new Color(sample, sample, sample);
                x++;
            }
            y++;
        }
        noiseTex.SetPixels(pix);
        noiseTex.Apply();
    }

    private void Update()
    {
        CalcNoise();
    }
}
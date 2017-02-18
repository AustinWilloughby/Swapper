using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int width = 5;
    public int height = 5;

    public GameObject dotPrefab;

    private Camera gameCam;
    private Vector2[,] positions;
    private GameObject[,] objects;

    // Use this for initialization
    void Start()
    {
        gameCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        CreateEmptyGrid();
        PopulateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateEmptyGrid()
    {
        Vector2 camVec = gameCam.gameObject.transform.position;
        Vector2 startVec = new Vector2(camVec.x - gameCam.orthographicSize, camVec.y - gameCam.orthographicSize);

        float cellScale;
        if (height > width)
        {
            cellScale = (gameCam.orthographicSize * 2) / height;
        }
        else
        {
            cellScale = (gameCam.orthographicSize * 2) / width;
        }

        positions = new Vector2[width, height];
        objects = new GameObject[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                positions[x, y] = new Vector2(startVec.x + (cellScale * x), startVec.y + (cellScale * y));
            }
        }
    }

    private void PopulateGrid()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                GameObject newPref = Instantiate(dotPrefab);
                newPref.transform.position = positions[x, y];
                newPref.transform.position += newPref.GetComponent<SpriteRenderer>().bounds.size / 2;
                objects[x, y] = newPref;
            }
        }
    }
}

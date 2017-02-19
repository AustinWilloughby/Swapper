using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public Vector2 axisOfDivision;

    public GameObject dotPrefab;

    private Camera gameCam;
    private Vector2[,] positions;
    private GameObject[,] objects;

    // Use this for initialization
    void Start()
    {
        gameCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        positions = new Vector2[width, height];
        objects = new GameObject[width, height];

        Vector2 prefabHalfwidth = dotPrefab.GetComponent<BoxCollider2D>().size;
        Vector2 startingPos = new Vector2(
            gameCam.transform.position.x - gameCam.orthographicSize, 
            gameCam.transform.position.y - gameCam.orthographicSize);

        float distBetween = (gameCam.orthographicSize * 2.0f / (float)height);
        Vector2 colliderScales = new Vector2(10.0f / (float)height, 10.0f / (float)height);

        float xOffset = 5.0f / (float)width;
        float yOffset = 5.0f / (float)height;


        for (int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                positions[x, y] = new Vector2(
                    (startingPos.x + distBetween * x) + xOffset,
                    (startingPos.y + distBetween * y) + yOffset);
                GameObject newDot = Instantiate(dotPrefab);
                newDot.transform.position = positions[x, y];
                newDot.GetComponent<BoxCollider2D>().size = colliderScales;
                objects[x, y] = newDot;
            }
        }

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if (x >= axisOfDivision.x && y >= axisOfDivision.y)
                {
                    objects[x, y].GetComponent<DotData>().Setup(Color.green,
                                        GetDotDataAtLoc(x, y + 1),
                                        GetDotDataAtLoc(x, y - 1),
                                        GetDotDataAtLoc(x + 1, y),
                                        GetDotDataAtLoc(x - 1, y),
                                        x, y);
                }
                else
                {
                    objects[x, y].GetComponent<DotData>().Setup(Color.red,
                        GetDotDataAtLoc(x, y + 1),
                        GetDotDataAtLoc(x, y - 1),
                        GetDotDataAtLoc(x + 1, y),
                        GetDotDataAtLoc(x - 1, y),
                        x, y);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public DotData GetDotDataAtLoc(int xLoc, int yLoc)
    {
        if(xLoc >= width || xLoc < 0 || yLoc >= height || yLoc < 0)
        {
            return null;
        }
        return objects[xLoc, yLoc].GetComponent<DotData>();
    }

    public Vector2 GetPositionAtLoc(int xLoc, int yLoc)
    {
        return positions[xLoc, yLoc];
    }

    public void EvaluateGrid()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    private int[,] levelMap;
    private int x;
    private int y;
    private int maxMoves;
    private Vector2 division;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(int xSize, int ySize, int[,] map, int moves, Vector2 div)
    {
        levelMap = map;
        x = xSize;
        y = ySize;
        maxMoves = moves;
        division = div;
    }

    public int[,] GetMap
    {
        get { return levelMap; }
    }

    public int GetXSize
    {
        get { return x; }
    }

    public int GetYSize
    {
        get { return y; }
    }

    public int MaxMoves
    {
        get { return maxMoves; }
    }

    public Vector2 GetDivision
    {
        get { return division; }
    }

}

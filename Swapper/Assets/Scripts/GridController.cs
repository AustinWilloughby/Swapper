using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public Vector2 axisOfDivision;
    public Color color1;
    public Color color2;

    public GameObject dotPrefab;

    private Camera gameCam;
    private Vector2[,] positions;
    private GameObject[,] objects;
    private Color[,] colors;

    private GameObject leftDiv;
    private GameObject rightDiv;

    private GameObject screenBlock;

    private bool checkThis = false;
    private bool gameOver = false;
    private bool gameWon = false;
    private float alphaValue = 0;

    private int clicksLeft = 3;

    private LevelCollection levels;

    public int ClicksLeft
    {
        get { return clicksLeft; }
        set { clicksLeft = value; }
    }

    // Use this for initialization
    void Start()
    {
        screenBlock = GameObject.Find("ScreenBlock");
        leftDiv = GameObject.Find("Left Divider");
        rightDiv = GameObject.Find("Right Divider");
        gameCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        levels = gameObject.GetComponent<LevelCollection>();
        levels.FillArray();

        LoadLevel(levels.GetLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            alphaValue += Time.deltaTime;
            SpriteRenderer blockRend = screenBlock.GetComponent<SpriteRenderer>();
            Color blockCol = blockRend.color;
            blockCol.a = alphaValue;
            blockRend.color = blockCol;

            TextMesh txtMesh = GameObject.Find("GameOverText").GetComponent<TextMesh>();
            Color meshCol = txtMesh.color;
            meshCol.a = alphaValue;
            txtMesh.color = meshCol;

            if (alphaValue >= 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ClearGrid();
                    LoadLevel(levels.GetLevel());

                    gameOver = false;
                }
            }
        }
        else
        {
            if(alphaValue >= 0)
            {
                alphaValue -= Time.deltaTime;
                SpriteRenderer blockRend = screenBlock.GetComponent<SpriteRenderer>();
                Color blockCol = blockRend.color;
                blockCol.a = alphaValue;
                blockRend.color = blockCol;

                TextMesh txtMesh = GameObject.Find("GameOverText").GetComponent<TextMesh>();
                Color meshCol = txtMesh.color;
                meshCol.a = alphaValue;
                txtMesh.color = meshCol;
                if(alphaValue <= 0)
                {
                    alphaValue = 0;
                }
            }
        }
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
        if (checkThis)
        {
            bool isComplete = true;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (objects[x, y].GetComponent<DotData>().Color != colors[x, y])
                    {
                        isComplete = false;
                    }
                }
            }

            if (!isComplete)
            {
                clicksLeft--;
                if(clicksLeft == 0)
                {
                    GameOver(false);
                }
            }
            else
            {
                clicksLeft--;
                GameOver(true);
            }
        }
        checkThis = !checkThis;
    }

    public void GameOver(bool win)
    {
        gameWon = win;
        gameOver = true;
        alphaValue = 0.0f;
        if(levels.LevelsRemaining <= 0)
        {
            levels.FillArray();
        }

        TextMesh txtMesh = GameObject.Find("GameOverText").GetComponent<TextMesh>();
        if (win)
        {
            txtMesh.text = "You Win!\nClick To Play Again!";
        }
        else
        {
            txtMesh.text = "You Ran Out Of Moves!\nYou Lose!\nClick To Play Again!";
        }
    }

    public void LoadLevel(LevelContainer level)
    {
        width = level.GetXSize;
        height = level.GetYSize;
        clicksLeft = level.MaxMoves;

        positions = new Vector2[width, height];
        objects = new GameObject[width, height];
        colors = new Color[width, height];

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
            for (int x = 0; x < width; x++)
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

        leftDiv.GetComponent<SpriteRenderer>().color = color1;
        rightDiv.GetComponent<SpriteRenderer>().color = color2;


        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (level.GetMap[height - y - 1 , x] == 2)
                {
                    objects[x, y].GetComponent<DotData>().Setup(color2,
                                        GetDotDataAtLoc(x, y + 1),
                                        GetDotDataAtLoc(x, y - 1),
                                        GetDotDataAtLoc(x + 1, y),
                                        GetDotDataAtLoc(x - 1, y),
                                        x, y);
                }
                else
                {
                    objects[x, y].GetComponent<DotData>().Setup(color1,
                        GetDotDataAtLoc(x, y + 1),
                        GetDotDataAtLoc(x, y - 1),
                        GetDotDataAtLoc(x + 1, y),
                        GetDotDataAtLoc(x - 1, y),
                        x, y);
                }
            }
        }

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if (x >= axisOfDivision.x && y >= axisOfDivision.y)
                {
                    colors[x, y] = color2;
                }
                else
                {
                    colors[x, y] = color1;
                }
            }
        }
    }

    private void ClearGrid()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                Destroy(objects[x, y]);
            }
        }
    }
}

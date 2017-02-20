using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotData : MonoBehaviour
{
    public DotData upperNeighbor;
    public DotData rightNeighbor;
    public DotData leftNeighbor;
    public DotData lowerNeighbor;

    private Color color;
    private Color nextColor;
    private int xLoc;
    private int yLoc;

    private Vector3 dotScale;
    public float dotScaleMod;
    private BoxCollider2D col;
    private Vector2 colScale;

    private bool sliding = false;
    private Vector2 slideDestination;
    private Vector2 slideDirection;
    public float slideSpeed = 1;
    public float marginOfSlideError = .04f;

    private ClickHandler cHandler;

    public Vector2 GridPos
    {
        get { return new Vector2(xLoc, yLoc);}
    }


    public Color Color
    {
        get { return color; }
    }
    public Color NextColor
    {
        get { return nextColor; }
    }

    public bool IsSliding
    {
        get { return sliding; }
    }

    // Use this for initialization
    void Start()
    {
        dotScale = transform.localScale;
        col = gameObject.GetComponent<BoxCollider2D>();
        colScale = col.size;
        cHandler = GameObject.Find("ClickHandler").GetComponent<ClickHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sliding)
        {
            transform.position += (Vector3)slideDirection * slideSpeed * Time.deltaTime;
            if(Vector2.Distance(transform.position, slideDestination) <= marginOfSlideError)
            {
                transform.position = GameObject.Find("GameManager").GetComponent<GridController>().GetPositionAtLoc(xLoc, yLoc);
                sliding = false;
                color = nextColor;
                gameObject.GetComponent<SpriteRenderer>().color = color;

                GridController gridCont = GameObject.Find("GameManager").GetComponent<GridController>();
                gridCont.EvaluateGrid();
            }
        }

        if(transform.localScale != dotScale * dotScaleMod)
        {
            if (Vector3.Magnitude(transform.localScale) < Vector3.Magnitude(dotScale * dotScaleMod))
            {
                transform.localScale += new Vector3(dotScaleMod, dotScaleMod, dotScaleMod) * Time.deltaTime * dotScaleMod;
                col.size = new Vector2(colScale.x / transform.localScale.x, colScale.y / transform.localScale.y);
                if (Vector3.Magnitude(transform.localScale) > Vector3.Magnitude(dotScale * dotScaleMod))
                {
                    transform.localScale = dotScale * dotScaleMod;
                }
            }

            if (Vector3.Magnitude(transform.localScale) > Vector3.Magnitude(dotScale * dotScaleMod))
            {
                transform.localScale -= new Vector3(dotScaleMod, dotScaleMod, dotScaleMod) * Time.deltaTime * dotScaleMod * 2;
                col.size = new Vector2(colScale.x / transform.localScale.x, colScale.y / transform.localScale.y);
                if (Vector3.Magnitude(transform.localScale) < Vector3.Magnitude(dotScale * dotScaleMod))
                {
                    transform.localScale = dotScale * dotScaleMod;
                }
            }
        }
    }

    void OnMouseDown()
    {
        cHandler.ResolveClick(this);
    }

    public void Setup(Color col, DotData upper, DotData lower, DotData right, DotData left, int x, int y)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = col;
        color = col;
        upperNeighbor = upper;
        lowerNeighbor = lower;
        rightNeighbor = right;
        leftNeighbor = left;
        xLoc = x;
        yLoc = y;
    }

    public bool Slide(Directions direc)
    {
        sliding = false;
        switch (direc)
        {
            case Directions.Up:
                if (upperNeighbor)
                {
                    slideDestination = upperNeighbor.gameObject.transform.position;
                    nextColor = upperNeighbor.Color;
                    sliding = true;
                }
                break;
            case Directions.Down:
                if (lowerNeighbor)
                {
                    slideDestination = lowerNeighbor.gameObject.transform.position;
                    nextColor = lowerNeighbor.Color;
                    sliding = true;
                }
                break;
            case Directions.Left:
                if (leftNeighbor)
                {
                    slideDestination = leftNeighbor.gameObject.transform.position;
                    nextColor = leftNeighbor.Color;
                    sliding = true;
                }
                break;
            case Directions.Right:
                if (rightNeighbor)
                {
                    slideDestination = rightNeighbor.gameObject.transform.position;
                    nextColor = rightNeighbor.Color;
                    sliding = true;
                }
                break;
            default: break;
        }
        slideDirection = slideDestination - (Vector2)transform.position;
        slideDirection.Normalize();
        return sliding;
    }


    public Directions IsNeighbor(DotData other)
    {
        if(other == upperNeighbor)
        {
            return Directions.Up;
        }
        else if(other == lowerNeighbor)
        {
            return Directions.Down;
        }
        else if(other == leftNeighbor)
        {
            return Directions.Left;
        }
        else if(other == rightNeighbor)
        {
            return Directions.Right;
        }
        else
        {
            return Directions.NaN;
        }
    }


}

public enum Directions
{
    Up,
    Down,
    Left,
    Right,
    NaN
};

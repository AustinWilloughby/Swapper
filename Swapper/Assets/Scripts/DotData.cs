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
    private int xLoc;
    private int yLoc;

    private bool sliding = false;
    private Vector2 slideDestination;
    private Vector2 slideDirection;
    public float slideSpeed = 1;
    public float marginOfSlideError = .04f;

    private ClickHandler cHandler;

    // Use this for initialization
    void Start()
    {
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
                transform.position = slideDestination;
                sliding = false;
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
                    sliding = true;
                }
                break;
            case Directions.Down:
                if (lowerNeighbor)
                {
                    slideDestination = lowerNeighbor.gameObject.transform.position;
                    sliding = true;
                }
                break;
            case Directions.Left:
                if (leftNeighbor)
                {
                    slideDestination = leftNeighbor.gameObject.transform.position;
                    sliding = true;
                }
                break;
            case Directions.Right:
                if (rightNeighbor)
                {
                    slideDestination = rightNeighbor.gameObject.transform.position;
                    sliding = true;
                }
                break;
            default: break;
        }
        slideDirection = slideDestination - (Vector2)transform.position;
        slideDirection.Normalize();
        return sliding;
    }

    public void SwapNeighbors(Directions swapDirection, DotData otherSwap)
    {
        switch (swapDirection)
        {
            case Directions.Up:
                upperNeighbor = otherSwap.upperNeighbor;
                rightNeighbor = otherSwap.rightNeighbor;
                leftNeighbor = otherSwap.leftNeighbor;
                lowerNeighbor = otherSwap;

                otherSwap.upperNeighbor.lowerNeighbor = this;
                otherSwap.rightNeighbor.leftNeighbor = this;
                otherSwap.leftNeighbor.rightNeighbor = this;
                //otherSwap.upperNeighbor = this;
                break;
            case Directions.Down:
                lowerNeighbor = otherSwap.lowerNeighbor;
                rightNeighbor = otherSwap.rightNeighbor;
                leftNeighbor = otherSwap.leftNeighbor;
                upperNeighbor = otherSwap;

                otherSwap.lowerNeighbor.upperNeighbor = this;
                otherSwap.rightNeighbor.leftNeighbor = this;
                otherSwap.leftNeighbor.rightNeighbor = this;
                //otherSwap.lowerNeighbor = this;
                break;
            case Directions.Left:
                upperNeighbor = otherSwap.upperNeighbor;
                lowerNeighbor = otherSwap.lowerNeighbor;
                leftNeighbor = otherSwap.leftNeighbor;
                rightNeighbor = otherSwap;

                otherSwap.upperNeighbor.lowerNeighbor = this;
                otherSwap.lowerNeighbor.upperNeighbor = this;
                otherSwap.leftNeighbor.rightNeighbor = this;
                //otherSwap.upperNeighbor = this;
                break;
            case Directions.Right:
                upperNeighbor = otherSwap.upperNeighbor;
                rightNeighbor = otherSwap.rightNeighbor;
                lowerNeighbor = otherSwap.lowerNeighbor;
                leftNeighbor = otherSwap;

                otherSwap.upperNeighbor.lowerNeighbor = this;
                otherSwap.lowerNeighbor.upperNeighbor = this;
                otherSwap.rightNeighbor.leftNeighbor = this;
                //otherSwap.upperNeighbor = this;
                break;
            default: break;
        }
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

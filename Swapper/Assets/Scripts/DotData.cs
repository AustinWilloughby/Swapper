using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotData : MonoBehaviour
{
    private DotData upperNeighbor;
    private DotData rightNeighbor;
    private DotData leftNeighbor;
    private DotData lowerNeighbor;

    private Color color;
    private int xLoc;
    private int yLoc;

    private bool sliding = false;
    private Vector2 slideDestination;
    private Vector2 slideDirection;
    public float slideSpeed = 1;
    public float marginOfSlideError = .04f;

    // Use this for initialization
    void Start()
    {

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
        Slide(Directions.Up);
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

    private bool Slide(Directions direc)
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
}

enum Directions
{
    Up,
    Down,
    Left,
    Right
};

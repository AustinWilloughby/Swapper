using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private DotData prevClicked;
    private TextMesh movesLeft;

    private GridController gridCont;

    // Use this for initialization
    void Start()
    {
        movesLeft = GameObject.Find("MoveCounter").GetComponent<TextMesh>();
        gridCont = GameObject.Find("GameManager").GetComponent<GridController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movesLeft.text != "Moves Left: " + (gridCont.ClicksLeft))
        {
            movesLeft.text = "Moves Left: " + (gridCont.ClicksLeft);
        }
    }

    public void ResolveClick(DotData clicked)
    {
        if (prevClicked)
        {
            if (clicked != prevClicked)
            {
                clicked.gameObject.GetComponent<DotData>().dotScaleMod = 1.0f;
                if (clicked.upperNeighbor)
                {
                    clicked.upperNeighbor.dotScaleMod = 1.0f;
                }
                if (clicked.lowerNeighbor)
                {
                    clicked.lowerNeighbor.dotScaleMod = 1.0f;
                }
                if (clicked.leftNeighbor)
                {
                    clicked.leftNeighbor.dotScaleMod = 1.0f;
                }
                if (clicked.rightNeighbor)
                {
                    clicked.rightNeighbor.dotScaleMod = 1.0f;
                }

                prevClicked.gameObject.GetComponent<DotData>().dotScaleMod = 1.0f;
                if (prevClicked.upperNeighbor)
                {
                    prevClicked.upperNeighbor.dotScaleMod = 1.0f;
                }
                if (prevClicked.lowerNeighbor)
                {
                    prevClicked.lowerNeighbor.dotScaleMod = 1.0f;
                }
                if (prevClicked.leftNeighbor)
                {
                    prevClicked.leftNeighbor.dotScaleMod = 1.0f;
                }
                if (prevClicked.rightNeighbor)
                {
                    prevClicked.rightNeighbor.dotScaleMod = 1.0f;
                }

                switch (prevClicked.IsNeighbor(clicked))
                {
                    case Directions.Up:
                        prevClicked.Slide(Directions.Up);
                        clicked.Slide(Directions.Down);
                        prevClicked = null;
                        break;
                    case Directions.Down:
                        prevClicked.Slide(Directions.Down);
                        clicked.Slide(Directions.Up);
                        prevClicked = null;
                        break;
                    case Directions.Left:
                        prevClicked.Slide(Directions.Left);
                        clicked.Slide(Directions.Right);
                        prevClicked = null;
                        break;
                    case Directions.Right:
                        prevClicked.Slide(Directions.Right);
                        clicked.Slide(Directions.Left);
                        prevClicked = null;
                        break;
                    default:
                        prevClicked = clicked;
                        clicked.gameObject.GetComponent<DotData>().dotScaleMod = 2.0f;
                        if (clicked.upperNeighbor)
                        {
                            clicked.upperNeighbor.dotScaleMod = 1.5f;
                        }
                        if (clicked.lowerNeighbor)
                        {
                            clicked.lowerNeighbor.dotScaleMod = 1.5f;
                        }
                        if (clicked.leftNeighbor)
                        {
                            clicked.leftNeighbor.dotScaleMod = 1.5f;
                        }
                        if (clicked.rightNeighbor)
                        {
                            clicked.rightNeighbor.dotScaleMod = 1.5f;
                        }
                        break;
                }
            }
        }
        else
        {
            clicked.gameObject.GetComponent<DotData>().dotScaleMod = 2.0f;
            if (clicked.upperNeighbor)
            {
                clicked.upperNeighbor.dotScaleMod = 1.5f;
            }
            if (clicked.lowerNeighbor)
            {
                clicked.lowerNeighbor.dotScaleMod = 1.5f;
            }
            if (clicked.leftNeighbor)
            {
                clicked.leftNeighbor.dotScaleMod = 1.5f;
            }
            if (clicked.rightNeighbor)
            {
                clicked.rightNeighbor.dotScaleMod = 1.5f;
            }
            prevClicked = clicked;
        }
    }
}

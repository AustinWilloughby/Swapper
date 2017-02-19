using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private DotData prevClicked;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResolveClick(DotData clicked)
    {
        if (prevClicked)
        {
            if (clicked != prevClicked)
            {
                DotData swap = clicked;
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
                        break;
                }
            }
        }
        else
        {
            prevClicked = clicked;
        }
    }
}

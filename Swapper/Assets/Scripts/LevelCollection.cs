using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollection : MonoBehaviour
{
    private List<LevelContainer> levels;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FillArray()
    {
        AddLevel(4, 4, 8, new Vector2(2, 0),
            new int[,]
            { { 1, 2, 1, 2 },
              { 2, 1, 2, 1 },
              { 1, 2, 1, 2 },
              { 2, 1, 2, 1 }
            });
        AddLevel(4, 4, 7, new Vector2(2, 0),
            new int[,]
            { { 1, 1, 2, 1 },
              { 1, 2, 2, 2 },
              { 1, 1, 2, 1 },
              { 2, 1, 2, 2 }
            });
        AddLevel(4, 4, 7, new Vector2(2, 0),
            new int[,]
            { { 2, 1, 2, 2 },
              { 2, 1, 2, 1 },
              { 1, 1, 1, 2 },
              { 1, 1, 2, 2 }
            });
        AddLevel(4, 4, 11, new Vector2(2, 0),
            new int[,]
            { { 2, 1, 2, 1 },
              { 1, 2, 2, 2 },
              { 1, 2, 2, 1 },
              { 2, 1, 1, 1 }
            });
    }

    private void AddLevel(int x, int y, int lvlMoves, Vector2 division, int[,] lvlMap)
    {
        if (levels == null)
        {
            levels = new List<LevelContainer>();
        }

        LevelContainer newLevel = new LevelContainer();
        newLevel.Setup(x, y, lvlMap, lvlMoves, division);
        levels.Add(newLevel);
    }

    public int LevelsRemaining
    {
        get { return levels.Count; }
    }

    public LevelContainer GetLevel()
    {
        if (levels.Count > 0)
        {
            LevelContainer returnLvl = levels[Random.Range(0, levels.Count)];
            levels.Remove(returnLvl);
            return returnLvl;
        }
        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathnode
{
    private Grid<Pathnode> grid;
    private int x;
    private int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public Pathnode previousNode;

    public Pathnode(Grid<Pathnode> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return x + ", " + y;
    }
}

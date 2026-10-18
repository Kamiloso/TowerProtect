using System;
using System.Collections.Generic;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Worldgen;

public class MazeGenerator
{
    private const int CellSize = 5;
    private const int WallSize = 1;
    private const int Stride = CellSize + WallSize;

    private static readonly int[] Dx = [ 0, 1, 0, -1 ];
    private static readonly int[] Dy = [ -1, 0, 1, 0 ];

    private readonly int _gridWidth;
    private readonly int _gridHeight;
    private readonly Random _random;

    private readonly int _offsetX;
    private readonly int _offsetY;

    public MazeGenerator(int mapSize, Random random)
    {
        _gridWidth = (mapSize + WallSize) / Stride;
        _gridHeight = _gridWidth;
        _random = random;

        int mazePixelSize = _gridWidth * Stride - WallSize;
        _offsetX = (mapSize - mazePixelSize) / 2;
        _offsetY = (mapSize - mazePixelSize) / 2;
    }

    public void Generate(TileType[,] grid)
    {
        FillWithStone(grid);
        GeneratePaths(grid);
        ClearCenter(grid);
        CleanIsolatedPillars(grid);
    }

    private void FillWithStone(TileType[,] grid)
    {
        int size = grid.GetLength(0);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                grid[x, y] = TileType.Stone;
            }
        }
    }

    private void GeneratePaths(TileType[,] grid)
    {
        var visited = new bool[_gridWidth, _gridHeight];
        Stack<(int x, int y)> stack = new();

        int startX = 0;
        int startY = 0;

        visited[startX, startY] = true;
        stack.Push((startX, startY));
        CarveCell(grid, startX, startY);

        int[] unvisitedDirs = new int[4];

        while (stack.Count > 0)
        {
            var current = stack.Peek();
            int cx = current.x;
            int cy = current.y;

            int unvisitedCount = 0;

            for (int i = 0; i < 4; i++)
            {
                int nx = cx + Dx[i];
                int ny = cy + Dy[i];

                if (nx >= 0 && nx < _gridWidth && ny >= 0 && ny < _gridHeight && !visited[nx, ny])
                {
                    unvisitedDirs[unvisitedCount++] = i;
                }
            }

            if (unvisitedCount > 0)
            {
                int dir = unvisitedDirs[_random.Next(unvisitedCount)];
                int nx = cx + Dx[dir];
                int ny = cy + Dy[dir];

                CarveCell(grid, nx, ny);
                CarveWall(grid, cx, cy, dir);

                visited[nx, ny] = true;
                stack.Push((nx, ny));
            }
            else
            {
                stack.Pop();
            }
        }
    }

    private void CarveCell(TileType[,] grid, int cx, int cy)
    {
        int startX = _offsetX + cx * Stride;
        int startY = _offsetY + cy * Stride;

        for (int y = 0; y < CellSize; y++)
        {
            for (int x = 0; x < CellSize; x++)
            {
                grid[startX + x, startY + y] = TileType.Air;
            }
        }
    }

    private void CarveWall(TileType[,] grid, int cx, int cy, int dir)
    {
        int startX = _offsetX + cx * Stride;
        int startY = _offsetY + cy * Stride;

        if (dir == 0)
        {
            for (int x = 0; x < CellSize; x++) grid[startX + x, startY - WallSize] = TileType.Air;
        }
        else if (dir == 1)
        {
            for (int y = 0; y < CellSize; y++) grid[startX + CellSize, startY + y] = TileType.Air;
        }
        else if (dir == 2)
        {
            for (int x = 0; x < CellSize; x++) grid[startX + x, startY + CellSize] = TileType.Air;
        }
        else if (dir == 3)
        {
            for (int y = 0; y < CellSize; y++) grid[startX - WallSize, startY + y] = TileType.Air;
        }
    }

    private void ClearCenter(TileType[,] grid)
    {
        int startCellX = (_gridWidth / 2) - 1;
        int startCellY = (_gridHeight / 2) - 1;
        int endCellX = (_gridWidth / 2) + 1;
        int endCellY = (_gridHeight / 2) + 1;

        int tileStartX = _offsetX + startCellX * Stride;
        int tileStartY = _offsetY + startCellY * Stride;
        int tileEndX = _offsetX + endCellX * Stride + CellSize - 1;
        int tileEndY = _offsetY + endCellY * Stride + CellSize - 1;

        for (int y = tileStartY; y <= tileEndY; y++)
        {
            for (int x = tileStartX; x <= tileEndX; x++)
            {
                grid[x, y] = TileType.Air;
            }
        }
    }

    private void CleanIsolatedPillars(TileType[,] grid)
    {
        int size = grid.GetLength(0);

        for (int y = 1; y < size - 1; y++)
        {
            for (int x = 1; x < size - 1; x++)
            {
                if (grid[x, y] == TileType.Stone)
                {
                    bool hasStoneNeighbor =
                        grid[x - 1, y] == TileType.Stone ||
                        grid[x + 1, y] == TileType.Stone ||
                        grid[x, y - 1] == TileType.Stone ||
                        grid[x, y + 1] == TileType.Stone;

                    if (!hasStoneNeighbor)
                    {
                        grid[x, y] = TileType.Air;
                    }
                }
            }
        }
    }
}

public sealed class AutoGeneratedMap : Map
{
    public override int Range => 255;

    private readonly Random _random = new();

    protected override TileType[,] GenerateTiles()
    {
        var grid = new TileType[Size, Size];

        MazeGenerator mazeGenerator = new(Size, _random);
        mazeGenerator.Generate(grid);

        grid[Size / 2, Size / 2] = TileType.Water;

        return grid;
    }
}

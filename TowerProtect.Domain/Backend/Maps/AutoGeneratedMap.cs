using System;
using System.Collections.Generic;
using TowerProtect.Domain.Model.Tiles;

namespace TowerProtect.Domain.Backend.Maps;

// TODO: refactor this AI slop
// This class generates a maze using a randomized depth-first search algorithm
// To be honest how it works is a bit of a mystery to me, but it seems to work well enough for test purposes

public class MazeGenerator
{
    private const int CellSize = 5;
    private const int WallSize = 1;
    private const int Stride = CellSize + WallSize;

    private readonly int _gridWidth;
    private readonly int _gridHeight;
    private readonly Random _random;

    // Zmienne centrujące labirynt na mapach, gdzie wymiar nie dzieli się idealnie przez Stride
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
        bool[,] visited = new bool[_gridWidth, _gridHeight];
        Stack<(int x, int y)> stack = new Stack<(int, int)>();

        int startX = 0;
        int startY = 0;

        visited[startX, startY] = true;
        stack.Push((startX, startY));
        CarveCell(grid, startX, startY);

        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { -1, 0, 1, 0 };

        while (stack.Count > 0)
        {
            var current = stack.Peek();
            int cx = current.x;
            int cy = current.y;

            List<int> unvisitedDirs = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                int nx = cx + dx[i];
                int ny = cy + dy[i];

                if (nx >= 0 && nx < _gridWidth && ny >= 0 && ny < _gridHeight && !visited[nx, ny])
                {
                    unvisitedDirs.Add(i);
                }
            }

            if (unvisitedDirs.Count > 0)
            {
                int dir = unvisitedDirs[_random.Next(unvisitedDirs.Count)];
                int nx = cx + dx[dir];
                int ny = cy + dy[dir];

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

        if (dir == 0) // Up
        {
            for (int x = 0; x < CellSize; x++) grid[startX + x, startY - WallSize] = TileType.Air;
        }
        else if (dir == 1) // Right
        {
            for (int y = 0; y < CellSize; y++) grid[startX + CellSize, startY + y] = TileType.Air;
        }
        else if (dir == 2) // Down
        {
            for (int x = 0; x < CellSize; x++) grid[startX + x, startY + CellSize] = TileType.Air;
        }
        else if (dir == 3) // Left
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
    public override int Range => 256;

    private readonly Random _random = new Random();

    protected override TileType[,] GenerateTiles()
    {
        TileType[,] grid = new TileType[Size, Size];

        MazeGenerator mazeGenerator = new MazeGenerator(Size, _random);
        mazeGenerator.Generate(grid);

        GenerateBoundaries(grid);
        FillIsolatedHoles(grid);

        return grid;
    }

    private void GenerateBoundaries(TileType[,] grid)
    {
        for (int i = 0; i < Size; i++)
        {
            GrowBedrock(grid, i, 0, 0, 1);
            GrowBedrock(grid, i, Size - 1, 0, -1);
            GrowBedrock(grid, 0, i, 1, 0);
            GrowBedrock(grid, Size - 1, i, -1, 0);
        }
    }

    private void GrowBedrock(TileType[,] grid, int startX, int startY, int dx, int dy)
    {
        int cx = startX;
        int cy = startY;

        grid[cx, cy] = TileType.Bedrock;

        if (_random.NextDouble() > 0.7) return;
        cx += dx;
        cy += dy;
        grid[cx, cy] = TileType.Bedrock;

        if (_random.NextDouble() > 0.7) return;
        cx += dx;
        cy += dy;
        grid[cx, cy] = TileType.Bedrock;

        if (_random.NextDouble() > 0.7) return;
        cx += dx;
        cy += dy;
        grid[cx, cy] = TileType.Bedrock;
    }

    private void FillIsolatedHoles(TileType[,] grid)
    {
        bool[,] visited = new bool[Size, Size];

        FloodFill(grid, visited, Range, Range);

        for (int y = 0; y < Size; y++)
        {
            for (int x = 0; x < Size; x++)
            {
                if (grid[x, y] == TileType.Air && !visited[x, y])
                {
                    grid[x, y] = TileType.Bedrock;
                }
            }
        }
    }

    private void FloodFill(TileType[,] grid, bool[,] visited, int startX, int startY)
    {
        Queue<(int x, int y)> queue = new Queue<(int, int)>();

        queue.Enqueue((startX, startY));
        visited[startX, startY] = true;

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };

        while (queue.Count > 0)
        {
            var (cx, cy) = queue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nx = cx + dx[i];
                int ny = cy + dy[i];

                if (nx >= 0 && nx < Size && ny >= 0 && ny < Size)
                {
                    if (!visited[nx, ny] && grid[nx, ny] != TileType.Bedrock)
                    {
                        visited[nx, ny] = true;
                        queue.Enqueue((nx, ny));
                    }
                }
            }
        }
    }
}

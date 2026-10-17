using System;
using System.Text;

namespace TowerProtect.Domain.Backend.Maps.Specific;

public sealed class PlaceholderMap : Map
{
    public override int Range => 64;

    protected override string Template => GenerateMap();

    private string GenerateMap()
    {
        int size = Range * 2 + 1;
        char[,] grid = new char[size, size];

        // Fill with air
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                grid[x, y] = '.';
            }
        }

        // Set bedrock borders
        for (int i = 0; i < size; i++)
        {
            grid[i, 0] = 'B';
            grid[i, size - 1] = 'B';
            grid[0, i] = 'B';
            grid[size - 1, i] = 'B';
        }

        // Place spawner in the middle
        grid[Range, Range] = '!';

        void CreateLake(int cx, int cy, int radius)
        {
            for (int y = cy - radius; y <= cy + radius; y++)
            {
                for (int x = cx - radius; x <= cx + radius; x++)
                {
                    if (x <= 0 || x >= size - 1 || y <= 0 || y >= size - 1) continue;

                    if ((x - cx) * (x - cx) + (y - cy) * (y - cy) <= radius * radius)
                    {
                        grid[x, y] = '+';
                    }
                }
            }
        }

        void CreateWall(int sx, int sy, int len, bool horizontal, bool addCrusher)
        {
            for (int i = 0; i < len; i++)
            {
                int x = horizontal ? sx + i : sx;
                int y = horizontal ? sy : sy + i;

                if (x <= 0 || x >= size - 1 || y <= 0 || y >= size - 1) continue;

                // Add crushers in the middle to block the path
                if (addCrusher && (i == len / 2 || i == len / 2 - 1))
                {
                    grid[x, y] = 'O';
                }
                else
                {
                    grid[x, y] = '#';
                }
            }
        }

        // Generate lakes
        CreateLake(30, 30, 8);
        CreateLake(95, 100, 10);
        CreateLake(100, 35, 6);
        CreateLake(40, 90, 7);

        // Generate walls with blocked paths
        CreateWall(20, 20, 30, true, true);
        CreateWall(20, 20, 30, false, true);

        CreateWall(80, 20, 30, true, true);
        CreateWall(108, 20, 30, false, true);

        CreateWall(20, 80, 30, false, true);
        CreateWall(20, 108, 30, true, true);

        CreateWall(80, 108, 30, true, true);
        CreateWall(108, 80, 30, false, true);

        // Scatter decorative rocks
        Random rnd = new Random(12345);
        for (int i = 0; i < 400; i++)
        {
            int rx = rnd.Next(1, size - 1);
            int ry = rnd.Next(1, size - 1);

            if (grid[rx, ry] == '.')
            {
                grid[rx, ry] = '*';
            }
        }

        // Parse to template string format
        StringBuilder sb = new StringBuilder(size * size * 2);
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                sb.Append(grid[x, y]);
                if (x < size - 1) sb.Append(' ');
            }
            if (y < size - 1) sb.AppendLine();
        }

        return sb.ToString();
    }
}

using System.Collections.Generic;
using KomaruGolf.Tiles;
using Microsoft.Xna.Framework;

namespace KomaruGolf;

public static class LevelBuilder
{
    private static Dictionary<char, Tile> charToTile = new Dictionary<char, Tile>();
    
    public static List<Tile> Build(string[] level)
    {
        var finalList = new List<Tile>();

        float yPos = 0;
        for (int y = 0; y < level.Length; y++)
        {
            float xPos = 0;
            for (int x = 0; x < level[y].Length; x++)
            {
                switch (level[y][x])
                {
                    case '#':
                        var wTile = new Wall();
                        wTile.Load(new Vector2(xPos, yPos));

                        finalList.Add(wTile);
                        break;
                    case '.':
                        var gTile = new Ground();
                        gTile.Load(new Vector2(xPos, yPos));

                        finalList.Add(gTile);
                        break;
                    case ',':
                        var sTile = new Sand();
                        sTile.Load(new Vector2(xPos, yPos));

                        finalList.Add(sTile);
                        break;
                    case '@':
                        var fTile = new Finish();
                        fTile.Load(new Vector2(xPos, yPos));

                        finalList.Add(fTile);
                        break;
                    case '0':
                        var wtTile = new Water();
                        wtTile.Load(new Vector2(xPos, yPos));

                        finalList.Add(wtTile);
                        break;
                }

                xPos += Tile.TILE_SIZE;
            }
            yPos += Tile.TILE_SIZE;
        }

        return finalList;
    }
}
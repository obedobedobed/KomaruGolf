using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KomaruGolf.Tiles;

public static class TilesTextures
{
    public const int WALL_INDEX = 0;
    public const int GROUND_INDEX = 1;
    public const int SAND_INDEX = 2;
    public const int WATER_INDEX = 3;
    public const int FINISH_INDEX = 4;
    public const int STICKY_WALL_INDEX = 5;

    public static Dictionary<int, Texture2D> indexToTexture = new Dictionary<int, Texture2D>();

    public static void Load(ContentManager Content)
    {
        indexToTexture.Add(WALL_INDEX, Content.Load<Texture2D>("Sprites/Wall"));
        indexToTexture.Add(GROUND_INDEX, Content.Load<Texture2D>("Sprites/Ground"));
        indexToTexture.Add(SAND_INDEX, Content.Load<Texture2D>("Sprites/Sand"));
        indexToTexture.Add(WATER_INDEX, Content.Load<Texture2D>("Sprites/Water"));
        indexToTexture.Add(FINISH_INDEX, Content.Load<Texture2D>("Sprites/Finish"));
        indexToTexture.Add(STICKY_WALL_INDEX, Content.Load<Texture2D>("Sprites/StickyWall"));
    }
}
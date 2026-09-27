using Microsoft.Xna.Framework;

namespace KomaruGolf.Tiles;

public class Sand : Tile
{
    public override void Load(Vector2 pos)
    {
        Position = pos;
        Type = TileType.Ground;
        SpeedMultiplier = 0.95f;
        TilesTextures.indexToTexture.TryGetValue(TilesTextures.SAND_INDEX, out texture);
    }
}
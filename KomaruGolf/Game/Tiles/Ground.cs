using Microsoft.Xna.Framework;

namespace KomaruGolf.Tiles;

public class Ground : Tile
{
    public override void Load(Vector2 pos)
    {
        Position = pos;
        Type = TileType.Ground;
        SpeedMultiplier = 1f;
        TilesTextures.indexToTexture.TryGetValue(TilesTextures.GROUND_INDEX, out texture);
    }
}
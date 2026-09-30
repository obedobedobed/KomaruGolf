using Microsoft.Xna.Framework;

namespace KomaruGolf.Tiles;

public class Water : Tile
{
    public override void Load(Vector2 pos)
    {
        Position = pos;
        Type = TileType.Water;
        TilesTextures.indexToTexture.TryGetValue(TilesTextures.WATER_INDEX, out texture);
    }
}
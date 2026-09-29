using Microsoft.Xna.Framework;

namespace KomaruGolf.Tiles;

public class Wall : Tile
{
    public override void Load(Vector2 pos)
    {
        Position = pos;
        Type = TileType.Wall;
        TilesTextures.indexToTexture.TryGetValue(TilesTextures.WALL_INDEX, out texture);
    }
}
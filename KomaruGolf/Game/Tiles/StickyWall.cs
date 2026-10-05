using Microsoft.Xna.Framework;

namespace KomaruGolf.Tiles;

public class StickyWall : Tile
{
    public override void Load(Vector2 pos)
    {
        Position = pos;
        Type = TileType.Wall;
        Sticky = true;
        TilesTextures.indexToTexture.TryGetValue(TilesTextures.STICKY_WALL_INDEX, out texture);
    }
}
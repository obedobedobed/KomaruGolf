using Microsoft.Xna.Framework;

namespace KomaruGolf.Tiles;

public class Finish : Tile
{
    public override void Load(Vector2 pos)
    {
        Position = pos;
        Type = TileType.Ground;
        SpeedMultiplier = 1f;
        TilesTextures.indexToTexture.TryGetValue(TilesTextures.FINISH_INDEX, out texture);
        isFinish = true;
    }
}
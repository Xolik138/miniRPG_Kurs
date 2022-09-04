using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace miniRPG
{
    public class Tiles
    {
        protected Texture2D texture;
        public Vector2 Vector2 { get; protected set; }
        public static ContentManager Content { protected get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2, Color.White);
        }
        public class CollisionTiles : Tiles
        {
            public CollisionTiles(int i, Vector2 newVector2)
            {
                texture = Content.Load<Texture2D>("Day/Tile" + i);
                this.Vector2 = newVector2;
            }
        }
    }
}

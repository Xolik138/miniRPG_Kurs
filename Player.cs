using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace miniRPG
{
    public class Player
    {
        public Texture2D _texture;
        public Vector2 Position;
        public Rectangle PlayerRectangle;
        Vector2 origin;
        public int health;
        public bool direction = true;
        public static float Speed = 3f;

    
        public Player(Texture2D texture, Vector2 position,int newHealth)
        {
            _texture = texture;
            Position = position;
            health = newHealth;
        }
        public void LoadContent()
        {
        }
        public void Update()
        {
            PlayerRectangle = new Rectangle((int)Position.X - _texture.Width / 2, (int)Position.Y - _texture.Height / 2, _texture.Width, _texture.Height);
            if (Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Position.X += Speed;
                direction = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Position.X -= Speed;
                direction = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Position.Y -= Speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Position.Y += Speed;
            }
            if (Position.X < 0)
                Position.X = 0;
            if (Position.Y < 0)
                Position.Y = 0;
            if (Position.X > Game1.ScreenWidth - PlayerRectangle.Width)
                Position.X = Game1.ScreenWidth - PlayerRectangle.Width;
            if (Position.Y > Game1.ScreenHeight - PlayerRectangle.Height)
                Position.Y = Game1.ScreenHeight - PlayerRectangle.Height;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (health > 0)
            {
                if (direction == true)
                    spriteBatch.Draw(_texture, Position, null, Color.White, 0, origin, 1f, SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(_texture, Position, null, Color.White, 0, origin, 1f, SpriteEffects.FlipHorizontally, 0);
            }
        }
    }
}

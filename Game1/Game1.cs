using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Character character;
        System.TimeSpan now;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 920;
            graphics.PreferredBackBufferWidth = 680;
            Content.RootDirectory = "Content";
            now = new GameTime().TotalGameTime;
            
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            // TODO: Add your initialization logic here
            Texture2D[] tempidle = new Texture2D[8];
            Texture2D[] templeft = new Texture2D[8];
            Texture2D[] tempright = new Texture2D[8];

            for (int i = 0; i < tempidle.Length; i++)
            {
                tempidle[i] = this.Content.Load<Texture2D>(string.Format("Reimu/idle{0}", i + 1));
                templeft[i] = this.Content.Load<Texture2D>(string.Format("Reimu/left{0}", i + 1));
                tempright[i] = this.Content.Load<Texture2D>(string.Format("Reimu/right{0}", i + 1));
            }
            character = new Character("Reimu", tempidle, templeft, tempright, this.Content.Load<Texture2D>("Reimu/focus"), new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            character.Update(Keyboard.GetState().GetPressedKeys(), Window);
            if (gameTime.TotalGameTime - now > System.TimeSpan.FromMilliseconds(120))
            {
                character.UpdateFrame(Keyboard.GetState().GetPressedKeys());
                now = gameTime.TotalGameTime;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.TransparentBlack);
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(character.CurrentImg, character.Position, character.GetSelfRectangle(), Color.White, 0, character.GetSelfCenter(), 1.0f, SpriteEffects.None, 1);
            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                spriteBatch.Draw(character.FocusImg, character.Position, character.GetFocusRectangle(), Color.White, character.Angle, character.GetFocusCenter(), 1.0f, SpriteEffects.None, 1);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SATCollisionDetection
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pixelTexture;
        BouncingThing[] bouncyThings;
        int numBouncyThings;
        int numBouncyTriangles;
        int numBouncyBox;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            int maxX = graphics.GraphicsDevice.Viewport.Width;
            int maxY = graphics.GraphicsDevice.Viewport.Height;
            numBouncyThings = 20;
            numBouncyBox = numBouncyThings / 2;
            numBouncyTriangles = numBouncyThings / 2;
            bouncyThings = new BouncingThing[numBouncyThings];
            Random r = new Random();
            for (int i = 0; i < numBouncyThings; i++)
			{
                if (i < numBouncyTriangles)
                {
                    bouncyThings[i] = new BouncingTriangle(maxX, maxY, r);
                }
                else
                {
                    bouncyThings[i] = new BouncingBox(maxX, maxY, r);
                }
			}

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //create a 1*1 texture
            pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData<Color>(
                new Color[] { Color.White });
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
           
            // TODO: Add your update logic here
            for (int i = 0; i < numBouncyThings; i++)
            {
                bouncyThings[i].update(gameTime);
            }


            bool collided;
            for (int i = 0; i < numBouncyThings; i++)
            {
                for (int j = i + 1; j < numBouncyThings; j++)
                {
                    collided = Colliding.CheckForCollisionSAT(bouncyThings[i], bouncyThings[j]);

                    if (collided == true)
                    {
                        bouncyThings[i].velocity *= -1;
                        bouncyThings[j].velocity *= -1;
                    }
                }
            }


            base.Update(gameTime);
    
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();
            for (int i = 0; i < numBouncyThings; i++)
            {
                bouncyThings[i].draw(spriteBatch, pixelTexture);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
    }
}

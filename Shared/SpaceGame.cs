#region File Description
//-----------------------------------------------------------------------------
// SpaceMazeGame.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

#endregion

namespace SpaceMaze
{
	/// <summary>
	/// Default Project Template
	/// </summary>
	public class SpaceGame : Game
	{

		public static SpaceGame singleton { get; protected set; }

		#region Fields

		public GraphicsDeviceManager graphics { get; protected set; }
		MouseState currentMouse, oldMouse;
		public SpriteBatch spriteBatch { get; protected set; }
		Screen currentScreen;

		#endregion

		#region Configuration

		static float targetFPS = 60.0f;
		public int framesSkipped = 0;
		int currentFrame = 0;
		static float msPerFrame = 1000.0f / targetFPS;

		public Vector2 physicalSize;
		private Vector2 _gameSize;
		public Matrix matrix;
		public Vector2 gameSize {
			get {
				return _gameSize;
			}
			set { 
				_gameSize = value;
				if (value.X / value.Y > physicalSize.X / physicalSize.Y) {
					float ratio = physicalSize.X / value.X;
					matrix = Matrix.CreateScale (ratio);
					matrix = matrix * Matrix.CreateTranslation (
						0,
						(physicalSize.Y - value.Y * ratio) * 0.5f,
						0);
				} else {
					float ratio = physicalSize.Y / value.Y;
					matrix = Matrix.CreateScale (ratio);
					matrix = matrix * Matrix.CreateTranslation (
						(physicalSize.X - value.X * ratio) * 0.5f,
						0,
						0);
				}
			}
		}

		#endregion

		#region Initialization

		public SpaceGame ()
		{

			graphics = new GraphicsDeviceManager (this);
			
			Content.RootDirectory = "Assets";

			graphics.IsFullScreen = false;

			singleton = this;
		}

		/// <summary>
		/// Overridden from the base Game.Initialize. Once the GraphicsDevice is setup,
		/// we'll use the viewport to initialize some values.
		/// </summary>
		protected override void Initialize ()
		{
			base.Initialize ();
			oldMouse = Mouse.GetState ();
		}


		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent ()
		{
			physicalSize = new Vector2 (GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
			gameSize = physicalSize;

			spriteBatch = new SpriteBatch (graphics.GraphicsDevice);
			currentScreen = new GameScreen ();
			currentScreen.LoadContent ();
			currentScreen.OnTouchMove (Mouse.GetState ().Position);
		}

		#endregion

		#region Update and Draw

		protected void UpdateScreen(GameTime gameTime)
		{
			framesSkipped = 0;
			while (gameTime.TotalGameTime.TotalMilliseconds > (msPerFrame * currentFrame)) {
				framesSkipped++;
				currentFrame++;
				currentScreen.Update ();
			}
		}

		protected void UpdateInput()
		{
			currentMouse = Mouse.GetState ();

			if (currentMouse.LeftButton == ButtonState.Pressed &&
					oldMouse.LeftButton == ButtonState.Released) {
				currentScreen.OnTouch (currentMouse.Position);
			}
			if (currentMouse.LeftButton == ButtonState.Released && 
					oldMouse.LeftButton == ButtonState.Pressed) {
				currentScreen.OnTouchUp (currentMouse.Position);
			}
			if (currentMouse.Position != oldMouse.Position) {
				currentScreen.OnTouchMove (currentMouse.Position);
			}

			oldMouse = currentMouse;
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			base.Update (gameTime);
			UpdateInput ();
			UpdateScreen (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself. 
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			base.Draw (gameTime);
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, matrix);
			currentScreen.Draw();
			spriteBatch.End ();

			spriteBatch.Begin ();
			currentScreen.DrawUnscaled ();
			spriteBatch.End ();
		}

		#endregion
	}
}

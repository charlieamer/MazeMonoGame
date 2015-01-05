using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SpaceMaze
{
	class GameScreen : Screen
	{
		GameObject targetObject;
		PhysicalObject testObject;
		Maze maze;

		GameState _gameState;
		GameState gameState {
			get {
				return _gameState;
			}
			set {
				if (value == GameState.Losing)
					Lost ();
				if (value == GameState.Winning)
					Won ();
				if (value == GameState.Playing)
					Started ();
				_gameState = value;
			}
		}

		public GameScreen() : base()
		{
			targetObject = new GameObject (Utils.CreateCircle (20));
			AppendChild (targetObject);

			testObject = new PhysicalObject (Utils.CreateCircle (10));
			testObject.maxSpeed = 4.0f;
			AppendChild (testObject);

			maze = new Maze ("gg.png");
			AppendChild (maze);

			gameState = GameState.Playing;

		}

		protected void UpdateTargetPosition() {
			targetObject.position = worldMousePosition;
		}

		public override void LoadContent ()
		{
			base.LoadContent ();
			SpaceGame.singleton.gameSize = new Vector2 (maze.bounds.Width, maze.bounds.Height);
		}

		public override void OnTouch(Point p)
		{
			base.OnTouch (p);
			UpdateTargetPosition ();
		}
		public override void OnTouchUp(Point p)
		{
			base.OnTouchUp (p);
			UpdateTargetPosition ();
		}
		public override void OnTouchMove(Point p)
		{
			base.OnTouchMove (p);
			if (isMouseDown)
				UpdateTargetPosition ();
		}
		public override void Draw()
		{
			base.Draw ();
		}
		public override void Update ()
		{
			switch (gameState) {
			case GameState.Losing:
				UpdateLosing ();
				break;
			case GameState.Playing:
				UpdatePlaying ();
				break;
			case GameState.Winning:
				UpdateWinning ();
				break;
			}
		}

		protected void UpdateWinning()
		{
		}

		protected void UpdateLosing()
		{
			if (isMouseDown)
				gameState = GameState.Playing;
		}

		protected void UpdatePlaying()
		{
			base.Update ();
			testObject.ModifyForce ("target", targetObject.position - testObject.position, 0.1f);
			if (maze.CanCollide (testObject))
				gameState = GameState.Losing;
		}

		protected void Lost ()
		{
			testObject.Stop ();
		}

		protected void Won ()
		{
		}

		protected void Started ()
		{
			targetObject.position = new Vector2 (100, 100);
			testObject.position = new Vector2 (100, 100);
		}
	}
}


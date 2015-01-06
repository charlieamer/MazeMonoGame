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
	class Screen : Drawable
	{
		protected bool isMouseDown;
		protected Vector2 screenMousePosition;
		protected Vector2 worldMousePosition {
			get {
				return Vector2.Transform (screenMousePosition, Matrix.Invert (SpaceGame.singleton.matrix));
			}
		}
		protected GameObject cursor;

		public Screen() : base()
		{
			InitMouse ();
		}
		public virtual void OnTouch(Point position)
		{
			isMouseDown = true;
			UpdateMousePosition (position);
		}
		public virtual void OnTouchUp(Point position)
		{
			isMouseDown = false;
			UpdateMousePosition (position);
		}
		public virtual void OnTouchMove(Point position)
		{
			UpdateMousePosition (position);
		}
		private void InitMouse()
		{
			cursor = new GameObject ("cursor.png");
			AppendChild (cursor);
			cursor.center = new Vector2 (0, 2);
			cursor.scale = new Vector2 (0.4f, 0.4f);
			cursor.isUnscaled = true;
		}
		private void UpdateMousePosition(Point position) {
			screenMousePosition = new Vector2(position.X, position.Y);
			cursor.position = screenMousePosition;
		}
	}
}


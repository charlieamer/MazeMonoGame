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
	public class Screen : Drawable, GuiListener
	{
		protected bool isMouseDown;
		protected Vector2 screenMousePosition;
		protected Vector2 physicalScreenSize;
		protected Vector2 scaledScreenSize;
		protected Gui gui;
		protected Vector2 worldMousePosition {
			get {
				return Vector2.Transform (screenMousePosition, Matrix.Invert (SpaceGame.singleton.matrix));
			}
		}
		protected GameObject cursor;

		public Screen() : base()
		{
			InitMouse ();
			gui = new Gui ();
			gui.listener = this;
			PrependChild (gui);
			physicalScreenSize = SpaceGame.singleton.physicalSize;
			scaledScreenSize = SpaceGame.singleton.gameSize;
		}
		public virtual void OnTouch(Point position)
		{
			isMouseDown = true;
			UpdateMousePosition (position);
			gui.OnTouchDown (screenMousePosition);
		}
		public virtual void OnTouchUp(Point position)
		{
			isMouseDown = false;
			UpdateMousePosition (position);
			gui.OnTouchUp (screenMousePosition);
		}
		public virtual void OnTouchMove(Point position)
		{
			UpdateMousePosition (position);
			gui.OnTouchMove (screenMousePosition);
		}
		private void InitMouse()
		{
			cursor = new GameObject ("cursor.png");
			#if !__MOBILE__
			AppendChild (cursor);
			#endif
			cursor.center = new Vector2 (0, 2);
			cursor.scale = new Vector2 (0.4f, 0.4f);
			cursor.isUnscaled = true;
		}
		private void UpdateMousePosition(Point position) {
			screenMousePosition = new Vector2(position.X, position.Y);
			cursor.position = screenMousePosition;
		}

		#region GuiListener implementation

		virtual public bool OnMouseDown (GuiObject obj, Vector2 p)
		{
			return false;
		}

		virtual public bool OnMouseMove (GuiObject obj, Vector2 p)
		{
			return false;
		}

		virtual public bool OnMouseUp (GuiObject obj, Vector2 p)
		{
			return false;
		}

		#endregion
	}
}


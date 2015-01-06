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
	class Maze : GameObject
	{
		public Maze(String fileLocation) : base(fileLocation) {}
		public Maze (Texture2D directTexture) : base (directTexture) {}
		private Color[] colors;

		List<Point> collidables;

		private int GetIndex(int x, int y) {
			return x + y * texture.Width;
		}

		private Color GetColor(int x, int y) {
			if (x >= 0 && x < texture.Width && y >= 0 && y < texture.Height)
				return colors [GetIndex (x, y)];
			return Color.TransparentBlack;
		}

		private bool IsColorCollidable(Color c) {
			if (c.A > 125)
				return true;
			else
				return false;
		}

		private bool IsCollidable(int x, int y)
		{
			if (IsColorCollidable (GetColor (x, y)) && (
			        !IsColorCollidable (GetColor (x + 1, y)) ||
			        !IsColorCollidable (GetColor (x - 1, y)) ||
			        !IsColorCollidable (GetColor (x, y + 1)) ||
			        !IsColorCollidable (GetColor (x, y - 1))))
				return true;
			return false;
		}

		public override void LoadContent ()
		{
			base.LoadContent ();

			collidables = new List<Point> ();
			colors = new Color[texture.Width * texture.Height];
			texture.GetData<Color> (colors);
			for (int x = 0; x < texture.Width; x++) {
				for (int y = 0; y < texture.Height; y++) {
					if (IsCollidable(x,y)) {
						collidables.Add (new Point (x, y));
						colors [x + y * texture.Width] = new Color (255, 0, 0);
					}
				}
			}

			center = new Vector2 (0, 0);
			texture.SetData<Color> (colors);

		}

		public bool CanCollide(GameObject obj)
		{
			foreach (Point collidable in collidables) {
				if (obj.bounds.Contains (collidable))
					return true;
			}
			return false;
		}
	}
}


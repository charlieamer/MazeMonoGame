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
		const int OCTREE_SIZE = 30;

		public Maze(String fileLocation) : base(fileLocation) {}
		public Maze (Texture2D directTexture) : base (directTexture) {}
		private Color[] colors;
		int totalPoints, checkedPoints;
		Rectangle objectBounds;

		//List<Point> collidables;
		List<Point>[,] octreeCollidables;

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

		private List<Point> getOctree(int x, int y) {
			if (x < 0 || y < 0 || x >= texture.Width || y >= texture.Height)
				return null;
			return octreeCollidables [x / OCTREE_SIZE, y / OCTREE_SIZE];
		}

		private List<Point> getOrCreateOctree(int x, int y) {
			List<Point> ret = getOctree (x, y);
			if (ret == null) {
				ret = new List<Point> ();
				octreeCollidables [x / OCTREE_SIZE, y / OCTREE_SIZE] = ret;
			}
			return ret;
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

			octreeCollidables = new List<Point>[texture.Width / OCTREE_SIZE + 1, texture.Height / OCTREE_SIZE + 1];
			colors = new Color[texture.Width * texture.Height];
			texture.GetData<Color> (colors);
			for (int x = 0; x < texture.Width; x++) {
				for (int y = 0; y < texture.Height; y++) {
					if (IsCollidable(x,y)) {
						getOrCreateOctree (x, y).Add (new Point (x, y));
						colors [x + y * texture.Width] = new Color (255, 0, 0);
						totalPoints++;
					}
				}
			}

			center = new Vector2 (0, 0);
			texture.SetData<Color> (colors);

		}

		public bool CanCollide(GameObject obj, List<Point> points)
		{
			if (points == null)
				return false;
			checkedPoints += points.Count;
			objectBounds = obj.bounds;
			foreach (var point in points) {
				if (objectBounds.Contains(point))
					return true;
			}
			return false;
		}
		public bool CanCollide(GameObject obj)
		{
			objectBounds = obj.bounds;
			checkedPoints = 0;
			for (int x = objectBounds.Left; x <= objectBounds.Right + OCTREE_SIZE; x += OCTREE_SIZE) {
				for (int y = objectBounds.Top; y <= objectBounds.Bottom + OCTREE_SIZE; y += OCTREE_SIZE) {
					if (CanCollide (obj, getOctree (x, y)))
						return true;
				}
			}
			return false;
		}

		#if DEBUG_VISUALISATION
		public override void Draw ()
		{
			base.Draw ();
			Texture2D whiteRectangle = Utils.CreateRectangle (OCTREE_SIZE, OCTREE_SIZE);
			for (int x = 0; x < texture.Width; x += OCTREE_SIZE) {
				for (int y = 0; y < texture.Height; y += OCTREE_SIZE) {
					if (getOctree (x, y) != null)
						spriteBatch.Draw (whiteRectangle, new Vector2 (x, y));
				}
			}
			Texture2D redRectangle = Utils.CreateRectangle (OCTREE_SIZE, OCTREE_SIZE, Color.Red);
			for (int x = objectBounds.Left / OCTREE_SIZE * OCTREE_SIZE; x <= objectBounds.Right / OCTREE_SIZE * OCTREE_SIZE; x += OCTREE_SIZE) {
				for (int y = objectBounds.Top / OCTREE_SIZE * OCTREE_SIZE; y <= objectBounds.Bottom / OCTREE_SIZE * OCTREE_SIZE; y += OCTREE_SIZE) {
					if (getOctree (x, y) != null)
						spriteBatch.Draw (redRectangle, new Vector2 (x, y));
				}
			}
			spriteBatch.Draw (Utils.CreateRectangle (objectBounds.Width, objectBounds.Height, Color.Green), new Vector2 (objectBounds.Left, objectBounds.Top));
		}
		#endif
	}
}


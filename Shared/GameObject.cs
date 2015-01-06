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
	class GameObject : Drawable
	{
		#region properties
		public Vector2 position { get; set; }
		public Vector2 scale { get; set; }
		public Vector2 center { get; set; }
		public float rotation { get; set; }
		public Rectangle bounds {
			get {
				return new Rectangle ((int)position.X - (int)center.X, (int)position.Y - (int)center.Y, texture.Width, texture.Height);
			}
		}
		#endregion

		#region private
		protected String textureLoadLocation;
		protected Vector2 textureDimension;
		protected Texture2D texture;
		#endregion

		#region overrides
		public GameObject() : base() {
			Init ();
		}
		public GameObject(String textureLocation) : base() {
			Init ();
			textureLoadLocation = textureLocation;
		}
		public GameObject(Texture2D directTexture) : base() {
			Init ();
			texture = directTexture;
		}
		public override void LoadContent ()
		{
			base.LoadContent ();
			if (texture == null) {
				try {
					texture = LoadTexture (textureLoadLocation);
				} catch (Exception ex) {
					Console.Error.WriteLine ("Error loading texture: " + textureLoadLocation);
					Console.Error.WriteLine ("Error was: " + ex.Message);
				}
			}
			if (texture != null) {
				textureDimension = new Vector2 (texture.Width, texture.Height);
			}
			InitDefault ();
		}
		public override void Draw ()
		{
			base.Draw ();
			if (texture != null)
				spriteBatch.Draw (texture, position - center, scale: scale, rotation: rotation);
		}
		#endregion

		#region private_functions
		protected virtual void Init() {
			texture = null;
			textureDimension = new Vector2 (0, 0);
			textureLoadLocation = null;
			position = new Vector2 (float.NegativeInfinity, float.NegativeInfinity);
			scale = new Vector2 (float.NegativeInfinity, float.NegativeInfinity);
			center = new Vector2 (float.NegativeInfinity, float.NegativeInfinity);
			rotation = float.NegativeInfinity;
		}
		protected void InitDefault() {
			if (float.IsNegativeInfinity(position.X) && float.IsNegativeInfinity(position.Y))
				position = new Vector2 (0, 0);
			if (float.IsNegativeInfinity(scale.X) && float.IsNegativeInfinity(scale.Y))
				scale = new Vector2 (1, 1);
			if (float.IsNegativeInfinity(center.X) && float.IsNegativeInfinity(center.Y))
				center = textureDimension * 0.5f;
			if (float.IsNegativeInfinity(rotation))
				rotation = 0.0f;
		}
		#endregion
	}
}


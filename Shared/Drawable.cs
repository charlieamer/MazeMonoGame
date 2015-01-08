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
	class Drawable
	{
		protected SpriteBatch spriteBatch;
		List<Drawable> children;
		bool contentLoaded;
		public bool isUnscaled { get; set; }

		public Drawable ()
		{
			contentLoaded = false;
			this.spriteBatch = SpaceGame.singleton.spriteBatch;
			this.children = new List<Drawable> ();
		}

		public virtual void LoadContent ()
		{
			if (!contentLoaded)
				foreach (Drawable child in children) {
					child.LoadContent ();
				}
			contentLoaded = true;
		}

		public virtual void Update ()
		{
			foreach (Drawable child in children) {
				child.Update ();
			}
		}

		public virtual void Draw ()
		{
			foreach (Drawable child in children) {
				if (!child.isUnscaled)
					child.Draw ();
			}
		}

		public virtual void DrawUnscaled()
		{
			foreach (Drawable child in children) {
				if (child.isUnscaled)
					child.Draw ();
			}
		}

		protected Texture2D LoadTexture(String name)
		{
			return SpaceGame.singleton.Content.Load<Texture2D> (name);
		}

		protected SpriteFont LoadFont(String name)
		{
			return SpaceGame.singleton.Content.Load<SpriteFont> (name);
		}

		protected void AppendChild(Drawable child) {
			children.Add(child);
			if (contentLoaded)
				child.LoadContent();
		}

		protected void PrependChild(Drawable child) {
			children.Insert (0, child);
		}

		protected void RemoveChild(Drawable child) {
			children.Remove (child);
		}

	}
}


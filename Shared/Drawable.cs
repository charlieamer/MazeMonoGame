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
	public class Drawable
	{
		protected SpriteBatch spriteBatch;
		protected List<Drawable> children;
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
				child.Draw ();
			}
		}

		public virtual void DrawUnscaled()
		{
			foreach (Drawable child in children) {
				child.DrawUnscaled ();
			}
		}

		protected void AppendChild(Drawable child) {
			children.Add(child);
			if (contentLoaded)
				child.LoadContent();
		}

		protected void PrependChild(Drawable child) {
			children.Insert (0, child);
			if (contentLoaded)
				child.LoadContent ();
		}

		protected void RemoveChild(Drawable child) {
			children.Remove (child);
		}

	}
}


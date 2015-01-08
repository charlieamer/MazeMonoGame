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
	class ImageFont
	{
		static float startSize = 300.0f;

		static SortedDictionary<String, ImageFont> fontCollection;

		String loadedFont;
		SortedDictionary<float, Texture2D[]> scaledCache;

		public ImageFont(String name) {
			loadedFont = name;
			if (fontCollection == null)
				fontCollection = new SortedDictionary<string, ImageFont> ();
			if (fontCollection.ContainsKey (name))
				return;
			fontCollection [name] = this;
			scaledCache = new SortedDictionary<float, Texture2D[]> ();
			scaledCache [startSize] = new Texture2D[256];
			for (int i = 0; i < 256; i++) {
				try {
					scaledCache [startSize] [i] = SpaceGame.singleton.Content.Load<Texture2D> (name + "/" + i + ".png");
				} catch {
					scaledCache [startSize] [i] = Utils.CreateRectangle ((int)startSize / 3, (int)startSize);
				}
			}
		}

		private void Cache(float size) {
			if (size != startSize) {
				scaledCache [size] = new Texture2D[256];
				for (int i = 0; i < 256; i++) {
					scaledCache [size] [i] = Utils.ScaleTexture (scaledCache [startSize] [i], size / startSize);
				}
			}
		}

		public Texture2D WriteText(string text, float size) {
			Point position = new Point ();
			Texture2D[] textures = new Texture2D[text.Length];
			Point[] offsets = new Point[text.Length];
			if (!scaledCache.ContainsKey (size))
				Cache (size);
			for (int i = 0; i < text.Length; i++) {
				textures[i] = scaledCache [size] [(int)text [i]];
				offsets [i] = new Point (position.X, position.Y);
				position.X += textures [i].Width;
			}
			return Utils.MergeTextures (textures, offsets);
		}
	}
}


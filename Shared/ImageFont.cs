﻿using System;
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
	public class ImageFont
	{
		System.Diagnostics.Stopwatch sw;

		static float startSize = 100.0f;

		static SortedDictionary<String, ImageFont> fontCollection;

		String loadedFont;
		SortedDictionary<float, Texture2D[]> scaledCache;

		AtlasCollection atlas;

		public static ImageFont Load(string name) {
			if (fontCollection == null)
				fontCollection = new SortedDictionary<string, ImageFont> ();
			if (!fontCollection.ContainsKey (name))
				fontCollection [name] = new ImageFont (name);
			return fontCollection [name];
		}

		ImageFont(String name) {
			sw = new System.Diagnostics.Stopwatch ();

			sw.Restart ();
			loadedFont = name;
			fontCollection [name] = this;
			scaledCache = new SortedDictionary<float, Texture2D[]> ();
			scaledCache [startSize] = new Texture2D[256];
			atlas = new AtlasCollection ("Assets/" + name + ".xml");
			for (int i = 0; i < 256; i++) {
				try {
					scaledCache [startSize] [i] = atlas[i + ".png"];
				} catch {
					scaledCache [startSize] [i] = Utils.CreateRectangle ((int)startSize / 3, (int)startSize);
				}
			}
			sw.Stop ();
			Console.WriteLine ("Loaded " + name + " font in " + sw.ElapsedMilliseconds + "ms");
		}

		private Texture2D GetGlyph(char c, float size) {
			if (!scaledCache.ContainsKey (size))
				scaledCache [size] = new Texture2D[256];
			if (scaledCache [size] [(int)c] == null)
				scaledCache [size] [(int)c] = Utils.ScaleTexture (scaledCache [startSize] [(int)c], size / startSize);
			return scaledCache [size] [(int)c];
		}

		public Texture2D WriteText(string text, float size) {
			Point position = new Point ();
			Texture2D[] textures = new Texture2D[text.Length];
			Point[] offsets = new Point[text.Length];
			sw.Restart ();
			for (int i = 0; i < text.Length; i++) {
				textures[i] = GetGlyph(text[i],size);
				offsets [i] = new Point (position.X, position.Y);
				position.X += textures [i].Width;
			}
			Texture2D ret = Utils.MergeTextures (textures, offsets);
			sw.Stop ();
			Console.WriteLine ("Wrote '" + text + "' in " + loadedFont + " font in " + sw.ElapsedMilliseconds + "ms"); 
			return ret;
		}
	}
}


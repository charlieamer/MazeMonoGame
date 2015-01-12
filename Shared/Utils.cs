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
	class Utils
	{
		public static Texture2D CreateCircle(int radius, Color color)
		{
			int outerRadius = radius*2 + 2; // So circle doesn't go out of bounds
			Texture2D texture = new Texture2D(SpaceGame.singleton.spriteBatch.GraphicsDevice, outerRadius, outerRadius);

			Color[] data = new Color[outerRadius * outerRadius];

			// Colour the entire texture transparent first.
			for (int i = 0; i < data.Length; i++)
				data[i] = Color.TransparentBlack;

			// Work out the minimum step necessary using trigonometry + sine approximation.
			double angleStep = 1f/radius;

			for (double angle = 0; angle < Math.PI*2; angle += angleStep)
			{
				// Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
				int x = (int)Math.Round(radius + radius * Math.Cos(angle));
				int y = (int)Math.Round(radius + radius * Math.Sin(angle));

				data[y * outerRadius + x + 1] = Color.White;
			}

			texture.SetData(data);
			return texture;
		}

		public static Texture2D CreateCircle(int radius) {
			return CreateCircle (radius, Color.White);
		}

		public static Texture2D CreateRectangle( int width, int height, Color color) {
			Texture2D texture = new Texture2D(SpaceGame.singleton.spriteBatch.GraphicsDevice, width, height);
			Color[] colors = new Color[ width * height ];

			for ( int x = 0; x < texture.Width; x++ ) {
				for ( int y = 0; y < texture.Height; y++ ) {
					if ( x == 0 || y == 0 || x == texture.Width - 1 || y == texture.Height - 1 ) {
						colors[x + y * texture.Width] = color;
					} else
						colors[ x + y * texture.Width ] = Color.TransparentBlack;
				}
			}

			texture.SetData( colors );
			return texture;
		}

		public static Texture2D CreateRectangle(int width, int height) {
			return CreateRectangle (width, height, Color.White);
		}

		public static Texture2D TextToTexture(String text, SpriteFont font, Color color) {
			Vector2 size = font.MeasureString (text) + new Vector2 (0.5f);
			int width = (int)size.X;
			int height = (int)size.Y;

			// Create a temporary render target and draw the font on it.
			GraphicsDevice device = SpaceGame.singleton.GraphicsDevice;
			RenderTarget2D target = new RenderTarget2D (device, width, height);
			device.SetRenderTarget (target);
			device.Clear (Color.Black);

			SpriteBatch spriteBatch = new SpriteBatch (device);
			spriteBatch.Begin ();
			spriteBatch.DrawString (font, text, Vector2.Zero, color);
			spriteBatch.End ();

			device.SetRenderTarget (null);   // unset the render target

			// read back the pixels from the render target
			Color[] data = new Color[width * height];
			target.GetData (data);
			target.Dispose ();

			Texture2D ret = new Texture2D (device, width, height);
			ret.SetData (data);
			return ret;
		}

		public static Texture2D TextToTexture(String text, SpriteFont font) {
			return TextToTexture (text, font, Color.White);
		}

		public static Texture2D ScaleTexture(Texture2D old, int width, int height) {
			if (width <= 1)
				width = 1;
			if (height <= 1)
				height = 1;
			GraphicsDevice device = SpaceGame.singleton.GraphicsDevice;
			RenderTarget2D target = new RenderTarget2D (device, width, height);
			device.SetRenderTarget (target);
			device.Clear (Color.Transparent);

			SpriteBatch spriteBatch = new SpriteBatch (device);
			spriteBatch.Begin ();
			spriteBatch.Draw (old, new Vector2 (0, 0), scale: new Vector2 ((float)width / (float)old.Width, (float)height / (float)old.Height));
			spriteBatch.End ();

			device.SetRenderTarget (null);   // unset the render target

			// read back the pixels from the render target
			Color[] data = new Color[width * height];
			target.GetData (data);
			target.Dispose ();

			Texture2D ret = new Texture2D (device, width, height);
			ret.SetData (data);
			return ret;
		}

		public static Texture2D ScaleTexture(Texture2D old, float scale) {
			return ScaleTexture (old, (int)(scale * old.Width), (int)(scale * old.Height));
		}

		public static Rectangle RectangleUnion(Rectangle[] rectangles) {
			Rectangle ret = new Rectangle (0, 0, 0, 0);
			foreach (var rectangle in rectangles) {
				ret = Rectangle.Union(rectangle, ret);
			}
			return ret;
		}

		public static Texture2D MergeTextures(Texture2D[] textures, Point[] offsets) {
			Rectangle[] rectangles = new Rectangle[textures.Length];
			for (int i = 0; i < textures.Length; i++) {
				rectangles [i] = new Rectangle (offsets [i].X, offsets [i].Y, textures [i].Width, textures [i].Height);
			}
			Rectangle union = RectangleUnion (rectangles);
			Texture2D ret = new Texture2D (SpaceGame.singleton.GraphicsDevice, union.Width, union.Height);
			Color[] colors = new Color[ret.Width * ret.Height];
			for (int i = 0; i < colors.Length; i++)
				colors [i] = Color.TransparentBlack;
			for (int i = 0; i < textures.Length; i++) {
				Color[] data = new Color[textures [i].Width * textures [i].Height];
				textures [i].GetData<Color> (data);
				for (int x = 0; x < textures [i].Width; x++) {
					for (int y = 0; y < textures [i].Height; y++) {
						int i1 = x + offsets [i].X + ret.Width * (y + offsets [i].Y);
						int i2 = x + y * textures [i].Width;
						Vector4 a = colors [i1].ToVector4 ();
						Vector4 b = data [i2].ToVector4 ();
						Vector4 c = new Vector4 ();
						c.W = a.W + b.W * (1 - a.W);
						c.X = (a.X * a.W + b.X * b.W * (1 - a.W)) / c.W;
						c.Y = (a.Y * a.W + b.Y * b.W * (1 - a.W)) / c.W;
						c.Z = (a.Z * a.W + b.Z * b.W * (1 - a.W)) / c.W;
						colors [i1] = new Color (c);
					}
				}
			}
			ret.SetData<Color> (colors);
			return ret;
		}

		public static Texture2D MergeTextures(Texture2D bottom, Point bottomOffset, Texture2D top, Point topOffset) {
			Texture2D[] textures = new Texture2D[] { bottom, top };
			Point[] points = new Point[] { bottomOffset, topOffset };
			return MergeTextures (textures, points);
		}

		public static Texture2D MergeTextures(Texture2D bottom, Texture2D top) {
			return MergeTextures (bottom, new Point (), top, new Point ());
		}

		public static Texture2D LoadTexture(String name)
		{
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch ();
			sw.Start ();
			Texture2D ret = SpaceGame.singleton.Content.Load<Texture2D> (name);
			Console.WriteLine ("Loaded " + name + " image in " + sw.ElapsedMilliseconds + "ms");
			return ret;
		}

		public static Texture2D CropTexture(Texture2D image, Rectangle target)
		{

			var graphics = image.GraphicsDevice;
			var ret = new RenderTarget2D(graphics, target.Width, target.Height);
			var sb = new SpriteBatch(graphics);

			graphics.SetRenderTarget(ret); // draw to image
			graphics.Clear(new Color(0, 0, 0, 0));

			sb.Begin();
			sb.Draw(image, Vector2.Zero, target, Color.White);
			sb.End();

			graphics.SetRenderTarget(null); // set back to main window

			Texture2D res = (Texture2D)ret;
			return res;

			/*
			Color [] colors = new Color[image.Width * image.Height];
			image.GetData<Color> (colors);
			return CropTexture (colors, image.Bounds, target);
			*/
		}
		/*
		public static Texture2D CropTexture(Color[] colors, Rectangle source, Rectangle target) {
			Texture2D tex = new Texture2D (SpaceGame.singleton.GraphicsDevice, target.Width, target.Height);
			Color[] ret = new Color[target.Width * target.Height];
			for (int x = target.Left; x < target.Right; x++)
				for (int y = target.Top; y < target.Bottom; y++)
					ret [(x - target.Left) + (y - target.Top) * target.Width] = colors [x + y * source.Width];
			tex.SetData<Color> (ret);
			return tex;
		}
		*/
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

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
	public class AtlasCollection : SortedDictionary<String, Texture2D>
	{
		private Texture2D atlasImage;
		public AtlasCollection(String xmlLocation) :base() {
			XmlReader reader = XmlReader.Create (
				#if __ANDROID__
				Game.Activity.Assets.Open (xmlLocation)
				#else
				xmlLocation
				#endif
			);
			while (reader.Read ()) {
				if (reader.NodeType == XmlNodeType.Element) {
					if (reader.Name == "TextureAtlas") {
						atlasImage = Utils.LoadTexture (reader.GetAttribute ("imagePath"));
					}
					if (reader.Name == "sprite") {
						Rectangle rectangle = new Rectangle ();
						int.TryParse (reader.GetAttribute ("x"), out rectangle.X);
						int.TryParse (reader.GetAttribute ("y"), out rectangle.Y);
						int.TryParse (reader.GetAttribute ("w"), out rectangle.Width);
						int.TryParse (reader.GetAttribute ("h"), out rectangle.Height);
						Texture2D tex = Utils.CropTexture (atlasImage, rectangle);
						Add (reader.GetAttribute ("n"), tex);
					}
					if (reader.Name == "include") {
						AtlasCollection tmp = new AtlasCollection (reader.GetAttribute ("file"));
						foreach (KeyValuePair<String, Texture2D> kv in tmp) {
							Add (kv.Key, kv.Value);
						}
						tmp = null;
					}
				}
			}
		}
	}
}


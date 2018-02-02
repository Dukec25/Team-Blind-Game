using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;


[XmlRoot("map")]
public class Level{
	// Tiled can work with multiple layers, and output a set of tiles for each
	// layer.  We don't need that functionality, but it nests the data element
	// in the layer element, so we need to account for it.
	public class Layer{
		[XmlElement("data")]
		public string data;
	}

	[XmlAttribute("width")]
	public int width;
	[XmlAttribute("height")]
	public int height;

	[XmlElement("layer")]
	public Layer layer;

	public int[,] tiles;
}

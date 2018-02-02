using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

class LevelIO {

	public Level importLevel (string path) {
		XmlSerializer serializer = new XmlSerializer(typeof(Level));
		FileStream stream = new FileStream(path + ".tmx", FileMode.Open);
		Level result = serializer.Deserialize(stream) as Level;
		stream.Close();

		// convert the tiles to a 2D array
		result.tiles = new int[result.height, result.width];
		result.layer.data = result.layer.data.Replace("\r\n", "\n");
		result.layer.data = result.layer.data.Replace("\r", "\n");
		string[] rows = result.layer.data.Split('\n');

		for (int row = 1; row < rows.Length; row += 1) {
			var cols = rows[row].Split(',');
			for (var col = 0; col < cols.Length - 1; col += 1) {
				result.tiles[row - 1, col] = int.Parse(cols[col].Trim());
			}
		}

		return result;
	}
}

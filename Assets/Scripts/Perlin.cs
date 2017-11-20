using UnityEngine;


public static class Perlin {
	public static float[,] GenNmap(int mapWidth, int mapHeight, float scale, int octaves, float persistence, float lacunarity, int seed){
		var nmap = new float[mapWidth,mapHeight];
		
		var sRandom = new System.Random(seed);
		var offset = new Vector2[octaves];
		for (var i = 0; i < octaves; i++)
		{
			var offsetX = sRandom.Next(-100000, 100000);
			var offsetY = sRandom.Next(-100000, 100000);
			offset[i] = new Vector2(offsetX,offsetY);
		}
		
		
		if (scale <= 0) {
			scale = 0.0001f;
		}
		
	
		var maxNHeight = float.MinValue;
		var minNHeight = float.MaxValue;


		for (var x = 0; x < mapWidth; x++) {
			for (var y = 0; y < mapHeight; y++) {

				float amplitude = 1;
				float frequency = 1;
				float nHeight = 0;

				for (var i = 0; i < octaves; i++) {
					var sampleX = x / scale * frequency + offset[i].x;
					var sampleY = y / scale * frequency + offset[i].y;

					var perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					nHeight += perlinValue * amplitude;	
					amplitude *= persistence;
					frequency *= lacunarity;
				}
				
				if (nHeight > maxNHeight)
				{
					maxNHeight = nHeight;
				}else if (nHeight < minNHeight)
				{
					minNHeight = nHeight;
				}
				
				nmap[x,y] = nHeight;
			}
		}
		for (var x = 0; x < mapWidth; x++)
		{
			for (var y = 0; y < mapHeight; y++)
			{
				nmap[x, y] = Mathf.InverseLerp(minNHeight, maxNHeight, nmap[x, y]);
			}
		}
		return nmap;
	}
}

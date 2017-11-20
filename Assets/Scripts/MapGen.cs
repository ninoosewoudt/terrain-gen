using UnityEngine;

public class MapGen : MonoBehaviour {
	[SerializeField]
	private int _mapWidth;
	[SerializeField]
	private int _mapHeight;
	[SerializeField]
	private float _noiseScale;
	[SerializeField]
	private int _octaves;
	[SerializeField]
	private float _lacunarity;
	[SerializeField]
	private float _persistance;
	[SerializeField]
	private int _seed;
	[SerializeField]
	private TerrainType[] _biomeTypes;
	[SerializeField]
	private float _meshHeightMultiplier;
	[SerializeField]
	private AnimationCurve _meshHeightCurve;

	private MapRenderer _mapRenderer;

	public bool AutoUpdate;

	public void Start(){
		InvokeRepeating("GenMap", 2.0f, 1f);
	}

	
	
	
	public void GenMap()
	{
		_seed = (int) Random.Range(0f, 100000.0f);
		var nmap = Perlin.GenNmap (_mapWidth,_mapHeight,_noiseScale,_octaves,_lacunarity,_persistance,_seed);
		var cmap = new Color[_mapWidth * _mapHeight];
		for (var x = 0; x < _mapHeight; x++)
		{
			for (var y = 0; y < _mapWidth; y++)
			{
				var currentHeight = nmap[x, y];
				for (var i = 0; i < _biomeTypes.Length; i++)
				{
					if (!(currentHeight <= _biomeTypes[i].Height)) continue;
					cmap[y * _mapWidth + x] = _biomeTypes[i].Color;
					break;
				}
			}
		}
		
		_mapRenderer = GetComponent<MapRenderer>();
		_mapRenderer.DrawMesh (MeshGen.Generate (nmap, _meshHeightMultiplier, _meshHeightCurve), _mapRenderer.ColorTexture(cmap, _mapWidth, _mapHeight));

	}

}
[System.Serializable]
public struct TerrainType
{
	
	public string Name;
	public float Height;
	public Color Color;

}

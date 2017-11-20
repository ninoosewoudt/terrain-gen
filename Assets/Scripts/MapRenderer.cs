using UnityEngine;

public class MapRenderer : MonoBehaviour {
	[SerializeField]
	private MeshFilter _meshFilter;
	[SerializeField]
	private MeshRenderer _meshRenderer;

	public Texture2D ColorTexture(Color[] cmap, int width, int height){
		var texture = new Texture2D(width, height)
		{
			filterMode = FilterMode.Point,
			wrapMode = TextureWrapMode.Clamp
		};

		texture.SetPixels(cmap);
		texture.Apply();

		return texture;
	}
	
	public void DrawMesh(MeshData meshData, Texture2D texture) {
		
		_meshFilter.sharedMesh = meshData.CreateMesh ();
		_meshRenderer.sharedMaterial.mainTexture = texture;
	}
}

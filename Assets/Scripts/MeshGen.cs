using UnityEngine;

public static class MeshGen {

    public static MeshData Generate(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve) {
        var width = heightMap.GetLength (0);
        var height = heightMap.GetLength (1);
        var topLeftX = (width - 1) / -2f;
        var topLeftZ = (height - 1) / 2f;

        var meshData = new MeshData (width, height);
        var vertexIndex = 0;

        for (var y = 0; y < height; y++) {
            for (var x = 0; x < width; x++) {

                meshData.Vertices [vertexIndex] = new Vector3 (topLeftX + x, heightCurve.Evaluate(heightMap [x, y]) * heightMultiplier, topLeftZ - y);
                meshData.Uvs [vertexIndex] = new Vector2 (x / (float)width, y / (float)height);


                if (x < width - 1 && y < height - 1) {
                    meshData.AddTriangle (vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle (vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;

    }
}

public class MeshData {
    public readonly Vector3[] Vertices;
    private readonly int[] _triangles;
    public readonly Vector2[] Uvs;

    private int _triangleIndex;

    public MeshData(int meshWidth, int meshHeight) {
        Vertices = new Vector3[meshWidth * meshHeight];
        Uvs = new Vector2[meshWidth * meshHeight];
        _triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
    }

    public void AddTriangle(int a, int b, int c) {
        _triangles [_triangleIndex] = a;
        _triangles [_triangleIndex + 1] = b;
        _triangles [_triangleIndex + 2] = c;
        _triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        var mesh = new Mesh
        {
            vertices = Vertices,
            triangles = _triangles,
            uv = Uvs
        };
        mesh.RecalculateNormals ();
        return mesh;
    }

}
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
namespace MeshBuilderSpace
{

    public class MeshBuilder
    {
        private List<Vector3> m_Vertices = new List<Vector3>();
        public List<Vector3> Vertices { get { return m_Vertices; } }

        private List<Vector3> m_Normals = new List<Vector3>();
        public List<Vector3> Normals { get { return m_Normals; } }

        private List<Vector2> m_UVs = new List<Vector2>();
        public List<Vector2> UVs { get { return m_UVs; } }

        private List<int> m_indexs = new List<int>();

        public void AddTriangle(int index0, int index1, int index2)
        {
            m_indexs.Add(index0);
            m_indexs.Add(index1);
            m_indexs.Add(index2);
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = m_Vertices.ToArray();
            mesh.triangles = m_indexs.ToArray();

            if (m_Normals.Count == m_Vertices.Count)
                mesh.normals = m_Normals.ToArray();

            if (m_UVs.Count == m_Vertices.Count)
                mesh.uv = m_UVs.ToArray();

            mesh.RecalculateBounds();

            return mesh;
        }
    }
}
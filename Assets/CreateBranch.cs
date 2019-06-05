using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshBuilderSpace;
using DatasSpace;
using System;


public class CreateBranch : MonoBehaviour {
    public Material material;
    public float initRadius =0;
    public float totalHeight;
    public int heightSegmentCount =0;
    public Vector3[] finalCentreBox;
    public float[] currentRadiusBox;
    public int finalHeightSegmentCount = 60;
    public float finalInitRadius = 0.6f;
    public float heightInc = 1f;
    public float lastHeightInc;


    public void Start() {
        transform.position = new Vector3(0,0,0);
        MeshFilter filter = this.gameObject.AddComponent<MeshFilter>();
        Datas datas = new Datas();
        finalCentreBox = datas.FinalCentreBox(finalHeightSegmentCount, finalInitRadius);
        //currentRadiusBox = datas.CurrentRadiusBox(heightSegmentCount, initRadius);
        //Vector3[] finalCentreBoxTmp = datas.Vector3Assignment(finalCentreBox, 0, heightSegmentCount); 
        //BuildCylinderMesh(meshBuilder, filter, datas, heightSegmentCount, heightInc, currentRadiusBox, finalCentreBoxTmp, 0f);
        MeshRenderer meshRender = this.gameObject.AddComponent<MeshRenderer>();
        meshRender.sharedMaterial = material;
       
    }

 
    private void FixedUpdate()
    {
        if (totalHeight < 120)
        {
            MeshBuilder meshBuilder = new MeshBuilder();
            MeshFilter filter = this.gameObject.GetComponent<MeshFilter>();
            Datas datas = new Datas();

            totalHeight += 5f * 1/(heightSegmentCount + 1);        //总长变化情况
            lastHeightInc = totalHeight - heightSegmentCount * heightInc;

            if (lastHeightInc > 1f && totalHeight < 60)           // 增加一小节的条件
            {       
                heightSegmentCount++;
                initRadius = finalInitRadius * heightSegmentCount / finalHeightSegmentCount;  // 分生时期的初始半径变化情况
            }
            if (totalHeight >= 60)                                // 当植物不再增加节数
            {
                heightInc = totalHeight / heightSegmentCount;
                lastHeightInc = heightInc;
                initRadius = initRadius + 0.0001f;                                                  // 分生期结束后半径变化情况

            }
                            
            currentRadiusBox = datas.CurrentRadiusBox(heightSegmentCount, initRadius);

            Vector3[] finalCentreBoxTmp = datas.Vector3Assignment(finalCentreBox, 0, heightSegmentCount);

            BuildCylinderMesh(meshBuilder, filter, datas, heightSegmentCount, heightInc, currentRadiusBox, finalCentreBoxTmp,lastHeightInc);
        }
    }


    public void BuildCylinderMesh(MeshBuilder meshBuilder, MeshFilter filter, Datas datas, int heightSegmentCount,
        float heightInc, float[] currentRadius, Vector3 [] centreBox, float lastHeightInc)
    {

        for (int i = 0; i <= heightSegmentCount; i++)
        {

            if (i == heightSegmentCount)
                centreBox[i].y = lastHeightInc + heightInc * (i - 1);
            centreBox[i].y = heightInc * i;

            float v = (float)i / heightSegmentCount;
            int radialSegmentCount = 20;
            BuildRing(meshBuilder, radialSegmentCount, centreBox[i], currentRadius[i], v, i > 0);
            if (filter != null)
            {
                filter.sharedMesh = meshBuilder.CreateMesh();
            }
        }
    }


    public void BuildRing(MeshBuilder meshBuilder, int segmentCount, Vector3 centre, float radius, float v, bool buildTriangles)
    {
    float angleInc = (Mathf.PI * 2.0f) / segmentCount;
    for (int i = 0; i <= segmentCount; i++)
    {
        float angle = angleInc * i;
        Vector3 unitPosition = Vector3.zero;
        unitPosition.x = Mathf.Cos(angle);
        unitPosition.z = Mathf.Sin(angle);
        meshBuilder.Vertices.Add(centre + unitPosition * radius);
        meshBuilder.Normals.Add(unitPosition);
        meshBuilder.UVs.Add(new Vector2((float)i / segmentCount, v));
        if (i > 0 && buildTriangles)
        {
            int baseIndex = meshBuilder.Vertices.Count - 1;
            int vertsPerRow = segmentCount + 1;
            int index0 = baseIndex;
            int index1 = baseIndex - 1;
            int index2 = baseIndex - vertsPerRow;
            int index3 = baseIndex - vertsPerRow - 1;
            meshBuilder.AddTriangle(index0, index2, index1);
            meshBuilder.AddTriangle(index2, index3, index1);
        }
    }
}
}

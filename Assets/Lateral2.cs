using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshBuilderSpace;
using DatasSpaceL;

public class Lateral2 : MonoBehaviour {
    public GameObject mainCamera;
    public GetLateralPos mainCameraScript;
    //public CreateBranch branchScript;
    public float branchHeight = 1;

    public Vector3[] finalVertices;
    public Vector3[] finalCentres;
    public float[] finalRadius;

    public Vector3 neededVertice;
    public Vector3 neededCentre;
    public Vector3 axis1;
    public Vector3 axis2;
    public Vector3 axis3;
    public float neededRadius;
    public float lateralAngle; 

    public Material material;
    public float initRadius; 
    public float totalHeight;
    public int heightSegmentCount = 50;
    public Vector3[] currentCentreBox;
    public float[] currentRadiusBox;
    public float heightInc;
    public float lastHeightInc;

    public void Start () {

        MeshFilter filter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRender = gameObject.AddComponent<MeshRenderer>();
        meshRender.sharedMaterial = material;

        
    }

    public void FixedUpdate()
    {
        //gameObject.transform.RotateAround(neededVertice, axis1,Time.fixedDeltaTime * 10f);

        branchHeight = GameObject.Find("branch").GetComponent<CreateBranch>().totalHeight;
        if (branchHeight >= 8)
        {
            Initialization();

            if (branchHeight <= 30)                     //主干多长时侧枝开始生长,多长是不在主要为该侧枝提供营养，半径不在随主干变换
                initRadius = 0.7f * neededRadius;
            //if (branchHeight > 30)
            //    initRadius += 0.00001f;

            MeshBuilder meshBuilder = new MeshBuilder();
            MeshFilter filter = this.gameObject.GetComponent<MeshFilter>();
            DatasL datasL = new DatasL();
            if (totalHeight < 15)                //侧枝长多长
                totalHeight += 0.2f;            //总长变化情况
            if (totalHeight >= 15)
                totalHeight += 0.0001f;          //侧枝长度大于指定长度，长度增长变慢

            heightInc = totalHeight / heightSegmentCount;

            if (heightSegmentCount != 0)
            {
                currentCentreBox = datasL.CurrentCentreBox(heightSegmentCount, initRadius, heightInc, totalHeight,
                    neededCentre, lateralAngle, axis1, axis3);
                currentRadiusBox = datasL.CurrentRadiusBox(heightSegmentCount, initRadius);

                BuildCylinderMesh(meshBuilder, filter, heightSegmentCount, heightInc, currentRadiusBox, currentCentreBox);
            }
        }
    }

    public Vector3 GetTangentVector(Vector3 centre, Vector3 target)
    {
        Vector3 tmpVertice = new Vector3(0, 0, 0)
        {
            x = target.x - centre.x, 
            y = target.y - centre.y,
            z = target.z - centre.z
        };
        Vector3 vertice = new Vector3(1, 0, 0);
        vertice.z = - vertice.x * tmpVertice.x / tmpVertice.z;
        return vertice; 
    }

    public void BuildCylinderMesh(MeshBuilder meshBuilder, MeshFilter filter, int heightSegmentCount, 
        float heightInc, float[] currentRadius, Vector3[] centreBox)
    {

        for (int i = 0; i <= heightSegmentCount; i++)
        {

            //if (i == heightSegmentCount)   //侧枝高度的设置包含在centreBox
            //    centreBox[i] = (lastHeightInc + heightInc * (i - 1)) * axis1;
            //centreBox[i] = heightInc * i * axis1;

            float v = (float)i / heightSegmentCount;
            int radialSegmentCount = 20;
            BuildRing(meshBuilder, radialSegmentCount, centreBox[i], currentRadius[i], v, i > 0);
            if (filter != null)
            {
                filter.sharedMesh = meshBuilder.CreateMesh();
            }
        }
    }


    public void BuildRing( MeshBuilder meshBuilder, int segmentCount, Vector3 centre, float radius, float v, bool buildTriangles)
    {
        float angleInc = (Mathf.PI * 2.0f) / segmentCount;
        for (int i = 0; i <= segmentCount; i++)
        {
            float angle = angleInc * i;
            Vector3 unitPosition = Vector3.zero; 
            float x = Mathf.Cos(angle);
            float z = Mathf.Sin(angle);
            unitPosition = axis3 * x + axis2 * z;
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

    public void Initialization()
    {
        mainCamera = GameObject.Find("Main Camera");
        mainCameraScript = mainCamera.GetComponent<GetLateralPos>();
        finalVertices = mainCameraScript.finalVertices;
        finalCentres = mainCameraScript.finalCentres;
        finalRadius = mainCameraScript.finalRadius;

        neededVertice = finalVertices[2];
        neededCentre = finalCentres[2] + new Vector3(0,1,0)* finalVertices[2].y;
        axis2 = Vector3.Normalize(GetTangentVector(neededCentre, neededVertice));// 着生点的切向量
        axis1 = Vector3.Normalize(neededVertice - neededCentre);                 // 所在节的圆心到着生点的向量
        axis3 = Vector3.up;
        neededRadius = finalRadius[2];

        //transform.position = neededCentre;
        initRadius = 0.7f * neededRadius;
        lateralAngle = Mathf.PI/6;
    }
}

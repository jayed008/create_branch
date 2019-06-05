using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeshBuilderSpace;
using DatasSpace;
using System;

    public class GetLateralPos : MonoBehaviour
    {
        public GameObject branch;
        public CreateBranch branchScript;
        public Vector3[] finalVertices; //侧枝的着生点集合
        public Vector3[] finalCentres;  //着生点集合所处的圆心点集合
        public float[] finalRadius;      //着生点集合所处圆的圆半径集合
                                         //public List<Vector3> verticeList0;
                                         //public List<Vector3> verticeList1;
                                         //public List<Vector3> verticeList2;
                                         //public List<Vector3> verticeList3;
                                         //public List<Vector3> verticeList4;
                                         //public List<Vector3> verticeList5;
                                         //public List<Vector3> verticeList6;
                                         //public List<Vector3> verticeList7;
                                         //public List<Vector3> verticeList8;
                                         //public List<Vector3> verticeList9;
                                         //public List<Vector3> verticeList10;
                                         //public List<Vector3> verticeList11;
                                         //public List<Vector3> verticeList12;
                                         //public List<Vector3> verticeList13;
                                         //public List<Vector3> verticeList14;
                                         //public List<Vector3> verticeList15;
                                         //public List<Vector3> verticeList16;
                                         //public List<Vector3> verticeList17;


        public void FixedUpdate()
        {
            //selfTransform = this.gameObject.transform;
            branch = GameObject.Find("branch");
            branchScript = branch.GetComponent<CreateBranch>();
            //Mesh branchMesh = branch.GetComponent<MeshFilter>().mesh;
            int branchHeightSegmentCount = branchScript.heightSegmentCount;
            float branchHeightInc = branchScript.heightInc;
            float branchLastHeightInc = branchScript.lastHeightInc;
            Vector3[] finalCentreBox = branchScript.finalCentreBox;
            float[] currentRadiusBox = branchScript.currentRadiusBox;
            //Vector3[] vertices = branchMesh.vertices;
            finalVertices = new Vector3[18];
            finalCentres = new Vector3[18];
            finalRadius = new float[18];

            for (int i = 0; i < 18; i++)
            {
                if (branchHeightSegmentCount >= 3 * (1 + i))
                {
                    finalVertices[i] = GetFinalVertices(finalCentreBox, currentRadiusBox, branchHeightInc, i);
                    finalCentres[i] = finalCentreBox[3 * (i + 1)];
                    finalRadius[i] = currentRadiusBox[3 * (i + 1)];
                }


            }


            /*  foreach (Vector3 vertice in vertices)
                {

                    float x = vertice.x;
                    float z = vertice.z;
                    if (vertice.y == branchHeightInc * 5 && x < 0 && z > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) >= 126 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 144)
                        verticeList0.Add(vertice);                 // 第1个侧枝的位置
                    if (vertice.y == branchHeightInc * 10 && z < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 81 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) <= 99)
                        verticeList1.Add(vertice);                 // ......
                    if (vertice.y == branchHeightInc * 15 && x > 0 && z > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) >= 36 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 54)
                        verticeList2.Add(vertice);
                    if (vertice.y == branchHeightInc * 20 && x < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 171)
                        verticeList3.Add(vertice);
                    if (vertice.y == branchHeightInc * 25 && x > 0 && z < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 36 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) <= 54)
                        verticeList4.Add(vertice);
                    if (vertice.y == branchHeightInc * 30 && z > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) >= 81 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 99)
                        verticeList5.Add(vertice);
                    if (vertice.y == branchHeightInc * 35 && x < 0 && z < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 126 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) <= 144)
                        verticeList6.Add(vertice);
                    if (vertice.y == branchHeightInc * 40 && x > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 9)
                        verticeList7.Add(vertice);
                    if (vertice.y == branchHeightInc * 45 && x < 0 && z > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) >= 126 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 144)
                        verticeList8.Add(vertice);
                    if (vertice.y == branchHeightInc * 50 && z < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 81 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) <= 99)
                        verticeList9.Add(vertice);
                    if (vertice.y == branchHeightInc * 55 && x > 0 && z > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) >= 36 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 54)
                        verticeList10.Add(vertice);
                    if (vertice.y == branchHeightInc * 60 && x < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 171)
                        verticeList11.Add(vertice);
                    if (vertice.y == branchHeightInc * 65 && x > 0 && z < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 36 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) <= 54)
                        verticeList12.Add(vertice);
                    if (vertice.y == branchHeightInc * 70 && z > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) >= 81 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 99)
                        verticeList13.Add(vertice);
                    if (vertice.y == branchHeightInc * 75 && x < 0 && z < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 126 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) <= 144)
                        verticeList14.Add(vertice);
                    if (vertice.y == branchHeightInc * 80 && x > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 9)
                        verticeList15.Add(vertice);
                    if (vertice.y == branchHeightInc * 85 && x < 0 && z > 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) >= 126 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) < 144)
                        verticeList16.Add(vertice);
                    if (vertice.y == branchHeightInc * 90 && z < 0 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) > 81 && Vector3.Angle(new Vector3(x, 0, z), Vector3.right) <= 99)
                        verticeList17.Add(vertice);
                }

                finalVertices[0] = GetFinalVertices(verticeList0);
                finalVertices[1] = GetFinalVertices(verticeList1);
                finalVertices[2] = GetFinalVertices(verticeList2);
                finalVertices[3] = GetFinalVertices(verticeList3);
                finalVertices[4] = GetFinalVertices(verticeList4);
                finalVertices[5] = GetFinalVertices(verticeList5);
                finalVertices[6] = GetFinalVertices(verticeList6);
                finalVertices[7] = GetFinalVertices(verticeList7);
                finalVertices[8] = GetFinalVertices(verticeList8);
                finalVertices[9] = GetFinalVertices(verticeList9);
                finalVertices[10] = GetFinalVertices(verticeList10);
                finalVertices[11] = GetFinalVertices(verticeList11);
                finalVertices[12] = GetFinalVertices(verticeList12);
                finalVertices[13] = GetFinalVertices(verticeList13);
                finalVertices[14] = GetFinalVertices(verticeList14);
                finalVertices[15] = GetFinalVertices(verticeList15);
                finalVertices[16] = GetFinalVertices(verticeList16);
                finalVertices[17] = GetFinalVertices(verticeList17);
                */
        }
        /*
               public Vector3 GetFinalVertices(List<Vector3> verticeList)
               {
                   Vector3[] verticeArray = verticeList.ToArray();
                   int final = verticeArray.GetLength(0);
                   Vector3 vector = new Vector3(0, 0, 0);
                   if (final >= 1)
                       vector = verticeArray[final - 1];
                   return vector;
               }
               */
        public Vector3 GetFinalVertices(Vector3[] finalCentreBox, float[] currentRadiusBox, float branchHeightInc, int i)
        {
            Vector3 vertice = new Vector3(0, 0, 0)
            {
                x = finalCentreBox[3 * (i + 1)].x + 0.7f * currentRadiusBox[3 * (i + 1)] * Mathf.Cos(0.75f * (i + 1) * Mathf.PI),
                z = finalCentreBox[3 * (i + 1)].z + 0.7f * currentRadiusBox[3 * (i + 1)] * Mathf.Sin(0.75f * (i + 1) * Mathf.PI),
                y = branchHeightInc * 3 * (i + 1)
            };
            return vertice;
        }
    }

                                                                               
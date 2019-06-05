using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace DatasSpace
{
    //public class DatasBox
    //{ 
    //    public Vector3[] currentCentreBox;
    //    public float[] currentRadiusBox;
    //}

    public class Datas
    {

        public Vector3 NextCentre(float currentRadius, float currentHeight, Vector3 currentCent)
        {
            Vector3 nextcent = Vector3.up * currentHeight;
            System.Random r = new System.Random(int.Parse(DateTime.Now.ToString("HHmmssfff")));
            float randomAngle = (float)(r.Next(2 * 314) * 0.01);
            //System.Random rand = new System.Random();
            //float randomAngle = (float)(rand.Next(2 * 314) * 0.01);
            nextcent.x = currentCent.x + Mathf.Cos(randomAngle) * currentRadius * 0.05f;
            nextcent.z = currentCent.z + Mathf.Sin(randomAngle) * currentRadius * 0.05f;
            return nextcent;
        }

        public float NextRadius(float currentRadius)
        {
            System.Random r = new System.Random(int.Parse(DateTime.Now.ToString("HHmmssfff")));
            float randomRadius = (float)(r.Next((int)(currentRadius * 970), (int)(currentRadius * 1000) ) * 0.001);
            return randomRadius;
        }

        public float [] CurrentRadiusBox(int heightSegmentCount, float initRadius) //主干半径分布
        {
            float[] currentRadius = new float[heightSegmentCount + 1];
            for (int i=0;i<=heightSegmentCount;i++)
            {
                currentRadius[i] = initRadius * Mathf.Cos(i * Mathf.PI / (2 * heightSegmentCount + 0.001f));
            }
            return currentRadius;
        } 


        public Vector3[] FinalCentreBox (int heightSegmentCount, float initRadius) //随机生成最终段数(100)的每个圆环的圆心坐标(x,0,z)
        {

            Vector3[] currentCentreBox = new Vector3[heightSegmentCount + 1];
            float[] currentRadiusBox = CurrentRadiusBox(heightSegmentCount,initRadius);
            for (int i = 0; i <= heightSegmentCount; i++)
            {
                if (i == 0) 
                {
                    currentCentreBox[i] = new Vector3(0, 0, 0);
                }
                else
                {
                    currentCentreBox[i] = NextCentre(currentRadiusBox[i], 0f, currentCentreBox[i - 1]);
                    while (Mathf.Abs(currentCentreBox[i].x - 0) > 2 || Mathf.Abs(currentCentreBox[i].z - 0) > 2)
                       currentCentreBox[i] = NextCentre(currentRadiusBox[i], 0f, currentCentreBox[i - 1]); 
                }
            }
            return currentCentreBox;
        }

        public float GetVolume(int startSeg, int endSeg,  float heighInc, float[] radiusBox )
        {
            float radiusSum = 0f;
            for (int i = startSeg; i <= endSeg; i++)
            {
                radiusSum += radiusBox[i];
            }
            float volume = radiusSum * heighInc;

            return volume;
        }

        public Vector3 [] Vector3Assignment(Vector3 [] oldMatrix, int start, int end )
        {
            if (end > 100)
                end = 100;
            Vector3[] newMatrix = new Vector3[end - start + 1];
            int j = 0;
            for (int i = start; i <= end; i++)
            {
                newMatrix[j] = oldMatrix[i];
                if (j <= end)
                    j++;

            }
            return newMatrix;
        }

        public float [] MatrixAssignment(float[] oldMatrix, int start, int end)
        {
            //if (end > 100)
            //    end = 100;
            float[] newMatrix = new float[end - start + 1];
            int j = 0;
            for (int i = start; i <= end; i++)
            {
                newMatrix[j] = oldMatrix[i];
                if (j<=end)
                    j++;
            }
            return newMatrix;
        }

        public float [] MatrixMul(float[] oldMatrix, float totalHigh)
        {
            int a = oldMatrix.GetLength(0);
            float[] newMatrix = new float[a]; 
            for (int i =0; i < a; i++)
            {
                newMatrix[i] = oldMatrix[i] * totalHigh;
            }
            return newMatrix;
        }
    }
}
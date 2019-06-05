using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace DatasSpaceL 
{
    public class DatasBox
    { 
        
    }

    public class DatasL 
    {

        public float [] CurrentRadiusBox(int heightSegmentCount, float initRadius)
        {
            float[] currentRadius = new float[heightSegmentCount + 1];
            for (int i=0;i<=heightSegmentCount;i++)
            {
                currentRadius[i] = initRadius * Mathf.Cos(i * Mathf.PI / (2.3f * heightSegmentCount + 0.001f)); // 单枝上的半径变化
            }
            return currentRadius;
        }
         

        public Vector3[] CurrentCentreBox(int heightSegmentCount, float initRadius, float heightInc, float totalHeight,
            Vector3 initCentre, float lateralAngle, Vector3 axis1, Vector3 axis3) //随机生成最终段数(100)的每个圆环的圆心坐标
        {
             
            Vector3[] currentCentreBox = new Vector3[heightSegmentCount + 1]; 
            float[] currentRadiusBox = CurrentRadiusBox(heightSegmentCount,initRadius);
            //float E = Mathf.Pow(11,9);

            float avgRadius = currentRadiusBox.Average(); //平均半径  
            float h0 = totalHeight;

            //float F = Mathf.Pow(avgRadius,2) * h0 * 0.00001f;         //梁受的力
            //float k0 = Mathf.Pow(2*F/(E*Mathf.PI* Mathf.Pow(avgRadius,2)*0.25f),1/2);  // k0与侧枝长度、材料、半径、夹角有关
            //float w0 = Mathf.Sin(1 - Mathf.Cos(k0 * h0 * Mathf.Pow(Mathf.Abs(lateralAngle), 1 / 2))) / 
            //    (Mathf.Cos(lateralAngle) * Mathf.Cos(k0 * h0 * Mathf.Pow(Mathf.Abs(lateralAngle),1/2)));

            float w0 = lateralAngle * Mathf.Pow(avgRadius, 2) * h0 * Mathf.PI;

            float[] w = new float[heightSegmentCount];  // w表示每一节弯曲的程度，直到w0


            w[0] = w0 / heightSegmentCount;
            for (int j = 1; j<heightSegmentCount;j++)
            {
                w[j] = w[j - 1] + w0 / heightSegmentCount;
            }


            for (int i = 0; i <= heightSegmentCount; i++)
            {
                if (i == 0) 
                    currentCentreBox[i] = initCentre; // 着生点设为当前圆环的圆心

                if (i >= 1)
                        currentCentreBox[i] = currentCentreBox[i - 1] + axis1 * Mathf.Sin(lateralAngle + w[i - 1]) * heightInc
                            / Mathf.Cos(w[1 - 1]) + axis3 * Mathf.Cos(lateralAngle + w[i - 1]) * heightInc / Mathf.Cos(w[1 - 1]);
            }
            return currentCentreBox;
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
    }
}
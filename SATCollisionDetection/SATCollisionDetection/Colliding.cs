using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SATCollisionDetection
{
    class Colliding
    {
        private static float minBt1;
        private static float maxBt1;
        private static float minBt2;
        private static float maxBt2;
        static float temp;

        private static Vector2[] allNormals;

        public static bool CheckForCollisionSAT(BouncingThing bt1, BouncingThing bt2)
        {
            allNormals = new Vector2[bt1.numNormals + bt2.numNormals];

            for (int i = 0; i < allNormals.Length; i++)
            {
                if (i < bt1.numNormals)
                {
                    allNormals[i] = bt1.normals[i];
                }
                else
                {
                    allNormals[i] = bt2.normals[i - bt1.numNormals];
                }

                //This for loop projects each of the vertices of the 2 
                //bouncingThings onto the seperating axes
                for (int j = 0; j < (bt1.vertices.Length + bt2.vertices.Length); j++)
                {
                    //Calculate min and max for BT1
                    if (j < bt1.vertices.Length)
                    {
                        //First time through the loop all the min and max values have to be initialized;
                        if (j == 0)
                        {
                            minBt1 = (allNormals[i].X * bt1.vertices[j].X) + (allNormals[i].Y * bt1.vertices[j].Y);
                            maxBt1 = minBt1;
                        }
                        else
                        {
                            temp = (allNormals[i].X * bt1.vertices[j].X) + (allNormals[i].Y * bt1.vertices[j].Y);
                            if (temp < minBt1)
                            {
                                minBt1 = temp;
                            }
                            else if (temp > maxBt1)
                            {
                                maxBt1 = temp;
                            }
                        }
                    }
                        //Calculate min and max for BT2
                    else
                    {
                        //First time through the loop all the min and max values have to be initialized;
                        if (j - bt1.vertices.Length == 0)
                        {
                            minBt2 = (allNormals[i].X * bt2.vertices[j - bt1.vertices.Length].X) + (allNormals[i].Y * bt2.vertices[j - bt1.vertices.Length].Y);
                            maxBt2 = minBt2;
                        }
                        else
                        {
                            temp = (allNormals[i].X * bt2.vertices[j - bt1.vertices.Length].X) + (allNormals[i].Y * bt2.vertices[j - bt1.vertices.Length].Y);
                            if (temp < minBt2)
                            {
                                minBt2 = temp;
                            }
                            else if (temp > maxBt2)
                            {
                                maxBt2 = temp;
                            }
                        }
                    }
                }//end for j

                if (!(minBt1 > minBt2 && minBt1 < maxBt2) && !(maxBt1 > minBt2 && maxBt1 < maxBt2))
                {
                    return false;
                }
            
            
            }//end for i


            //bt1.velocity *= -1;
            //bt2.velocity *= -1;

            return true;
        }
    }
}

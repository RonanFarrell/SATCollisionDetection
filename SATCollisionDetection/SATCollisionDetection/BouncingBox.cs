using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SATCollisionDetection
{
    class BouncingBox : BouncingThing
    {
        public BouncingBox(int maxX, int maxY, Random r)
        {
            widthVP = maxX - radius;
            heightVP = maxY - radius;
            origin.X = r.Next(radius, (int)widthVP);
            origin.Y = r.Next(radius, (int)heightVP);
            velocity.X = r.Next(-2,2);
            velocity.Y = r.Next(-2,2);
            setupVertices(4, radius, MathHelper.Pi * 0.25);
            numNormals = 2;
            normals = new Vector2[numNormals];
            calculateNormals();
        }

        public void calculateNormals()
        {
            normals[0] = new Vector2(-(vertices[1].Y - vertices[0].Y), (vertices[1].X - vertices[0].X));
            normals[1] = new Vector2(-(vertices[2].Y - vertices[1].Y), (vertices[2].X - vertices[1].X));
            
        }

        public override void update(GameTime t)
        {
            base.update(t);
        }
    }
}

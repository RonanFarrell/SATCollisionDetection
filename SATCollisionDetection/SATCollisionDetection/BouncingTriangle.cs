using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SATCollisionDetection
{
    class BouncingTriangle : BouncingThing
    {
        
        
        public BouncingTriangle(int maxX, int maxY, Random r)
        {
            widthVP = maxX - radius;
            heightVP = maxY - radius;
            origin.X = r.Next(radius, (int)widthVP);
            origin.Y = r.Next(radius, (int)heightVP);
            velocity.X = r.Next(-2,2);
            velocity.Y = r.Next(-2,2);
            setupVertices(3, radius, MathHelper.Pi * 1.5);
            numNormals = 3;
            normals = new Vector2[numNormals];
            calculateNormals();
        }

        public void calculateNormals()
        {
            normals[0] = new Vector2(-(vertices[1].Y - vertices[0].Y), (vertices[1].X - vertices[0].X));
            normals[1] = new Vector2(-(vertices[2].Y - vertices[1].Y), (vertices[2].X - vertices[1].X));
            normals[2] = new Vector2(-(vertices[0].Y - vertices[2].Y), (vertices[0].X - vertices[2].X));
        }
      

        public void draw(SpriteBatch sb, Texture2D t)
        {
            draw(sb, t);
        }

        public override void update(GameTime t)
        {
            base.update(t);
        }
    }
}

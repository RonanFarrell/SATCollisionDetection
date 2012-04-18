using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SATCollisionDetection
{
    class BouncingThing
    {
        public Vector2 velocity;
        public Vector2 origin;
        public Vector2[] vertices;
        public Vector2[] centreToVertices;
        public Vector2[] normals;
        public int numNormals;
        protected int radius;
        protected int numVertices;

        protected float widthVP, heightVP;

        public BouncingThing()
        {
            radius = 20;   
        }

        /// <summary>
        /// Sets up the vertices of a polygon
        /// Use rCos(Angle) and rSin(Angle)
        /// </summary>
        /// <param name="_numVertices">The number of vertices the polygon has</param>
        /// <param name="_radius">The radius of the polygon</param>
        /// <param name="angleInRadians">The angle to rotate the polygon in radians</param>
        protected void setupVertices(int _numVertices, int _radius, double angleInRadians)
        {
            numVertices = _numVertices;
            radius = _radius;
            vertices = new Vector2[numVertices];
            for (int i = 0; i < numVertices; ++i)
            {
                vertices[i].X = (float)(radius * (Math.Cos((i * (2 * MathHelper.Pi) / numVertices) + angleInRadians))) + origin.X;
                vertices[i].Y = (float)(radius * (Math.Sin((i * (2 * MathHelper.Pi) / numVertices) + angleInRadians))) + origin.Y;
            }
        }

        /// <summary>
        /// Draws a polygon
        /// </summary>
        /// <param name="sb">The spritebatch</param>
        /// <param name="t">The texture</param>
        public void draw(SpriteBatch sb, Texture2D t)
        {

            for (int i = 0; i < numVertices - 1; i++)
            {
                drawLine(sb, vertices[i], vertices[i + 1], t);
            }

            drawLine(sb, vertices[numVertices - 1], vertices[0], t);

        }

        /// <summary>
        /// Draws a line between 2 points
        /// </summary>
        /// <param name="sb">The Spritebatch</param>
        /// <param name="start">The start of the line</param>
        /// <param name="end">The end of the line</param>
        /// <param name="t">The texture</param>
        private void drawLine(SpriteBatch sb, Vector2 start, Vector2 end, Texture2D t)
        {
            Vector2 edge;
            float angle;
            edge = end - start;
            angle = (float)Math.Atan(edge.Y / edge.X);
            
            if (edge.X < 0)
                angle += MathHelper.Pi;

            sb.Draw(t,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    1),
                null,
                Color.Black,
                angle,
                new Vector2(0, 0),
                SpriteEffects.None,
                0);

        }

        /// <summary>
        /// Updates the object
        /// </summary>
        /// <param name="t"></param>
        public virtual void update(GameTime t)
        {


            for (int i = 0; i < numVertices; i++)
            {
                vertices[i] += velocity;
            }
            origin += velocity;

            collideEdge();
        }

        /// <summary>
        /// Checks for collisions with the edge of the screen
        /// </summary>
        private void collideEdge()
        {
            if (origin.X < radius / 2 || origin.X > widthVP)
            {
                velocity.X *= -1;
            }

            if (origin.Y < radius / 2 || origin.Y > heightVP)
            {
                velocity.Y *= -1;
            }
        }
    }
}

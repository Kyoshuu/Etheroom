using Etheroom.Entities;
using Etheroom.Entities.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etheroom.Utility.Collision
{
    public class QuadTree
    {
        public int MaxObjects { get; }
        public int MaxLevels { get; }

        private int level;
        private List<Entity> entities;
        private Rectangle bounds;
        private QuadTree[] nodes;

        public QuadTree(int level, Rectangle bounds, int maxObjects = 10, int maxLevels = 5)
        {
            MaxObjects = maxObjects;
            MaxLevels = maxLevels;

            this.level = level;
            this.bounds = bounds;

            entities = new List<Entity>();
            nodes = new QuadTree[4];
        }

        public void Clear()
        {
            entities.Clear();

            for(int i = 0; i < nodes.Length; i++)
            {
                if(nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            int subWidth = bounds.Width / 2;
            int subHeight = bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            nodes[0] = new QuadTree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new QuadTree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new QuadTree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new QuadTree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        private int GetFittingNodeIndex(Entity entity)
        {
            Rectangle rect = entity.GetComponent<Collider>().GetPositionedCollisionBox();

            int index = -1;
            double verticalMidpoint = bounds.X + bounds.Width / 2;
            double horizontalMidpoint = bounds.Y + bounds.Height / 2;

            bool topQuadrant = rect.Y < horizontalMidpoint && rect.Height < horizontalMidpoint;
            bool bottomQuadrant = rect.Y > horizontalMidpoint;

            // Object can completely fit within the left quadrants
            if (rect.X < verticalMidpoint && rect.X + rect.Width < verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }

            // Object can completely fit within the right quadrants
            else if (rect.X > verticalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        public void Insert(Entity entity)
        {
            if(nodes[0] != null)
            {
                int index = GetFittingNodeIndex(entity);

                if(index != -1)
                {
                    nodes[index].Insert(entity);

                    return;
                }
            }

            entities.Add(entity);

            if(entities.Count > MaxObjects && level < MaxLevels)
            {
                if(nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while(i < entities.Count)
                {
                    int index = GetFittingNodeIndex(entities[i]);
                    if(index != -1)
                    {
                        nodes[index].Insert(entities[i]);
                        entities.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public List<Entity> Retrieve(Entity collidingWith, List<Entity> returnObjects)
        {
            int index = GetFittingNodeIndex(collidingWith);

            if(index != -1 && nodes[0] != null)
            {
                nodes[index].Retrieve(collidingWith, returnObjects);
            }

            returnObjects.AddRange(entities);

            return returnObjects;
        }
    }
}

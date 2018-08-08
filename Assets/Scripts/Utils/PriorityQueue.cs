using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Pathfinding
{
    public class GridSpace : IComparable<GridSpace>
    {
        public GameObject gs;
        public float x;
        public float y;
        public double dist;

        public GridSpace(GameObject gs, GameObject source, double previousDist)
        {
            this.gs = gs;
            Vector3 t_gs = gs.GetComponent<Transform>().position;
            Vector3 t_src = source.GetComponent<Transform>().position;
            // should equal 0, 1, or ~1.4
            double d = Math.Sqrt(Math.Pow(t_gs.x + t_src.x, 2) + Math.Pow(t_gs.y + t_src.y, 2));
            this.dist = d + previousDist;
            this.x = t_gs.x;
            this.y = t_gs.y;
        }

		public override string ToString()
		{
            return "GridSpace (" + x.ToString() + ", " + y.ToString() + ")";
		}

        public int CompareTo(GridSpace other)
        {
            if (this.dist < other.dist) return -1;
            else if (this.dist > other.dist) return 1;
            else return 0;
        }
	}


    /* simple, unoptomized priority queue for pathfinding
     * heavily adapted from Visual Studio magazine implementation
     * https://visualstudiomagazine.com/Articles/2012/11/01/Priority-Queues-with-C.aspx?Page=2
     */
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> spaces;

        public PriorityQueue()
        {
            this.spaces = new List<T>();
        }

        public void Insert(T t)
        {
            spaces.Add(t);

            int index = spaces.Count - 1;
            while (index > 0) // finds appropriate index (heap convention)
            {
                int parent = (index - 1) / 2;
                if (spaces[index].CompareTo(spaces[parent]) >= 0)
                {
                    break;
                }
                // switch items
                T temp = spaces[index];
                spaces[index] = spaces[parent];
                spaces[parent] = temp;
                index = parent;
            }
        }

        public T DeleteMin()
        {
            // assumes pq is not empty
            int end = spaces.Count - 1;
            T minItem = spaces[0];

            // this whole thing seems dumb
            spaces[0] = spaces[end];
            spaces.RemoveAt(end);
            end--;

            int parent = 0; // start at front
            while (true) // could be better
            {
                int left = parent * 2 + 1;
                if (left > end)
                {
                    break;
                }
                int right = left + 1;
                if (right <= end && spaces[right].CompareTo(spaces[left]) < 0)
                {
                    left = right;
                }
                if (spaces[parent].CompareTo(spaces[left]) <= 0)
                {
                    break;
                }

                T temp = spaces[parent];
                spaces[parent] = spaces[left];
                spaces[left] = temp;
            }
            return minItem;
        }

        public bool isEmpty()
        {
            return spaces.Count == 0;
        }
    }
}
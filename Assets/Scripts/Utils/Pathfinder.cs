using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pathfinder
    {
        public int gridSize;

        public Pathfinder(int gs)
        {
            this.gridSize = gs;
        }

        public List<GameObject> FindPath(GameObject src, GameObject dest)
        {
            List<GameObject> path = new List<GameObject>();
            PriorityQueue<GridSpace> pq = new PriorityQueue<GridSpace>();
            GridSpace gridSpace = new GridSpace(src, src, 0);
            pq.Insert(gridSpace);
            return dijkstra(path, pq, dest);
        }

        private List<GameObject> dijkstra(List<GameObject> path, PriorityQueue<GridSpace> pq, GameObject dest)
        {
            if (pq.isEmpty()) return null; // exhausted options, no path to dest

            GridSpace tile = pq.DeleteMin();

            if (tile.gs == dest)
            {
                path.Add(dest);
                return path;
            }

            if (path.Contains(tile.gs)) return dijkstra(path, pq, dest);

            path.Add(tile.gs);

            List<GameObject> neighbors = FindNeighboringTiles(tile.gs);

            foreach (GameObject n in neighbors)
            {
                GridSpace gridSpace = new GridSpace(n, tile.gs, tile.dist);
                pq.Insert(gridSpace);
            }

            return dijkstra(path, pq, dest);
        }

        private List<GameObject> FindNeighboringTiles(GameObject tile)
        {
            List<GameObject> tiles = new List<GameObject>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    // checks for existence of adjacent tile in all 8 directions
                    Vector3 dir = new Vector3(i, 0, j);
                    GameObject t = CheckTile(tile.transform.position + dir);
                    if (t != null) tiles.Add(t);
                }
            }
            return tiles;
        }

        private GameObject CheckTile(Vector3 position)
        {
            Vector3 halfExtents = new Vector3(0.5f, 0, 0.5f);
            Collider[] colliders = Physics.OverlapBox(position, halfExtents);

            foreach (Collider item in colliders)
            {
                Tile tile = item.GetComponent<Tile>();
                if (tile != null && !tile.hasActorOn) return tile.gameObject;
            }
            return null; // no empty tile exists in this space
        }
    }
}
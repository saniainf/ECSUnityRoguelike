using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Client
{
    public enum LayersName
    {
        Floor,
        Wall,
        Object,
        Character,
        Effect
    }

    public enum SpriteEffect
    {
        NONE,
        CHOP
    }

    public static class VExt
    {
        private static Transform GameObjectsOther = new GameObject("GameObjectsOther").transform;

        public static GameObject LayoutSpriteObjects(GameObject prefab, int x, int y, string sortingLayer, Sprite sprite)
        {
            string name = Guid.NewGuid().ToString();
            GameObject go = UnityEngine.Object.Instantiate(prefab);
            go.transform.SetParent(GameObjectsOther);
            go.transform.localPosition = new Vector2(x, y);
            go.name = ($"{name}_(x{go.transform.localPosition.x}, y{go.transform.localPosition.y})");

            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = sortingLayer;
            sr.sprite = sprite;

            return go;
        }

        public static GameObject LayoutSpriteObjects(GameObject prefab, int x, int y, string name, Transform parent, string sortingLayer, Sprite sprite)
        {
            GameObject go = UnityEngine.Object.Instantiate(prefab);
            go.transform.SetParent(parent);
            go.transform.localPosition = new Vector2(x, y);
            go.name = ($"{name}_(x{go.transform.localPosition.x}, y{go.transform.localPosition.y})");

            SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = sortingLayer;
            sr.sprite = sprite;

            return go;
        }

        public static GameObject LayoutAnimationObjects(GameObject prefab, int x, int y, string name, Transform parent, string sortingLayer, RuntimeAnimatorController controller)
        {
            GameObject go = LayoutSpriteObjects(prefab, x, y, name, parent, sortingLayer, null);

            Animator animator = go.GetComponent<Animator>();
            animator.runtimeAnimatorController = controller;

            return go;
        }

        public static Vector2Int ToInt2(this Vector2 v)
        {
            return new Vector2Int((int)v.x, (int)v.y);
        }

        public static void ReverseArray<T>(ref T[,] array)
        {
            T[,] arrayReverse = new T[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    arrayReverse[i, j] = array[array.GetLength(0) - 1 - i, j];
                }

            array = arrayReverse;
        }

        public static T[] ExtractSubArray<T>(T[] array, int[] indx)
        {
            T[] result = new T[indx.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = array[indx[i]];

            return result;
        }

        public static T NextFromArray<T>(T[] array)
        {
            int index = Random.Range(0, array.Length);
            return array[index];
        }

        public static T NextFromList<T>(List<T> list)
        {
            int i = Random.Range(0, list.Count);
            return list[i];
        }

        public static T NextEnum<T>()
        {
            var array = (T[])Enum.GetValues(typeof(T));
            return NextFromArray(array);
        }
    }
}
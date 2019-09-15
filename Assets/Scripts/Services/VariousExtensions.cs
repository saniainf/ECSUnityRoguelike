using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Client
{
    enum LayersName
    {
        Floor,
        Wall,
        Object
    }

    public static class VExt
    {
        public static Random random = new Random();

        public static Vector3 ToVector3(this Vector2Int vector, float height = 0)
        {
            return new Vector3(vector.x, height, vector.y);
        }

        public static Vector2Int ToVector2Int(this Vector3 vector)
        {
            return new Vector2Int
            {
                x = Mathf.RoundToInt(vector.x),
                y = Mathf.RoundToInt(vector.z),
            };
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

    struct Coords
    {
        public int X;
        public int Y;
    }
}
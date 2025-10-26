using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HandyScripts
{
    public static class Randomizer
    {
        public static int RandomNumber(int min, int max)
        {
            return Random.Range(min, max);
        }

        public static T RandomElement<T>(IEnumerable<T> elements)
        {
            List<T> elementsList = elements.ToList();
            int elementsCount = elementsList.Count();
            int randomIndex = Random.Range(0, elementsCount);
            return elementsList.ElementAt(randomIndex);
        }

        public static bool RandomBool()
        {
            return Random.Range(0, 2) == 1;
        }

        public static bool RandomBool(float trueProbability)
        {
            if (trueProbability < 0f || trueProbability > 1f)
                throw new ArgumentException("Probability must be between 0 and 1");

            return Random.Range(0f, 1f) < trueProbability;
        }

        public static float RandomFloat(float min, float max)
        {
            return Random.Range(min, max);
        }

        public static Vector2 RandomDirection()
        {
            return Random.insideUnitCircle;
        }

        public static Vector3 RandomDirection3D()
        {
            return Random.insideUnitSphere;
        }

        public static T WeightedRandomElement<T>(IEnumerable<T> elements, IEnumerable<int> weights)
        {
            List<T> elementList = elements.ToList();
            List<int> weightsList = weights.ToList();

            if (elementList.Count != weightsList.Count)
                throw new ArgumentException("There has to be the same number of weights as elements");

            if (weightsList.Any(w => w < 0))
                throw new ArgumentException("Weights cannot be negative");

            int totalWeight = weightsList.Sum();
            if (totalWeight <= 0)
                throw new ArgumentException("Total weight must be greater than zero");

            int randomValue = Random.Range(0, totalWeight);

            int cumulativeWeight = 0;
            for (int i = 0; i < elementList.Count; i++)
            {
                cumulativeWeight += weightsList[i];
                if (randomValue < cumulativeWeight)
                    return elementList[i];
            }

            throw new InvalidOperationException("Failed to select a weighted random element");
        }

        public static T WeightedRandomElement<T>(IEnumerable<T> elements, IEnumerable<float> weights)
        {
            List<T> elementList = elements.ToList();
            List<float> weightsList = weights.ToList();
            if (elementList.Count != weightsList.Count)
                throw new ArgumentException("There has to be the same number of weights as elements");

            if (weightsList.Any(w => w < 0))
                throw new ArgumentException("Weights cannot be negative");

            float totalWeight = weightsList.Sum();
            if (totalWeight <= 0)
                throw new ArgumentException("Total weight must be greater than zero");

            float randomValue = Random.Range(0, totalWeight);

            float cumulativeWeight = 0;
            for (int i = 0; i < elementList.Count; i++)
            {
                cumulativeWeight += weightsList[i];
                if (randomValue < cumulativeWeight)
                    return elementList[i];
            }

            throw new InvalidOperationException("Failed to select a weighted random element");
        }


        public static T WeightedRandomElement<T>(Dictionary<T, int> weightsByElement)
        {
            Dictionary<T, int>.KeyCollection elements = weightsByElement.Keys;
            Dictionary<T, int>.ValueCollection values = weightsByElement.Values;

            return WeightedRandomElement(elements, values);
        }

        public static T WeightedRandomElement<T>(Dictionary<T, float> weightsByElement)
        {
            Dictionary<T, float>.KeyCollection elements = weightsByElement.Keys;
            Dictionary<T, float>.ValueCollection values = weightsByElement.Values;

            return WeightedRandomElement(elements, values);
        }

        public static List<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            List<T> list = source.ToList();
            int n = list.Count;


            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }

            return list;
        }

        public static T RandomPickAndRemove<T>(List<T> elements)
        {
            if (elements == null || elements.Count == 0)
                throw new InvalidOperationException("The list is empty or null.");

            int randomIndex = Random.Range(0, elements.Count);
            T selectedElement = elements[randomIndex];
            elements.RemoveAt(randomIndex);
            return selectedElement;
        }

        public static Vector3 RandomPointInBounds(Bounds bounds)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            float z = Random.Range(bounds.min.z, bounds.max.z);
            return new Vector3(x, y, z);
        }
    }
}
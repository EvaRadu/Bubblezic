﻿using UnityEngine;

namespace Assets.Script
{
    [System.Serializable]
    public class Bulle
    {
        public int id;
        public float posX;
        public float posY;
        public string couleur;
        public int rayon;
        public float temps;
        public int type;

        public static Bulle CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<Bulle>(jsonString);
        }
    }
}
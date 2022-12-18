﻿using System;
using UnityEngine;
//Classe contenant nos Objets Bubble deserialized.
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
        public float duration;
        public float rotation;
        public int side;
        public string texture;
        public Boolean created = false;

        public static Bulle CreateFromJSON(string jsonString)
        {
            return JsonUtility.FromJson<Bulle>(jsonString);
        }
    }
}

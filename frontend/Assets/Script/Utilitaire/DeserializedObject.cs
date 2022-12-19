using System;
using UnityEngine;
using Object = System.Object;
//Classe contenant nos Objets Bubble deserialized.
namespace Assets.Script
{
    [System.Serializable] public struct CommonFields
    {
        public string typeName;

    }


    public class myObjects : Object
    {
        public int id;
        public float posX;
        public float posY;
        public string couleur;
        public float temps;
        public Boolean created = false;
        public float duration;
    }

    [System.Serializable]
    public class Trajectoire : myObjects
    { 
        public int idBubble;
        public int idCible;
        public int width;
        public int height;
    }

    [System.Serializable]
    public class Bulle : myObjects
    {
        public int rayon;
        public int type;
        public int rotation;
        public int side;
        public string texture;

    }


    public class DesarializedObject
    {
        
        public String typeName;

        public static String typeForTheList(string jsonString)
        {
            var cf = JsonUtility.FromJson<CommonFields>(jsonString);
            return cf.typeName;
        }


        public static myObjects CreateFromJSON(string jsonString)
        {
            switch (typeForTheList(jsonString))
            {
                case "bubble":
                    return JsonUtility.FromJson<Bulle>(jsonString);
                default:
                    return JsonUtility.FromJson<Trajectoire>(jsonString);
             };
        }
    }
}


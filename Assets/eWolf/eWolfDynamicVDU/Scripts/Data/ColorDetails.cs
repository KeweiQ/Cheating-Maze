using System;
using UnityEngine;

namespace eWolf.eWolfDynamicVDU.Scripts.Data
{
    [Serializable]
    public class ColorDetails
    {
        public Color BackGround = Color.black;
        public Color Border = Color.white;
        public Color Main = Color.green;
        public Color Warning = Color.yellow;
        public Color Error = Color.red;
    }
}

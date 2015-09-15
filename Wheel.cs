using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using UnityEngine;
using spaar.ModLoader;
using System.Runtime.Serialization.Formatters.Binary;
using TheGuysYouDespise;

namespace Wheel
{
    public class BesiegeModLoader : Mod{
        public override string Name { get { return "wheelMod"; } }
        public override string DisplayName { get { return "wheel Mod"; } }
        public override string BesiegeVersion { get { return "v0.11"; } }
        public override string Author { get { return "覅是 & tony1020"; } }
        public override Version Version { get { return new Version(100, 100); } }
        public override bool CanBeUnloaded { get { return true; } }
        public bool enabled;

        public GameObject temp;

        VisualOffset offset = new VisualOffset(Vector3.zero, Vector3.zero, Vector3.zero);

        Icon icon = new Icon(0.2f, new Vector3(-0.1f, -0.1f, 1f), new Vector3(-90f, 45f, 0f));

        BlockProperties blockProp = new BlockProperties().Slider("Speed",0f,5f,1f).Key1("Forward","up").Key2("backward","down");
        List<ColliderComposite> points2 = new List<ColliderComposite> {new ColliderComposite("wheel.obj", new Vector3(0,0,0),new Vector3(0,0,0))
    };
        WheelCollider points3 = new WheelCollider();//Not using
        
        
        Type[] scripts = new Type[] { typeof(wheelMod) };

        List<NeededResource> neededResources = new List<NeededResource> { /*new NeededResource(ResourceType.Audio, "Wheel.ogg")*/ };



        public override void OnLoad()
        {
            //GameObject temp = new GameObject ();
            List<AddingPoint> points = new List<AddingPoint> { (AddingPoint)new BasePoint(true, true).Motionable(false/*旋转*/, false,false).SetStickyRadius(0.5f),
                                                    /*new AddingPoint(new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 0f)),
                                                    new AddingPoint(new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 90f)),
                                                    new AddingPoint(new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 180f)),
                                                    new AddingPoint(new Vector3(0f, 0f, 0.5f), new Vector3(0f, 0f, 270f)),
                                                    //Top:
                                                    new AddingPoint(new Vector3(0f, 0f, 0.5f), new Vector3(-90f, 0f, 0f), true)*/ };
            BlockLoader.AddModBlock(501, "wheel.obj", "whell_map.png", "Morden Wheel", icon, blockProp, points2, neededResources, 2f, offset, scripts, false, points);
            //temp.AddComponent<ChiBanMod> ();
            //GameObject.DontDestroyOnLoad (temp);
        }
        public override void OnUnload()
        {
            //GameObject.Destroy (temp);
        }
    }


    public class wheelMod : MonoBehaviour
    {
        public Vector3 Torque;
        void Start()
        {        }
        void Update()
        {
            Collider col = this.GetComponentInChildren<Collider>();//Problems are here :/
            col.material.bounciness = 0.75f;
            col.material.dynamicFriction = 1;
            col.material.staticFriction = 1;
            //col.material.bounceCombine = PhysicMaterialCombine.Maximum;
            if (AddPiece.isSimulating)
            {
                if (Input.GetKeyDown(this.GetComponent<MyBlockInfo>().key1)) { Torque = new Vector3(this.GetComponent<MyBlockInfo>().sliderValue, -0.01f /*this.GetComponent<MyBlockInfo>().sliderValue*/, 0f); }
                if ((Input.GetKeyDown(this.GetComponent<MyBlockInfo>().key2))) { Torque = new Vector3(-this.GetComponent<MyBlockInfo>().sliderValue, 0.01f/* this.GetComponent<MyBlockInfo>().sliderValue*/, 0f); }
                rigidbody.AddTorque(Torque*1);//This is not working, too
                Torque = new Vector3(0,0,0);
            }
           
            
        }
    }
}

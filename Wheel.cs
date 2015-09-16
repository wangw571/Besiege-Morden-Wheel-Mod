    using System;
    using System.Collections.Generic;
    using spaar.ModLoader;
    using TheGuysYouDespise;
    using UnityEngine;

    namespace Blocks
    {
        public class WheelMod : BlockMod
        {
            public override Version Version { get { return new Version("1.0"); } }
            public override string Name { get { return "wheelMod"; } }
            public override string DisplayName { get { return "wheel Mod"; } }
            public override string BesiegeVersion { get { return "v0.11"; } }
            public override string Author { get { return "覅是 & tony1020"; } }
        protected Block wheel = new Block()
                .ID(501)
                .TextureFile("whell_map.png")
                .BlockName("Morden Wheel")
                .Obj(new List<Obj> { new Obj("wheel.obj", new VisualOffset(Vector3.one, Vector3.zero, Vector3.zero)) })//
                .Scripts(new Type[] { typeof(wheelModS) })
                .Properties(new BlockProperties()//.Slider("Speed", 0f, 5f, 1f).Key1("Forward", "8").Key2("backward", "2")*/
                                                 /*.ToggleModeEnabled("Freeze me", false)*/
                                                 .CanBeDamaged(3)
                                                 )
                .Mass(3f)
                .IconOffset(new Icon(0.2f, new Vector3(-0.1f, -0.1f, 1f), new Vector3(-90f, 45f, 0f)))
                .ShowCollider(true)
                .CompoundCollider(new List<ColliderComposite> { new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 0f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 10f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 20f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 30f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 40f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 50f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 60f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 70f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 80f)/*角度*/)})
                .AddingPoints(new List<AddingPoint> {(AddingPoint)new BasePoint(true, true).Motionable(false, false,false)})
                .NeededResources(new List<NeededResource>()
                );
            public override void OnLoad()
            {
                LoadFancyBlock(wheel);
            }
            public override void OnUnload() { }
        }


        public class wheelModS : BlockScript
        {
        public Vector3 Torque;
        public Collider coll;
        /*public float diffx;
        public float diffy;
        public float diffz;
        public float sv;*/
        protected override void OnSimulateStart() {
            //Thank ITR for those bounciness and friction!
            Collider[] allColliders = this.GetComponentsInChildren<Collider>();
            foreach (Collider col in allColliders){
                col.material.bounciness = 0.5f;
                col.material.dynamicFriction = 1;
                col.material.staticFriction = 1;
                col.material.bounceCombine = PhysicMaterialCombine.Maximum;
                col.material.frictionCombine = PhysicMaterialCombine.Minimum;
            }
            /*sv = 30*this.GetComponent<MyBlockInfo>().sliderValue;*/
        }
            


        protected override void OnSimulateUpdate()
        {
            
        }
        protected override void OnSimulateFixedUpdate()
        {
            if (AddPiece.isSimulating)
            {
                
                /*diffz = Vector3.Angle(transform.forward, Vector3.forward);
                diffx = Vector3.Angle(transform.up, Vector3.up);
                diffy = Vector3.Angle(transform.right, Vector3.right);
                Torque = new Vector3(diffx/180 * sv, diffy/180 * sv, diffz/180 * sv);
                if (Input.GetKey(this.GetComponent<MyBlockInfo>().key1)) { rigidbody.AddTorque(Torque * 1); }
                if (Input.GetKey(this.GetComponent<MyBlockInfo>().key2)) { rigidbody.AddTorque(Torque * -1); }*/
                //Physics stuff
            }
        }

        protected override void OnSimulateExit()
        {
        }
    }
    }
    




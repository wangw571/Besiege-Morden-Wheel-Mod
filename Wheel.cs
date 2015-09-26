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
                .TextureFile("wheel.png")
                .BlockName("Morden Wheel")
                .Obj(new List<Obj> { new Obj("wheel.obj", new VisualOffset(Vector3.one, Vector3.zero, Vector3.zero)) })
                .Scripts(new Type[] { typeof(wheelModS) })
                .Properties(new BlockProperties().Slider("Speed", 0f, 5f, 1f).Key1("Forward", "[8]").Key2("backward", "[2]")
                                                 .ToggleModeEnabled("Flip Me", false)
                                                 .CanBeDamaged(3)

                                                 )
                .Mass(3f)
                .IconOffset(new Icon(0.3f, new Vector3(0f, 0f, 1f), new Vector3(-90f, 45f, 0f)))//第一个float是图标缩放，五六七是我找的比较好的角度，另外大小你最好不要设0.2f，我轮子ICON太大才0.2的
                .ShowCollider(true)
                .CompoundCollider(new List<ColliderComposite>/*碰撞箱，这里用的都是方块碰撞箱，其他的还有球，胶囊，网格（不规则），你看看引用就好*/ { new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 0f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 10f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 20f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 30f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 40f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 50f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 60f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 70f)/*角度*/),
            new ColliderComposite(new Vector3(5f, 5f, 1.4f)/*大小*/, new Vector3(0f, 0f, 0.7f)/*位置*/, new Vector3(0f, 0f, 80f)/*角度*/)})
                .AddingPoints(new List<AddingPoint> { (AddingPoint)new BasePoint(true, true).Motionable(true, false, false) })//连接点，这里只有一个底点，motionable就是能不能活动;添加别的连接点就用, (AddingPoint) new AddingPoint(Vector3坐标,vector3角度，bool是否粘性)
                .NeededResources(new List<NeededResource>()//需要的资源，例如音乐
                );
        public override void OnLoad()
        {
            LoadFancyBlock(wheel);//加载该模块
        }
        public override void OnUnload() { }
    }


    public class wheelModS : BlockScript
    {
        public Collider coll;
        public float sv;
        public int flippp;
        protected override void OnSimulateStart()
        {
            Collider[] allColliders = this.GetComponentsInChildren<Collider>();
            foreach (Collider col in allColliders)
            {
                col.material.bounciness = 0.3f;
                col.material.dynamicFriction = 1;
                col.material.staticFriction = 1;
                col.material.bounceCombine = PhysicMaterialCombine.Maximum;
                col.material.frictionCombine = PhysicMaterialCombine.Minimum;
            }
            sv = 600 * this.GetComponent<MyBlockInfo>().sliderValue;


        }


        protected override void MonoFixedUpdate()
        {
            base.MonoFixedUpdate();
            if (this.GetComponent<MyBlockInfo>().toggleModeEnabled) { flippp = 1; }
            else { flippp = -1; }
        }
        protected override void OnSimulateFixedUpdate()
        {
            if (AddPiece.isSimulating)
            {
                if (Input.GetKey(this.GetComponent<MyBlockInfo>().key2)) { rigidbody.AddTorque(transform.forward * -flippp * sv); }
                if (Input.GetKey(this.GetComponent<MyBlockInfo>().key1)) { rigidbody.AddTorque(transform.forward * flippp * sv); }
                //Physics stuff
            }
        }

        protected override void OnSimulateExit()
        {
        }
    }
}





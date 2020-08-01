using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using UnityEngine;
using MonoMod.Utils;
using MonoMod.RuntimeDetour;
using MonoMod.RuntimeDetour.HookGen;
using MultiplayerBasicExample;

namespace ExampleMod
{
     class Chippy : IounStoneOrbitalItem
    {
        public static void Init()
        {
            string name = "Chippy";

            string resourceName = "ExampleMod/Resources/Chippy.png";


            GameObject gameObject = new GameObject();
          
            
            var item = gameObject.AddComponent<Chippy>();

            ItemBuilder.AddSpriteToObject(name, resourceName, gameObject);

            string shortDesc = "HELLO YELLO";
            string longDesc = "Orbits player as a protective shield\n\n" +
                "A small friend from a far off planet full of turtles, plumbers, and world destroying items.\n\n" +
                "For some reason she will not shut up about these two italians she's friends with";

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "frm");

            item.quality = PickupObject.ItemQuality.C;
            Chippy.BuildPrefab();
            item.OrbitalPrefab = Chippy.orbitalPrefab;
            item.Identifier = IounStoneOrbitalItem.IounStoneIdentifier.GENERIC;
        }

        public static void BuildPrefab()
        {
            bool flag = Chippy.orbitalPrefab != null;
            if (!flag)
            {
                GameObject gameObject = SpriteBuilder.SpriteFromResource("ExampleMod/Resources/Chippy.png", null);
                gameObject.name = "Chippy Orbital";
                SpeculativeRigidbody speculativeRigidbody = gameObject.GetComponent<tk2dSprite>().SetUpSpeculativeRigidbody(IntVector2.Zero, new IntVector2(7, 13));
                speculativeRigidbody.CollideWithTileMap = false;
                speculativeRigidbody.CollideWithOthers = true;
                speculativeRigidbody.PrimaryPixelCollider.CollisionLayer = CollisionLayer.EnemyBulletBlocker;
                Chippy.orbitalPrefab = gameObject.AddComponent<PlayerOrbital>();
                Chippy.orbitalPrefab.motionStyle = PlayerOrbital.OrbitalMotionStyle.ORBIT_PLAYER_ALWAYS;
                Chippy.orbitalPrefab.shouldRotate = false;
                Chippy.orbitalPrefab.orbitRadius = 2.5f;
                Chippy.orbitalPrefab.SetOrbitalTier(0);
                UnityEngine.Object.DontDestroyOnLoad(gameObject);
                FakePrefab.MarkAsFakePrefab(gameObject);
                gameObject.SetActive(false);

            }

        }

        public override void Pickup(PlayerController player)
        {
            foreach (IPlayerOrbital playerOrbital in player.orbitals)
            {
                PlayerOrbital playerOrbital2 = (PlayerOrbital)playerOrbital;
                playerOrbital2.orbitDegreesPerSecond = 90f;
            }
            base.Pickup(player);
        }

        public override DebrisObject Drop(PlayerController player)
        {
            Chippy.speedUp = false;
            
            return base.Drop(player);
        }

        public static bool speedUp = false;

        public static PlayerOrbital orbitalPrefab;
    }
}

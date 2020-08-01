using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using UnityEngine;
using MonoMod.Utils;
using MonoMod.RuntimeDetour;
using MonoMod.ModInterop;
using MultiplayerBasicExample;
namespace ExampleMod
{
    class InkyHeart : PlayerItem

    {
        public static void Init()
        {
            string itemName = "Inky Heart";
            string resourceName = "ExampleMod/Resources/Inkyheart";

            GameObject obj = new GameObject(itemName);

            var item = obj.AddComponent<InkyHeart>();

            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);


            string shortDesc = "Uhhh...That's Still Moving...";
            string longDesc = "Heals player\n\n" + "This appears to be a heart that is not only made out of ink, but is constantly dripping as if it has a infinite source...weird.";

            ItemBuilder.SetupItem(item, shortDesc, longDesc, "frm");
            ItemBuilder.SetCooldownType(item, ItemBuilder.CooldownType.None, 1f);

            ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.Curse, 1);
            item.consumable = true;
            item.quality = ItemQuality.B;
        }

        protected override void DoEffect(PlayerController user)
        {
            float curHealth = user.healthHaver.GetCurrentHealth();
            if (curHealth >= .5f)
            {
                AkSoundEngine.PostEvent("Play_OBJ_dead_again_01", base.gameObject);
                user.healthHaver.ForceSetCurrentHealth(curHealth + 70);
            }
            
        }

        public override bool CanBeUsed(PlayerController user)
        {
            return user.healthHaver.GetCurrentHealth() >= .5f ;
        }


        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            Tools.Print($"Player picked up {this.DisplayName}");
        }








    }
}

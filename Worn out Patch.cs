using ItemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ExampleMod
{

    
        public class Wornoutpatch : PassiveItem
        {
            //Call this method from the Start() method of your ETGModule extension
            public static void Register()
            {
                //The name of the item
                string itemName = "Worn Out Patch";

                //Refers to an embedded png in the project. Make sure to embed your resources! Google it
                string resourceName = "ExampleMod/Resources/Wornoutpatch";

                //Create new GameObject
                GameObject obj = new GameObject(itemName);

                //Add a PassiveItem component to the object
                var item = obj.AddComponent<Wornoutpatch>();

                //Adds a sprite component to the object and adds your texture to the item sprite collection
                ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);

                //Ammonomicon entry variables
                string shortDesc = "Do A Good Turn Daily";
            string longDesc = "Increases Item capacity and ammo capacity\n\n" +
                "Worn by youth around the world as a symbol of their organization.\n\n" + "Someone, somewhere, is probably pissed that this old patch fell off.";

                //Adds the item to the gungeon item list, the ammonomicon, the loot table, etc.
                //Do this after ItemBuilder.AddSpriteToObject!
                ItemBuilder.SetupItem(item, shortDesc, longDesc, "frm");

                //Adds the actual passive effect to the item
                ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AdditionalItemCapacity, 1, StatModifier.ModifyMethod.ADDITIVE);
                ItemBuilder.AddPassiveStatModifier(item, PlayerStats.StatType.AmmoCapacityMultiplier, 2, StatModifier.ModifyMethod.MULTIPLICATIVE);

                //Set the rarity of the item
                item.quality = PickupObject.ItemQuality.B;
            }

            public override void Pickup(PlayerController player)
            {
                base.Pickup(player);
                Tools.Print($"Player picked up {this.DisplayName}");
            }

            public override DebrisObject Drop(PlayerController player)
            {
                Tools.Print($"Player dropped {this.DisplayName}");
                return base.Drop(player);
            }
        }
    }



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindHelper
{
    public struct ItemFinder
    {
        public static WeaponTemplate FindItem(int code)
        {
            return Resources.Load($"WeaponData/{code}") as WeaponTemplate;
        }

        public static Sprite FindItemSprite(int code)
        {
            if (code < 200)
                return Resources.Load<Sprite>($"WeaponSprite/{code}"); //<- 이거로 하면 잘됨
            else
                return Resources.Load<Sprite>($"ItemSprite/{code}");
        }
    }

    public struct SkinFinder
    {
        public static SkinData FindSkin(int code)
        {
            return Resources.Load($"SkinData/{code}") as SkinData;
        }
    }



}


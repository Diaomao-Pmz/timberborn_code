using System;
using Timberborn.Modding;
using Timberborn.ModdingUI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.ModManagerSceneUI
{
	// Token: 0x02000004 RID: 4
	public class ModManagerSceneItemFactory : MonoBehaviour, IModItemFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public ModItem CreateModItem(Mod mod, Action<Mod, bool> onPriorityIncreased, Action<Mod, bool> onPriorityDecreased)
		{
			VisualElement root = this._modItemVisualTreeAsset.CloneTree().ElementAt(0);
			ModItem modItem = new ModItem(base.GetComponent<ModManagerSceneTooltipRegistrar>(), root, mod, new Func<bool>(ModManagerSceneItemFactory.IsShiftPressed));
			modItem.Initialize(onPriorityIncreased, onPriorityDecreased);
			return modItem;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002100 File Offset: 0x00000300
		public static bool IsShiftPressed()
		{
			return Keyboard.current[51].isPressed || Keyboard.current[52].isPressed;
		}

		// Token: 0x04000006 RID: 6
		[SerializeField]
		public VisualTreeAsset _modItemVisualTreeAsset;
	}
}

using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;

namespace Timberborn.KeyBindingSystem
{
	// Token: 0x0200001E RID: 30
	public class KeyBindingRegistry : ILoadableSingleton
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00003C32 File Offset: 0x00001E32
		public KeyBindingRegistry(IKeyBindingBlocker keyBindingBlocker, KeyBindingSpecService keyBindingSpecService, KeyBindingFactory keyBindingFactory)
		{
			this._keyBindingBlocker = keyBindingBlocker;
			this._keyBindingSpecService = keyBindingSpecService;
			this._keyBindingFactory = keyBindingFactory;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003C65 File Offset: 0x00001E65
		public ReadOnlyList<KeyBinding> KeyBindings
		{
			get
			{
				return this._keyBindings.AsReadOnlyList<KeyBinding>();
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003C74 File Offset: 0x00001E74
		public void Load()
		{
			foreach (KeyBindingDefinition keyBindingDefinition in this._keyBindingSpecService.KeyBindingDefinitions)
			{
				this.Add(keyBindingDefinition);
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003CD0 File Offset: 0x00001ED0
		public bool IsDown(string id)
		{
			KeyBinding keyBinding = this._keyBindingsById[id];
			return !this._keyBindingBlocker.IsKeyBlocked(keyBinding) && keyBinding.IsDown;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003D00 File Offset: 0x00001F00
		public bool IsHeld(string id)
		{
			KeyBinding keyBinding = this._keyBindingsById[id];
			return !this._keyBindingBlocker.IsKeyBlocked(keyBinding) && keyBinding.IsHeld;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003D30 File Offset: 0x00001F30
		public bool IsLongHeld(string id)
		{
			KeyBinding keyBinding = this._keyBindingsById[id];
			return !this._keyBindingBlocker.IsKeyBlocked(keyBinding) && keyBinding.IsLongHeld;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003D60 File Offset: 0x00001F60
		public bool IsUp(string id)
		{
			KeyBinding keyBinding = this._keyBindingsById[id];
			return !this._keyBindingBlocker.IsKeyBlocked(keyBinding) && keyBinding.IsUp;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003D90 File Offset: 0x00001F90
		public bool IsUpAfterShortHeld(string id)
		{
			KeyBinding keyBinding = this._keyBindingsById[id];
			return !this._keyBindingBlocker.IsKeyBlocked(keyBinding) && keyBinding.IsUpAfterShortHeld;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003DC0 File Offset: 0x00001FC0
		public float GetRawValue(string id)
		{
			KeyBinding keyBinding = this._keyBindingsById[id];
			if (this._keyBindingBlocker.IsKeyBlocked(keyBinding))
			{
				return 0f;
			}
			return keyBinding.GetRawValue();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public KeyBinding Get(string id)
		{
			return this._keyBindingsById[id];
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003E04 File Offset: 0x00002004
		public void Add(KeyBindingDefinition keyBindingDefinition)
		{
			KeyBinding keyBinding = this._keyBindingFactory.Create(keyBindingDefinition);
			this._keyBindings.Add(keyBinding);
			this._keyBindingsById.Add(keyBinding.Id, keyBinding);
		}

		// Token: 0x04000056 RID: 86
		public readonly IKeyBindingBlocker _keyBindingBlocker;

		// Token: 0x04000057 RID: 87
		public readonly KeyBindingSpecService _keyBindingSpecService;

		// Token: 0x04000058 RID: 88
		public readonly KeyBindingFactory _keyBindingFactory;

		// Token: 0x04000059 RID: 89
		public readonly List<KeyBinding> _keyBindings = new List<KeyBinding>();

		// Token: 0x0400005A RID: 90
		public readonly Dictionary<string, KeyBinding> _keyBindingsById = new Dictionary<string, KeyBinding>();
	}
}

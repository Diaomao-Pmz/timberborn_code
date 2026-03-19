using System;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.SoundSystem;
using UnityEngine;

namespace Timberborn.GameSound
{
	// Token: 0x0200000E RID: 14
	public class SoundListenerDebugger : ILoadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00002EF8 File Offset: 0x000010F8
		public SoundListenerDebugger(ISoundSystem soundSystem, RootObjectProvider rootObjectProvider)
		{
			this._soundSystem = soundSystem;
			this._rootObjectProvider = rootObjectProvider;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002F10 File Offset: 0x00001110
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("SoundListenerDebugger");
			Transform transform = GameObject.CreatePrimitive(2).transform;
			transform.SetParent(this._parent.transform);
			transform.localScale = new Vector3(0.1f, 10f, 0.1f);
			transform.localPosition = new Vector3(0f, -10f, 0f);
			Object.Destroy(transform.GetComponent<Collider>());
			Transform transform2 = GameObject.CreatePrimitive(0).transform;
			transform2.SetParent(this._parent.transform);
			transform2.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			Object.Destroy(transform2.GetComponent<Collider>());
			this._parent.SetActive(false);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002FD9 File Offset: 0x000011D9
		public void LateUpdateSingleton()
		{
			this._parent.transform.position = this._soundSystem.ListenerPosition;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FF6 File Offset: 0x000011F6
		public void ToggleActive()
		{
			this._parent.SetActive(!this._parent.activeSelf);
		}

		// Token: 0x0400002C RID: 44
		public readonly ISoundSystem _soundSystem;

		// Token: 0x0400002D RID: 45
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400002E RID: 46
		public GameObject _parent;
	}
}

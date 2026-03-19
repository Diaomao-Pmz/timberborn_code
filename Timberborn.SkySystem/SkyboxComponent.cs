using System;
using Bindito.Core;
using UnityEngine;

namespace Timberborn.SkySystem
{
	// Token: 0x0200000E RID: 14
	public class SkyboxComponent : MonoBehaviour
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002D75 File Offset: 0x00000F75
		[Inject]
		public void InjectDependencies(SkyboxPositioner skyboxPositioner)
		{
			this._skyboxPositioner = skyboxPositioner;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D7E File Offset: 0x00000F7E
		public void Start()
		{
			base.GetComponent<Skybox>().material = this._skyboxPositioner.SkyboxMaterial;
		}

		// Token: 0x04000028 RID: 40
		[HideInInspector]
		public SkyboxPositioner _skyboxPositioner;
	}
}

using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.TemplateAttachmentSystem;
using UnityEngine;

namespace Timberborn.Illumination
{
	// Token: 0x02000015 RID: 21
	public class IlluminatorLightObjects : BaseComponent, IInitializableEntity
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x0000339C File Offset: 0x0000159C
		public void InitializeEntity()
		{
			TemplateAttachments component = base.GetComponent<TemplateAttachments>();
			foreach (string id in base.GetComponent<IlluminatorLightObjectsSpec>().AttachmentIds)
			{
				TemplateAttachment orCreateAttachment = component.GetOrCreateAttachment(id);
				this._lightObjects.AddRange(orCreateAttachment.GameObject.GetComponentsInChildren<Light>());
			}
			this.SetActive(false);
			if (this._lightObjects.Count == 0)
			{
				throw new NotSupportedException("No lights found in IlluminatorLightObjects on " + base.Name + ".");
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003428 File Offset: 0x00001628
		public void SetActive(bool isActive)
		{
			foreach (Light light in this._lightObjects)
			{
				light.enabled = isActive;
			}
		}

		// Token: 0x04000038 RID: 56
		public readonly List<Light> _lightObjects = new List<Light>();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.PrefabOptimization;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.TemplateAttachmentSystem
{
	// Token: 0x0200000A RID: 10
	public class TemplateAttachments : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002672 File Offset: 0x00000872
		public TemplateAttachments(OptimizedPrefabInstantiator optimizedPrefabInstantiator)
		{
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000268C File Offset: 0x0000088C
		public void Awake()
		{
			this._entityMaterials = base.GetComponent<EntityMaterials>();
			this._materialLightingRenderers = base.GetComponent<MaterialLightingRenderers>();
			this._highlightableObject = base.GetComponent<HighlightableObject>();
			this._templateAttachmentsSpec = base.GetComponent<TemplateAttachmentsSpec>();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026C0 File Offset: 0x000008C0
		public void InitializeEntity()
		{
			foreach (AttachmentDefinition attachmentDefinition in this._templateAttachmentsSpec.Attachments)
			{
				if (attachmentDefinition.CreateInstantly)
				{
					this.GetOrCreateAttachment(attachmentDefinition.AttachmentId);
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000270C File Offset: 0x0000090C
		public TemplateAttachment GetOrCreateAttachment(string id)
		{
			TemplateAttachment templateAttachment;
			if (!this._attachmentCache.TryGetValue(id, out templateAttachment))
			{
				AttachmentDefinition attachmentDefinition = this.GetAttachmentDefinition(id);
				templateAttachment = this.CreateAttachment(attachmentDefinition);
				templateAttachment.ActiveStateChanged += this.OnActiveStateChanged;
				this._attachmentCache.Add(id, templateAttachment);
			}
			return templateAttachment;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002759 File Offset: 0x00000959
		public bool HasAttachment(string id)
		{
			return this._attachmentCache.ContainsKey(id);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002768 File Offset: 0x00000968
		public AttachmentDefinition GetAttachmentDefinition(string id)
		{
			AttachmentDefinition attachmentDefinition = this._templateAttachmentsSpec.Attachments.SingleOrDefault((AttachmentDefinition a) => a.AttachmentId == id);
			if (attachmentDefinition != null)
			{
				return attachmentDefinition;
			}
			throw new KeyNotFoundException("Attachment with ID '" + id + "' not found for " + base.GameObject.name);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000027D0 File Offset: 0x000009D0
		public TemplateAttachment CreateAttachment(AttachmentDefinition attachmentDefinition)
		{
			GameObject asset = attachmentDefinition.Prefab.Asset;
			Transform parent = base.GameObject.FindChildTransform(attachmentDefinition.Parent);
			GameObject gameObject = this._optimizedPrefabInstantiator.Instantiate(asset, parent);
			gameObject.transform.SetLocalPositionAndRotation(attachmentDefinition.Position, Quaternion.Euler(attachmentDefinition.Rotation));
			gameObject.transform.localScale = attachmentDefinition.Scale;
			this._entityMaterials.AddMaterials(gameObject);
			this._materialLightingRenderers.CollectRenderers();
			return new TemplateAttachment(gameObject);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002853 File Offset: 0x00000A53
		public void OnActiveStateChanged(object sender, bool isActive)
		{
			if (isActive)
			{
				this._highlightableObject.RefreshHighlight();
			}
		}

		// Token: 0x04000015 RID: 21
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x04000016 RID: 22
		public EntityMaterials _entityMaterials;

		// Token: 0x04000017 RID: 23
		public MaterialLightingRenderers _materialLightingRenderers;

		// Token: 0x04000018 RID: 24
		public HighlightableObject _highlightableObject;

		// Token: 0x04000019 RID: 25
		public TemplateAttachmentsSpec _templateAttachmentsSpec;

		// Token: 0x0400001A RID: 26
		public readonly Dictionary<string, TemplateAttachment> _attachmentCache = new Dictionary<string, TemplateAttachment>();
	}
}

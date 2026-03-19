using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.UnderstructureSystem
{
	// Token: 0x02000008 RID: 8
	public class UnderstructureConstraint : BaseComponent, IAwakableComponent, IInitializableEntity, IPostInitializableEntity, IDeletableEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002217 File Offset: 0x00000417
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000221F File Offset: 0x0000041F
		public string ErrorMessage { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x00002228 File Offset: 0x00000428
		public UnderstructureConstraint(EntityService entityService, UnderstructureFinder understructureFinder, TemplateNameMapper templateNameMapper, ILoc loc)
		{
			this._entityService = entityService;
			this._understructureFinder = understructureFinder;
			this._templateNameMapper = templateNameMapper;
			this._loc = loc;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000224D File Offset: 0x0000044D
		public EntityComponent UnderstructureEntity
		{
			get
			{
				if (!this._initialized)
				{
					return this.FindUnderstructureEntity();
				}
				return this._understructureEntity;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002264 File Offset: 0x00000464
		public ImmutableArray<string> UnderstructureTemplateNames
		{
			get
			{
				return this._understructureConstraintSpec.UnderstructureTemplateNames;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002271 File Offset: 0x00000471
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._understructureConstraintSpec = base.GetComponent<UnderstructureConstraintSpec>();
			this._understructureConstructionSiteValidator = base.GetComponent<UnderstructureConstructionSiteValidator>();
			this.ErrorMessage = this.BuildErrorMessage();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022A3 File Offset: 0x000004A3
		public void InitializeEntity()
		{
			this._understructureEntity = this.FindUnderstructureEntity();
			this._initialized = true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022B8 File Offset: 0x000004B8
		public void PostInitializeEntity()
		{
			if (this.UnderstructureEntity)
			{
				Understructure component = this.UnderstructureEntity.GetComponent<Understructure>();
				component.Deleted += this.OnUnderstructureDeleted;
				component.EnteredFinishedState += this.OnUnderstructureEnteredFinishedState;
				return;
			}
			Debug.LogWarning("Understructure not found for " + base.Name + " - deleting it.");
			this.Delete();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002321 File Offset: 0x00000521
		public void DeleteEntity()
		{
			if (this.UnderstructureEntity)
			{
				Understructure component = this.UnderstructureEntity.GetComponent<Understructure>();
				component.Deleted -= this.OnUnderstructureDeleted;
				component.EnteredFinishedState -= this.OnUnderstructureEnteredFinishedState;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000235E File Offset: 0x0000055E
		public void OnUnderstructureDeleted(object sender, EventArgs e)
		{
			this.Delete();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002366 File Offset: 0x00000566
		public void OnUnderstructureEnteredFinishedState(object sender, EventArgs e)
		{
			this._understructureConstructionSiteValidator.Validate();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002373 File Offset: 0x00000573
		public EntityComponent FindUnderstructureEntity()
		{
			BlockObject blockObject = this._understructureFinder.FindNonStrict(this._blockObject);
			if (blockObject == null)
			{
				return null;
			}
			return blockObject.GetComponent<EntityComponent>();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002391 File Offset: 0x00000591
		public void Delete()
		{
			this._entityService.Delete(this);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023A0 File Offset: 0x000005A0
		public string BuildErrorMessage()
		{
			IEnumerable<string> values = this.UnderstructureTemplateNames.Select(new Func<string, string>(this.GetTemplateDisplayName));
			return this._loc.T<string>(UnderstructureConstraint.UnsuitableBuildingBelowLocKey, string.Join(", ", values));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023E0 File Offset: 0x000005E0
		public string GetTemplateDisplayName(string templateName)
		{
			TemplateSpec templateSpec;
			if (!this._templateNameMapper.TryGetTemplate(templateName, out templateSpec))
			{
				return templateName;
			}
			return this._loc.T(templateSpec.GetSpec<LabeledEntitySpec>().DisplayNameLocKey);
		}

		// Token: 0x0400000A RID: 10
		public static readonly string UnsuitableBuildingBelowLocKey = "Buildings.UnsuitableBuildingBelow";

		// Token: 0x0400000C RID: 12
		public readonly EntityService _entityService;

		// Token: 0x0400000D RID: 13
		public readonly UnderstructureFinder _understructureFinder;

		// Token: 0x0400000E RID: 14
		public readonly TemplateNameMapper _templateNameMapper;

		// Token: 0x0400000F RID: 15
		public readonly ILoc _loc;

		// Token: 0x04000010 RID: 16
		public BlockObject _blockObject;

		// Token: 0x04000011 RID: 17
		public UnderstructureConstraintSpec _understructureConstraintSpec;

		// Token: 0x04000012 RID: 18
		public UnderstructureConstructionSiteValidator _understructureConstructionSiteValidator;

		// Token: 0x04000013 RID: 19
		public bool _initialized;

		// Token: 0x04000014 RID: 20
		public EntityComponent _understructureEntity;
	}
}

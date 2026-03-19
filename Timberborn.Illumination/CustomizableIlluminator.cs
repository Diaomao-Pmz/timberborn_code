using System;
using Timberborn.BaseComponentSystem;
using Timberborn.DuplicationSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.Illumination
{
	// Token: 0x02000009 RID: 9
	public class CustomizableIlluminator : BaseComponent, IAwakableComponent, IInitializableEntity, IPersistentEntity, IDuplicable<CustomizableIlluminator>, IDuplicable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000019 RID: 25 RVA: 0x00002298 File Offset: 0x00000498
		// (remove) Token: 0x0600001A RID: 26 RVA: 0x000022D0 File Offset: 0x000004D0
		public event EventHandler CustomColorChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001B RID: 27 RVA: 0x00002308 File Offset: 0x00000508
		// (remove) Token: 0x0600001C RID: 28 RVA: 0x00002340 File Offset: 0x00000540
		public event EventHandler AppliedColorChanged;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002375 File Offset: 0x00000575
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000237D File Offset: 0x0000057D
		public bool IsCustomized { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002386 File Offset: 0x00000586
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000238E File Offset: 0x0000058E
		public bool IsLocked { get; private set; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002397 File Offset: 0x00000597
		public CustomizableIlluminator(IlluminationService illuminationService)
		{
			this._illuminationService = illuminationService;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023A8 File Offset: 0x000005A8
		public Color CustomColor
		{
			get
			{
				Color? customColor = this._customColor;
				if (customColor == null)
				{
					return this._defaultColor;
				}
				return customColor.GetValueOrDefault();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023D3 File Offset: 0x000005D3
		public Color IconColor
		{
			get
			{
				return this._illuminationService.LightingColorToIconColor(this.EffectiveColor);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023E6 File Offset: 0x000005E6
		public void Awake()
		{
			this._defaultIlluminatorColor = base.GetComponent<DefaultIlluminatorColor>();
			this._illuminatorColorizer = base.GetComponent<Illuminator>().CreateColorizer(100);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002408 File Offset: 0x00000608
		public void InitializeEntity()
		{
			DefaultIlluminatorColor defaultIlluminatorColor = this._defaultIlluminatorColor;
			this._defaultColor = ((defaultIlluminatorColor != null) ? defaultIlluminatorColor.Color : this._illuminationService.DefaultColor);
			Color value = this._customColor.GetValueOrDefault();
			if (this._customColor == null)
			{
				value = this._defaultColor;
				this._customColor = new Color?(value);
			}
			this.Apply();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002469 File Offset: 0x00000669
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsCustomized)
			{
				IObjectSaver component = entitySaver.GetComponent(CustomizableIlluminator.ComponentKey);
				component.Set(CustomizableIlluminator.IsCustomizedKey, this.IsCustomized);
				component.Set(CustomizableIlluminator.CustomColorKey, this.CustomColor);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024A0 File Offset: 0x000006A0
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(CustomizableIlluminator.ComponentKey, out objectLoader))
			{
				this.IsCustomized = objectLoader.Get(CustomizableIlluminator.IsCustomizedKey);
				this._customColor = new Color?(objectLoader.Get(CustomizableIlluminator.CustomColorKey));
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024E3 File Offset: 0x000006E3
		public void DuplicateFrom(CustomizableIlluminator source)
		{
			this.IsCustomized = source.IsCustomized;
			this._customColor = new Color?(source.CustomColor);
			this.Apply();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002508 File Offset: 0x00000708
		public void SetIsCustomized(bool value)
		{
			if (this.IsCustomized != value)
			{
				this.IsCustomized = value;
				this.Apply();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002520 File Offset: 0x00000720
		public void SetCustomColor(Color value)
		{
			Color? customColor = this._customColor;
			if (customColor == null || (customColor != null && customColor.GetValueOrDefault() != value))
			{
				this._customColor = new Color?(value);
				this.Apply();
				EventHandler customColorChanged = this.CustomColorChanged;
				if (customColorChanged == null)
				{
					return;
				}
				customColorChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002585 File Offset: 0x00000785
		public void Lock()
		{
			this.IsLocked = true;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000258E File Offset: 0x0000078E
		public void Unlock()
		{
			this.IsLocked = false;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002597 File Offset: 0x00000797
		public Color EffectiveColor
		{
			get
			{
				if (!this.IsCustomized)
				{
					return this._defaultColor;
				}
				return this.CustomColor;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000025B0 File Offset: 0x000007B0
		public void Apply()
		{
			Color? color = this.IsCustomized ? new Color?(this.CustomColor) : null;
			if (this._appliedColor != color)
			{
				if (color != null)
				{
					this._illuminatorColorizer.SetColor(color.Value);
				}
				else
				{
					this._illuminatorColorizer.ClearColor();
				}
				this._appliedColor = color;
				EventHandler appliedColorChanged = this.AppliedColorChanged;
				if (appliedColorChanged == null)
				{
					return;
				}
				appliedColorChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0400000A RID: 10
		public static readonly ComponentKey ComponentKey = new ComponentKey("CustomizableIlluminator");

		// Token: 0x0400000B RID: 11
		public static readonly PropertyKey<bool> IsCustomizedKey = new PropertyKey<bool>("IsCustomized");

		// Token: 0x0400000C RID: 12
		public static readonly PropertyKey<Color> CustomColorKey = new PropertyKey<Color>("CustomColor");

		// Token: 0x04000011 RID: 17
		public readonly IlluminationService _illuminationService;

		// Token: 0x04000012 RID: 18
		public DefaultIlluminatorColor _defaultIlluminatorColor;

		// Token: 0x04000013 RID: 19
		public IlluminatorColorizer _illuminatorColorizer;

		// Token: 0x04000014 RID: 20
		public Color _defaultColor;

		// Token: 0x04000015 RID: 21
		public Color? _customColor;

		// Token: 0x04000016 RID: 22
		public Color? _appliedColor;
	}
}

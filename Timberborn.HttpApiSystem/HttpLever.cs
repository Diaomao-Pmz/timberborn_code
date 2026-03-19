using System;
using Timberborn.Automation;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.EntityNaming;
using Timberborn.EntitySystem;
using Timberborn.Illumination;
using UnityEngine;

namespace Timberborn.HttpApiSystem
{
	// Token: 0x02000020 RID: 32
	public class HttpLever : BaseComponent, IAwakableComponent, IInitializableEntity, IPostInitializableEntity, IDeletableEntity, IAutomatorListener
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004691 File Offset: 0x00002891
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004699 File Offset: 0x00002899
		public string SwitchOnUrl { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000046A2 File Offset: 0x000028A2
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000046AA File Offset: 0x000028AA
		public string SwitchOffUrl { get; private set; }

		// Token: 0x060000C0 RID: 192 RVA: 0x000046B3 File Offset: 0x000028B3
		public HttpLever(HttpApi httpApi, HttpApiIntermediary httpApiIntermediary, HttpApiUrlGenerator httpApiUrlGenerator)
		{
			this._httpApi = httpApi;
			this._httpApiIntermediary = httpApiIntermediary;
			this._httpApiUrlGenerator = httpApiUrlGenerator;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000046D0 File Offset: 0x000028D0
		public void Awake()
		{
			this._uniquelyNamedEntity = base.GetComponent<UniquelyNamedEntity>();
			this._lever = base.GetComponent<Lever>();
			this._customizableIlluminator = base.GetComponent<CustomizableIlluminator>();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000046F8 File Offset: 0x000028F8
		public void InitializeEntity()
		{
			this._lever.IsSpringReturnChanged += this.OnIsSpringReturnChanged;
			this.AddSnapshot();
			this._uniquelyNamedEntity.EntityNameChanged += this.OnUniqueNameChanged;
			this._uniquelyNamedEntity.IsUniqueChanged += this.OnUniqueNameChanged;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004750 File Offset: 0x00002950
		public void PostInitializeEntity()
		{
			this.UpdateUrls();
			this._httpApi.UrlChanged += this.OnApiUrlChanged;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000476F File Offset: 0x0000296F
		public void DeleteEntity()
		{
			this.RemoveSnapshot();
			this._httpApi.UrlChanged -= this.OnApiUrlChanged;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000478E File Offset: 0x0000298E
		public void OnAutomatorStateChanged()
		{
			this.AddSnapshot();
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004796 File Offset: 0x00002996
		public void SetState(bool state)
		{
			this._lever.SwitchState(state);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000047A4 File Offset: 0x000029A4
		public void SetColor(Color color)
		{
			this._customizableIlluminator.SetCustomColor(color);
			this._customizableIlluminator.SetIsCustomized(true);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000047BE File Offset: 0x000029BE
		public void OnUniqueNameChanged(object sender, EventArgs e)
		{
			this.RemoveSnapshot();
			this.UpdateUrls();
			this.AddSnapshot();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000047D2 File Offset: 0x000029D2
		public void OnApiUrlChanged(object sender, EventArgs e)
		{
			this.UpdateUrls();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000478E File Offset: 0x0000298E
		public void OnIsSpringReturnChanged(object sender, EventArgs e)
		{
			this.AddSnapshot();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000047DC File Offset: 0x000029DC
		public void UpdateUrls()
		{
			this.SwitchOnUrl = new UriBuilder(this._httpApi.Url)
			{
				Path = this._httpApiUrlGenerator.SwitchOnLeverUrlPath(this._uniquelyNamedEntity.EntityName)
			}.ToString();
			this.SwitchOffUrl = new UriBuilder(this._httpApi.Url)
			{
				Path = this._httpApiUrlGenerator.SwitchOffLeverUrlPath(this._uniquelyNamedEntity.EntityName)
			}.ToString();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004858 File Offset: 0x00002A58
		public void AddSnapshot()
		{
			if (this._uniquelyNamedEntity.IsUnique)
			{
				this._snapshotName = this._uniquelyNamedEntity.EntityName;
				this._httpApiIntermediary.AddLeverSnapshot(new HttpLeverSnapshot(this._snapshotName, this._lever.IsOn, this._lever.IsSpringReturn));
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000048AF File Offset: 0x00002AAF
		public void RemoveSnapshot()
		{
			if (this._snapshotName != null)
			{
				this._httpApiIntermediary.RemoveLeverSnapshot(this._snapshotName);
				this._snapshotName = null;
			}
		}

		// Token: 0x04000080 RID: 128
		public readonly HttpApi _httpApi;

		// Token: 0x04000081 RID: 129
		public readonly HttpApiIntermediary _httpApiIntermediary;

		// Token: 0x04000082 RID: 130
		public readonly HttpApiUrlGenerator _httpApiUrlGenerator;

		// Token: 0x04000083 RID: 131
		public UniquelyNamedEntity _uniquelyNamedEntity;

		// Token: 0x04000084 RID: 132
		public Lever _lever;

		// Token: 0x04000085 RID: 133
		public CustomizableIlluminator _customizableIlluminator;

		// Token: 0x04000086 RID: 134
		public string _snapshotName;
	}
}

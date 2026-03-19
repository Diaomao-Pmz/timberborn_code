using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Workshops;
using UnityEngine;

namespace Timberborn.WorkshopsEffects
{
	// Token: 0x0200000A RID: 10
	public class ManufactoryRecipeVisualizer : BaseComponent, IAwakableComponent, IFinishedStateListener
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000242C File Offset: 0x0000062C
		public void Awake()
		{
			this._manufactory = base.GetComponent<Manufactory>();
			this._manufactoryRecipeVisualizerSpec = base.GetComponent<ManufactoryRecipeVisualizerSpec>();
			this._manufactory.RecipeChanged += this.OnProductionRecipeChanged;
			this._initialModel = base.GameObject.FindChild(this._manufactoryRecipeVisualizerSpec.InitialModelName);
			base.DisableComponent();
			this.InitializeRecipeModels();
			this.UpdateVisualization();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002496 File Offset: 0x00000696
		public void OnEnterFinishedState()
		{
			base.EnableComponent();
			this.UpdateVisualization();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000024A4 File Offset: 0x000006A4
		public void OnExitFinishedState()
		{
			base.DisableComponent();
			this.UpdateVisualization();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024B2 File Offset: 0x000006B2
		public void OnProductionRecipeChanged(object sender, EventArgs e)
		{
			this.UpdateVisualization();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024BC File Offset: 0x000006BC
		public void InitializeRecipeModels()
		{
			foreach (RecipeModel recipeModel in this._manufactoryRecipeVisualizerSpec.RecipeModels)
			{
				this._recipeModels.Add(recipeModel.RecipeId, base.GameObject.FindChild(recipeModel.ModelName));
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002514 File Offset: 0x00000714
		public void UpdateVisualization()
		{
			bool flag = !base.Enabled || !this._manufactory.HasCurrentRecipe;
			this._initialModel.SetActive(flag);
			string text;
			if (!flag)
			{
				RecipeSpec currentRecipe = this._manufactory.CurrentRecipe;
				text = ((currentRecipe != null) ? currentRecipe.Id : null);
			}
			else
			{
				text = string.Empty;
			}
			string b = text;
			foreach (KeyValuePair<string, GameObject> keyValuePair in this._recipeModels)
			{
				string text2;
				GameObject gameObject;
				keyValuePair.Deconstruct(ref text2, ref gameObject);
				string a = text2;
				gameObject.SetActive(a == b);
			}
		}

		// Token: 0x0400000C RID: 12
		public Manufactory _manufactory;

		// Token: 0x0400000D RID: 13
		public ManufactoryRecipeVisualizerSpec _manufactoryRecipeVisualizerSpec;

		// Token: 0x0400000E RID: 14
		public GameObject _initialModel;

		// Token: 0x0400000F RID: 15
		public readonly Dictionary<string, GameObject> _recipeModels = new Dictionary<string, GameObject>();
	}
}

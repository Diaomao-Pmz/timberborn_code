using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlueprintSystem;
using Timberborn.Forestry;
using Timberborn.LevelVisibilitySystem;
using Timberborn.Rendering;
using Timberborn.RootProviders;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.Yielding;
using UnityEngine;

namespace Timberborn.ForestryUI
{
	// Token: 0x02000014 RID: 20
	public class TreeCuttingAreaVisualizer : ILoadableSingleton
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002C93 File Offset: 0x00000E93
		public TreeCuttingAreaVisualizer(Highlighter highlighter, EventBus eventBus, TreeCuttingArea treeCuttingArea, ISpecService specService, AreaTileDrawerFactory areaTileDrawerFactory, RootObjectProvider rootObjectProvider, ILevelVisibilityService levelVisibilityService)
		{
			this._highlighter = highlighter;
			this._eventBus = eventBus;
			this._treeCuttingArea = treeCuttingArea;
			this._specService = specService;
			this._areaTileDrawerFactory = areaTileDrawerFactory;
			this._rootObjectProvider = rootObjectProvider;
			this._levelVisibilityService = levelVisibilityService;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public void Load()
		{
			this._parent = this._rootObjectProvider.CreateRootObject("TreeCuttingAreaVisualizer");
			this._levelVisibilityService.MaxVisibleLevelChanged += this.OnMaxVisibleLevelChanged;
			TreeCuttingColorsSpec singleSpec = this._specService.GetSingleSpec<TreeCuttingColorsSpec>();
			this._areaTileDrawer = this._areaTileDrawerFactory.Create(singleSpec.CuttingAreaTile, this._parent);
			this._cuttingAreaHighlightColor = singleSpec.CuttingAreaHighlight;
			this._updateAreaOnEnter = true;
			this._eventBus.Register(this);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002D54 File Offset: 0x00000F54
		[OnEvent]
		public void OnToolGroupEntered(ToolGroupEnteredEvent toolGroupEnteredEvent)
		{
			ToolGroupSpec toolGroup = toolGroupEnteredEvent.ToolGroup;
			if (toolGroup != null && toolGroup.HasSpec<TreeCuttingToolGroupSpec>())
			{
				this._enabled = true;
				if (this._updateAreaOnEnter)
				{
					this._updateAreaOnEnter = false;
					this._areaTileDrawer.UpdateArea(this.GetCuttingArea());
				}
				this.Highlight();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002DA2 File Offset: 0x00000FA2
		[OnEvent]
		public void OnToolGroupExited(ToolGroupExitedEvent toolGroupExitedEvent)
		{
			ToolGroupSpec toolGroup = toolGroupExitedEvent.ToolGroup;
			if (toolGroup != null && toolGroup.HasSpec<TreeCuttingToolGroupSpec>())
			{
				this._enabled = false;
				this._highlighter.UnhighlightAllSecondary();
				this._areaTileDrawer.HideAllTiles();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002DD5 File Offset: 0x00000FD5
		[OnEvent]
		public void OnTreeCuttingAreaChanged(TreeCuttingAreaChangedEvent treeCuttingAreaChangedEvent)
		{
			this.UpdateOrMarkForUpdate();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DE0 File Offset: 0x00000FE0
		[OnEvent]
		public void OnTreeAddedToCuttingArea(TreeAddedToCuttingAreaEvent treeAddedToCuttingAreaEvent)
		{
			if (this._enabled)
			{
				TreeComponent treeComponent = treeAddedToCuttingAreaEvent.TreeComponent;
				this._highlighter.HighlightSecondary(treeComponent, this._cuttingAreaHighlightColor);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DD5 File Offset: 0x00000FD5
		public void OnMaxVisibleLevelChanged(object sender, int e)
		{
			this.UpdateOrMarkForUpdate();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E0E File Offset: 0x0000100E
		public void UpdateOrMarkForUpdate()
		{
			if (this._enabled)
			{
				this._areaTileDrawer.UpdateArea(this.GetCuttingArea());
				this.Highlight();
				return;
			}
			this._updateAreaOnEnter = true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E37 File Offset: 0x00001037
		public IEnumerable<Vector3Int> GetCuttingArea()
		{
			return from coords in this._treeCuttingArea.CuttingArea
			where coords.z <= this._levelVisibilityService.MaxVisibleLevel
			select coords;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E58 File Offset: 0x00001058
		public void Highlight()
		{
			this._highlighter.UnhighlightAllSecondary();
			this._areaTileDrawer.ShowAllTiles();
			foreach (Yielder target in this._treeCuttingArea.YieldersInArea)
			{
				this._highlighter.HighlightSecondary(target, this._cuttingAreaHighlightColor);
			}
		}

		// Token: 0x04000049 RID: 73
		public readonly Highlighter _highlighter;

		// Token: 0x0400004A RID: 74
		public readonly EventBus _eventBus;

		// Token: 0x0400004B RID: 75
		public readonly TreeCuttingArea _treeCuttingArea;

		// Token: 0x0400004C RID: 76
		public readonly ISpecService _specService;

		// Token: 0x0400004D RID: 77
		public readonly AreaTileDrawerFactory _areaTileDrawerFactory;

		// Token: 0x0400004E RID: 78
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x0400004F RID: 79
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000050 RID: 80
		public GameObject _parent;

		// Token: 0x04000051 RID: 81
		public AreaTileDrawer _areaTileDrawer;

		// Token: 0x04000052 RID: 82
		public bool _updateAreaOnEnter;

		// Token: 0x04000053 RID: 83
		public bool _enabled;

		// Token: 0x04000054 RID: 84
		public Color _cuttingAreaHighlightColor;
	}
}

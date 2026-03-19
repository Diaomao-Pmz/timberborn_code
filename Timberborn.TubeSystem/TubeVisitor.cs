using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterModelSystem;
using Timberborn.EnterableSystem;
using Timberborn.EntitySystem;
using Timberborn.Navigation;
using UnityEngine;

namespace Timberborn.TubeSystem
{
	// Token: 0x02000016 RID: 22
	public class TubeVisitor : BaseComponent, IAwakableComponent, IInitializableEntity, IDeletableEntity
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00003015 File Offset: 0x00001215
		public TubeVisitor(TubeMap tubeMap)
		{
			this._tubeMap = tubeMap;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003024 File Offset: 0x00001224
		public void Awake()
		{
			this._characterModel = base.GetComponent<CharacterModel>();
			this._enterer = base.GetComponent<Enterer>();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003040 File Offset: 0x00001240
		public void InitializeEntity()
		{
			this.UpdateVisit(this._lastGridPosition = this.GetGridPosition());
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003062 File Offset: 0x00001262
		public void DeleteEntity()
		{
			this.ExitTube(this._currentTube, true);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003074 File Offset: 0x00001274
		public void UpdateVisit()
		{
			if (!this._enterer.IsInside)
			{
				Vector3Int gridPosition = this.GetGridPosition();
				if (this._lastGridPosition != gridPosition)
				{
					this.UpdateVisit(gridPosition);
				}
				this._lastGridPosition = gridPosition;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000030B4 File Offset: 0x000012B4
		public void UpdateVisit(Vector3Int gridPosition)
		{
			Tube tubeAt = this._tubeMap.GetTubeAt(gridPosition);
			Tube tube = (tubeAt && tubeAt.CanBeVisited) ? tubeAt : null;
			if (tube != this._currentTube)
			{
				this.ExitTube(this._currentTube, !tube);
				this.EnterTube(tube);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003108 File Offset: 0x00001308
		public void ExitTube(Tube tube, bool showModel = true)
		{
			if (tube)
			{
				tube.RemoveVisitor(this);
				tube.TubeDeleted -= this.OnTubeDeleted;
				if (showModel)
				{
					this._characterModel.Show();
				}
				this._currentTube = null;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003140 File Offset: 0x00001340
		public void EnterTube(Tube tube)
		{
			if (tube)
			{
				tube.AddVisitor(this);
				this._characterModel.Hide();
				this._currentTube = tube;
				this._currentTube.TubeDeleted += this.OnTubeDeleted;
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003062 File Offset: 0x00001262
		public void OnTubeDeleted(object sender, EventArgs eventArgs)
		{
			this.ExitTube(this._currentTube, true);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000317A File Offset: 0x0000137A
		public Vector3Int GetGridPosition()
		{
			return NavigationCoordinateSystem.WorldToGridInt(this._characterModel.Position);
		}

		// Token: 0x04000034 RID: 52
		public readonly TubeMap _tubeMap;

		// Token: 0x04000035 RID: 53
		public CharacterModel _characterModel;

		// Token: 0x04000036 RID: 54
		public Enterer _enterer;

		// Token: 0x04000037 RID: 55
		public Vector3Int _lastGridPosition;

		// Token: 0x04000038 RID: 56
		public Tube _currentTube;
	}
}

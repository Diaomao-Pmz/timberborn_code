using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Coordinates;
using Timberborn.Rendering;
using Timberborn.SelectionSystem;
using UnityEngine;
using UnityEngine.Rendering;

namespace Timberborn.ZiplineSystem
{
	// Token: 0x0200000D RID: 13
	public class ZiplineCableModel
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002622 File Offset: 0x00000822
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000262A File Offset: 0x0000082A
		public bool IsActive { get; set; } = true;

		// Token: 0x06000036 RID: 54 RVA: 0x00002634 File Offset: 0x00000834
		public ZiplineCableModel(MaterialColorer materialColorer, Highlighter highlighter, GameObject leftCableRoot, GameObject rightCableRoot)
		{
			this._materialColorer = materialColorer;
			this._highlighter = highlighter;
			this._left = leftCableRoot.GetComponentSlow<Cable>();
			this._right = rightCableRoot.GetComponentSlow<Cable>();
			this._leftMeshRenderer = leftCableRoot.GetComponentInChildren<MeshRenderer>();
			this._rightMeshRenderer = rightCableRoot.GetComponentInChildren<MeshRenderer>();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000268E File Offset: 0x0000088E
		public void Destroy()
		{
			Object.Destroy(this._left.GameObject);
			Object.Destroy(this._right.GameObject);
			this._left = null;
			this._right = null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026BE File Offset: 0x000008BE
		public void SetVisibility(bool isVisible)
		{
			this._left.GameObject.SetActive(isVisible);
			this._right.GameObject.SetActive(isVisible);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026E4 File Offset: 0x000008E4
		public void SetGreyscale(bool isGrayscale)
		{
			if (isGrayscale)
			{
				this._materialColorer.EnableGrayscale(this._left.GameObject);
				this._materialColorer.EnableGrayscale(this._right.GameObject);
				return;
			}
			this._materialColorer.DisableGrayscale(this._left.GameObject);
			this._materialColorer.DisableGrayscale(this._right.GameObject);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002750 File Offset: 0x00000950
		public void SetShadowOnly(bool isShadowOnly)
		{
			ShadowCastingMode shadowCastingMode = isShadowOnly ? 3 : 1;
			this._leftMeshRenderer.shadowCastingMode = (this._rightMeshRenderer.shadowCastingMode = shadowCastingMode);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000277F File Offset: 0x0000097F
		public void Highlight(Color color)
		{
			this._highlighter.HighlightPrimary(this._left, color);
			this._highlighter.HighlightPrimary(this._right, color);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027A5 File Offset: 0x000009A5
		public void Unhighlight()
		{
			this._highlighter.UnhighlightPrimary(this._left);
			this._highlighter.UnhighlightPrimary(this._right);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027CC File Offset: 0x000009CC
		public void UpdateModel(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			bool isOperative = ziplineTower.GetComponent<ZiplineTowerOperationValidator>().IsOperative && otherZiplineTower.GetComponent<ZiplineTowerOperationValidator>().IsOperative;
			ZiplineCableModel.UpdateModel(ziplineTower, otherZiplineTower, this._left.GameObject, this._leftMeshRenderer, isOperative);
			ZiplineCableModel.UpdateModel(otherZiplineTower, ziplineTower, this._right.GameObject, this._rightMeshRenderer, isOperative);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002828 File Offset: 0x00000A28
		public static void UpdateModel(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, GameObject model, MeshRenderer meshRenderer, bool isOperative)
		{
			Vector3 start = CoordinateSystem.GridToWorld(ziplineTower.CableAnchorPoint);
			Vector3 end = CoordinateSystem.GridToWorld(otherZiplineTower.CableAnchorPoint);
			ValueTuple<Vector3, Vector3> valueTuple = ZiplineCalculator.CalculateWorldConnections(start, end);
			Vector3 item = valueTuple.Item1;
			Vector3 vector = valueTuple.Item2 - item;
			float magnitude = vector.magnitude;
			model.transform.position = item + 0.5f * vector;
			model.transform.rotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
			model.transform.localScale = new Vector3(1f, 1f, magnitude);
			meshRenderer.material.SetFloat(ZiplineCableModel.LengthId, magnitude);
			meshRenderer.material.SetFloat(ZiplineCableModel.IsOperativeId, isOperative ? 1f : 0f);
		}

		// Token: 0x0400000A RID: 10
		public static readonly int LengthId = Shader.PropertyToID("_Length");

		// Token: 0x0400000B RID: 11
		public static readonly int IsOperativeId = Shader.PropertyToID("_IsOperative");

		// Token: 0x0400000D RID: 13
		public readonly MaterialColorer _materialColorer;

		// Token: 0x0400000E RID: 14
		public readonly Highlighter _highlighter;

		// Token: 0x0400000F RID: 15
		public Cable _left;

		// Token: 0x04000010 RID: 16
		public Cable _right;

		// Token: 0x04000011 RID: 17
		public readonly MeshRenderer _leftMeshRenderer;

		// Token: 0x04000012 RID: 18
		public readonly MeshRenderer _rightMeshRenderer;
	}
}

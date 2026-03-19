using System;
using System.Collections.Generic;
using Bindito.Unity;
using Timberborn.Coordinates;
using Timberborn.PrefabOptimization;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.TemplateSystem;
using UnityEngine;

namespace Timberborn.ModularShafts
{
	// Token: 0x02000015 RID: 21
	public class ShaftFrameFactory : ILoadableSingleton
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00003FB9 File Offset: 0x000021B9
		public ShaftFrameFactory(RootObjectProvider rootObjectProvider, TemplateService templateService, OptimizedPrefabInstantiator optimizedPrefabInstantiator)
		{
			this._rootObjectProvider = rootObjectProvider;
			this._templateService = templateService;
			this._optimizedPrefabInstantiator = optimizedPrefabInstantiator;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003FEC File Offset: 0x000021EC
		public void Load()
		{
			ModularShaftPartsSpec single = this._templateService.GetSingle<ModularShaftPartsSpec>();
			this._root = this._rootObjectProvider.CreateRootObject("ShaftFrameFactory").transform;
			this._shaftBase = this.Instantiate(single.ShaftBase.Asset, this._root);
			this._shaftLowerFrame = this.Instantiate(single.ShaftLowerFrame.Asset, this._root);
			this._shaftSupport = this.Instantiate(single.ShaftSupport.Asset, this._root);
			this._shaftFrame = this.Instantiate(single.ShaftFrame.Asset, this._root);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004094 File Offset: 0x00002294
		public GameObject GetFrame(FrameVariant variant)
		{
			GameObject gameObject;
			if (!this._frames.TryGetValue(variant, out gameObject))
			{
				gameObject = this.BuildFrame(variant);
				this._frames.Add(variant, gameObject);
			}
			return this.Instantiate(gameObject, this._root);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000040D3 File Offset: 0x000022D3
		public GameObject Instantiate(GameObject gameObject, Transform root)
		{
			GameObject gameObject2 = this._optimizedPrefabInstantiator.Instantiate(gameObject, root);
			gameObject2.SetActive(false);
			return gameObject2;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000040EC File Offset: 0x000022EC
		public GameObject BuildFrame(FrameVariant frameVariant)
		{
			GameObject gameObject = new GameObject(frameVariant.GetName());
			gameObject.transform.SetParent(this._root);
			BuiltMesh builtMesh = this.BuildFrameMesh(frameVariant);
			gameObject.AddComponent<MeshFilter>().sharedMesh = builtMesh.Mesh;
			gameObject.AddComponent<MeshRenderer>().sharedMaterials = builtMesh.Materials;
			gameObject.SetActive(false);
			return gameObject;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000414C File Offset: 0x0000234C
		public BuiltMesh BuildFrameMesh(FrameVariant variant)
		{
			this._meshBuilder.Reset(variant.GetName());
			this.AppendMesh(this._shaftBase, null);
			if (variant.Up)
			{
				this.AppendMesh(this._shaftFrame, null);
			}
			if (variant.Right)
			{
				OrientationTransform orientationTransform = new OrientationTransform(Orientation.Cw90);
				this.AppendMesh(this._shaftFrame, orientationTransform);
			}
			if (variant.Down)
			{
				OrientationTransform orientationTransform2 = new OrientationTransform(Orientation.Cw180);
				this.AppendMesh(this._shaftFrame, orientationTransform2);
			}
			if (variant.Left)
			{
				OrientationTransform orientationTransform3 = new OrientationTransform(Orientation.Cw270);
				this.AppendMesh(this._shaftFrame, orientationTransform3);
			}
			if (variant.Bottom)
			{
				this.AppendMesh(this._shaftLowerFrame, null);
			}
			if (variant.Support)
			{
				this.AppendMesh(this._shaftSupport, null);
			}
			return this._meshBuilder.Build(1);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004230 File Offset: 0x00002430
		public void AppendMesh(GameObject gameObject, ITransform transform = null)
		{
			Transform child = gameObject.transform.GetChild(0);
			MeshFilter component = child.GetComponent<MeshFilter>();
			MeshRenderer component2 = child.GetComponent<MeshRenderer>();
			this._meshBuilder.AppendMesh<ITransform>(component.sharedMesh, component2.sharedMaterials, transform ?? default(TranslationTransform));
		}

		// Token: 0x04000061 RID: 97
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000062 RID: 98
		public readonly TemplateService _templateService;

		// Token: 0x04000063 RID: 99
		public readonly OptimizedPrefabInstantiator _optimizedPrefabInstantiator;

		// Token: 0x04000064 RID: 100
		public readonly IInstantiator _instantiator;

		// Token: 0x04000065 RID: 101
		public readonly MeshBuilder _meshBuilder = new MeshBuilder();

		// Token: 0x04000066 RID: 102
		public readonly Dictionary<FrameVariant, GameObject> _frames = new Dictionary<FrameVariant, GameObject>();

		// Token: 0x04000067 RID: 103
		public Transform _root;

		// Token: 0x04000068 RID: 104
		public GameObject _shaftBase;

		// Token: 0x04000069 RID: 105
		public GameObject _shaftLowerFrame;

		// Token: 0x0400006A RID: 106
		public GameObject _shaftSupport;

		// Token: 0x0400006B RID: 107
		public GameObject _shaftFrame;
	}
}


using System;
using System.Collections.Generic;

using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.Converters.WellKnownText;

using UnityEngine;

using UnitySlippyMap.Markers;
using UnitySlippyMap.Layers;
using UnitySlippyMap.GUI;
using UnitySlippyMap.Input;
using UnitySlippyMap.Helpers;

namespace UnitySlippyMap.Map
{
	
	public class MapBehaviour : MonoBehaviour
	{
	#region Singleton stuff

		
		private static MapBehaviour instance = null;

		
		public static MapBehaviour Instance {
			get {
				if (null == (object)instance) {
					instance = FindObjectOfType (typeof(MapBehaviour)) as MapBehaviour;
					if (null == (object)instance) {
						var go = new GameObject ("[Map]");
						
						instance = go.AddComponent<MapBehaviour> ();
						instance.EnsureMap ();
					}
				}

				return instance;
			}
		}
	
		
		private void EnsureMap ()
		{
		}
	
		
		private MapBehaviour ()
		{
		}

		
		private void OnDestroy ()
		{
			instance = null;
		}

		
		private void OnApplicationQuit ()
		{
			DestroyImmediate (this.gameObject);
		}
	
	#endregion
	
	#region Variables & properties

		private Camera currentCamera;
		
		
		public Camera CurrentCamera {
			get { return currentCamera; }
			set { currentCamera = value; }
		}

		
		private bool isDirty = false;

		
		public bool IsDirty {
			get { return isDirty; }
			set { isDirty = value; }
		}

		
		private double[] centerWGS84 = new double[2];

		
		public double[] CenterWGS84 {
			get { return centerWGS84; }
			set {
				if (value == null) {
#if DEBUG_LOG
				Debug.LogError("ERROR: Map.CenterWGS84: value cannot be null");
#endif
					return;
				}
				
				if (value [0] > 180.0)
					value [0] -= 360.0;
				else if (value [0] < -180.0)
					value [0] += 360.0;
				
				centerWGS84 = value;

				double[] newCenterESPG900913 = wgs84ToEPSG900913Transform.Transform (centerWGS84);

				centerEPSG900913 = ComputeCenterEPSG900913 (newCenterESPG900913);

				Debug.Log("center: " + centerEPSG900913[0] + " " + centerEPSG900913[1]);

				FitVerticalBorder ();
				IsDirty = true;
			}
		}
	
	
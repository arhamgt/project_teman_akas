#region Using statements

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

namespace Bitgem.VFX.StylisedWater
{
    public class WateverVolumeFloater : MonoBehaviour
    {
        #region Public fields
        public float offsetY;
        public WaterVolumeHelper WaterVolumeHelper = null;

        #endregion

        #region MonoBehaviour events

        void Update()
        {
            var instance = WaterVolumeHelper ? WaterVolumeHelper : WaterVolumeHelper.Instance;
            if (!instance)
            {
                return;
            }

            transform.position = new Vector3(transform.position.x, instance.GetHeight(transform.position)+offsetY ?? transform.position.y+0.5f, transform.position.z);
        }

        #endregion
    }
}
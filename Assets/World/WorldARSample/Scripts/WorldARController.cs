namespace Preview
{
    using UnityEngine;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using System.Collections;
    using System;
    using HuaweiARInternal;
    using Common;

    public class WorldARController : MonoBehaviour
    {
        [Tooltip("hand prefabs")]
        public GameObject handPrefabs;

        private List<ARHand> newHands = new List<ARHand>();

        [Tooltip("plane visualizer")]
        public GameObject planePrefabs;

        [Tooltip("plane label visualizer")]
        public GameObject planeLabelPrefabs;

        [Tooltip("green logo visualizer")]
        public GameObject arDiscoveryLogoPlanePrefabs;

       /* public GameObject enemy1;
        public GameObject enemy2;*/

        private List<ARAnchor> addedAnchors = new List<ARAnchor>();
        private List<ARPlane> newPlanes = new List<ARPlane>();
        private bool anchorPlaced = false;

        private void Start()
        {
            DeviceChanged.OnDeviceChange += ARSession.SetDisplayGeometry;
        }

        public void Update()
        {
            _DrawHand();
            //_DrawPlane();
            if (ARFrame.GetTrackingState() == ARTrackable.TrackingState.TRACKING
                && !this.anchorPlaced)
            {
                var dimentions = ARSession.GetCameraConfig().GetImageDimensions();
                Debug.Log("[DIM] dimentions "+ dimentions.x +" "+ dimentions.y);

                List<ARHitResult> hitResults = ARFrame.HitTest(Screen.width/2,Screen.height/2);
                if(hitResults.Count > 0)
                {

                    this.anchorPlaced = true;
                    List<ARHitResult> oneHitList = new List<ARHitResult>() { hitResults[0] };

                    _DrawARLogo(oneHitList);
                }
               
            }
            /*
            Touch touch;
             * if (ARFrame.GetTrackingState() != ARTrackable.TrackingState.TRACKING
                || Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
            {
                
            }
            else
            {
                _DrawARLogo(touch);
                
            }*/
        }

        private void _DrawPlane()
        {
            newPlanes.Clear();
            ARFrame.GetTrackables<ARPlane>(newPlanes,ARTrackableQueryFilter.NEW);
            for (int i = 0; i < newPlanes.Count; i++)
            {
                GameObject planeObject = Instantiate(planePrefabs, Vector3.zero, Quaternion.identity, transform);
                planeObject.GetComponent<TrackedPlaneVisualizer>().Initialize(newPlanes[i]);


                GameObject planeLabelObject = Instantiate(planeLabelPrefabs, Vector3.zero, Quaternion.identity, transform);
                planeLabelObject.GetComponent<PlaneLabelVisualizer>().Initialize(newPlanes[i]);
            }
        }
        private void _DrawARLogo(Touch touch) {

            List<ARHitResult> hitResults = ARFrame.HitTest(touch);
            this._DrawARLogo(hitResults);
        }


        private void _DrawHand()
        {
            newHands.Clear();
            ARFrame.GetTrackables(newHands, ARTrackableQueryFilter.NEW);
            for (int i = 0; i < newHands.Count; i++)
            {
                GameObject handObject = Instantiate(handPrefabs, Vector3.zero, Quaternion.identity, transform);
                handObject.GetComponent<HandVisualizer>().Initialize(newHands[i]);
            }
        }
        private void _DrawARLogo(List<ARHitResult> hitResults)
        {
            ARHitResult hitResult = null;
            ARTrackable trackable = null;
            Boolean hasHitFlag = false;
            ARDebug.LogInfo("_DrawARLogo hitResults count {0}", hitResults.Count);
            foreach (ARHitResult singleHit in hitResults)
            {
                trackable = singleHit.GetTrackable();
                ARDebug.LogInfo("_DrawARLogo GetTrackable {0}", singleHit.GetTrackable());
                if((trackable is ARPlane && ((ARPlane)trackable).IsPoseInPolygon(singleHit.HitPose)) 
                    && ((ARPlane)trackable).GetARPlaneType()== ARPlane.ARPlaneType.HORIZONTAL_UPWARD_FACING
                    && ((ARPlane)trackable).GetARPlaneLabel()== ARPlane.ARPlaneSemanticLabel.PLANE_FLOOR
                    || (trackable is ARPoint))
                {
                    hitResult = singleHit;
                    hasHitFlag = true;
                    if (trackable is ARPlane)
                    {
                        break;
                    }                 
                }
            }

            if (hasHitFlag != true)
            {
                ARDebug.LogInfo("_DrawARLogo can't hit!");
                return;
            }

            if (addedAnchors.Count > 16)
            {
                ARAnchor toRemove = addedAnchors[0];
                toRemove.Detach();
                addedAnchors.RemoveAt(0);
            }

            ARAnchor anchor = hitResult.CreateAnchor();
            /*GameObject prefab1 = enemy1;

            GameObject prefab2 = enemy2;

            var enemy1Obj = Instantiate(prefab1, anchor.GetPose().position, anchor.GetPose().rotation);
            var enemy2Obj = Instantiate(prefab2, anchor.GetPose().position, anchor.GetPose().rotation);*/
            Debug.LogError("CONTROLLER 149");
            //passing anchor to enemey spawner.
            this.gameObject.GetComponent< EnemeySpawnerCTRL >().InitSpawner(anchor);
            Debug.LogError("CONTROLLER 152");


            /*enemy1Obj.GetComponent<ARDiscoveryLogoVisualizer>().Initialize(anchor);
            enemy2Obj.GetComponent<ARDiscoveryLogoVisualizer>().Initialize(anchor);*/
            addedAnchors.Add(anchor);
        }
    }
}

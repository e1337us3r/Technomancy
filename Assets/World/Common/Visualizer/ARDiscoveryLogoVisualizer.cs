namespace Common
{
    using UnityEngine;
    using HuaweiARUnitySDK;
    using HuaweiARInternal;
    public class ARDiscoveryLogoVisualizer:MonoBehaviour
    {
        private ARAnchor m_anchor;
        private MeshRenderer m_MeshRenderer;

        public void Awake()
        {
            //m_MeshRenderer = GetComponent<MeshRenderer>();
            
        }
        public void Initialize(ARAnchor anchor)
        {
            m_anchor = anchor;
            Update();
        }
        public void Update()
        {
            //Debug.LogError("HERE 23");
            if (null == m_anchor)
            {
                //Debug.LogError("HERE 26");
                //m_MeshRenderer.enabled = false;
                //Debug.LogError("HERE 28");
                return;
            }
            switch (m_anchor.GetTrackingState())
            {
                case ARTrackable.TrackingState.TRACKING:
                    //Debug.LogError("HERE 33");
                    Pose p = m_anchor.GetPose();
                    //Debug.LogError("HERE 35");
                    gameObject.transform.position = p.position;
                    gameObject.transform.rotation = p.rotation;
                    //gameObject.transform.Rotate(0f, 225f, 0f, Space.Self);
                    //Debug.LogError("HERE 39");
                    //m_MeshRenderer.enabled = true;
                    break;
                case ARTrackable.TrackingState.PAUSED:
                    //m_MeshRenderer.enabled = false;
                    break;
                case ARTrackable.TrackingState.STOPPED:
                default:
                    //m_MeshRenderer.enabled = false;
                    Destroy(gameObject);
                    break;
            }
        }
    }
}

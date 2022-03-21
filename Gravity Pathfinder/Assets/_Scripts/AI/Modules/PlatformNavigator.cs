using System.Collections.Generic;
using UnityEngine;

public class PlatformNavigator : MonoBehaviour
{
    public PlatformNode CurrentPlatform { get; private set; }
    public PlatformNode NextPlatform { get; private set; }
    public PlatformNode TargetPlatform { get; private set; }
    public Stack<PlatformNode> PreviousPlatforms { get; private set; } = new Stack<PlatformNode>();

    void OnTriggerStay(Collider other)
    {
        if (!CurrentPlatform && other.TryGetComponent(out PlatformNode platform))
        {
            CurrentPlatform = platform;
            CurrentPlatform.OnConnected += SetTargetPlatform;

            if (CurrentPlatform == TargetPlatform)
            {
                PreviousPlatforms.Clear();
                NextPlatform = null;
            }
            else
            {
                GoToNextPlatform();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (CurrentPlatform && CurrentPlatform.gameObject == other.gameObject)
        {
            CurrentPlatform.OnConnected -= SetTargetPlatform;
            CurrentPlatform = null;
        }
    }

    void SetTargetPlatform(PlatformNode connectedNode)
    {
        TargetPlatform = connectedNode;
        GoToNextPlatform();
    }

    void GoToNextPlatform()
    {
        foreach (var platform in CurrentPlatform.ConnectedNodes)
        {
            if (!PreviousPlatforms.Contains(platform))
            {
                PreviousPlatforms.Push(CurrentPlatform);
                NextPlatform = platform;
            }
        }
    }

    public void ResetPlatformInfo()
    {
        CurrentPlatform = null;
        NextPlatform = null;
        TargetPlatform = null;
        PreviousPlatforms.Clear();
    }
}

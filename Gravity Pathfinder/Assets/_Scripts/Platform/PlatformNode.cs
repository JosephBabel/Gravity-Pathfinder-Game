using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class PlatformNode : MonoBehaviour
{
    [SerializeField] float _sizeXMultiplier = 1.5f;
    [SerializeField] float _sizeYMultiplier = 2f;

    public PlatformNetwork AttachedNetwork;

    public bool IsConnected => ConnectedNodes.Count > 0;
    public BoxCollider PlatformCollider { get; private set; }
    public HashSet<PlatformNode> ConnectedNodes { get; private set; } = new HashSet<PlatformNode>();

    public delegate void Connected(PlatformNode connectedNode);
    public event Connected OnConnected;

    public delegate void Disconnected();
    public event Disconnected OnDisconnected;

    BoxCollider _trigger;

    void Awake()
    {
        AttachedNetwork = new PlatformNetwork(this);

        PlatformCollider = GetComponent<BoxCollider>();

        CreateTrigger();
    }

    void CreateTrigger()
    {
        _trigger = gameObject.AddComponent<BoxCollider>();
        _trigger.isTrigger = true;
        _trigger.size = new Vector3(PlatformCollider.size.x * _sizeXMultiplier, PlatformCollider.size.y * _sizeYMultiplier, PlatformCollider.size.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out PlatformNode otherPlatformNode) && !ConnectedNodes.Contains(otherPlatformNode))
        {
            ConnectedNodes.Add(otherPlatformNode);
            otherPlatformNode.ConnectedNodes.Add(this);

            PropogateOnConnectedEvent(otherPlatformNode);
            otherPlatformNode.PropogateOnConnectedEvent(this);

            PlatformNetwork.CombineNetworks(AttachedNetwork, otherPlatformNode.AttachedNetwork);
        }
    }

    void PropogateOnConnectedEvent(PlatformNode newNode) 
    {
        foreach (var platformNode in AttachedNetwork.NodesInNetwork)
        {
            platformNode.OnConnected?.Invoke(newNode);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out PlatformNode otherPlatformNode) && ConnectedNodes.Contains(otherPlatformNode))
        {
            ConnectedNodes.Remove(otherPlatformNode);
            otherPlatformNode.ConnectedNodes.Remove(this);

            PropogateOnDisconnectedEvent();

            PlatformNetwork.UpdateNetwork(this);
            PlatformNetwork.UpdateNetwork(otherPlatformNode);
        }
    }

    void PropogateOnDisconnectedEvent()
    {
        foreach (var platformNode in AttachedNetwork.NodesInNetwork)
        {
            platformNode.OnDisconnected?.Invoke();
        }
    }
}

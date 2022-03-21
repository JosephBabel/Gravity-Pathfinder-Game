using System.Collections.Generic;

public class PlatformNetwork
{
    public HashSet<PlatformNode> NodesInNetwork = new HashSet<PlatformNode>();
    
    public PlatformNetwork() { }

    public PlatformNetwork(PlatformNode platformNode) => NodesInNetwork.Add(platformNode);

    public bool Contains(PlatformNode platformNode) => NodesInNetwork.Contains(platformNode);

    /// <summary>
    /// Merge two networks together.
    /// </summary>
    /// <param name="firstNetwork">Network to merge into</param>
    /// <param name="secondNetwork">Network to use in merge</param>
    public static void CombineNetworks(PlatformNetwork firstNetwork, PlatformNetwork secondNetwork)
    {
        if (firstNetwork != secondNetwork)
        {
            firstNetwork.NodesInNetwork.UnionWith(secondNetwork.NodesInNetwork);

            foreach (var platformNode in secondNetwork.NodesInNetwork)
            {
                platformNode.AttachedNetwork = firstNetwork;
            }
        }
    }

    /// <summary>
    /// Recreate network starting from parameter node. Used in situations where a network is split.
    /// </summary>
    /// <param name="nodeToMakeNetwork"></param>
    public static void UpdateNetwork(PlatformNode nodeToMakeNetwork)
    {
        PlatformNetwork newNetwork = new PlatformNetwork();

        TraverseConnectedNodes(nodeToMakeNetwork);

        foreach (var platformNode in _traversedNodes)
        {
            newNetwork.NodesInNetwork.Add(platformNode);
            platformNode.AttachedNetwork = newNetwork;
        }

        _traversedNodes.Clear();
    }

    static HashSet<PlatformNode> _traversedNodes = new HashSet<PlatformNode>();

    static void TraverseConnectedNodes(PlatformNode nodeFromNetwork)
    {
        _traversedNodes.Add(nodeFromNetwork);

        foreach(var platformNode in nodeFromNetwork.ConnectedNodes)
        {
            if (!_traversedNodes.Contains(platformNode))
            {
                TraverseConnectedNodes(platformNode);
            }
        }
    }
}

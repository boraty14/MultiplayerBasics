using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Renderer displayColorRenderer;
    
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))] [SerializeField] private string displayName = "Missing name";
    [SyncVar(hook = nameof(HandleDisplayColorUpdated))] [SerializeField] private Color displayColor = Color.black;

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }
    
    [Server]
    public void SetDisplayColor(Color newDisplayColor)
    {
        displayColor = newDisplayColor;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        if (newDisplayName.Length < 2 || newDisplayName.Length > 8) return;
        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor",newColor);
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My new name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion
    
}

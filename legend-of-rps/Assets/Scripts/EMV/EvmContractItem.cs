using System;
using System.Collections.Generic;
#if !UNITY_WEBGL

using Nethereum.Contracts;
using Nethereum.Web3;

/// <summary>
/// Wraps a list on Nethereum contract instances by chain
/// </summary>
[Obsolete("This class has been deprecated.")]
public class EvmContractItem
{
    /// <summary>
    /// the raw contract ABI
    /// </summary>
    public string Abi { get; set; }

    /// <summary>
    /// Contract instance by chain
    /// </summary>
    public Dictionary<string, EvmContractInstance> ChainContractMap { get; set; }

    public EvmContractItem()
    {
        ChainContractMap = new Dictionary<string, EvmContractInstance>();
    }

    public EvmContractItem(Web3 client, string abi, string chainId, string contractAddress)
    {
        ChainContractMap = new Dictionary<string, EvmContractInstance>();
        this.Abi = abi;

        AddChainInstance(client, chainId, contractAddress);
    }

    /// <summary>
    /// Add a contract instance for a specific chain.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="chainId"></param>
    /// <param name="contractAddress"></param>
    public void AddChainInstance(Web3 client, string chainId, string contractAddress)
    {
        if (!ChainContractMap.ContainsKey(chainId))
        {
            ChainEntry chainInfo = SupportedEvmChains.FromChainList(chainId);
            Contract contractInstance = client.Eth.GetContract(this.Abi, contractAddress);
            EvmContractInstance eci = new EvmContractInstance()
            {
                ChainInfo = chainInfo,
                ContractAddress = contractAddress,
                ContractInstance = contractInstance
            };

            ChainContractMap.Add(chainId, eci);
        }
    }
}
#endif

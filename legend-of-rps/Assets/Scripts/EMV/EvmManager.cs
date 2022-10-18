using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_WEBGL
using Cysharp.Threading.Tasks;
using Moralis.WebGL;
using Moralis.WebGL.Models;
using Moralis.WebGL.Hex.HexTypes;
using Moralis.WebGL.Web3Api.Client;
using Moralis.WebGL.SolanaApi.Client;
using Moralis.WebGL.Platform;
using Moralis.WebGL.Platform.Objects;
#else
using System.Threading.Tasks;
using Nethereum.Web3;
using WalletConnectSharp.Unity;
using WalletConnectSharp.Core;
using WalletConnectSharp.Core.Models;
using WalletConnectSharp.NEthereum;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Transactions;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
#endif

public class EvmManager : MonoBehaviour
{
    private static EvmContractManager contractManager;
    public static Web3 Web3Client { get; set; }
    private static ClientMeta clientMetaData;
    public static bool Initialized { get; set; }

    /// <summary>
    /// Initializes the connection to a Moralis server.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="serverUri"></param>
    /// <param name="hostData"></param>
    /// <param name="web3ApiKey"></param>
    public static async Task Initialize(ClientMeta clientMeta, string web3ApiKey = null)
    {
        // Create instance of Evm Contract Manager.
        contractManager = new EvmContractManager();

        clientMetaData = clientMeta;

        Initialized = true;
    }

    /// <summary>
    /// Initializes the Web3 connection to the supplied RPC Url. Call this to change the target chain.
    /// </summary>
    /// <param name="rpcUrl"></param>
    /// <returns></returns>
    public static void SetupWeb3()
    {
        if (clientMetaData == null)
        {
            Debug.Log("Wallet Connect Metadata not provided.");
            return;
        }

        WalletConnectSession client = WalletConnect.Instance.Session;

        // Create a web3 client using Wallet Connect as write client and a dummy client as read client.
        // Read operations should be via Web3API. Read operation are not implemented in the Web3 Client
        // Use the Web3API for read operations as available. If you must make run a read request that is
        // not supported by Web3API you will need to use the Wallet Connect method:
        // CreateProviderWithInfura(this WalletConnectProtocol protocol, string infruaId, string network = "mainnet", AuthenticationHeaderValue authenticationHeader = null)
        // We do not recommned this though
        Web3Client = new Web3(client.CreateProvider(new DeadRpcReadClient((string s) => {
            Debug.LogError(s);
        })));
    }

    /// <summary>
    /// Creates and adds a contract instance based on ABI and associates it to specified chain and address.
    /// </summary>
    /// <param name="key">How you identify the contract instance.</param>
    /// <param name="abi">ABI of the contract in standard ABI json format</param>
    /// <param name="baseChainId">The initial chain Id used to interact with this contract</param>
    /// <param name="baseContractAddress">The initial contract address of the contract on specified chain</param>
    [Obsolete("This method is deprecated. This method will not be replaced.")]
    public static void InsertContractInstance(string key, string abi, string baseChainId, string baseContractAddress)
    {
        if (Web3Client == null)
        {
            Debug.LogError("Web3 has not been setup yet.");
        }
        else
        {
            EvmContractItem eci = new EvmContractItem(Web3Client, abi, baseChainId, baseContractAddress);

            contractManager.InsertContractInstance(key, eci);
        }
    }

    /// <summary>
    /// Adds a contract address for a chain to a specific contract. Contract for key must exist.
    /// </summary>
    /// <param name="key">How you identify the contract instance.</param>
    /// <param name="chainId">The The chain the contract is deployed on.</param>
    /// <param name="contractAddress">Address the contract is deployed at</param>
    [Obsolete("This method is deprecated. This method will not be replaced.")]
    public static void AddContractChainAddress(string key, string chainId, string contractAddress)
    {
        if (Web3Client == null)
        {
            Debug.LogError("Web3 has not been setup yet.");
        }
        else
        {
            contractManager.AddChainInstanceToContract(key, Web3Client, chainId, contractAddress);
        }
    }

    /// <summary>
    /// Send Evm Transaction Async.
    /// </summary>
    /// <param name="contractKey"></param>
    /// <param name="chainId"></param>
    /// <param name="functionName"></param>
    /// <param name="fromaddress"></param>
    /// <param name="gas"></param>
    /// <param name="value"></param>
    /// <param name="functionInput"></param>
    /// <returns></returns>
    [Obsolete("This method is deprecated. Please use SendTransactionAsync or ExecuteContractFunction as appropriate.")]
    public static async Task<string> SendEvmTransactionAsync(string contractKey, string chainId, string functionName, string fromaddress, HexBigInteger gas, HexBigInteger value, object[] functionInput)
    {
        string result = null;

        if (contractManager.Contracts.ContainsKey(contractKey) &&
                contractManager.Contracts[contractKey].ChainContractMap.ContainsKey(chainId))
        {
            Tuple<bool, string, string> resp = await contractManager.SendTransactionAsync(contractKey, chainId, functionName, fromaddress, gas, value, functionInput);

            if (resp.Item1)
            {
                result = resp.Item2;
            }
            else
            {
                Debug.LogError($"Evm Transaction failed: {resp.Item3}");
            }
        }

        return result;
    }
}

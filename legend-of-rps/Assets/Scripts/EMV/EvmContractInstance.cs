using System;
#if !UNITY_WEBGL
using Nethereum.Contracts;
#endif
using System.Runtime.Serialization;

[DataContract]
public enum ChainList
{
    eth = 0x1,
    ropsten = 0x3,
    rinkeby = 0x4,
    goerli = 0x5,
    kovan = 0x2a,
    polygon = 0x89,
    mumbai = 0x13881,
    bsc = 0x38,
    bsc_testnet = 0x61,
    avalanche = 0xa86a,
    avalanche_testnet = 0xa869,
    fantom = 0xfa
};

public class ChainEntry
{
    /// <summary>
    /// Name of the chain.
    /// </summary>
    public string Name;
    /// <summary>
    /// Chain Id as integer
    /// </summary>
    public int ChainId;
    /// <summary>
    /// Chain Id as Enum value.
    /// </summary>
    public ChainList EnumValue;
}

/// <summary>
/// Defines specific copontract / chain instnace
/// </summary>
[Obsolete("This class has been deprecated.")]
public class EvmContractInstance
{
    /// <summary>
    /// Contract address on this chain.
    /// </summary>
    public string ContractAddress { get; set; }

#if !UNITY_WEBGL
    /// <summary>
    /// Contract Instance derived from ABI
    /// </summary>
    public Contract ContractInstance { get; set; }

#endif
    /// <summary>
    /// Evm Chain information.
    /// </summary>
    public ChainEntry ChainInfo { get; set; }
}

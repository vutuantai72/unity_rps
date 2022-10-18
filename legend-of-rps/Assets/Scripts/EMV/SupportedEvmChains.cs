using System;
using System.Collections.Generic;
using System.Linq;

#if UNITY_WEBGL

/// <summary>
/// Provides a easy way to get detail about an EVM chain for all EVM chains 
/// supported by the Moralis Web3API
/// </summary>
public class SupportedEvmChains
{
    private static List<ChainEntry> chains = new List<ChainEntry>();

    /// <summary>
    /// The list of EVM chains supported by the Moralis Web3API.
    /// </summary>
    public static List<ChainEntry> SupportedChains
    {
        get
        {
            if (chains.Count < 1) PopulateChainList();

            return chains;
        }
    }

    /// <summary>
    /// Retrieve an chain entry by enum value.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ChainEntry FromChainList(ChainList target)
    {
        ChainEntry result = null;

        var searchResult = from c in SupportedChains
                           where target.Equals(c.EnumValue)
                           select c;

        result = searchResult.FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Retrieve an chain entry by enum value.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ChainEntry FromChainList(string target)
    {
        ChainEntry result = null;

        var searchResult = from c in SupportedChains
                           where target.Equals(c.Name)
                           select c;

        result = searchResult.FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Retrieve an chain entry by enum value.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ChainEntry FromChainList(int target)
    {
        ChainEntry result = null;

        var searchResult = from c in SupportedChains
                           where target.Equals(c.ChainId)
                           select c;

        result = searchResult.FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Loops through the current ChainList enum and builds a friendly to use 
    /// name / chainId, enum val entry.
    /// </summary>
    private static void PopulateChainList()
    {
        chains.Clear();

        foreach (ChainList chain in Enum.GetValues(typeof(ChainList)))
        {
            chains.Add(new ChainEntry()
            {
                ChainId = (int)chain,
                EnumValue = chain,
                Name = chain.ToString()
            });
        }
    }
}
#else
/// <summary>
/// Provides a easy way to get detail about an EVM chain for all EVM chains 
/// supported by the Moralis Web3API
/// </summary>
public class SupportedEvmChains
{
    private static List<ChainEntry> chains = new List<ChainEntry>();

    /// <summary>
    /// The list of EVM chains supported by the Moralis Web3API.
    /// </summary>
    public static List<ChainEntry> SupportedChains 
    { 
        get 
        {
            if (chains.Count < 1) PopulateChainList();

            return chains; 
        } 
    }

    /// <summary>
    /// Retrieve an chain entry by enum value.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ChainEntry FromChainList(ChainList target)
    {
        ChainEntry result = null;

        var searchResult = from c in SupportedChains
                            where target.Equals(c.EnumValue)
                            select c;

        result = searchResult.FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Retrieve an chain entry by enum value.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ChainEntry FromChainList(string target)
    {
        ChainEntry result = null;

        var searchResult = from c in SupportedChains
                            where target.Equals(c.Name)
                            select c;

        result = searchResult.FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Retrieve an chain entry by enum value.
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public static ChainEntry FromChainList(int target)
    {
        ChainEntry result = null;

        var searchResult = from c in SupportedChains
                            where target.Equals(c.ChainId)
                            select c;

        result = searchResult.FirstOrDefault();

        return result;
    }

    /// <summary>
    /// Loops through the current ChainList enum and builds a friendly to use 
    /// name / chainId, enum val entry.
    /// </summary>
    private static void PopulateChainList()
    {
        chains.Clear();

        foreach (ChainList chain in Enum.GetValues(typeof(ChainList)))
        {
            chains.Add(new ChainEntry()
            {
                ChainId = (int)chain,
                EnumValue = chain,
                Name = chain.ToString()
            });
        }
    }
}
#endif

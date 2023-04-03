using Nethereum.Web3;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace PancakeSwapNET.Classes
{
    public class GasSettings
    {
        public readonly BigInteger gasPrice;
        public readonly BigInteger gasLimit;

        public GasSettings(BigInteger gasLimitInWei, BigInteger gasPrice)
        {
            this.gasLimit = gasLimitInWei;
            this.gasPrice = gasPrice;
        }

        public GasSettings(int gasLimit, BigInteger gasPrice)
        {
            this.gasLimit = Web3.Convert.ToWei(gasLimit);
            this.gasPrice = gasPrice;
        }
    }
}
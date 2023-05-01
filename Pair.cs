using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using System.Numerics;

namespace PancakeSwapNET
{
    public class Pair
    {
        private readonly Web3? _web3;
        private Contract? _contract;

        private const string _contractAbi = """
                        [
              {
                "inputs": [],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "constructor"
              },
              {
                "anonymous": false,
                "inputs": [
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "owner",
                    "type": "address"
                  },
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "spender",
                    "type": "address"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "value",
                    "type": "uint256"
                  }
                ],
                "name": "Approval",
                "type": "event"
              },
              {
                "anonymous": false,
                "inputs": [
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "sender",
                    "type": "address"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "amount0",
                    "type": "uint256"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "amount1",
                    "type": "uint256"
                  },
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  }
                ],
                "name": "Burn",
                "type": "event"
              },
              {
                "anonymous": false,
                "inputs": [
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "sender",
                    "type": "address"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "amount0",
                    "type": "uint256"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "amount1",
                    "type": "uint256"
                  }
                ],
                "name": "Mint",
                "type": "event"
              },
              {
                "anonymous": false,
                "inputs": [
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "sender",
                    "type": "address"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "amount0In",
                    "type": "uint256"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "amount1In",
                    "type": "uint256"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "amount0Out",
                    "type": "uint256"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "amount1Out",
                    "type": "uint256"
                  },
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  }
                ],
                "name": "Swap",
                "type": "event"
              },
              {
                "anonymous": false,
                "inputs": [
                  {
                    "indexed": false,
                    "internalType": "uint112",
                    "name": "reserve0",
                    "type": "uint112"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint112",
                    "name": "reserve1",
                    "type": "uint112"
                  }
                ],
                "name": "Sync",
                "type": "event"
              },
              {
                "anonymous": false,
                "inputs": [
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "from",
                    "type": "address"
                  },
                  {
                    "indexed": true,
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "indexed": false,
                    "internalType": "uint256",
                    "name": "value",
                    "type": "uint256"
                  }
                ],
                "name": "Transfer",
                "type": "event"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "DOMAIN_SEPARATOR",
                "outputs": [
                  {
                    "internalType": "bytes32",
                    "name": "",
                    "type": "bytes32"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "MINIMUM_LIQUIDITY",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "PERMIT_TYPEHASH",
                "outputs": [
                  {
                    "internalType": "bytes32",
                    "name": "",
                    "type": "bytes32"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "name": "allowance",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "spender",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "value",
                    "type": "uint256"
                  }
                ],
                "name": "approve",
                "outputs": [
                  {
                    "internalType": "bool",
                    "name": "",
                    "type": "bool"
                  }
                ],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "name": "balanceOf",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  }
                ],
                "name": "burn",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amount0",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amount1",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "decimals",
                "outputs": [
                  {
                    "internalType": "uint8",
                    "name": "",
                    "type": "uint8"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "factory",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "getReserves",
                "outputs": [
                  {
                    "internalType": "uint112",
                    "name": "_reserve0",
                    "type": "uint112"
                  },
                  {
                    "internalType": "uint112",
                    "name": "_reserve1",
                    "type": "uint112"
                  },
                  {
                    "internalType": "uint32",
                    "name": "_blockTimestampLast",
                    "type": "uint32"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "_token0",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "_token1",
                    "type": "address"
                  }
                ],
                "name": "initialize",
                "outputs": [],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "kLast",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  }
                ],
                "name": "mint",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "name",
                "outputs": [
                  {
                    "internalType": "string",
                    "name": "",
                    "type": "string"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "name": "nonces",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "owner",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "spender",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "value",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint8",
                    "name": "v",
                    "type": "uint8"
                  },
                  {
                    "internalType": "bytes32",
                    "name": "r",
                    "type": "bytes32"
                  },
                  {
                    "internalType": "bytes32",
                    "name": "s",
                    "type": "bytes32"
                  }
                ],
                "name": "permit",
                "outputs": [],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "price0CumulativeLast",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "price1CumulativeLast",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  }
                ],
                "name": "skim",
                "outputs": [],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amount0Out",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amount1Out",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "bytes",
                    "name": "data",
                    "type": "bytes"
                  }
                ],
                "name": "swap",
                "outputs": [],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "symbol",
                "outputs": [
                  {
                    "internalType": "string",
                    "name": "",
                    "type": "string"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [],
                "name": "sync",
                "outputs": [],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "token0",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "token1",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": true,
                "inputs": [],
                "name": "totalSupply",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "",
                    "type": "uint256"
                  }
                ],
                "payable": false,
                "stateMutability": "view",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "value",
                    "type": "uint256"
                  }
                ],
                "name": "transfer",
                "outputs": [
                  {
                    "internalType": "bool",
                    "name": "",
                    "type": "bool"
                  }
                ],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "constant": false,
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "from",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "value",
                    "type": "uint256"
                  }
                ],
                "name": "transferFrom",
                "outputs": [
                  {
                    "internalType": "bool",
                    "name": "",
                    "type": "bool"
                  }
                ],
                "payable": false,
                "stateMutability": "nonpayable",
                "type": "function"
              }
            ]
            """;

        //contract vars
        public const decimal fee = 0.0025m;

        public string Factory
        { get { return factory!; } }

        private string? factory;

        public string Token0
        { get { return token0!; } }

        private string? token0;

        public string Token1
        { get { return token1!; } }

        private string? token1;

        public Pair(Web3 web3)
        {
            _web3 = web3;
        }

        /// <summary>
        /// Initializes a new instance of the Pair class with the given address.
        /// </summary>
        /// <param name="pairAddress">The address of the pair.</param>
        /// <returns>A Task that represents the asynchronous operation.</returns>
        public async Task InitPair(string pairAddress)
        {
            if (_web3 is null)
                throw new Exception("Web3 not initialized");
            _contract = _web3.Eth.GetContract(_contractAbi, pairAddress);

            factory = await GetFactoryAddressAsync();
            token0 = await GetToken0AddressAsync();
            token1 = await GetToken1AddressAsync();
        }

        /// <summary>
        /// Gets the address of the factory contract.
        /// </summary>
        /// <returns>The address of the factory contract.</returns>
        private async Task<string> GetFactoryAddressAsync()
        {
            Function function = _contract!.GetFunction("factory");
            string address = await function.CallAsync<string>();
            if (String.IsNullOrEmpty(address))
                throw new Exception("Incorrect pair address");
            return address;
        }

        /// <summary>
        /// Gets the address of the token0 from the contract.
        /// </summary>
        /// <returns>The address of the token0.</returns>
        private async Task<string> GetToken0AddressAsync()
        {
            Function function = _contract!.GetFunction("token0");
            string address = await function.CallAsync<string>();
            if (String.IsNullOrEmpty(address))
                throw new Exception("Incorrect pair address");
            return address;
        }

        /// <summary>
        /// Gets the address of the token1 from the contract.
        /// </summary>
        /// <returns>The address of the token1.</returns>
        private async Task<string> GetToken1AddressAsync()
        {
            Function function = _contract!.GetFunction("token1");
            string address = await function.CallAsync<string>();
            if (String.IsNullOrEmpty(address))
                throw new Exception("Incorrect pair address");
            return address;
        }

        public async Task<BigInteger> GetPrice0CumulativeLastAsync()
        {
            Function function = _contract!.GetFunction("price0CumulativeLast");
            return await function.CallAsync<BigInteger>();
        }

        public async Task<BigInteger> GetPrice1CumulativeLastAsync()
        {
            Function function = _contract!.GetFunction("price1CumulativeLast");
            return await function.CallAsync<BigInteger>();
        }

        public async Task<BigInteger> GetkLastAsync()
        {
            Function function = _contract!.GetFunction("kLast");
            return await function.CallAsync<BigInteger>();
        }

        /// <summary>
        /// Asynchronously calls the getReserves function on the smart contract.
        /// </summary>
        /// <returns>
        /// A GetReservesFunc object containing the result of the function call.
        /// </returns>
        public async Task<GetReservesFunc> GetReservesAsync()
        {
            Function function = _contract!.GetFunction("getReserves");
            return await function.CallAsync<GetReservesFunc>();
        }

        /// <summary>
        /// Deducts the price impact from the specified percentage
        /// </summary>
        /// <param name="percent">Percent (0.01 = 1%)</param>
        /// <param name="firstTokenDecimals"></param>
        /// <param name="secondTokenDecimals"></param>
        /// <returns>Returns the percentage price and number of tokens to exchange for each token</returns>
        public async Task<decimal[]> CalculatePriceImpactAsync(decimal percent, int firstTokenDecimals = 18, int secondTokenDecimals = 18)
        {
            var reserves = await GetReservesAsync();
            var amount_traded_token = Web3.Convert.FromWei(reserves.Reserve0, firstTokenDecimals) * percent / ((1 - percent) * (1 - fee));
            var amount_traded_token1 = Web3.Convert.FromWei(reserves.Reserve1, secondTokenDecimals) * percent / ((1 - percent) * (1 - fee));
            var amountInToken = amount_traded_token * (1 - fee);
            var amountInToken1 = amount_traded_token1 * (1 - fee);
            var price_impact_trade_token = amountInToken / (Web3.Convert.FromWei(reserves.Reserve0, firstTokenDecimals) + amountInToken);
            var price_impact_trade_token1 = amountInToken1 / (Web3.Convert.FromWei(reserves.Reserve1, secondTokenDecimals) + amountInToken1);
            return new decimal[] { price_impact_trade_token, amount_traded_token, price_impact_trade_token1, amount_traded_token1 };
        }

        [FunctionOutput]
        public class GetReservesFunc : IFunctionOutputDTO
        {
            [Parameter("uint112", "_reserve0")]
            public BigInteger Reserve0 { get; set; }

            [Parameter("uint112", "_reserve1")]
            public BigInteger Reserve1 { get; set; }

            [Parameter("uint32", "_blockTimestampLast")]
            public BigInteger BlockTimestampLast { get; set; }
        }
    }
}
﻿using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PancakeSwapNET
{
    public class Pair
    {
        private readonly Web3 _web3;
        private readonly Account _account;
        private readonly Contract _contract;
        private const decimal fee = 0.0025m;

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

        public Pair(string pairAddress, string connectURL, string primaryKey)
        {
            _account = new(primaryKey);
            _web3 = new(_account, connectURL);
            _contract = _web3.Eth.GetContract(_contractAbi, pairAddress);
        }

        public async Task<GetReservesFunc> GetReserves()
        {
            Function function = _contract.GetFunction("getReserves");
            var price = await function.CallAsync<GetReservesFunc>();
            return await Task.FromResult(price);
        }

        /// <summary>
        /// Высчитывает указаный импакт цены
        /// </summary>
        /// <param name="priceImpact">Процент(0.01)</param>
        /// <returns></returns>
        public async Task<decimal[]> CalculatePriceImpact(decimal priceImpact)
        {
            var reserves = await GetReserves();
            var amount_traded_token = Web3.Convert.FromWei(reserves.Reserve0) * priceImpact / ((1 - priceImpact) * (1 - fee));
            var amount_traded_token1 = Web3.Convert.FromWei(reserves.Reserve1) * priceImpact / ((1 - priceImpact) * (1 - fee));
            var amountInToken = amount_traded_token * (1 - fee);
            var amountInToken1 = amount_traded_token1 * (1 - fee);
            var price_impact_trade_token = amountInToken / (Web3.Convert.FromWei(reserves.Reserve0) + amountInToken);
            var price_impact_trade_token1 = amountInToken1 / (Web3.Convert.FromWei(reserves.Reserve1) + amountInToken1);
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
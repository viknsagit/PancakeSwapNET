﻿using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.HostWallet;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using PancakeSwapNET.Classes;
using System.Numerics;

namespace PancakeSwapNET
{
    public class Router
    {
        private readonly Web3 _web3;
        private readonly Account _account;
        private readonly Contract _contract;
        public readonly Factory Factory;

        private const string _contractAbi = """
            [
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "_factory",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "_WETH",
                    "type": "address"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "constructor"
              },
              {
                "inputs": [],
                "name": "WETH",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "stateMutability": "view",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "tokenA",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "tokenB",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountADesired",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountBDesired",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountAMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountBMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "addLiquidity",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountA",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountB",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "token",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountTokenDesired",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountTokenMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountETHMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "addLiquidityETH",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountToken",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountETH",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "payable",
                "type": "function"
              },
              {
                "inputs": [],
                "name": "factory",
                "outputs": [
                  {
                    "internalType": "address",
                    "name": "",
                    "type": "address"
                  }
                ],
                "stateMutability": "view",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountOut",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "reserveIn",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "reserveOut",
                    "type": "uint256"
                  }
                ],
                "name": "getAmountIn",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountIn",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "pure",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountIn",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "reserveIn",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "reserveOut",
                    "type": "uint256"
                  }
                ],
                "name": "getAmountOut",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountOut",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "pure",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountOut",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  }
                ],
                "name": "getAmountsIn",
                "outputs": [
                  {
                    "internalType": "uint256[]",
                    "name": "amounts",
                    "type": "uint256[]"
                  }
                ],
                "stateMutability": "view",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountIn",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  }
                ],
                "name": "getAmountsOut",
                "outputs": [
                  {
                    "internalType": "uint256[]",
                    "name": "amounts",
                    "type": "uint256[]"
                  }
                ],
                "stateMutability": "view",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountA",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "reserveA",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "reserveB",
                    "type": "uint256"
                  }
                ],
                "name": "quote",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountB",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "pure",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "tokenA",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "tokenB",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountAMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountBMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "removeLiquidity",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountA",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountB",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "token",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountTokenMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountETHMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "removeLiquidityETH",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountToken",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountETH",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "token",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountTokenMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountETHMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "removeLiquidityETHSupportingFeeOnTransferTokens",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountETH",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "token",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountTokenMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountETHMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  },
                  {
                    "internalType": "bool",
                    "name": "approveMax",
                    "type": "bool"
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
                "name": "removeLiquidityETHWithPermit",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountToken",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountETH",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "token",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountTokenMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountETHMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  },
                  {
                    "internalType": "bool",
                    "name": "approveMax",
                    "type": "bool"
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
                "name": "removeLiquidityETHWithPermitSupportingFeeOnTransferTokens",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountETH",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "address",
                    "name": "tokenA",
                    "type": "address"
                  },
                  {
                    "internalType": "address",
                    "name": "tokenB",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "liquidity",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountAMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountBMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  },
                  {
                    "internalType": "bool",
                    "name": "approveMax",
                    "type": "bool"
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
                "name": "removeLiquidityWithPermit",
                "outputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountA",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountB",
                    "type": "uint256"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountOut",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapETHForExactTokens",
                "outputs": [
                  {
                    "internalType": "uint256[]",
                    "name": "amounts",
                    "type": "uint256[]"
                  }
                ],
                "stateMutability": "payable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountOutMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapExactETHForTokens",
                "outputs": [
                  {
                    "internalType": "uint256[]",
                    "name": "amounts",
                    "type": "uint256[]"
                  }
                ],
                "stateMutability": "payable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountOutMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapExactETHForTokensSupportingFeeOnTransferTokens",
                "outputs": [],
                "stateMutability": "payable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountIn",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountOutMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapExactTokensForETH",
                "outputs": [
                  {
                    "internalType": "uint256[]",
                    "name": "amounts",
                    "type": "uint256[]"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountIn",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountOutMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapExactTokensForETHSupportingFeeOnTransferTokens",
                "outputs": [],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountIn",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountOutMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapExactTokensForTokens",
                "outputs": [
                  {
                    "internalType": "uint256[]",
                    "name": "amounts",
                    "type": "uint256[]"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountIn",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountOutMin",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapExactTokensForTokensSupportingFeeOnTransferTokens",
                "outputs": [],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountOut",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountInMax",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapTokensForExactETH",
                "outputs": [
                  {
                    "internalType": "uint256[]",
                    "name": "amounts",
                    "type": "uint256[]"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "inputs": [
                  {
                    "internalType": "uint256",
                    "name": "amountOut",
                    "type": "uint256"
                  },
                  {
                    "internalType": "uint256",
                    "name": "amountInMax",
                    "type": "uint256"
                  },
                  {
                    "internalType": "address[]",
                    "name": "path",
                    "type": "address[]"
                  },
                  {
                    "internalType": "address",
                    "name": "to",
                    "type": "address"
                  },
                  {
                    "internalType": "uint256",
                    "name": "deadline",
                    "type": "uint256"
                  }
                ],
                "name": "swapTokensForExactTokens",
                "outputs": [
                  {
                    "internalType": "uint256[]",
                    "name": "amounts",
                    "type": "uint256[]"
                  }
                ],
                "stateMutability": "nonpayable",
                "type": "function"
              },
              {
                "stateMutability": "payable",
                "type": "receive"
              }
            ]
            """;

        /// <summary>
        /// Constructor for Router class. Initializes web3, account and contract objects.
        /// </summary>
        /// <param name="chainURL">URL of the blockchain</param>
        /// <param name="contractAddress">Address of the contract</param>
        /// <param name="primaryKey">Primary key of the account</param>
        /// <returns>
        /// Instance of Router class
        /// </returns>
        public Router(string chainURL, string contractAddress, string primaryKey)
        {
            _account = new(primaryKey);
            _web3 = new(_account, chainURL);

            //Разобраться с газом
            _account.TransactionManager.UseLegacyAsDefault = true;
            _account.TransactionManager.DefaultGasPrice = 1000000000;
            _account.TransactionManager.DefaultGas = 450000;

            _contract = _web3.Eth.GetContract(_contractAbi, contractAddress);
            Factory = new(_web3, GetFactoryAddressAsync().GetAwaiter().GetResult());
        }

        /// <summary>
        /// Constructor for Router class. Initializes web3, account, contract and factory.
        /// </summary>
        /// <param name="chainURL">URL of the blockchain</param>
        /// <param name="contractAddress">Address of the contract</param>
        /// <returns>
        /// Instance of Router class.
        /// </returns>
        public Router(string chainURL, string contractAddress)
        {
            _account = new(EthECKey.GenerateKey());
            _web3 = new(_account, chainURL);

            //Разобраться с газом
            _account.TransactionManager.UseLegacyAsDefault = true;
            _account.TransactionManager.DefaultGasPrice = 1000000000;
            _account.TransactionManager.DefaultGas = 450000;

            _contract = _web3.Eth.GetContract(_contractAbi, contractAddress);
            Factory = new(_web3, GetFactoryAddressAsync().GetAwaiter().GetResult());
        }

        /// <summary>
        /// Constructor for Router class. Initializes web3, account and contract.
        /// </summary>
        /// <param name="chainURL">URL of the blockchain</param>
        /// <param name="contractAddress">Address of the contract</param>
        /// <param name="primaryKey">Primary key of the account</param>
        /// <param name="gasSettings">Gas settings for the transaction</param>
        /// <returns>
        /// Instance of Router class
        /// </returns>
        public Router(string chainURL, string contractAddress, string primaryKey, GasSettings gasSettings)
        {
            _account = new(primaryKey);
            _web3 = new(_account, chainURL);

            //Разобраться с газом
            _account.TransactionManager.UseLegacyAsDefault = true;
            _account.TransactionManager.DefaultGasPrice = gasSettings.gasPrice;
            _account.TransactionManager.DefaultGas = gasSettings.gasLimit;

            _contract = _web3.Eth.GetContract(_contractAbi, contractAddress);
            Factory = new(_web3, GetFactoryAddressAsync().GetAwaiter().GetResult());
        }

        /// <summary>
        /// Constructor for Router class. Initializes web3, account, contract and factory.
        /// </summary>
        /// <param name="chainURL">URL of the blockchain</param>
        /// <param name="contractAddress">Address of the contract</param>
        /// <param name="gasSettings">Gas settings for the transaction</param>
        /// <returns>
        /// Instance of Router class
        /// </returns>
        public Router(string chainURL, string contractAddress, GasSettings gasSettings)
        {
            _account = new(EthECKey.GenerateKey());
            _web3 = new(_account, chainURL);

            //Разобраться с газом
            _account.TransactionManager.UseLegacyAsDefault = true;
            _account.TransactionManager.DefaultGasPrice = gasSettings.gasPrice;
            _account.TransactionManager.DefaultGas = gasSettings.gasLimit;

            _contract = _web3.Eth.GetContract(_contractAbi, contractAddress);
            Factory = new(_web3, GetFactoryAddressAsync().GetAwaiter().GetResult());
        }

        /// <summary>
        /// Gets a Pair object for the given pair address.
        /// </summary>
        /// <param name="pairAddress">The address of the pair.</param>
        /// <returns>A Pair object for the given pair address.</returns>
        public async Task<Pair> GetPairAsync(string pairAddress)
        {
            Pair pair = new(_web3);
            await pair.InitPair(pairAddress);
            return pair;
        }

        /// <summary>
        /// Gets the Pair object for the given token addresses.
        /// </summary>
        /// <param name="token0Address">The address of the first token.</param>
        /// <param name="token1Address">The address of the second token.</param>
        /// <returns>
        /// The Pair object for the given token addresses.
        /// </returns>
        public async Task<Pair> GetPairAsync(string token0Address, string token1Address)
        {
            Pair pair = new(_web3);
            await pair.InitPair(await Factory.GetPairAddressByTokensAsync(token0Address, token1Address));
            return pair;
        }

        /// <summary>
        /// Get (pair) the relationship of a token to another token
        /// </summary>
        /// <param name="amountToSell"></param>
        /// <param name="tokenAddress"></param>
        /// <param name="tokenAddress1"></param>
        /// <returns>Returns (pair) the relationship of a token to another token</returns>
        public async Task<List<BigInteger>> GetAmountsOutAsync(BigInteger amountToSell, string tokenAddress, string tokenAddress1)
        {
            Function function = _contract.GetFunction("getAmountsOut");
            var price = await function.CallAsync<List<BigInteger>>(amountToSell, new string[] { tokenAddress, tokenAddress1 });
            return price;
        }

        /// <summary>
        /// Get (pair) the relationship of a token to another token
        /// </summary>
        /// <param name="amountToSell"></param>
        /// <param name="tokenAddress"></param>
        /// <returns>Returns (pair) the WETH relation to another token</returns>
        public async Task<List<BigInteger>> GetAmountsOutAsync(BigInteger amountToSell, string tokenAddress)
        {
            Function function = _contract.GetFunction("getAmountsOut");
            List<BigInteger> price = await function.CallAsync<List<BigInteger>>(amountToSell, new string[] { await GetWETHAddressAsync(), tokenAddress });
            return price;
        }

        /// <summary>
        /// Gets the amount out from the given amount in, reserve in and reserve out.
        /// </summary>
        /// <param name="amountIn">The amount in.</param>
        /// <param name="reserveIn">The reserve in.</param>
        /// <param name="reserveOut">The reserve out.</param>
        /// <returns>The amount out.</returns>
        public async Task<BigInteger> GetAmountOutAsync(BigInteger amountIn, BigInteger reserveIn, BigInteger reserveOut)
        {
            Function function = _contract.GetFunction("getAmountOut");
            BigInteger price = await function.CallAsync<BigInteger>(amountIn, reserveIn, reserveOut);
            return price;
        }

        /// <summary>
        /// Gets the amount of the input token given the amount of the output token, the reserve of the input token and the reserve of the output token.
        /// </summary>
        /// <param name="amountOut">The amount of the output token.</param>
        /// <param name="reserveIn">The reserve of the input token.</param>
        /// <param name="reserveOut">The reserve of the output token.</param>
        /// <returns>The amount of the input token.</returns>
        public async Task<BigInteger> GetAmountInAsync(BigInteger amountOut, BigInteger reserveIn, BigInteger reserveOut)
        {
            Function function = _contract.GetFunction("getAmountIn");
            BigInteger price = await function.CallAsync<BigInteger>(amountOut, reserveIn, reserveOut);
            return price;
        }

        /// <summary>
        /// Gets the amounts in the specified token addresses.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="tokenAddress">The token address.</param>
        /// <param name="tokenAddress1">The token address 1.</param>
        /// <returns>
        /// A list of BigIntegers representing the amounts in the specified token addresses.
        /// </returns>
        public async Task<List<BigInteger>> GetAmountsInAsync(BigInteger amount, string tokenAddress, string tokenAddress1)
        {
            Function function = _contract.GetFunction("getAmountsIn");
            List<BigInteger> price = await function.CallAsync<List<BigInteger>>(amount, new string[] { tokenAddress, tokenAddress1 });
            return price;
        }

        /// <summary>
        /// Gets the WETH address from the contract.
        /// </summary>
        /// <returns>The WETH address.</returns>
        public async Task<string> GetWETHAddressAsync()
        {
            Function function = _contract.GetFunction("WETH");
            return await function.CallAsync<string>();
        }

        /// <summary>
        /// Gets the address of the factory contract.
        /// </summary>
        /// <returns>The address of the factory contract.</returns>
        public async Task<string> GetFactoryAddressAsync()
        {
            Function function = _contract.GetFunction("factory");
            return await function.CallAsync<string>();
        }

        /// <summary>
        /// Receive as many output tokens as possible for an exact amount of BNB.
        /// </summary>
        /// <param name="amountInEth">Payable amount of input tokens.</param>
        /// <param name="tokenAddress">The address of the token to which you want to exchange</param>
        /// <returns>As many output tokens as possible for an exact amount of BNB.</returns>
        public async Task<TransactionReceipt> SwapExactETHForTokensAsync(decimal amountInEth, string tokenAddress)
        {
            var amounts = await GetAmountsOutAsync(Web3.Convert.ToWei(amountInEth, Nethereum.Util.UnitConversion.EthUnit.Ether), tokenAddress);
            Function function = _contract.GetFunction("swapExactETHForTokens");
            var input = function.CreateTransactionInput(_account.Address, null, value: new Nethereum.Hex.HexTypes.HexBigInteger(Web3.Convert.ToWei(amountInEth, Nethereum.Util.UnitConversion.EthUnit.Ether)), amounts[1], new string[] { await GetWETHAddressAsync(), tokenAddress }, _account.Address, DateTimeOffset.Now.AddMinutes(15).ToUnixTimeSeconds());
            return await _account.TransactionManager.SendTransactionAndWaitForReceiptAsync(input);
        }

        /// <summary>
        /// Receive as much BNB as possible for an exact amount of input tokens.
        /// </summary>
        /// <param name="amountInEth">Payable amount of input tokens.</param>
        /// <param name="tokenAddress">The address of the token to which you want to exchange</param>
        /// <returns></returns>
        public async Task<TransactionReceipt> SwapExactTokensForETHAsync(decimal amountInEth, string tokenAddress)
        {
            var amountsout = await GetAmountsOutAsync(DecimalToWei(amountInEth), tokenAddress, await GetWETHAddressAsync());
            var amountsin = await GetAmountsInAsync(DecimalToWei(amountInEth), tokenAddress, await GetWETHAddressAsync());
            Function function = _contract.GetFunction("swapExactTokensForETH");
            var input = function.CreateTransactionInput(_account.Address, amountsin[1], amountsout[1], new string[] { tokenAddress, await GetWETHAddressAsync() }, _account.Address, DateTimeOffset.Now.AddMinutes(15).ToUnixTimeSeconds());
            return await _account.TransactionManager.SendTransactionAndWaitForReceiptAsync(input);
        }

        /// <summary>
        /// Receive an exact amount of ETH for as few input tokens as possible.
        /// </summary>
        /// <param name="amountInEth">Payable amount of input tokens.</param>
        /// <param name="tokenAddress">The address of the token to which you want to exchange</param>
        /// <returns></returns>
        public async Task<TransactionReceipt> SwapTokensForExactETHAsync(decimal amountInEth, string tokenAddress)
        {
            var amountsout = await GetAmountsOutAsync(DecimalToWei(amountInEth), tokenAddress, await GetWETHAddressAsync());
            var amountsin = await GetAmountsInAsync(DecimalToWei(amountInEth), tokenAddress, await GetWETHAddressAsync());
            Function function = _contract.GetFunction("swapTokensForExactETH");
            var input = function.CreateTransactionInput(_account.Address, amountsout[1], amountsin[1], new string[] { tokenAddress, await GetWETHAddressAsync() }, _account.Address, DateTimeOffset.Now.AddMinutes(15).ToUnixTimeSeconds());
            return await _account.TransactionManager.SendTransactionAndWaitForReceiptAsync(input);
        }

        /// <summary>
        /// Receive as much BNB as possible for an exact amount of tokens. Supports tokens that take a fee on transfer.
        /// </summary>
        /// <param name="amountInEth">Payable amount of input tokens.</param>
        /// <param name="tokenAddress">The address of the token to which you want to exchange</param>
        /// <returns></returns>
        public async Task<TransactionReceipt> SwapExactTokensForETHSupportingFeeOnTransferTokensAsync(decimal amountInEth, string tokenAddress)
        {
            var amountsout = await GetAmountsOutAsync(DecimalToWei(amountInEth), tokenAddress, await GetWETHAddressAsync());
            var amountsin = await GetAmountsInAsync(DecimalToWei(amountInEth), tokenAddress, await GetWETHAddressAsync());
            Function function = _contract.GetFunction("swapExactTokensForETHSupportingFeeOnTransferTokens");
            var input = function.CreateTransactionInput(_account.Address, amountsin[1], amountsout[1], new string[] { tokenAddress, await GetWETHAddressAsync() }, _account.Address, DateTimeOffset.Now.AddMinutes(15).ToUnixTimeSeconds());
            return await _account.TransactionManager.SendTransactionAndWaitForReceiptAsync(input);
        }

        /// <summary>
        /// Receive as many output tokens as possible for an exact amount of input tokens.
        /// </summary>
        /// <param name="amountIn">Payable amount of input tokens.</param>
        /// <param name="tokenAddress"></param>
        /// <param name="tokenAddress1"></param>
        /// <returns></returns>
        public async Task<TransactionReceipt> SwapExactTokensForTokensAsync(decimal amountIn, string tokenAddress, string tokenAddress1)
        {
            var amountsout = await GetAmountsOutAsync(DecimalToWei(amountIn), tokenAddress, tokenAddress1);
            var amountsin = await GetAmountsInAsync(DecimalToWei(amountIn), tokenAddress, tokenAddress1);
            Function function = _contract.GetFunction("swapExactTokensForTokens");
            var input = function.CreateTransactionInput(_account.Address, amountsin[1], amountsout[1], new string[] { tokenAddress, tokenAddress1 }, _account.Address, DateTimeOffset.Now.AddMinutes(15).ToUnixTimeSeconds());
            return await _account.TransactionManager.SendTransactionAndWaitForReceiptAsync(input);
        }

        /// <summary>
        /// Executes a swap of tokens for tokens with exact amounts specified.
        /// </summary>
        /// <param name="amountsIn">The amount of tokens to be swapped in.</param>
        /// <param name="amountsOut">The amount of tokens to be swapped out.</param>
        /// <param name="tokenAddress">The address of the token to be swapped in.</param>
        /// <param name="tokenAddress1">The address of the token to be swapped out.</param>
        /// <returns>A transaction receipt.</returns>
        public async Task<TransactionReceipt> SwapExactTokensForTokensManualAsync(BigInteger amountsIn, BigInteger amountsOut, string tokenAddress, string tokenAddress1)
        {
            Function function = _contract.GetFunction("swapExactTokensForTokens");
            var input = function.CreateTransactionInput(_account.Address, amountsIn, amountsOut, new string[] { tokenAddress, tokenAddress1 }, _account.Address, DateTimeOffset.Now.AddMinutes(15).ToUnixTimeSeconds());
            return await _account.TransactionManager.SendTransactionAndWaitForReceiptAsync(input);
        }

        private static BigInteger DecimalToWei(decimal amount)
        {
            return Web3.Convert.ToWei(amount, Nethereum.Util.UnitConversion.EthUnit.Ether);
        }
    }
}
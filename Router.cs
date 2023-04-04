using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
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

        /// <param name="chainURL">URL to connect to blockchain</param>
        /// <param name="contractAddress">PancakeSwap contract address</param>
        /// <param name="primaryKey">Wallet primary key from which the functions will be used</param>
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

        /// <param name="chainURL">URL to connect to blockchain</param>
        /// <param name="contractAddress">PancakeSwap contract address</param>
        /// <param name="primaryKey">Wallet primary key from which the functions will be used</param>
        /// <param name="gasSettings"></param>
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

        public async Task<Pair> GetPairAsync(string pairAddress)
        {
            Pair pair = new(_web3);
            await pair.InitPair(pairAddress);
            return pair;
        }

        /// <summary>
        /// Returns (pair) the relationship of a token to another token
        /// </summary>
        /// <param name="amountToSell"></param>
        /// <param name="token1"></param>
        /// <param name="token2"></param>
        /// <returns></returns>
        public async Task<List<BigInteger>> GetAmountsOutAsync(BigInteger amountToSell, string tokenAddress, string tokenAddress1)
        {
            Function function = _contract.GetFunction("getAmountsOut");
            var price = await function.CallAsync<List<BigInteger>>(amountToSell, new string[] { tokenAddress, tokenAddress1 });
            return price;
        }

        /// <summary>
        /// Returns (pair) the WETH relation to another token
        /// </summary>
        /// <param name="amountToSell"></param>
        /// <param name="tokenAddress"></param>
        /// <returns></returns>
        public async Task<List<BigInteger>> GetAmountsOutAsync(BigInteger amountToSell, string tokenAddress)
        {
            Function function = _contract.GetFunction("getAmountsOut");
            List<BigInteger> price = await function.CallAsync<List<BigInteger>>(amountToSell, new string[] { await GetWETHAddressAsync(), tokenAddress });
            return price;
        }

        public async Task<BigInteger> GetAmountInAsync(decimal amountOut, decimal reserveIn, decimal reserveOut)
        {
            Function function = _contract.GetFunction("getAmountIn");
            BigInteger price = await function.CallAsync<BigInteger>(amountOut, reserveIn, reserveOut);
            return price;
        }

        public async Task<List<BigInteger>> GetAmountsInAsync(BigInteger amount, string tokenAddress, string tokenAddress1)
        {
            Function function = _contract.GetFunction("getAmountsIn");
            List<BigInteger> price = await function.CallAsync<List<BigInteger>>(amount, new string[] { tokenAddress, tokenAddress1 });
            return price;
        }

        public async Task<string> GetWETHAddressAsync()
        {
            Function function = _contract.GetFunction("WETH");
            return await function.CallAsync<string>();
        }

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
        /// <returns></returns>
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

        public async Task<TransactionReceipt> SwapExactTokensForETHWithDecimalsAsync(decimal amount, string tokenAddress, int decimalPlacesFromUnit)
        {
            var amountsout = await GetAmountsOutAsync(Web3.Convert.ToWei(amount, decimalPlacesFromUnit), tokenAddress, await GetWETHAddressAsync());
            var amountsin = await GetAmountsInAsync(Web3.Convert.ToWei(amount, decimalPlacesFromUnit), tokenAddress, await GetWETHAddressAsync());
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

        private static BigInteger DecimalToWei(decimal amount)
        {
            return Web3.Convert.ToWei(amount, Nethereum.Util.UnitConversion.EthUnit.Ether);
        }
    }
}